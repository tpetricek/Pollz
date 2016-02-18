#r "System.Xml.Linq.dll"
#r "packages/Suave/lib/net40/Suave.dll"
#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"
#r "packages/DotLiquid/lib/NET40/DotLiquid.dll"
#r "packages/Suave.DotLiquid/lib/net40/Suave.DotLiquid.dll"
#r "packages/XPlot.GoogleCharts/lib/net45/XPlot.GoogleCharts.dll"
#r "packages/Google.DataTable.Net.Wrapper/lib/Google.DataTable.Net.Wrapper.dll"
open System
open System.Web
open System.IO
open Suave
open Suave.Web
open Suave.Http
open Suave.Types
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Http.Writers

// -------------------------------------------------------------------------------------------------
// Server entry-point and routing
// -------------------------------------------------------------------------------------------------

// Load the different components of the web application (from separate modules)  
#if DEVENV
// When building using Visual Studio, the files are already included using 
// the project file and so we do not want to #load them again.
#else
#load "code/data.fs"
#load "code/home.fs"
#load "code/results.fs"
#load "code/vote.fs"
#endif
open Pollz

// Tell DotLiquid templating engine where the page templates live
DotLiquid.setTemplatesDir (__SOURCE_DIRECTORY__ + "/templates")

// Compose the web application from individual components that handle the 
// different aspects (home page, voting page and results page)
let app =
  choose
    [ Home.part
      Vote.part1
      Results.part
      RequestErrors.NOT_FOUND "Found no handlers." ]

// -------------------------------------------------------------------------------------------------
// To run the web site, you can use `build.sh` or `build.cmd` script, which is nice because it
// automatically reloads the script when it changes. But for debugging, you can also use run or
// run with debugger in VS or XS. This runs the code below.
// -------------------------------------------------------------------------------------------------

#if INTERACTIVE
#else
let cfg =
  { defaultConfig with
      bindings = [ HttpBinding.mk' HTTP  "127.0.0.1" 8011 ]
      homeFolder = Some __SOURCE_DIRECTORY__ }
let _, server = startWebServerAsync cfg app
Async.Start(server)
System.Diagnostics.Process.Start("http://localhost:8011")
System.Console.ReadLine() |> ignore
#endif
