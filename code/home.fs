module Pollz.Home

open Suave
open Suave.Http
open Suave.Types
open Suave.Http.Applicatives
open System.IO
open FSharp.Data
open Pollz.Data

// ----------------------------------------------------------------------------
// STEP #1: Implement loading of polls for the home page. The `Poll` type 
// represents information about individual poll and we want to get a list of
// those - one for each JSON file in the "polls" sub-directory
// ----------------------------------------------------------------------------

/// This type represents the model for the home page
/// (we call the rendering with a sequence of `Poll` values)
type Poll =
  { Link : string
    Title : string 
    Question : string }

// The polls are stored in the following folder
// (`dataFolder` is defined in `data.fs`)
let pollsFolder = dataFolder + "/polls"

// Get all the files in `pollsFolder` using `Directory.GetFiles`
// and use the JSON provider generated file (`PollJson` in `data.fs`)
// to load the detailed information about the file. To get a nice
// name for the link, use `Path.GetFileNameWithoutExtension` on the
// full file name:

let ``TUTORIAL DEMO #1`` () = 
  // Say we have a file name 'file'
  let file = "???"
  // Then we can load poll data for the file
  let pollInfo = PollJson.Load(file)
  // And we can easily access data from JSON!
  pollInfo


// You can delee the `TUTORIAL DEMO #1` function and implement the following:
let homePolls () = 
  [ { Link = "test"
      Title = "Fake question"
      Question = "What is your favourite colour?"} ]

// When we receive a request to the '/' path, the lambda function is
// invoked, we load data using the 'homePolls' function and pass it
// as the model to the `home.html` DotLiquid template.
let part = path "/" >>= request (fun req -> 
  DotLiquid.page "home.html" (homePolls ()))

// ----------------------------------------------------------------------------
// STEP #8: Sort the polls on the home page by the total number of votes.
// To do this, add `Votes` of type `int` to the `Poll` record and then
// Use `List.sortBy` to sort the list. You should also go to `home.html`
// and modify it so that it displays the total number of votes!
// ----------------------------------------------------------------------------
