#r @"../packages/MathNet.Numerics.3.2.1\lib\net40\MathNet.Numerics.dll"
#r @"../packages/FSharp.Data.2.0.9\lib\net40\FSharp.Data.dll"
#r @"../packages/MathNet.Numerics.FSharp.3.2.1\lib\net40\MathNet.Numerics.FSharp.dll"
#r @"../packages/RProvider.1.0.15\lib\net40\RProvider.dll"
#I "../packages/FSharp.Charting.0.90.7"
#I "../packages/Deedle.1.0.2"
#load "FSharp.Charting.fsx"
#load "Deedle.fsx"

open System
open FSharp.Charting
open FSharp.Data
open Deedle
open RProvider

type UsDebt = CsvProvider<"C:\Data\us-debt.csv">
let csv = UsDebt.Load("C:\Data\us-debt.csv")

let debtSeries =
    series [ for row in csv.Rows -> row.Year, row.``Debt (percent GDP)`` ]

Chart.Line([ for row in csv.Rows -> row.Year, row.``Debt (percent GDP)`` ])

[debtSeries |> Series.observations |> Chart.Line]

let debt = Frame.ofColumns [ "Debt" => debtSeries ]



//Listing US Presidents using Freebase Data
let fb = FreebaseData.GetDataContext()
let presidentInfos =
    query { for p in fb.Society.Government.``US Presidents`` do
        sortBy (Seq.max p.``President number``)
        skip 23 }

let presidentTerms =
    [ for pres in presidentInfos do
        for pos in pres.``Government Positions Held`` do
        if string pos.``Basic title`` = "President" then
            // Get start and end year of the position
            let starty = DateTime.Parse(pos.From).Year
            let endy = if pos.To = null then 2013 else
                DateTime.Parse(pos.To).Year
            // Return three element tuple with the info
            yield (pres.Name, starty, endy) ]

let presidents =
    presidentTerms
    |> Frame.ofRecords
    |> Frame.indexColsWith ["President"; "Start"; "End"]

// Analyse debt change
let byEnd = presidents |> Frame.indexRowsInt "End"
let endDebt = byEnd.Join(debt, JoinKind.Left)

