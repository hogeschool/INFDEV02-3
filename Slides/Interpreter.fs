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
    | Dots -> 
      return None
    | None -> 
      return None
    | Hidden c | Private c | Public c | Static c -> 
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
    | ToString(v) ->
      let! v_val = interpret v
      match v_val with
      | ConstInt x -> return ConstString(x.ToString())
      | ConstBool x -> return ConstString(x.ToString())
      | ConstFloat x -> return ConstString(x.ToString())
      | ConstString x -> return ConstString(x)
      | _ -> return failwith "ToString() on objects is not yet implemented."
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
      | ConstString x, ConstString y -> 
        match op with
        | Plus -> return ConstString(x + y)
        | _ -> return failwithf "Cannot perform %s %s %s" (toString a) op.AsPython (toString b)
      | _ -> return failwithf "Cannot perform %s %s %s" (toString a) op.AsPython (toString b)
    | While(c,body) ->
      let! cVal = interpret c
      let body_nl = body |> numberOfLines
      match cVal with
      | ConstBool true ->
        let! res = interpret (Sequence(End,body))
        do! changePC (fun pc -> pc - body_nl)
        do! pause
        return! interpret(While(c,body))
      | ConstBool false ->
        do! changePC ((+) ((body_nl) + 1))
        return None
      | _ -> return failwith "Malformed if"
    | If(c,t,e) ->
      let! cVal = interpret c
      match cVal with
      | ConstBool true ->
        let! res = interpret (Sequence(End,t))
        do! changePC ((+) ((e |> numberOfLines) + 2))
        return res
      | ConstBool false ->
        do! changePC ((+) ((t |> numberOfLines) + 1))
        let! res = interpret (Sequence(End,e))
        do! incrPC
        return res
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
      let allMethods = ms |> List.filter (function Implementation _ -> false | _ -> true)
      let! msVals = allMethods |> mapCo interpret
      let mutable m_pc = pc + 1
      let fields = ms |> List.filter (function TypedDecl(_) | Private(TypedDecl(_)) | Public(TypedDecl(_)) -> true | _ -> false) 
                      |> List.map (fun f -> match f with 
                                            | TypedDecl(n,t,_) | Public(TypedDecl(n,t,_)) | Private(TypedDecl(n,t,_)) as d -> n,d 
                                            | _ -> failwith "Malformed field declaration")
      let msValsByName = 
        fields @
        [
          for m,m_orig in Seq.zip msVals allMethods do
            match m with
            | Assign(f,ConstLambda(_,args,body)) -> 
              let pc = m_pc + 1
              m_pc <- m_pc + 2 + (body |> numberOfLines)
              match m_orig with
              | Static _ -> 
                yield f,ConstLambda(pc,args,body)
              | _ -> 
                yield f,ConstLambda(pc,addThisToMethodArgs n args,body)
            | _ -> 
              m_pc <- m_pc + 1
        ] |> Map.ofList
      do! setState { s with Heap = (s.Heap |> Map.add n (Hidden(Object(msValsByName |> Map.add "__name" (ConstString n))))) }
      let nl = cls |> numberOfLines
      do! changePC ((+) (nl - 1))
      return None
    | MainCall ->
      return! interpret (StaticMethodCall("Program","Main",[None]))
    | StaticMethodCall("Console","WriteLine",[arg]) ->
      let! argVal = interpret arg
      let! s = getState
      do! setState { s with OutputStream = (argVal |> toString) :: s.OutputStream }
      return None
    | StaticMethodCall("Console","ReadLine",[]) ->
      let! s = getState
      do! setState { s with InputStream = s.InputStream.Tail }
      do! pause
      return ConstString(s.InputStream.Head)
    | StaticMethodCall("Int32","Parse",[i]) ->
      let! i_val = interpret i
      match i_val with
      | ConstString(s) ->
        return ConstInt(System.Int32.Parse(s))
      | _ -> 
        return failwithf "Cannot convert %s to an integer" (i |> toString)
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
