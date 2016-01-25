module CodeDefinitionFunctional

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
  | Dots
  | End
  | None
  | Ref of string
  | Object of Map<string, Code>
  | New of string * List<Code>
  | Implementation of string
  | Return of Code
  | TypedDecl of string * string * Option<Code>
  | Var of string
  | Hidden of Code
  | ConstLambda of int * List<string> * Code
  | ConstBool of bool
  | ConstInt of int
  | ConstFloat of float
  | ConstString of string
  | TypedDef of string * List<string * string> * string * Code
  | Def of string * List<string> * Code
  | Call of string * List<Code>
  | If of Code * Code * Code
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
      | Return c ->
        sprintf "%sreturn %s\n" pre ((c.AsPython "").Replace("\n",""))
      | Var s -> s
      | Dots -> "...\n"
      | ConstBool b -> b.ToString()
      | ConstInt i -> i.ToString()
      | ConstFloat f -> f.ToString()
      | ConstString s -> sprintf "\"%s\"" s
      | Ref s -> sprintf "ref %s" s
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
      | If(c,t,e) ->
        let tS = (t.AsPython (pre + "  "))
        sprintf "%sif %s:\n%s%selse:\n%s" pre (c.AsPython "") tS pre (e.AsPython (pre + "  "))
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
      | Object bs ->
        let argss = bs |> Map.remove "__type" |> Seq.map (fun a -> a.Key + "=" + (a.Value.AsCSharp "") + ", ") |> Seq.toList
        sprintf "%s%s" pre ((!+argss).TrimEnd[|','; ' '|])
      | End -> ""
      | None -> "null"
      | Dots -> "...\n"
      | Implementation i -> 
        ""    
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
      | ConstLambda (pc,args,body) ->
        let argss = args |> List.map (fun a -> a + ",")
        sprintf "%s(%s) => %s" pre ((!+argss).TrimEnd[|','|]) (body.AsCSharp (pre + "  "))
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
      | If(c,t,e) ->
        sprintf "%sif(%s) {\n%s } else {\n%s }" pre (c.AsCSharp "") (t.AsCSharp (pre + "  ")) (e.AsCSharp (pre + "  "))
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


