module CodeDefinition

type Operator = Plus | Minus | Times | DividedBy | GreaterThan
  with
    member this.AsPython =
      match this with
      | Plus -> "+"
      | Minus -> "-"
      | Times -> "*"
      | DividedBy -> "/"
      | GreaterThan -> ">"

let (!+) = List.fold (+) ""

type Code =
  | ClassDecl of string * List<Code>
  | Return of Code
  | TypedVar of string * string
  | Var of string
  | ConstLambda of Code
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
    member this.AsPython pre = 
      match this with
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
      | _ -> failwith "Unsupported Python statement"

type RuntimeState = { Stack : List<Map<string, Code>>; Heap : Map<string, Code>; InputStream : List<Code> }

let classDecl c m = ClassDecl(c,m)
let (:=) x y = Assign(x,y)
let constInt x = ConstInt(x)
let constFloat x = ConstFloat(x)
let constString x = ConstString(x)
let typedVar x t = TypedVar(x,t)
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

