module CodeDefinitionLambda

open Coroutine
open CommonLatex

type Term =
  | Var of string
  | Application of Term * Term
  | Lambda of string * Term
  with
    member this.ToLambda =
      match this with
      | Var s -> s
      | Application(t,u) -> sprintf "(%s %s)" (t.ToLambda) (u.ToLambda)
      | Lambda(x,t) -> sprintf @"($\lambda$%s$\rightarrow$%s)" x (t.ToLambda)
    member this.ToString =
      match this with
      | Var s -> s
      | Application(t,u) -> sprintf "(%s %s)" (t.ToString) (u.ToString)
      | Lambda(x,t) -> sprintf @"(\%s.%s)" x (t.ToString)


let rec reduce p : Coroutine<(Term -> Term) * Term, Unit> =
  co{
    let! (k,t) = getState
    match t with
    | Var x -> 
      return ()
    | Lambda(x,f) -> 
      return ()
    | Application(Lambda(x,f),u) ->
        do! setState ((fun u -> k(Application(Lambda(x,f),u))), u)
        do! reduce p
        let! (_,v) = getState
        let f_new = replace f x v
        let test = k f_new
        do! setState (k,f_new)
        do! p
        return ()
    | Application(Var x,u) -> 
      return ()
    | Application(t,u) ->
        do! setState ((fun t -> k(Application(t,u))), t)
        do! reduce p
        do! p
        let! (_,t_new) = getState
        do! setState (k, Application(t_new,u))
  }

and replace t x u =
  match t with
  | Var s when s = x -> u
  | Lambda(t,f) when t <> x -> Lambda(t, replace f x u)
  | Application(t,f) -> Application(replace t x u,replace f x u)
  | _ -> t

let (!!) x = Var x
let (>>>) t u = Application(t,u)
let (==>) x t = Lambda(x, t)
