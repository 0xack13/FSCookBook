open Microsoft.FSharp.Reflection

let printTupleValues x =
    if FSharpType.IsTuple( x.GetType() ) then
        let s =
            FSharpValue.GetTupleFields( x )
            |> Array.map (fun a -> a.ToString())
            |> Array.reduce (fun a b -> sprintf "%s, %s" a b)
        printfn "(%s)" s
    else 
        printfn "not a tuple"

//printTupleValues ("hello world", 1)
