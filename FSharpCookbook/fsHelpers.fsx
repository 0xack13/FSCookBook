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

//Active Patterns
let (|Even|Odd|) input = if input % 2 = 0 then Even else Odd

let TestNumber input =
   match input with
   | Even -> printfn "%d is even" input
   | Odd -> printfn "%d is odd" input

TestNumber 7
TestNumber 11
TestNumber 32

open System.Text.RegularExpressions

let testString = "http://www.bob.com http://www.b.com http://www.bob.com http://www.bill.com"

let matches input =
    Regex.Matches(input, "(http:\/\/\S+)") 
    |> Seq.cast<Match>
    |> Seq.groupBy (fun m -> m.Value)
    |> Seq.map (fun (value, groups) -> value, (groups |> Seq.length))

//matches testString;;

open System.Text.RegularExpressions

// ParseRegex parses a regular expression and returns a list of the strings that match each group in 
// the regular expression. 
// List.tail is called to eliminate the first element in the list, which is the full matched expression, 
// since only the matches for each group are wanted. 
let (|ParseRegex|_|) regex str =
   let m = Regex(regex).Match(str)
   if m.Success
   then Some (List.tail [ for x in m.Groups -> x.Value ])
   else None

// Three different date formats are demonstrated here. The first matches two- 
// digit dates and the second matches full dates. This code assumes that if a two-digit 
// date is provided, it is an abbreviation, not a year in the first century. 
let parseDate str =
   match str with
     | ParseRegex "(\d{1,2})/(\d{1,2})/(\d{1,2})$" [Integer m; Integer d; Integer y]
          -> new System.DateTime(y + 2000, m, d)
     | ParseRegex "(\d{1,2})/(\d{1,2})/(\d{3,4})" [Integer m; Integer d; Integer y]
          -> new System.DateTime(y, m, d)
     | ParseRegex "(\d{1,4})-(\d{1,2})-(\d{1,2})" [Integer y; Integer m; Integer d]
          -> new System.DateTime(y, m, d)
     | _ -> new System.DateTime()

let dt1 = parseDate "12/22/08" 
let dt2 = parseDate "1/1/2009" 
let dt3 = parseDate "2008-1-15" 
let dt4 = parseDate "1995-12-28"

printfn "%s %s %s %s" (dt1.ToString()) (dt2.ToString()) (dt3.ToString()) (dt4.ToString())


open System
 
let rec fact x =
    if x < 1 then 1
    else x * fact (x - 1)
 
(* // can also be written using pattern matching syntax:
let rec fact = function
    | n when n < 1 -> 1
    | n -> n * fact (n - 1) *)
 
Console.WriteLine(fact 6)


// create an immutable list
let list1 = [1;2;3;4]   

// prepend to make a new list
let list2 = 0::list1    

// get the last 4 of the second list 
let list3 = list2.Tail

// the two lists are the identical object in memory!
System.Object.ReferenceEquals(list1,list3)



let nums = [1; 2; 3; 4; 5] 
let odds = nums |> List.filter (fun x -> x%2 = 1) 
printfn "odds = %A" odds // odds = [1; 3; 5]

// Lexical scoping
let a = 1 in  
  (printfn "%i" a;
   (let a = a + 1 in printfn "%i" a);
   printfn "%i" a)

// Sequence using Range Expressions
let one= seq { 0 .. 100 }
printfn "%A" one
 
//Sequence using Range Expressions of characters
let two= seq {'a'..'z'}
printfn "%A" two 
 
//Sequence with increment
let three= seq {1..2..10}
printfn "%A" three
 
//sequence with yield keyword
let four= seq { for i in 1 .. 5 do yield i * i }
printfn "%A" four
 
//sequence with -> operator 
let five= seq { for i in 1 .. 5 -> i * i }
printfn "%A" five
 
//Sequence with decrement
let six= seq {10..-1..1}
printfn "%A" six


let extractLinks url =
    async {
        let webClient = new System.Net.WebClient() 
 
        printfn "Downloading %s" url
        let html = webClient.DownloadString(url : string)
        printfn "Got %i bytes" html.Length
 
        let matches = System.Text.RegularExpressions.Regex.Matches(html, @"http://\S+")
        printfn "Got %i links" matches.Count
 
        return url, matches.Count
    };;
 
val extractLinks : string -> Async<string * int>

Async.RunSynchronously (extractLinks "http://www.yahoo.com/");;


//Using yield keyword
let tens = seq { 0 .. 10 .. 100}




open System.Text.RegularExpressions
open System.Net
 
let download url =
    let webclient = new System.Net.WebClient()
    webclient.DownloadString(url : string)
 
let extractLinks html = Regex.Matches(html, @"http://\S+")
 
let downloadAndExtractLinks url =
    let links = (url |> download |> extractLinks)
    url, links.Count
 
let urls =
     [@"http://www.craigslist.com/";
     @"http://www.msn.com/";
     @"http://en.wikibooks.org/wiki/Main_Page";
     @"http://www.wordpress.com/";
     @"http://news.google.com/";]
 
let pmap f l =
    seq { for a in l -> async { return f a } }
    |> Async.Parallel
    |> Async.Run
 
let testSynchronous() = List.map downloadAndExtractLinks urls
let testAsynchronous() = pmap downloadAndExtractLinks urls
 
let time msg f =
    let stopwatch = System.Diagnostics.Stopwatch.StartNew()
    let temp = f()
    stopwatch.Stop()
    printfn "(%f ms) %s: %A" stopwatch.Elapsed.TotalMilliseconds msg temp
 
let main() =
    printfn "Start..."
    time "Synchronous" testSynchronous
    time "Asynchronous" testAsynchronous
    printfn "Done."
 
main()