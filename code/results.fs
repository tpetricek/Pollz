module Pollz.Results
open Pollz.Data
open Suave
open Suave.Http
open Suave.Http.Applicatives
open XPlot.GoogleCharts

// ----------------------------------------------------------------------------
// STEP #2: Implement code to display the poll results. To do this, you first
// need to complete STEP #2 in `data.fs`. Then you'll need to implement the
// `getResults` function below using the `Data.loadPoll` helper and using
// `Data.votes` (which contains the votes)
// ----------------------------------------------------------------------------

/// For each answer, we show the answer and the number of votes
type Answer = 
  { Option : string 
    Votes : int }

/// The model contains basic poll info and a sequence of results
type Poll =
  { Title : string
    Question : string 
    Results : seq<Answer> }

let ``TUTORIAL DEMO #2`` () = 
  // Say we have a map (which is what `Data.votes` will give you)
  let testVotes = Map.ofSeq [ ("testpoll", [0;1;0;0;0;1]) ]
  // You can get the data for a given key using `TryFind`
  let optVotes = testVotes.TryFind "testpoll"
  // And you can provide a default value using `defaultArg`
  // (by default, we record no votes)
  let votes = defaultArg optVotes []

  // To count how many votes for a certain option are there, you
  // then need to count how many times a certain value occurs in 
  // the votes list. You can do that using `List.filter` and `List.length`
  [ 0;1;0;1;0 ]
  |> List.filter (fun x -> x = 0)
  |> List.length


// You can delee the `TUTORIAL DEMO #1` function and implement the following:
let getResults pollName =
  { Title = "Testing"
    Question = "What is your favourite color?"
    Results = 
        [ { Option = "Yo"; Votes = 2 }
          { Option = "Nae"; Votes = 5 } ] }

let part =
  pathScan "/results/%s" (fun name ->
    DotLiquid.page "results.html" (getResults name) )

// ----------------------------------------------------------------------------
// STEP #6: Plot the results on a chart. To do this add `Chart` property of 
// type `string` to the `Poll` (and modify `results.html` to display this).
// To get the HTML, just call `Chart.Pie` (or whichever chart you like) and
// get the `InlineHtml`.
// ----------------------------------------------------------------------------

let ``TUTORIAL DEMO #6`` () = 
  // Make a chart using XPlot
  let chart = Chart.Pie [ ("Good",12); ("Bad",1) ]
  // Get the HTML of the chart
  chart.InlineHtml