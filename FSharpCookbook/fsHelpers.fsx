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
    printf "%f" average
    values
    |> Seq.map (fun x -> (1.0 / float (Seq.length values)) * (x - average) ** 2.0)
    |> Seq.sum
// variance [1.0 .. 6.0];;


//Using Map Module 
let capitals =
    [("Australia", "Canberra"); ("Canada", "Ottawa"); ("China", "Beijing");
        ("Denmark", "Copenhagen"); ("Egypt", "Cairo"); ("Finland", "Helsinki");
        ("France", "Paris"); ("Germany", "Berlin"); ("India", "New Delhi");
        ("Japan", "Tokyo"); ("Mexico", "Mexico City"); ("Russia", "Moscow");
        ("Slovenia", "Ljubljana"); ("Spain", "Madrid"); ("Sweden", "Stockholm");
        ("Taiwan", "Taipei"); ("USA", "Washington D.C.")]
    |> Map.ofList
 
let rec main() =
    Console.Write("Find a capital by country (type 'q' to quit): ")
    match Console.ReadLine() with
    | "q" -> Console.WriteLine("Bye bye")
    | country ->
        match capitals.TryFind(country) with
        | Some(capital) -> Console.WriteLine("The capital of {0} is {1}\n", country, capital)
        | None -> Console.WriteLine("Country not found.\n")
        main() (* loop again *)
 
//main()

//Map of List

 let monkeys =
    [ "Squirrel Monkey", "Simia sciureus";
        "Marmoset", "Callithrix jacchus";
        "Macaque", "Macaca mulatta";
        "Gibbon", "Hylobates lar";
        "Gorilla", "Gorilla gorilla";
        "Humans", "Homo sapiens";
        "Chimpanzee", "Pan troglodytes" ]
    |> Map.ofList;; (* Convert list to Map *)