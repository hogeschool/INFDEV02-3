module Coroutine

type Coroutine<'s,'a> = 's -> Result<'s,'a>
and Result<'s,'a> = Done of 's * 'a | Pause of 's * Coroutine<'s,'a>

let ret x : Coroutine<'s,'a> = fun s -> Done(s,x)
let rec (>>=) (p : Coroutine<'s,'a>) (k : 'a -> Coroutine<'s,'b>) : Coroutine<'s,'b> =
  fun s0 -> 
    match p s0 with
    | Done(s1,x) -> k x s1
    | Pause(s1,p') -> Pause(s1,p' >>= k)
let pause : Coroutine<'s,Unit> = fun s -> Pause(s,ret())

type CoroutineBuilder() =
  member this.ReturnFrom p = p
  member this.Return x = ret x
  member this.Zero() = ret ()
  member this.Bind(p,k) = p >>= k
  member this.Combine(p,k) = p >>= (fun () -> k)
let co = CoroutineBuilder()

let getState : Coroutine<'s,'s> = fun s -> Done(s,s)
let setState s' : Coroutine<'s,Unit> = fun s -> Done(s',())
let rec mapCo f l = 
  co{
    match l with
    | [] -> return []
    | x::xs -> 
      let! y = f x
      let! ys = mapCo f xs
      return y :: ys
  }

let rec runToEnd p s = 
  match p s with
  | Done(s',_) -> []
  | Pause(s',p') -> s' :: runToEnd p' s'
