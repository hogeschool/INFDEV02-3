module CodeDefinitionImperative

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
    member this.AsJava = this.AsCSharp

let (!+) = List.fold (+) ""

type Code =
  | Static of Code
  | Public of Code
  | Private of Code
  | Dots
  | End
  | ToString of Code
  | None
  | Ref of string
  | Object of Map<string, Code>
  | New of string * List<Code>
  | Implementation of string
  | InterfaceDef of string * List<Code>
  | ClassDef of string * List<Code>
  | Return of Code
  | TypedDecl of string * string * Option<Code>
  | Var of string
  | Hidden of Code
  | ConstLambda of int * List<string> * Code
  | ConstBool of bool
  | ConstInt of int
  | ConstFloat of float
  | ConstString of string
  | Assign of string * Code
  | TypedDef of string * List<string * string> * string * Code
  | TypedSig of string * List<string * string> * string
  | Def of string * List<string> * Code
  | Call of string * List<Code>
  | MainCall
  | MethodCall of string * string * List<Code>
  | StaticMethodCall of string * string * List<Code>
  | If of Code * Code * Code
  | While of Code * Code
  | Op of Code * Operator * Code
  | Sequence of Code * Code
  with 
    member this.AsPython pre = 
      match this with
      | Object bs ->
        let argss = bs |> Map.remove "__type" |> Seq.map (fun a -> a.Key + "=" + (a.Value.AsPython "") + ", ") |> Seq.toList
        sprintf "%s%s" pre ((!+argss).TrimEnd[|','; ' '|])
      | End -> ""
      | None -> "None"
      | ClassDef(s,ms) -> 
        let mss = ms |> List.map (fun m -> m.AsPython (pre + "  "))
        sprintf "class %s:\n%s" s ((!+mss).Replace("\n\n", "\n"))
      | Return c ->
        sprintf "%sreturn %s\n" pre ((c.AsPython "").Replace("\n",""))
      | Var s -> s
      | Dots -> "...\n"
      | ConstBool b -> b.ToString()
      | ConstInt i -> i.ToString()
      | ConstFloat f -> f.ToString()
      | ConstString s -> sprintf "\"%s\"" s
      | Ref s -> sprintf "ref %s" s
      | Assign (v,c) -> sprintf "%s%s = %s\n" pre v ((c.AsPython "").TrimEnd([|'\n'|]))
      | ConstLambda (pc,args,body) ->
        let argss = args |> List.map (fun a -> a + ",")
        sprintf "%slambda(%s): %s" pre ((!+argss).TrimEnd[|','|]) (body.AsPython (pre + "  "))
      | Def (n,args,body) ->
        let argss = args |> List.map (fun a -> a + ",")
        sprintf "%sdef %s(%s):\n%s" pre n ((!+argss).TrimEnd[|','|]) (body.AsPython (pre + "  "))
      | New(c,args) ->
        let argss = args |> List.map (fun a -> (a.AsPython "").TrimEnd([|'\n'|]) + ",")
        sprintf "%s%s(%s)\n" pre c ((!+argss).TrimEnd[|','|])
      | Call(n,args) ->
        let argss = args |> List.map (fun a -> (a.AsPython "").TrimEnd([|'\n'|]) + ",")
        sprintf "%s%s(%s)\n" pre n ((!+argss).TrimEnd[|','|])
      | MethodCall(n,m,args) ->
        let argss = args |> List.map (fun a -> (a.AsPython "").TrimEnd([|'\n'|]) + ",")
        sprintf "%s%s.%s(%s)\n" pre n m ((!+argss).TrimEnd[|','|])
      | StaticMethodCall(c,m,args) ->
        let argss = args |> List.map (fun a -> (a.AsPython "").TrimEnd([|'\n'|]) + ",")
        sprintf "%s%s.%s(%s)\n" pre c m ((!+argss).TrimEnd[|','|])
      | If(c,t,e) ->
        let tS = (t.AsPython (pre + "  "))
        sprintf "%sif %s:\n%s%selse:\n%s" pre (c.AsPython "") tS pre (e.AsPython (pre + "  "))
      | While(c,b) ->
        let bs = (b.AsPython (pre + "  "))
        sprintf "%swhile %s:\n%s" pre (c.AsPython "") bs
      | Op(a,op,b) ->
        sprintf "(%s %s %s)" ((a.AsPython "").Replace("\n","")) (op.AsPython) ((b.AsPython (pre + "  ")).Replace("\n",""))
      | Sequence (p,q) ->
        let res = sprintf "%s%s" (p.AsPython pre) (q.AsPython pre)
        res
      | Hidden(_) -> ""
      | s -> failwithf "Unsupported Python statement %A" s
    member this.NumberOfPythonLines = 
      let code = ((this.AsPython ""):string).TrimEnd([|'\n'|])
      let lines = code.Split([|'\n'|])
      lines.Length

    member this.AsJava pre =
      match this with
      | Private p ->
        (sprintf "%sprivate%s" pre (p.AsJava pre)).Replace("private" + pre, "private ")
      | Static p ->
        (sprintf "%sstatic%s" pre (p.AsJava pre)).Replace("static" + pre, "static ")
      | Public p ->
        (sprintf "%spublic%s" pre (p.AsJava pre)).Replace("public" + pre, "public ")
      | ToString p ->
        (sprintf "%s%s.toString()" pre (p.AsJava ""))
      | Object bs ->
        let argss = bs |> Map.remove "__type" |> Seq.map (fun a -> a.Key + "=" + (a.Value.AsJava "") + ", ") |> Seq.toList
        sprintf "%s%s" pre ((!+argss).TrimEnd[|','; ' '|])
      | MainCall -> ""
      | End -> ""
      | None -> "null"
      | Dots -> "...\n"
      | Implementation i -> 
        ""
      | InterfaceDef(s,ms) ->
        let mss = ms |> List.map (fun m -> m.AsJava (pre + "  "))
        let res = sprintf "interface %s {\n%s%s}\n" s (!+mss) pre
        res        
      | ClassDef(s,ms) -> 
        let mss = ms |> List.map (fun m -> m.AsJava (pre + "  "))
        let is = ms |> List.filter (function Implementation _ -> true | _ -> false)
        match is with
        | [] ->
          let res = sprintf "class %s {\n%s%s}\n" s (!+mss) pre
          res
        | _ -> 
          let isNames = is |> List.map (function Implementation i -> i | _ -> failwith "Invalid interface") 
                           |> List.reduce (fun a b -> a + ", " + b)
          let res = sprintf "class %s implements %s {\n%s%s}\n" s isNames (!+mss) pre
          res
      | Return c ->
        sprintf "%sreturn %s;\n" pre ((c.AsJava "").TrimEnd[|','; '\n'; ';'|])
      | TypedDecl(s,t,Option.None) -> 
        if t = "" then sprintf "%s%s;\n" pre s
        else sprintf "%s%s %s;\n" pre t s
      | TypedDecl(s,t,Some v) -> 
        if t = "" then sprintf "%s%s = %s;\n" pre s ((v.AsJava "").TrimEnd[|','; '\n'; ';'|])
        else sprintf "%s%s %s = %s;\n" pre t s ((v.AsJava "").TrimEnd[|','; '\n'; ';'|])
      | Var s -> s
      | ConstBool b -> b.ToString().ToLower()
      | ConstInt i -> i.ToString()
      | ConstFloat f -> f.ToString()
      | ConstString s -> sprintf "\"%s\"" s
      | Ref s -> sprintf "ref %s" s
      | Assign (v,c) -> sprintf "%s%s = %s;\n" pre v ((c.AsJava "").TrimEnd[|','; '\n'; ';'|])
      | ConstLambda (pc,args,body) ->
        let argss = args |> List.map (fun a -> a + ",")
        sprintf "%s(%s) => %s" pre ((!+argss).TrimEnd[|','|]) (body.AsJava (pre + "  "))
      | TypedSig (n,args,t) ->
        let argss = args |> List.map (fun (t,a) -> t + " " + a + ",")
        sprintf "%s%s %s(%s);\n" pre t n ((!+argss).TrimEnd[|','; '\n'|])
      | TypedDef (n,args,t,body) ->
        let argss = args |> List.map (fun (t,a) -> t + " " + a + ",")
        (if t = "" then sprintf "%s%s(%s) {\n%s%s}\n" pre n
         else sprintf "%s%s %s(%s) {\n%s%s}\n" pre t n) ((!+argss).TrimEnd[|','; '\n'|]) (body.AsJava (pre + "  ")) pre
      | New(c,args) ->
        let argss = args |> List.map (fun a -> ((a.AsJava "").TrimEnd[|','; '\n'; ';'|]) + ",")
        sprintf "%snew %s(%s);\n" pre c ((!+argss).TrimEnd[|','; '\n'; ';'|])
      | Call(n,args) ->
        let argss = args |> List.map (fun a -> ((a.AsJava "").TrimEnd[|','; '\n'; ';'|]) + ",")
        sprintf "%s%s(%s);\n" pre n ((!+argss).TrimEnd[|','; '\n'; ';'|])
      | MethodCall(n,m,args) ->
        let argss = args |> List.map (fun a -> ((a.AsJava "").TrimEnd[|','; '\n'; ';'|]) + ",")
        sprintf "%s%s.%s(%s);\n" pre n m ((!+argss).TrimEnd[|','; '\n'; ';'|])
      | StaticMethodCall("Int32","Parse",args) ->
        StaticMethodCall("Integer","parseInt",args).AsJava pre
      | StaticMethodCall("Console","WriteLine",args) ->
        StaticMethodCall("System.out","println",args).AsJava pre
      | StaticMethodCall("Console","ReadLine",args) ->
        StaticMethodCall("new Scanner(System.in)","nextLine()",args).AsJava pre
      | StaticMethodCall(c,m,args) ->
        let argss = args |> List.map (fun a -> (a.AsJava "").TrimEnd([|'\n'|]) + ",")
        sprintf "%s%s.%s(%s)\n" pre c m ((!+argss).TrimEnd[|','; '\n'; ';'|])
      | If(c,t,e) ->
        sprintf "%sif %s {\n%s } else {\n%s }\n" pre (c.AsJava "") (t.AsJava (pre + "  ")) (e.AsJava (pre + "  "))
      | While(c,b) ->
        sprintf "%swhile %s {\n%s }\n" pre (c.AsJava "") (b.AsJava (pre + "  "))
      | Op(a,op,b) ->
        sprintf "(%s %s %s)" ((a.AsJava "").Replace("\n","").Replace(";","").Replace("  ","")) (op.AsJava) ((b.AsJava (pre + "")).Replace("\n","").Replace(";","").Replace("  ",""))
      | Sequence (p,q) ->
        sprintf "%s%s" (p.AsJava pre) (q.AsJava pre)
      | Hidden(_) -> ""
      | s -> failwithf "Unsupported Java statement %A" s
    member this.NumberOfJavaLines = 
      let code = ((this.AsJava ""):string).TrimEnd([|'\n'|])
      let lines = code.Split([|'\n'|])
      lines.Length

    member this.AsCSharp pre = 
      match this with
      | Private p ->
        (sprintf "%sprivate%s" pre (p.AsCSharp pre)).Replace("private" + pre, "private ")
      | Static p ->
        (sprintf "%sstatic%s" pre (p.AsCSharp pre)).Replace("static" + pre, "static ")
      | Public p ->
        (sprintf "%spublic%s" pre (p.AsCSharp pre)).Replace("public" + pre, "public ")
      | ToString p ->
        (sprintf "%s%s.ToString()" pre (p.AsCSharp ""))
      | Object bs ->
        let argss = bs |> Map.remove "__type" |> Seq.map (fun a -> a.Key + "=" + (a.Value.AsCSharp "") + ", ") |> Seq.toList
        sprintf "%s%s" pre ((!+argss).TrimEnd[|','; ' '|])
      | MainCall -> ""
      | End -> ""
      | None -> "null"
      | Dots -> "...\n"
      | Implementation i -> 
        ""
      | InterfaceDef(s,ms) ->
        let mss = ms |> List.map (fun m -> m.AsCSharp (pre + "  "))
        let res = sprintf "interface %s {\n%s%s}\n" s (!+mss) pre
        res        
      | ClassDef(s,ms) -> 
        let mss = ms |> List.map (fun m -> m.AsCSharp (pre + "  "))
        let is = ms |> List.filter (function Implementation _ -> true | _ -> false)
        match is with
        | [] ->
          let res = sprintf "class %s {\n%s%s}\n" s (!+mss) pre
          res
        | _ -> 
          let isNames = is |> List.map (function Implementation i -> i | _ -> failwith "Invalid interface") 
                           |> List.reduce (fun a b -> a + ", " + b)
          let res = sprintf "class %s : %s {\n%s%s}\n" s isNames (!+mss) pre
          res
      | Return c ->
        sprintf "%sreturn %s;\n" pre ((c.AsCSharp "").TrimEnd[|','; '\n'; ';'|])
      | TypedDecl(s,t,Option.None) -> 
        if t = "" then sprintf "%s%s;\n" pre s
        else sprintf "%s%s %s;\n" pre t s
      | TypedDecl(s,t,Some v) -> 
        if t = "" then sprintf "%s%s = %s;\n" pre s ((v.AsCSharp "").TrimEnd[|','; '\n'; ';'|])
        else sprintf "%s%s %s = %s;\n" pre t s ((v.AsCSharp "").TrimEnd[|','; '\n'; ';'|])
      | Var s -> s
      | ConstBool b -> b.ToString().ToLower()
      | ConstInt i -> i.ToString()
      | ConstFloat f -> f.ToString()
      | ConstString s -> sprintf "\"%s\"" s
      | Ref s -> sprintf "ref %s" s
      | Assign (v,c) -> sprintf "%s%s = %s;\n" pre v ((c.AsCSharp "").TrimEnd[|','; '\n'; ';'|])
      | ConstLambda (pc,args,body) ->
        let argss = args |> List.map (fun a -> a + ",")
        sprintf "%s(%s) => %s" pre ((!+argss).TrimEnd[|','|]) (body.AsCSharp (pre + "  "))
      | TypedSig (n,args,t) ->
        let argss = args |> List.map (fun (t,a) -> t + " " + a + ",")
        sprintf "%s%s %s(%s);\n" pre t n ((!+argss).TrimEnd[|','; '\n'|])
      | TypedDef (n,args,t,body) ->
        let argss = args |> List.map (fun (t,a) -> t + " " + a + ",")
        (if t = "" then sprintf "%s%s(%s) {\n%s%s}\n" pre n
         else sprintf "%s%s %s(%s) {\n%s%s}\n" pre t n) ((!+argss).TrimEnd[|','; '\n'|]) (body.AsCSharp (pre + "  ")) pre
      | New(c,args) ->
        let argss = args |> List.map (fun a -> ((a.AsCSharp "").TrimEnd[|','; '\n'; ';'|]) + ",")
        sprintf "%snew %s(%s);\n" pre c ((!+argss).TrimEnd[|','; '\n'; ';'|])
      | Call(n,args) ->
        let argss = args |> List.map (fun a -> ((a.AsCSharp "").TrimEnd[|','; '\n'; ';'|]) + ",")
        sprintf "%s%s(%s);\n" pre n ((!+argss).TrimEnd[|','; '\n'; ';'|])
      | MethodCall(n,m,args) ->
        let argss = args |> List.map (fun a -> ((a.AsCSharp "").TrimEnd[|','; '\n'; ';'|]) + ",")
        sprintf "%s%s.%s(%s);\n" pre n m ((!+argss).TrimEnd[|','; '\n'; ';'|])
      | StaticMethodCall(c,m,args) ->
        let argss = args |> List.map (fun a -> (a.AsCSharp "").TrimEnd([|'\n'|]) + ",")
        sprintf "%s%s.%s(%s)\n" pre c m ((!+argss).TrimEnd[|','; '\n'; ';'|])
      | If(c,t,e) ->
        sprintf "%sif(%s) {\n%s } else {\n%s }\n" pre (c.AsCSharp "") (t.AsCSharp (pre + "  ")) (e.AsCSharp (pre + "  "))
      | While(c,b) ->
        sprintf "%swhile(%s) {\n%s }\n" pre (c.AsCSharp "") (b.AsCSharp (pre + "  "))
      | Op(a,op,b) ->
        sprintf "(%s %s %s)" ((a.AsCSharp "").Replace("\n","").Replace(";","").Replace("  ","")) (op.AsCSharp) ((b.AsCSharp (pre + "")).Replace("\n","").Replace(";","").Replace("  ",""))
      | Sequence (p,q) ->
        sprintf "%s%s" (p.AsCSharp pre) (q.AsCSharp pre)
      | Hidden(_) -> ""
      | s -> failwithf "Unsupported C# statement %A" s
    member this.NumberOfCSharpLines = 
      let code = ((this.AsCSharp ""):string).TrimEnd([|'\n'|])
      let lines = code.Split([|'\n'|])
      lines.Length



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
let mainCall = MainCall
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
let toString c = ToString c