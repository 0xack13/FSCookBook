//F# cookbook

//Using Seq
let values = seq { 1 .. 10 }
let doubled = Seq.map (fun v -> v * 2) values // [2; 4; 6; 8; 10; 12; 14; 16; 18; 20]

// 3 ways to print Seq variable (mainly in the interactive shell)
printfn "%A" doubled
printfn "%A" (Seq.toList doubled);;
doubled;;