#light

type Elem = int

type Tree = E | T of Tree * Elem * Tree

let empty = E

let rec mem = function
    | x, E -> false
    | x, T(a, y, b) when x < y -> mem(x, a)
    | x, T(a, y, b) when y < x -> mem(x, b)
    | _ -> true

let rec insert = function
    | x, E -> T(E, x, E)
    | x, T(a, y, b) when x < y -> T(insert(x, a), y, b)
    | x, T(a, y, b) when y < x -> T(a, y, insert(x, b))
    | _, s -> s

(*
let t = T(E, 1, E);;
t;;
let t1 = insert(0, t);;
t1;;
let t2 = insert(10, t1);;
t2;;
let t3 = insert(5, t2);;
t3;;
mem(10, t1);;
mem(10, t2);;
*)
