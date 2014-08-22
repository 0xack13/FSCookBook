//Linked List
type List<'a> = 
 | Empty 
 | Cons of 'a * List<'a>

let ll7 = Cons(2, Cons(3,Empty))

let rec SumList l = 
    match l with 
    | Cons(x,right) -> x + (SumList right) 
    | Empty -> 0

let rec SubList l =
    match l with
    | Cons(x,right) -> x - (SubList right)
    | Empty -> 0 

printfn "%d" (SumList ll7) // 5 
printfn "%d" (SubList ll7) // -1 

type Tree<'a> = 
    | Node of (*data*)'a * (*left*)Tree<'a> * (*right*)Tree<'a> 
    | Leaf
//     4 
//  2     6 
// 1 3   5 7 
//Sample Tree
let tree7 = Node(4, Node(2, Node(1, Leaf, Leaf), Node(3, Leaf, Leaf)), Node(6, Node(5, Leaf, Leaf), Node(7, Leaf, Leaf)))

// SumTree : Tree<int> -> int 
let rec SumTree t = 
    match t with 
    | Node(x,left,right) -> x + (SumTree left) + (SumTree right) 
    | Leaf -> 0

// SumTree : Tree<int> -> int 
let rec SubTree t = 
    match t with 
    | Node(x,left,right) -> x - (SumTree left) - (SumTree right) 
    | Leaf -> 0

// InOrder : Tree<‘a> -> list<‘a>    
let rec InOrder t = 
    match t with 
    | Node(x,left,right) -> (InOrder left) @ [x] @ (InOrder right) 
    | Leaf -> []

// Height : Tree<‘a> -> int 
let rec Height t = 
    match t with 
    | Node(x,left,right) -> 1 + (max (Height left) (Height right)) 
    | Leaf -> 0

printfn "%d" (SumTree tree7) // 28 
printfn "%A" (InOrder tree7) // [1; 2; 3; 4; 5; 6; 7] 
printfn "%d" (Height tree7)  // 3
printfn "%d" (SubTree tree7)  // -20

//Fold over trees
// FoldTree : (‘a -> ‘r -> ‘r -> ‘r) -> ‘r -> Tree<‘a> -> ‘r 
let FoldTree nodeF leafV t = 
    let rec Loop t cont = 
        match t with 
        | Node(x,left,right) -> Loop left  (fun lacc ->  
                                Loop right (fun racc -> 
                                cont (nodeF x lacc racc))) 
        | Leaf -> cont leafV 
    Loop t (fun x -> x)


let InOrder t   = (FoldTree (fun x l r acc -> l (x :: (r acc))) (fun acc -> acc) t) [] 
let PreOrder t  = (FoldTree (fun x l r acc -> x :: l (r acc))   (fun acc -> acc) t) [] 
let PostOrder t = (FoldTree (fun x l r acc -> l (r (x :: acc))) (fun acc -> acc) t) [] 
//     4 
//  2     6    <– tree7 
// 1 3   5 7 
printfn "%A" (InOrder tree7)   // [1; 2; 3; 4; 5; 6; 7] 
printfn "%A" (PreOrder tree7)  // [4; 2; 1; 3; 6; 5; 7] 
printfn "%A" (PostOrder tree7) // [1; 3; 2; 5; 7; 6; 4]


let strings = [ "one"; "two"; "three" ]
let r = strings |> List.fold (fun r s -> r + s + " \n") ""
printfn "%s" r

let strings = [ "one"; "two"; "three" ]
let r = strings |> List.reduce (+) 
printfn "%s" r