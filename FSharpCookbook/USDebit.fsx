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
    series [ for row in csv.Rows ->
        row.Year, row.``Debt (percent GDP)`` ]