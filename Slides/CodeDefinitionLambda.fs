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
  | Not
  | And
  | Or
  | Plus
  | Mult
  | Highlighted of Term
  with
    member this.ToLambdaInner =
      match this with
      | Lambda(x,t) -> sprintf @"%s$\rightarrow$%s" x (t.ToLambdaInner)
      | _ -> this.ToLambda
    member this.ToLambda =
      match this with
      | Var s -> s
      | Highlighted t -> sprintf @"(*@\textul{%s}@*)" (t.ToLambda)
      | Application(Application(And,t),u) -> sprintf "(%s $\wedge$ %s)" (t.ToLambda) (u.ToLambda)
      | Application(Application(Or,t),u) -> sprintf "(%s $\wedge$ %s)" (t.ToLambda) (u.ToLambda)
      | Application(t,u) -> sprintf "(%s %s)" (t.ToLambda) (u.ToLambda)
      | Lambda(x,t) -> sprintf @"($\lambda$%s$\rightarrow$%s)" x (t.ToLambdaInner)
      | True -> sprintf "TRUE"
      | False -> sprintf "FALSE"
      | Not -> sprintf "$\neg$"
      | And -> sprintf "$\wedge$"
      | Or -> sprintf "$\vee$"
      | Plus -> sprintf "+"
      | Mult -> sprintf "$\times$"
    member this.ToString =
      match this with
      | Var s -> s
      | Highlighted t -> t.ToString
      | Application(Application(And,t),u) -> sprintf "(%s AND %s)" (t.ToString) (u.ToString)
      | Application(Application(Or,t),u) -> sprintf "(%s OR %s)" (t.ToString) (u.ToString)
      | Application(t,u) -> sprintf "(%s %s)" (t.ToString) (u.ToString)
      | Lambda(x,t) -> sprintf @"(\%s.%s)" x (t.ToString)
      | True -> sprintf "TRUE"
      | False -> sprintf "FALSE"
      | And -> sprintf "AND"
      | Or -> sprintf "OR"
      | Not -> sprintf "!"
      | Plus -> sprintf "+"
      | Mult -> sprintf "*"

let (!!) x = Var x
let (>>>) t u = Application(t,u)
let (==>) x t = Lambda(x, t)

let defaultTerms : Map<Term, Term> =
  [
    True, ("t" ==> ("f" ==> (!!"t")))
    False, ("t" ==> ("f" ==> (!!"f")))
    Not, ("p" ==> ("a" ==> ("b" ==> (!!"p" >>> !!"b" >>> !!"a"))))
    And, ("p" ==> ("q" ==> (!!"p" >>> !!"q" >>> !!"p")))
    Or, ("p" ==> ("q" ==> (!!"p" >>> !!"p" >>> !!"q")))
    Plus, ("m" ==> ("n" ==> ("s" ==> ("z" ==> ((!!"m" >>> !!"s") >>> ((!!"n" >>> !!"s") >>> !!"z"))))))
    Mult, ("m" ==> ("n" ==> ("s" ==> (!!"m" >>> (!!"n" >>> !!"s")))))
  ] |> Map.ofList

let deltaRules (t:Term) : Option<Term> =
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

let rec reduce maxSteps p : Coroutine<(Term -> Term) * Term, bool> =
  let rec reduce_step p = 
    co{
      if maxSteps <= 0 then
        return false
      else
        let! (k,t) = getState
        match t with
        | Highlighted t ->
          do! setState((fun t -> k t), t)
          let! res = reduce_step p
          let! (_,t) = getState
          do! setState((fun t -> k(Highlighted(t))), t)
          return res
        | Var x -> 
          return false
        | Lambda(x,f) -> 
          return false
        | Application(Lambda(x,f),u) ->
            do! setState ((fun u -> k(Application(Lambda(x,f),u))), u)
            let! replaced = reduce_step p
            do! if replaced then p else ret ()
            let! (k1,v) = getState
            do! setState ((fun v -> k(Highlighted(Application(Lambda(x,f),v)))), v)
            do! p
            let f_new = replace f x v
            do! setState (k,f_new)
            do! p
            return true
        | Application(Var x,u) -> 
          return false
        | Application(t,u) ->
            do! setState ((fun t -> k(Application(t,u))), t)
            let! replacedT = reduce_step p
            let! (k1,t_new) = getState
            do! setState ((fun u -> k(Application(t_new,u))), u)
            let! replacedU = reduce_step p
            let! (k2,u_new) = getState
            do! setState (k, Application(t_new,u_new))
            return replacedT || replacedU
        | t -> 
          match deltaRules t with
          | Some t' ->
            do! setState (k, Highlighted(t))
            do! p
            do! setState (k, t')
            do! p
            return true
          | _ ->
            return false
    }
  co{
    let! replaced = reduce_step p
    if replaced then
      return! reduce (maxSteps-1) p
    else
      return false
  }

and replace t x u =
  match t with
  | Var s when s = x -> u
  | Lambda(t,f) when t <> x -> Lambda(t, replace f x u)
  | Application(t,f) -> Application(replace t x u,replace f x u)
  | _ -> t