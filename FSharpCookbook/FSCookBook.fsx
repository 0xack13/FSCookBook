//Using Seq
let values = seq { 1 .. 10 }
let doubled = Seq.map (fun v -> v * 2) values

// 3 ways to print Seq variable (mainly in the interactive shell)
printfn "%A" doubled
printfn "%A" (Seq.toList doubled);;
doubled;;