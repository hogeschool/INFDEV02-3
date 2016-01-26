module CodeDefinitionLambda

open Coroutine
open CommonLatex

type Term =
  | Var of string
  | Application of Term * Term
  | Lambda of string * Term
  with
    override this.ToString() =
      match this with
      | Var s -> s
      | Application(t,u) -> sprintf "(%s %s)" (t.ToString())(u.ToString())
      | Lambda(x,t) -> sprintf "(\%s -> %s)" x (t.ToString())


    member this.Reduce =
      match this with
      | Var x -> Var x
      | Lambda(x,f) -> this
      | Application(Lambda(x,f),u) ->
          let v = u.Reduce
          f.Replace x v
      | Application(Var x,u) -> this
      | Application(t,u) ->
          let f = t.Reduce
          Application(f,u)


    member this.Replace x u =
      match this with
      | Var s when s = x -> u
      | Lambda(t,f) when t <> x -> Lambda(t, f.Replace x u)
      | Application(t,f) -> Application(t.Replace x u,f.Replace x u)
      | _ -> this

    member this.ToLambdaCalculus code separator =
      match this with
      | Var x as v-> code + separator + v.ToString()
      | Application(Var x,u) as a -> code + separator + a.ToString()
      | Lambda(x,f) as l -> code + separator + l.ToString() 
      | Application(Lambda(x,f),u) as a ->
          let reduction = a.Reduce
          reduction.ToLambdaCalculus (code + this.ToString() + separator + reduction.ToString()) separator
      | Application(t,u) as a ->
          let reduction = a.Reduce
          reduction.ToLambdaCalculus (code + this.ToString() + separator + reduction.ToString()) separator


let (!!) x = Var x
let (>>>) t u = Application(t,u)
let (==>) x t = Lambda(x, t)

let rec replace t x u =
  match t with
  | Var s when s = x -> u
  | Lambda(t,f) when t <> x -> Lambda(t,replace f x u)
  | Application(t,f) -> Application(replace t x u,replace f x u)
  | _ -> t

