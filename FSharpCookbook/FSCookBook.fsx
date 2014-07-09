//F# cookbook

// single line comments use a double slash
(* multi line comments use (* . . . *) pair

-end of multi line comment- *)

// ------ Lists ------
let twoToFive = [2;3;4;5]        // Square brackets create a list with
                                 // semicolon delimiters.
let oneToFive = 1 :: twoToFive   // :: creates list with new 1st element
// The result is [1;2;3;4;5]
let zeroToFive = [0;1] @ twoToFive   // @ concats two lists

// to define a multiline function, just use indents. No semicolons needed.
let evens list =
   let isEven x = x%2 = 0     // Define "isEven" as a sub function
   List.filter isEven list    // List.filter is a library function
                              // with two parameters: a boolean function
                              // and a list to work on

evens oneToFive               // Now run the function


// Match with example 
let simplePatternMatch =
   let x = "Jordan"
   match x with
    | "Jordan" -> printfn "x is Jordan"
    | "alibaba" -> printfn "x is Alibaba"
    | _ -> printfn "x is known yet!"   // underscore matches anything


// Using List properties
//'%A' formats any value white '%d' formats any integer value
let names = [ 1; 2; 3; 4; 5; 6; ]
printfn "names.IsEmpty is %b" (names.IsEmpty)
printfn "names.Length is %d" (names.Length)
printfn "names.Head is %d" (names.Head)
printfn "names.Tail is %A" (names.Tail)
printfn "names.Tail.Head is %A" (names.Tail.Head)
printfn "names.Tail.Tail.Head is %d" (names.Tail.Tail.Head)
printfn "names.Item(1) is %d" (names.Item(1))

// Define a generic variable
let genericA<'a> = 2 //"Hello World!" will work as well
printfn "%A" genericA

// Generic List: a & b are inferred to be the same type
let makeList a b = [a; b]
makeList "Hello " "There!"

// Type of the return type
let genericAType (a: int) : int = a + 1
printfn "%A" (genericAType 2)

// Define Generic Function
let genericFunc (x : 'a) (y : 'a) =
    printf "%A %A" x y
genericFunc("Hello ")("There!") // Result: "Hello " "There!"

// Type extension for Generic Array
type 'a ``[]`` with
  member x.GetOrDefault(n) = 
    if x.Length > n then x.[n]
    else Unchecked.defaultof<'a>
 
let arr = [|1; 2; 3|]
arr.GetOrDefault(1) //2
arr.GetOrDefault(4) //0

//Using Sequence Expression
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


// Descriminated Union
type switchstate =
    | On
    | Off
 
let toggle = function  (* pattern matching input *)
    | On -> Off
    | Off -> On
 
let x = On
let y = Off
let z = toggle y
 
printfn "x: %A" x
printfn "y: %A" y
printfn "z: %A" z
printfn "toggle z: %A" (toggle z)



// sequences use curly braces
let seq1 = seq { yield "a"; yield "b" }

// sequences can use yield and 
// can contain subsequences
let strange = seq {
    // "yield! adds one element
    yield 1; yield 2;
    
    // "yield!" adds a whole subsequence
    yield! [5..10]  
    yield! seq {
        for i in 1..10 do 
          if i%2 = 0 then yield i }}
// test                
strange |> Seq.toList              


// Sequences can be created using "unfold"
// Here's the fibonacci series - unfold will be returning a sequence based on a computation
let fib = Seq.unfold (fun (fst,snd) ->
    Some(fst + snd, (snd, fst + snd))) (0,1)

// take returns elements up to the specified count "10 in our case"
let fib10 = fib |> Seq.take 10 |> Seq.toList
printf "first 10 fibs are %A" fib10     
