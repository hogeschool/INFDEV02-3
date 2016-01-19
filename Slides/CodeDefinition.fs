module CodeDefinition
open Coroutine
open CommonLatex

type Operator = Plus | Minus | Times | DividedBy | GreaterThan
  with
    member this.AsPython =
      match this with
      | Plus -> "+"
      | Minus -> "-"
      | Times -> "*"
      | DividedBy -> "/"
      | GreaterThan -> ">"
    member this.AsCSharp =
      match this with
      | _ -> this.AsPython

let (!+) = List.fold (+) ""

type Code =
  | End
  | None
  | ClassDecl of string * List<Code>
  | Return of Code
  | TypedDecl of string * string * Option<Code>
  | Var of string
  | Hidden of Code
  | ConstLambda of int * List<string> * Code
  | ConstInt of int
  | ConstFloat of float
  | ConstString of string
  | Assign of string * Code
  | TypedDef of string * List<string * string> * string * Code
  | Def of string * List<string> * Code
  | Call of string * List<Code>
  | MethodCall of string * string * List<Code>
  | If of Code * Code * Code
  | Op of Code * Operator * Code
  | Sequence of Code * Code
  with 
    member this.NumberOfPythonLines = 
      match this with
      | ClassDecl(s,ms) -> 1 + (ms |> List.map (fun m -> m.NumberOfPythonLines) |> List.fold (+) 0)
      | None | Return _ | Var _ -> 1
      | ConstInt _ | ConstFloat _ | ConstString _ -> 1
      | Assign (v,c) -> 1
      | Def (n,args,body) -> 1 + body.NumberOfPythonLines
      | Call(n,args) -> 1
      | MethodCall(n,m,args) -> 1
      | If(c,t,e) ->
        1 + t.NumberOfPythonLines + 1 + e.NumberOfPythonLines
      | Op(a,op,b) ->
        1
      | Sequence (p,q) ->
        p.NumberOfPythonLines + q.NumberOfPythonLines
      | _ -> failwith "Unsupported Python statement"
    member this.AsPython pre = 
      match this with
      | End -> ""
      | None -> "None"
      | ClassDecl(s,ms) -> 
        let mss = ms |> List.map (fun m -> m.AsPython (pre + "  ") + "\n")
        sprintf "class %s:\n%s\n" s !+mss
      | Return c ->
        sprintf "%sreturn %s\n" pre (c.AsPython "")
      | Var s -> s
      | ConstInt i -> i.ToString()
      | ConstFloat f -> f.ToString()
      | ConstString s -> sprintf "\"%s\"" s
      | Assign (v,c) -> sprintf "%s%s = %s\n" pre v (c.AsPython "")
      | ConstLambda (pc,args,body) ->
        let argss = args |> List.map (fun a -> a + ",")
        sprintf "%slambda(%s): %s" pre ((!+argss).TrimEnd[|','|]) (body.AsPython (pre + "  "))
      | Def (n,args,body) ->
        let argss = args |> List.map (fun a -> a + ",")
        sprintf "%sdef %s(%s):\n%s" pre n ((!+argss).TrimEnd[|','|]) (body.AsPython (pre + "  "))
      | Call(n,args) ->
        let argss = args |> List.map (fun a -> a.AsPython "" + ",")
        sprintf "%s%s(%s)\n" pre n ((!+argss).TrimEnd[|','|])
      | MethodCall(n,m,args) ->
        let argss = args |> List.map (fun a -> a.AsPython "" + ",")
        sprintf "%s%s.%s(%s)\n" pre n m ((!+argss).TrimEnd[|','|])
      | If(c,t,e) ->
        sprintf "%sif %s:\n%selse:\n%s" pre (c.AsPython "") (t.AsPython (pre + "  ")) (e.AsPython (pre + "  "))
      | Op(a,op,b) ->
        sprintf "%s%s%s" ((a.AsPython "").Replace("\n","")) (op.AsPython) ((b.AsPython (pre + "  ")).Replace("\n",""))
      | Sequence (p,q) ->
        sprintf "%s%s" (p.AsPython pre) (q.AsPython pre)
      | Hidden(_) -> ""
      | _ -> failwith "Unsupported Python statement"

    member this.AsCSharp pre = 
      match this with
      | End -> ""
      | None -> "void"
      | ClassDecl(s,ms) -> 
        let mss = ms |> List.map (fun m -> m.AsCSharp (pre + "  ") + "\n")
        sprintf "class %s {\n%s }\n" s !+mss
      | Return c ->
        sprintf "%sreturn %s;\n" pre ((c.AsCSharp "").TrimEnd[|','; '\n'; ';'|])
      | TypedDecl(s,t,Option.None) -> sprintf "%s%s %s;\n" pre t s
      | TypedDecl(s,t,Some v) -> sprintf "%s%s %s = %s;\n" pre t s ((v.AsCSharp "").TrimEnd[|','; '\n'; ';'|])
      | Var s -> s
      | ConstInt i -> i.ToString()
      | ConstFloat f -> f.ToString()
      | ConstString s -> sprintf "\"%s\"" s
      | Assign (v,c) -> sprintf "%s%s = %s;\n" pre v ((c.AsCSharp "").TrimEnd[|','; '\n'; ';'|])
      | ConstLambda (pc,args,body) ->
        let argss = args |> List.map (fun a -> a + ",")
        sprintf "%s(%s) => %s" pre ((!+argss).TrimEnd[|','|]) (body.AsCSharp (pre + "  "))
      | TypedDef (n,args,t,body) ->
        let argss = args |> List.map (fun (t,a) -> t + " " + a + ",")
        sprintf "%s%s %s(%s) {\n%s }\n" pre t n ((!+argss).TrimEnd[|','|]) (body.AsCSharp (pre + "  "))
      | Call(n,args) ->
        let argss = args |> List.map (fun a -> ((a.AsCSharp "").TrimEnd[|','; '\n'; ';'|]) + ",")
        sprintf "%s%s(%s);\n" pre n ((!+argss).TrimEnd[|','; '\n'; ';'|])
      | MethodCall(n,m,args) ->
        let argss = args |> List.map (fun a -> ((a.AsCSharp "").TrimEnd[|','; '\n'; ';'|]) + ",")
        sprintf "%s%s.%s(%s);\n" pre n m ((!+argss).TrimEnd[|','; '\n'; ';'|])
      | If(c,t,e) ->
        sprintf "%sif(%s) {\n%s } else {\n%s }" pre (c.AsCSharp "") (t.AsCSharp (pre + "  ")) (e.AsCSharp (pre + "  "))
      | Op(a,op,b) ->
        sprintf "%s%s%s" ((a.AsCSharp "").Replace("\n","").Replace(";","")) (op.AsCSharp) ((b.AsCSharp (pre + "  ")).Replace("\n","").Replace(";",""))
      | Sequence (p,q) ->
        sprintf "%s%s" (p.AsCSharp pre) (q.AsCSharp pre)
      | _ -> failwith "Unsupported Python statement"


let printBindings (b:Map<string, Code>) =
  let pc = b |> Map.tryFind "PC"
  let ret = b |> Map.tryFind "ret"
  let b' = b |> Map.remove "PC" |> Map.remove "ret"
  let entries = [ for x in b' do match x.Value with Hidden _ -> () | _ -> yield x ]
  let innerNames = [ for x in entries do yield x.Key ]
  let innerValues = [ for x in entries do yield x.Value ]
  let names = (match ret with | Option.None -> innerNames | _ -> "ret" :: innerNames)
  let values = (match ret with | Option.None -> innerValues | Some x -> x :: innerValues)
  let names = (match pc with | Option.None -> innerNames | _ -> "PC" :: innerNames)
  let values = (match pc  with | Option.None -> innerValues | Some x -> x :: innerValues)
  let allNames = if names |> List.isEmpty then "" else names |> List.reduce (fun a b -> a + " & " + b)
  let allValues = if values |> List.isEmpty then "" else values |> List.map (fun v -> v.AsPython "") |> List.reduce (fun a b -> a + " & " + b)
  allNames,allValues


type RuntimeState = { Stack : List<Map<string, Code>>; Heap : Map<string, Code>; InputStream : List<Code> }
  with 
    member this.AsSlideContent =
      let stackFrames = 
        [
          for sf in this.Stack do
          yield printBindings sf 
        ] |> List.rev
      let stackNamesByFrame = stackFrames |> List.map fst
      let stackValuesByFrame = stackFrames |> List.map snd
      let stackNames = stackNamesByFrame |> List.reduce (fun a b -> a + " & & " + b)
      let stackValues = stackValuesByFrame |> List.reduce (fun a b -> a + " & & " + b)

      let hd = stackNames |> Seq.map (fun _ -> "c") |> Seq.toList
      let stackTableContent = sprintf "%s \\\\\n\\hline\n%s \\\\\n\\hline\n" stackNames stackValues
      let stackTable = sprintf "%s\n%s\n%s\n" (beginTabular hd) stackTableContent endTabular

      let heap = 
        if this.Heap |> Map.isEmpty then
          ""
        else
          let hd = this.Heap |> Seq.map (fun _ -> "c") |> Seq.toList
          let heapNames,heapValues = printBindings this.Heap
          let heapTableContent = sprintf "%s \\\\\n\\hline\n%s \\\\\n\\hline\n" heapNames heapValues
          sprintf "%s\n%s\n%s" (beginTabular hd) heapTableContent endTabular
      stackTable, heap


let rec lookup (s:List<Map<string, Code>>) v =
  match s with
  | [] -> failwith "Cannot find variable in empty stack"
  | c :: rs when c |> Map.containsKey v -> c.[v]
  | _ -> (s |> List.rev |> List.head).[v]

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

let rec runPython (p:Code) : Coroutine<RuntimeState,Code> =
  co{
    match p with
    | Hidden c -> 
      return! runPython c
    | Var v -> 
      let! s = getState
      return lookup s.Stack v
    | ConstInt i ->
      return ConstInt i
    | ConstFloat f ->
      return ConstFloat f
    | ConstString s ->
      return ConstString s
    | Assign (v,e) ->
      let! res = runPython e
      let! s = getState
      match s.Stack with
      | c::rs ->
        let c' = c |> Map.add v res
        do! setState { s with Stack = c :: rs }
        return None
      | _ -> return failwith "Cannot assign variable on empty stack"
    | Return e ->
      let! res = runPython e
      let! s = getState
      match s.Stack with
      | c::rs ->
        do! setState { s with Stack = (Map.empty |> Map.add "PC" c.["PC"] |> Map.add "res" res) :: rs }
        do! pause
        do! setState { s with Stack = rs }
        return res
      | _ -> return failwith "Cannot return from empty stack"
    | Def(f,args,body) ->
      let! pc = getPC
      let nl = body.NumberOfPythonLines
      do! changePC ((+) nl)
      let! s = getState
      match s.Stack with
      | c::rs ->
        do! setState { s with Stack = (c |> Map.add f (Hidden(ConstLambda(pc+1,args,body)))) :: rs }
        return None
      | _ -> return failwith "Cannot return from empty stack"
    | Call(f,argExprs) ->
      let! argVals = argExprs |> mapCo runPython
      let! s = getState
      match lookup s.Stack f with
      | Hidden(ConstLambda(pc,argNames,body))
      | ConstLambda(pc,argNames,body) ->
        let c = Seq.zip argNames argVals |> Map.ofSeq |> Map.add "PC" (ConstInt pc) |> Map.add "res" None
        do! setState { s with Stack = c :: s.Stack }
        do! pause
        return! runPython body
      | _ -> return failwithf "Cannot find function %s" f            
    | Op (a,Times,b) -> 
      let! aVal = runPython a
      let! bVal = runPython b
      match aVal,bVal with
      | ConstInt x, ConstInt y -> return ConstInt(x * y)
      | _ -> return failwithf "Cannot add %s and %s" (a.AsPython "") (b.AsPython "")
    | Sequence (p,k) ->
      let! _ = runPython p
      do! incrPC
      do! pause
      return! runPython k
    | End -> return None
    | c -> return failwithf "Unsupported construct %A" c
//    | ClassDecl of string * List<Code>
//    | TypedVar of string * string
//    | TypedDef of string * List<string * string> * string * Code
//    | MethodCall of string * string * List<Code>
//    | If of Code * Code * Code
  }


let classDecl c m = ClassDecl(c,m)
let (:=) x y = Assign(x,y)
let constInt x = ConstInt(x)
let constFloat x = ConstFloat(x)
let constString x = ConstString(x)
let typedDecl x t = TypedDecl(x,t,Option.None)
let typedDeclAndInit x t c = TypedDecl(x,t,Some c)
let var x = Var(x)
let ret x = Return(x)
let def x l b = Def(x,l,b)
let typedDef x l t b = TypedDef(x,l,t,b)
let call x l = Call(x,l)
let methodCall x m l = MethodCall(x,m,l)
let ifelse c t e = If(c,t,e)
let (.+) a b = Op(a, Plus, b)
let (.-) a b = Op(a, Minus, b)
let (.*) a b = Op(a, Times, b)
let (./) a b = Op(a, DividedBy, b)
let (.>) a b = Op(a, GreaterThan, b)
let (>>) a b = Sequence(a, b)
let endProgram = End
