module CodeDefinitionLambda

open Coroutine
open CommonLatex
open System.Collections.Generic


type Term =
  | Var of string
  | Application of Term * Term
  | Lambda of string * Term
  | True
  | False
  | And
  | Or
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

let (!!) x = Var x
let (>>>) t u = Application(t,u)
let (==>) x t = Lambda(x, t)

let defaultTerms : Map<Term, Term> =
  [
    !!"TRUE", ("t" ==> ("f" ==> (!!"t")))
    !!"FALSE", ("t" ==> ("f" ==> (!!"f")))
    !!"NOT", ("p" ==> ("a" ==> ("b" ==> (!!"p" >>> !!"b" >>> !!"a"))))
    !!"AND", ("p" ==> ("q" ==> (!!"p" >>> !!"q" >>> !!"p")))
    !!"OR", ("p" ==> ("q" ==> (!!"p" >>> !!"p" >>> !!"q")))
  ] |> Map.ofList

let replace_default (t:Term) : Option<Term> =
  match defaultTerms |> Map.tryFind t with
  | Some v -> Some v
  | _ ->
    match t with
    | Var v ->
      let res = ref 0
      if System.Int32.TryParse(v, res) then
        let mutable t = !!"z"
        for i = 1 to res.Value do
          t <- !!"s" >>> t
        t <- "s" ==> ("z" ==> t)
        Some t
      else
        Option.None
    | _ -> 
      Option.None

let rec reduce p : Coroutine<(Term -> Term) * Term, bool> =
  let rec reduce_step p = 
    co{
      let! (k,t) = getState
      match t with
      | Var x -> 
        match replace_default t with
        | Some t' ->
          do! setState (k, t')
          return true
        | _ ->
          return false
      | Lambda(x,f) -> 
        return false
      | Application(Lambda(x,f),u) ->
          do! setState ((fun u -> k(Application(Lambda(x,f),u))), u)
          let! replaced = reduce_step p
          let! (_,v) = getState
          let f_new = replace f x v
          do! setState (k,f_new)
          do! p
          return true
      | Application(Var x,u) -> 
        return false
      | Application(t,u) ->
          do! setState ((fun t -> k(Application(t,u))), t)
          let! replacedT = reduce_step p
          let! (_,t_new) = getState
          do! setState ((fun u -> k(Application(t,u))), u)
          let! replacedU = reduce_step p
          let! (_,u_new) = getState
          do! setState (k, Application(t_new,u_new))
          return replacedT || replacedU
    }
  co{
    let! (a,b) = getState
    let! (k,_) = getState
    let! replaced = reduce_step p
    let! (_,t) = getState
    do! setState (k,t)
    let! (c,d) = getState
    //do printfn "%s => %s (r=%b)" (a b).ToString (c d).ToString replaced
    if replaced then
      //do! p
      return! reduce p
    else
      return false
  }

and replace t x u =
  match t with
  | Var s when s = x -> u
  | Lambda(t,f) when t <> x -> Lambda(t, replace f x u)
  | Application(t,f) -> Application(replace t x u,replace f x u)
  | _ -> t