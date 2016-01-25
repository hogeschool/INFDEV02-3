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
  | Static of Code
  | Public of Code
  | Private of Code
  | Dots
  | End
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
        sprintf "%s %s %s" ((a.AsPython "").Replace("\n","")) (op.AsPython) ((b.AsPython (pre + "  ")).Replace("\n",""))
      | Sequence (p,q) ->
        let res = sprintf "%s%s" (p.AsPython pre) (q.AsPython pre)
        res
      | Hidden(_) -> ""
      | s -> failwithf "Unsupported Python statement %A" s
    member this.NumberOfPythonLines = 
      let code = ((this.AsPython ""):string).TrimEnd([|'\n'|])
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
      | Object bs ->
        let argss = bs |> Map.remove "__type" |> Seq.map (fun a -> a.Key + "=" + (a.Value.AsCSharp "") + ", ") |> Seq.toList
        sprintf "%s%s" pre ((!+argss).TrimEnd[|','; ' '|])
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
      | ConstBool b -> b.ToString()
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
        sprintf "%sif(%s) {\n%s } else {\n%s }" pre (c.AsCSharp "") (t.AsCSharp (pre + "  ")) (e.AsCSharp (pre + "  "))
      | While(c,b) ->
        sprintf "%swhile(%s) {\n%s }" pre (c.AsCSharp "") (b.AsCSharp (pre + "  "))
      | Op(a,op,b) ->
        sprintf "%s %s %s" ((a.AsCSharp "").Replace("\n","").Replace(";","")) (op.AsCSharp) ((b.AsCSharp (pre + "  ")).Replace("\n","").Replace(";",""))
      | Sequence (p,q) ->
        sprintf "%s%s" (p.AsCSharp pre) (q.AsCSharp pre)
      | Hidden(_) -> ""
      | s -> failwithf "Unsupported C# statement %A" s
    member this.NumberOfCSharpLines = 
      let code = ((this.AsCSharp ""):string).TrimEnd([|'\n'|])
      let lines = code.Split([|'\n'|])
      lines.Length


