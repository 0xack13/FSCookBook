//F# cookbook

//Using Seq
let values = seq { 1 .. 10 }
let doubled = Seq.map (fun v -> v * 2) values // [2; 4; 6; 8; 10; 12; 14; 16; 18; 20]

// 3 ways to print Seq variable (mainly in the interactive shell)
printfn "%A" doubled
printfn "%A" (Seq.toList doubled);;
doubled;;

// Using Some and None keywords
let keepIfPositive (a : int) = if a > 0 then Some(a) else None
keepIfPositive(-2)
keepIfPositive(2)

// Common Usage of "Option" in F#
let rec tryFindMatch pred list =
    match list with
    | head :: tail -> if pred(head)
                        then Some(head)
                        else tryFindMatch pred tail
    | [] -> None

// result1 is Some 100 and its type is int option.
let result1 = tryFindMatch (fun elem -> elem = 100) [ 200; 100; 50; 25 ] 

// result2 is None and its type is int option.
let result2 = tryFindMatch (fun elem -> elem = 26) [ 200; 100; 50; 25 ]

//Using recursive function
let add a b = a + b
add 1 2 
let rec add_all l = 
  match l with
    | [] -> 0
    | head :: tail -> head + add_all tail
add_all [1; 2; 3; 4] // returns 10
