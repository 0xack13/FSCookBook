//To be executed in the IS
#I "../packages/FSharp.Charting.0.90.7"
#I "../packages/Deedle.1.0.2"
#I "../packages/FSharp.Data.2.0.9"
#load "FSharp.Charting.fsx"
#load "Deedle.fsx"
#load "FSharp.Data.fsx"

// Reference the DLL to the related path
//#r "\\packages\\FSharp.Data.2.0.9\\lib\\net40\\FSharp.Data.dll"

open FSharp.Data
open System
open Deedle
open FSharp.Charting

//using F# identifier to get the current directory of the project/script
let root = __SOURCE_DIRECTORY__ + "\\"

// Read Titanic data & group rows by 'Sex'
//let root = "D:\\DevCode\\0xack13\\FSCookbook\\FSharpCookbook\\"
let titanic = Frame.ReadCsv(root + "titanic.csv").GroupRowsBy<int>("pclass")

// Get 'Survived' column and count survival count per clsas
let byClass =
  titanic.GetColumn<bool>("survived")
  |> Series.applyLevel fst (fun s ->
      // Get counts for 'True' and 'False' values of 'Survived'
      // (fun x -> x) = id
      // series = 'Series.ofObservations'
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
