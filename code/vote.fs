module Pollz.Vote
open Pollz.Data
open Suave
open Suave.Cookie
open Suave.Types
open Suave.Http
open Suave.Http.Applicatives
open XPlot.GoogleCharts

// ----------------------------------------------------------------------------
// STEP #3: Now we need to generate a page to display the voting option.
// Given a poll "foo", with two options, we need to generate a page with 
// links to "/vote/foo/0" and "/vote/foo/1". To do this, you'll need to define
// the model (`Option` and `Poll` bellow), implement the `getOptions` function
// and also modify `vote.html` page in `templates` to generate the right HTML.
// ----------------------------------------------------------------------------

// The following two types define the domain. Look inside `vote.html`
// to see what properties they should have. A poll is a collection of
// options with some other information!

type Option = { TODO1 : int }

type Poll = { TODO2 : int }

// To implement `getOptions`, you can load information about the poll using
// the existing `Data.loadPoll` function. Then you can use `Array.mapi` 
// because you'll also need the index of the option (to put into the URL).

let getOptions pollName : Poll = 
  failwith "Not implemented!"

let part1 =
  pathScan "/vote/%s" (fun name ->
    DotLiquid.page "vote.html" (getOptions name) )

// ----------------------------------------------------------------------------
// STEP #4: Now we need to add another Suave web part to handle URLs of the
// format "/vote/<name>/<option>". Use `pathScan` as in `part1` above. Note
// that the format string can contain both %d and %s! Once you implement
// `part2`, go back to `app.fsx` and add `part2` to the top-level routing.
//
// Note that the order of web parts matters! The pattern `/vote/<name>`
// can handle `/vote/<name/<option>` too - but the name will be e.g. `foo/1`
// and so you need to put `part2` handler BEFORE the `part1` handler!
// ----------------------------------------------------------------------------

// TODO: Handle "/vote/<name>/<option>" ->
//    Then: Cast the vote here using Data.castVote!
//    Then: Redirect to the results page using `Redirection.FOUND "/url/to/redirect"`



// ----------------------------------------------------------------------------
// STEP #7: Using cookies to allow only one voting from each machine.
// This might not be the best protection, but better than nothing!
// After user votes, we set a cookie with the name of the poll. When they
// try to vote, we check cookies and if they already have the cookie, we
// redirect them directly to results.
// ----------------------------------------------------------------------------


let ``TUTORIAL DEMO #7`` () = 
  // The easiest way to create a cookie is probably this one
  let ck = { HttpCookie.empty with name = "testing"; value = "123"}
  
  // You can set cookie as part of a response by chaining the `setCookie`
  // web part with a web part that returns the result, e.g.
  let part1 = 
    Cookie.setCookie ck >>=
    Successful.OK "Done!"

  // To access cookies, you can use the `request` web part:
  let part2 = 
    request (fun req -> 
      let cookieMap = req.cookies
      Successful.OK "Done!" )

  ()