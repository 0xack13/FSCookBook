type Tree<'a> = 
    | Node of (*data*)'a * (*left*)Tree<'a> * (*right*)Tree<'a> 
    | Leaf
//     4 
//  2     6 
// 1 3   5 7 
let tree7 = Node(4, Node(2, Node(1, Leaf, Leaf), Node(3, Leaf, Leaf)), Node(6, Node(5, Leaf, Leaf), Node(7, Leaf, Leaf)))