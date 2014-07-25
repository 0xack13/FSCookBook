//To be executed in the IS
#I "../packages/FSharp.Charting.0.90.7"
#I "../packages/Deedle.1.0.2"
#I "../packages/FSharp.Data.2.0.9"
#load "FSharp.Charting.fsx"
#load "Deedle.fsx"
#load "FSharp.Data.fsx"

#r @"D:\DevCode\0xack13\FSCookbook\packages\FSharp.Data.2.0.9\lib\net40\FSharp.Data.dll"

open FSharp.Data
open System
open Deedle
open FSharp.Charting

// Read Titanic data & group rows by 'Sex'
let root = "D:\\DevCode\\0xack13\\FSCookbook\\FSharpCookbook\\"
let titanic = Frame.ReadCsv(root + "titanic.csv").GroupRowsBy<int>("Pclass")

// Get 'Survived' column and count survival count per clsas
let byClass =
  titanic.GetColumn<bool>("Survived")
  |> Series.applyLevel fst (fun s ->
      // Get counts for 'True' and 'False' values of 'Survived'
      series (Seq.countBy id s.Values))
  // Create frame with 'Pclass' as row and 'Died' & 'Survived' columns
  |> Frame.ofRows 
  |> Frame.sortRowsByKey
  |> Frame.indexColsWith ["Died"; "Survived"]

// Add column with Total number of males/females on Titanic
byClass?Total <- byClass?Died + byClass?Survived

// Build a data frame with nice summary of rates in percents
frame [ "Died (%)" => round (byClass?Died / byClass?Total * 100.0)
        "Survived (%)" => round (byClass?Survived / byClass?Total * 100.0) ]