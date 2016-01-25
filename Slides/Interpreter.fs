module Interpreter

open CodeDefinitionImperative
open Coroutine
open Runtime

let lookup (s:RuntimeState<Code>) (v:string) =
  let rec lookupHeap (h:Map<string, Code>) vs =
    match vs with
    | [] -> failwith "Empty lookup string"
    | [v] ->
      h.[v]
    | v::vs ->
      match h.[v] with
      | Hidden(Object(bs)) | Object(bs) ->
        lookupHeap bs vs
      | _ -> failwithf "Cannot find %s" v

  let vs = v.Split([|'.'|]) |> Seq.toList
  let y = 
    match s.Stack with
    | [] -> failwith "Cannot find variable in empty stack"
    | c :: rs when c |> Map.containsKey vs.Head -> c.[vs.Head]
    | _ -> (s.Stack |> List.rev |> List.head).[vs.Head]
  match y,vs with
  | _,[v] -> y
  | Ref r,v::vs ->
    match s.Heap.[r] with
    | Object(bs) | Hidden(Object(bs)) ->
      lookupHeap bs vs
    | _ -> failwith "Lookup on non-object value."
  | _ -> failwith "Malformed lookup"

let store (s:RuntimeState<Code>) (v:string) (y:Code) : RuntimeState<Code> =
  let rec storeHeap (bs:Map<string,Code>) (vs:List<string>) : Map<string,Code> =
    match vs with
    | [] -> bs
    | [v] -> bs |> Map.add v y
    | v::vs ->
      match bs.[v] with
      | Object(bs_inner) | Hidden(Object(bs_inner)) ->
        let k = match bs.[v] with | Object(bs_inner) -> id | _ -> Hidden
        bs |> Map.add v (k(Object(storeHeap bs_inner vs)))
      | _ -> failwith "..."

  let vs = (v.Split([|'.'|]) |> Seq.toList)
  match vs with
  | [v] -> 
    match s.Stack with
    | c :: rs -> { s with Stack = (c |> Map.add v y) :: rs }
    | [] -> failwith "Cannot find variable in empty stack"
  | v::vs ->
    match s.Stack.Head.[v] with
    | Ref r ->
      match s.Heap.[r] with
      | Object(bs) | Hidden(Object(bs)) ->
        let k = match s.Heap.[r] with | Object(bs_inner) -> id | _ -> Hidden
        { s with Heap = s.Heap |> Map.add r (k(Object(storeHeap bs vs))) }
      | _ -> failwith "Malformed assignment"
    | _ -> failwith "Cannot lookup a non-ref object"
  | _ -> failwith "Malformed assignment"



let getPC =
  co{
    let! s = getState
    match s.Stack.Head.["PC"] with
    | ConstInt pc -> return pc
    | _ -> return failwith "Cannot find PC"
  }

let changePC f =
  co{
    let! s = getState
    match s.Stack with
    | c :: rs ->
      match c.["PC"] with
      | ConstInt pc ->
        do! setState { s with Stack = (c |> Map.add "PC" (ConstInt (f pc))) :: rs }
      | _ -> 
        return failwith "Cannot find PC"
    | _ -> 
      return failwith "Cannot find PC"
  }

let incrPC = changePC ((+) 1)

let rec interpret addThisToMethodArgs consName toString numberOfLines (p:Code) : Coroutine<RuntimeState<Code>,Code> =
  let interpret = interpret addThisToMethodArgs consName toString numberOfLines
  co{
    match p with
    | None -> 
      return None
    | Hidden c | Private c | Public c -> 
      return! interpret c
    | Ref _ as r ->
      return r
    | Var v -> 
      let! s = getState
      return lookup s v
    | ConstInt i ->
      return ConstInt i
    | ConstFloat f ->
      return ConstFloat f
    | ConstString s ->
      return ConstString s
    | Assign (v,e) ->
      let! res = interpret e
      let! s = getState
      let s_new = store s v res
      do! setState s_new
      return None
    | Return e ->
      let! res = interpret e
      let! s = getState
      match s.Stack with
      | c::rs ->
        do! setState { s with Stack = (Map.empty |> Map.add "PC" c.["PC"] |> Map.add "ret" res) :: rs }
        do! pause
        do! setState { s with Stack = rs }
        return res
      | _ -> return failwith "Cannot return from empty stack"
    | Def(f,args,body) ->
      let! pc = getPC
      let nl = body |> numberOfLines
      do! changePC ((+) nl)
      let! s = getState
      do! setState { s with Heap = (s.Heap |> Map.add f (Hidden(ConstLambda(pc+1,args,body)))) }
      return Assign(f, ConstLambda(pc+1,args,body))
    | Call(f,argExprs) ->
      let! argVals = argExprs |> mapCo interpret
      let! s = getState
      match s.Heap.[f] with
      | Hidden(ConstLambda(pc,argNames,body))
      | ConstLambda(pc,argNames,body) ->
        let c = Seq.zip argNames argVals |> Map.ofSeq |> Map.add "PC" (ConstInt pc) |> Map.add "ret" None
        do! setState { s with Stack = c :: s.Stack }
        do! pause
        return! interpret body
      | _ -> return failwithf "Cannot find function %s" f            
    | Op (a,op,b) -> 
      let! aVal = interpret a
      let! bVal = interpret b
      match aVal,bVal with
      | ConstInt x, ConstInt y -> 
        match op with
        | Times -> return ConstInt(x * y)
        | Plus -> return ConstInt(x + y)
        | Minus -> return ConstInt(x - y)
        | DividedBy -> return ConstInt(x / y)
        | GreaterThan -> return ConstBool(x > y)
      | _ -> return failwithf "Cannot perform %s %s %s" (toString a) op.AsPython (toString b)
    | If(c,t,e) ->
      let! cVal = interpret c
      match cVal with
      | ConstBool true ->
        return! interpret (Sequence(End,t))
      | ConstBool false ->
        do! changePC ((+) ((t |> numberOfLines) + 1))
        return! interpret (Sequence(End,e))
      | _ -> return failwith "Malformed if"
    | Sequence (p,k) ->
      let! _ = interpret p
      do! incrPC
      do! match p with
          | Def(_) | ClassDef(_) | InterfaceDef(_) -> 
            ret ()
          | _ ->
            pause
      return! interpret k
    | End -> return None
    | Implementation i -> return None
    | InterfaceDef (n,ms) as intf ->
      let! pc = getPC
      let! s = getState
      let mutable m_pc = pc + 1
      let msValsByName = 
        [
          for m in ms do
            match m with
            | TypedSig(f,args,t) -> 
              let pc = m_pc + 1
              m_pc <- m_pc + 1
              yield f,TypedSig(f,args,t)
            | _ -> 
              m_pc <- m_pc + 1
        ] |> Map.ofList
      do! setState { s with Heap = (s.Heap |> Map.add n (Hidden(Object(msValsByName |> Map.add "__name" (ConstString n))))) }
      let nl = intf |> numberOfLines
      do! changePC ((+) (nl - 1))
      return None
    | ClassDef (n,ms) as cls ->
      let! pc = getPC
      let! s = getState
      let! msVals = ms |> List.filter (function Implementation _ -> false | _ -> true) |> mapCo interpret
      let mutable m_pc = pc + 1
      let fields = ms |> List.filter (function TypedDecl(_) | Private(TypedDecl(_)) | Public(TypedDecl(_)) -> true | _ -> false) 
                      |> List.map (fun f -> match f with 
                                            | TypedDecl(n,t,_) | Public(TypedDecl(n,t,_)) | Private(TypedDecl(n,t,_)) as d -> n,d 
                                            | _ -> failwith "Malformed field declaration")
      let msValsByName = 
        fields @
        [
          for m in msVals do
            match m with
            | Assign(f,ConstLambda(_,args,body)) -> 
              let pc = m_pc + 1
              m_pc <- m_pc + 1 + (body |> numberOfLines)
              yield f,ConstLambda(pc,addThisToMethodArgs n args,body)
            | _ -> 
              m_pc <- m_pc + 1
        ] |> Map.ofList
      do! setState { s with Heap = (s.Heap |> Map.add n (Hidden(Object(msValsByName |> Map.add "__name" (ConstString n))))) }
      let nl = cls |> numberOfLines
      do! changePC ((+) (nl - 1))
      return None
    | StaticMethodCall(c,m,argExprs) ->
      let! s = getState
      match s.Heap.[c] with
      | Hidden(Object(ms)) 
      | Object(ms) ->
        let! argVals = argExprs |> mapCo interpret
        let! s = getState
        match ms.[m] with
        | Hidden(ConstLambda(pc,argNames,body))
        | ConstLambda(pc,argNames,body) ->
          let c = Seq.zip argNames argVals |> Map.ofSeq |> Map.add "PC" (ConstInt pc) |> Map.add "ret" None
          do! setState { s with Stack = c :: s.Stack }
          do! pause
          let! res = interpret body
          match res with
          | None -> // automatically returned, pop stack frame here
            let! s = getState
            do! setState { s with Stack = (Map.empty |> Map.add "PC" s.Stack.Head.["PC"] |> Map.add "ret" res) :: s.Stack.Tail }
            do! pause
            do! setState { s with Stack = s.Stack.Tail }
            return res
          | _ -> 
            return res
        | _ -> return failwithf "Cannot call method %s on %s as it is not an object" m c
      | _ -> return failwithf "Cannot find class %s" c
    | MethodCall(v,m,argExprs) ->
      let! s = getState
      match lookup s v with
      | Ref v_ref as v_val ->
        match s.Heap.[v_ref] with
        | Hidden(Object(bs))
        | Object(bs) as o ->
          match bs.["__type"] with
          | Ref(c_name) ->
            match s.Heap.[c_name] with
            | Hidden(Object(ms)) | Object(ms) ->
              match ms.["__name"] with
              | ConstString v_type_name ->
                return! interpret (StaticMethodCall(v_type_name, m, v_val :: argExprs))
              | _ -> return failwith ""
            | _ -> return failwith ""
          | _ -> return failwith ""
        | _ -> return failwith ""
      | _ -> return failwith ""
    | New(c,argExprs) ->
      let! s = getState
      match s.Heap.[c] with
      | Hidden(Object(ms))
      | Object(ms) as o ->
        let fields = ms |> Seq.filter (fun x -> match x.Value with | ConstLambda(_) | Hidden(ConstLambda(_)) -> false | _ -> x.Key.StartsWith("__") |> not) 
                        |> Seq.map (fun x -> x.Key,Hidden(None)) |> Map.ofSeq
        let self = Object (fields |> Map.add "__type" (Ref c))
        let self_ref_id = s.HeapSize.ToString()
        let self_ref = Ref self_ref_id
        do! setState { s with Stack = s.Stack; Heap = s.Heap |> Map.add self_ref_id self; HeapSize = s.HeapSize + 1 }
        do! pause
        let! bodyRes = interpret (StaticMethodCall(c, consName c, self_ref :: argExprs))
        return self_ref
      | _ -> return failwithf "Cannot find class %s" c
    | TypedDef(n,args,t,body) -> 
      return! interpret (Def(n,args |> List.map snd, body))
    | TypedDecl(v,t,Option.None) ->
      return! interpret (Assign(v, Hidden(None)))
    | TypedDecl(v,t,Some y) ->
      return! interpret (Assign(v, y))
    | c -> return failwithf "Unsupported construct %A" c
  }


let runPython p = interpret (fun _ args -> args) (fun _ -> "__init__") (fun c -> c.AsPython "") (fun c -> c.NumberOfPythonLines) p
let runCSharp p = interpret (fun c args -> "this" :: args) id (fun c -> c.AsCSharp "") (fun c -> c.NumberOfCSharpLines) p

let makeStatic c = Static(c)
let makePublic c = Public(c)
let makePrivate c = Private(c)
let implements i = Implementation(i)
let interfaceDef c m = InterfaceDef(c,m)
let classDef c m = ClassDef(c,m)
let (:=) x y = Assign(x,y)
let newC c a = New(c,a)
let constInt x = ConstInt(x)
let constFloat x = ConstFloat(x)
let constString x = ConstString(x)
let dots = Dots
let typedDecl x t = TypedDecl(x,t,Option.None)
let typedDeclAndInit x t c = TypedDecl(x,t,Some c)
let var x = Var(x)
let ret x = Return(x)
let def x l b = Def(x,l,b)
let typedDef x l t b = TypedDef(x,l,t,b)
let typedSig x l t = TypedSig(x,l,t)
let call x l = Call(x,l)
let staticMethodCall c m l = StaticMethodCall(c,m,l)
let methodCall x m l = MethodCall(x,m,l)
let ifelse c t e = If(c,t,e)
let whiledo c b = While(c,b)
let (.+) a b = Op(a, Plus, b)
let (.-) a b = Op(a, Minus, b)
let (.*) a b = Op(a, Times, b)
let (./) a b = Op(a, DividedBy, b)
let (.>) a b = Op(a, GreaterThan, b)
let (>>) a b = Sequence(a, b)
let endProgram = End
