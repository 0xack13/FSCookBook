// Create from sequence of keys and sequence of values
let dates  = 
  [ DateTime(2013,1,1); 
    DateTime(2013,1,4); 
    DateTime(2013,1,8) ]
let values = 
  [ 10.0; 20.0; 30.0 ]
let first = Series(dates, values)

// Create from a single list of observations
Series.ofObservations
  [ DateTime(2013,1,1) => 10.0
    DateTime(2013,1,4) => 20.0
    DateTime(2013,1,8) => 30.0 ]

// Shorter alternative to 'Series.ofObservations'
series [ 1 => 1.0; 2 => 2.0 ]

// Create series with implicit (ordinal) keys
Series.ofValues [ 10.0; 20.0; 30.0 ]

Series.ofValues [334.4; 333.4; 55.0; 5676.0;]

// A series with values for 10 days 
let second = Series(dateRange (DateTime(2013,1,1)) 20, rand 20)

//Construct a frame that comprises the two columns First & Second
let df1 = Frame(["first"; "second"], [first; second])

// The same as previously
let df2 = Frame.ofColumns ["first" => first; "second" => second]

// Transposed - here, rows are "first" and "second" & columns are dates
let df3 = Frame.ofRows ["first" => first; "second" => second]

// Create from individual observations (row * column * value)
let df4 = 
  [ ("Monday", "Tomas", 1.0); ("Tuesday", "Adam", 2.1)
    ("Tuesday", "Tomas", 4.0); ("Wednesday", "Tomas", -5.4) ]
  |> Frame.ofValues

// Assuming we have a record 'Price' and a collection 'values'
type Price = { Day : DateTime; Open : float }
let prices = 
  [ { Day = DateTime.Now; Open = 10.1 }
    { Day = DateTime.Now.AddDays(1.0); Open = 15.1 }
    { Day = DateTime.Now.AddDays(2.0); Open = 9.1 } ]

// Creates a data frame with columns 'Day' and 'Open'
let df5 = Frame.ofRecords prices

//Number of odds or even numbers in a sequence
let mySeq1 = seq { 1.. 100 }
let printSeq seq1 = Seq.iter (printf "%A ") seq1; printfn "" 
let seqResult = Seq.countBy (fun elem ->
    if (elem % 2 = 0) then 0 else 1) mySeq1
printSeq seqResult

//using id
let list = [1;2;3;4;5;6;1;2;3;1;1;2]
let results = list |> Seq.countBy id |> Seq.toList 
printfn "%A" results

//using frame
let df = frame [ "S" => series [ 1 => (1, "One"); 2 => (2, "Two") ] ]  
df |> Frame.expandCols ["S"]