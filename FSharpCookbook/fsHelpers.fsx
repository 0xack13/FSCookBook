#light
#indent "off"

open System
//let random = new System.Random()
//let rnd() = random.NextDouble()
//let data = [for i in 1 .. 500 -> rnd() * 10.0]

//let avg = data |> Seq.average

//let random = new System.Random()
//let rnd() = random.NextDouble()
//let data = [for i in 1 .. 10 -> rnd() * 10.0]

//val data : float list =[5.0530272; 6.389536232; 6.126554094; 7.276151291; 0.9457452972; 7.774030933; 7.654594368; 8.517372011; 3.924642724; 6.572755164]

//let min = data |> Seq.min

let random = new System.Random()
let rnd() = random.NextDouble()
let data = [for i in 1 .. 5 -> rnd() * 100.0]
let avg = data |> Seq.average

//val data : float list =[7.586052086; 22.3457242; 76.95953826; 59.31953153; 33.53864822]

let max = data |> Seq.max

let variance (values) = 
    let average = Seq.average values
    values
    |> Seq.map (fun x -> (1.0 / float (Seq.length values)) * (x - average) ** 2.0)
    |> Seq.sum