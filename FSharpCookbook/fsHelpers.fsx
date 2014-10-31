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

monkeys.["Marmoset"];;

let holidays =
    Map.empty. (* Start with empty Map *)
        Add("Christmas", "Dec. 25").
        Add("Halloween", "Oct. 31").
        Add("Darwin Day", "Feb. 12").
        Add("World Vegan Day", "Nov. 1");;
holidays.["Christmas"];;
//val holidays : Map<string,string>

let simpleArray = [|1;3;4;5;6;|];

type record1 = {
  value1:string;
  value2:string
}

let myArray  = Array.init 100 (fun x -> {value1 = "x"; value2 = "y"})

//using Generics
//let myArray  = Array.init<record1> 100 (fun x -> {value1 = "x"; value2 = "y"})
//Moving Average
let sma period f (list:float list) =
    let sma_aux queue v =
        let q = Seq.truncate period (v :: queue)
        Seq.average q, Seq.toList q
    List.fold (fun s v ->
        let avg,state = sma_aux s v
        f avg
        state) [] list
 
printf "sma3: "
[ 1.;2.;3.;4.;5.;5.;4.;3.;2.;1.] |> sma 3 (printf "%.2f ")
printf "\nsma5: "
[ 1.;2.;3.;4.;5.;5.;4.;3.;2.;1.] |> sma 5 (printf "%.2f ")
printfn ""



//Using Take and use full sequence printing
let list = [ 1 .. 10 ]
let res = list |> Seq.take 5
printf "Res Sequence: %A" (res |> Seq.toList)

let mySeq = seq { for i in 1 .. 10 -> i*i }
let truncatedSeq = Seq.truncate 5 mySeq
let takenSeq = Seq.take 5 mySeq

let truncatedSeq2 = Seq.truncate 20 mySeq
let takenSeq2 = Seq.take 20 mySeq

let printSeq seq1 = Seq.iter (printf "Sequence is: %A ") seq1; printfn "" 

// Up to this point, the sequences are not evaluated. 
// The following code causes the sequences to be evaluated.
truncatedSeq |> printSeq
truncatedSeq2 |> printSeq
takenSeq |> printSeq
// The following line produces a run-time error (in printSeq):
takenSeq2 |> printSeq

//Using Take and use full sequence printing
let list = [ 1 .. 10 ]
let res = list |> Seq.take 5
printf "Res Sequence: %A" (res |> Seq.toList)

let mySeq = seq { for i in 1 .. 10 -> i*i }
let truncatedSeq = Seq.truncate 5 mySeq
let takenSeq = Seq.take 5 mySeq

let truncatedSeq2 = Seq.truncate 20 mySeq
let takenSeq2 = Seq.take 20 mySeq

let printSeq seq1 = Seq.iter (printf "%A ") seq1; printfn "" 

// Up to this point, the sequences are not evaluated. 
// The following code causes the sequences to be evaluated.
truncatedSeq |> printSeq
truncatedSeq2 |> printSeq
takenSeq |> printSeq
// The following line produces a run-time error (in printSeq):
takenSeq2 |> printSeq



// Simple example of map-reduce  in F#
// Counts the total numbers of each animal
 
// Map function for our problem domain
let mapfunc (k,v) =
    v |> Seq.map (fun(pet) -> (pet, 1))
 
// Reduce function for our problem domain
let reducefunc (k,(vs:seq<int>)) =
    let count = vs |> Seq.sum
    k, Seq.ofList([count])
 
// Performs map-reduce operation on a given set of input tuples
let mapreduce map reduce (inputs:seq<_*_>) =
    let intermediates = inputs |> Seq.map map |> Seq.concat
    let groupings = intermediates |> Seq.groupBy fst |> Seq.map (fun(x,y) -> x, Seq.map snd y)
    let results = groupings |> Seq.map reduce
    results
 
// Run the example...
let alice = ("Alice",["Dog";"Cat"])
let bob = ("Bob",["Cat"])
let charlie = ("Charlie",["Mouse"; "Cat"; "Dog"])
let dennis = ("Dennis",[])
 
let people = [alice;bob;charlie;dennis]
 
let results = people |> mapreduce mapfunc reducefunc
 
for result in results do
    let animal = fst result
    let count = ((snd result) |> Seq.toArray).[0]
    printfn "%s : %s" animal (count.ToString())
 
printfn "Press any key to exit."
 
System.Console.ReadKey() |> ignore

/// Map/Reduce Count Words
/// maps an input into a tuple of input and count
let mapper x =
    (x, 1)

/// reduces a list of input and count tuples, summing the counts
let reducer (a:(string * int) list) (x:string * int) =
    if List.length a > 0 && fst (List.head a) = fst x then
        (fst (List.head a), snd (List.head a) + snd x)::List.tail a
    else
        x::a            

/// maps the mapper function over a list of input
let map xs =
    List.map (mapper) xs

/// maps the reducer function over a list of input and count tuples
let reduce (xs:(string * int) list) = 
    List.fold (reducer) [] xs

/// maps the input, sorts the intermediate data, and reduces the results
let mapReduce xs = 
    map xs
    |> List.sort
    |> reduce
