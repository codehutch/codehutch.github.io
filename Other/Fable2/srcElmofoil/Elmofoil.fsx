(*@
    Layout = "post";
    Title = "Fable / Three.js  - Hello Cube";
    Date = "2017-06-22T07:19:37";
    Tags = "fsharp threejs fable hello cube functional";
    Description = "Showcasing Fable's power with a spinning cube";
*)
(*** more ***)
(**

** F#, _Elmish_, SVG **
---------------------------------

<script src="https://cdn.polyfill.io/v2/polyfill.js?features=default,fetch"></script>
<script src="/otherOutput/fable2/elmofoil.js"></script>

<div id="elmish-app"></div>

**The F# to JS compiler [Fable](http://fable.io/) is looking ever more impressive. What better way to showcase its abilities than
by putting a spinning a cube on your screen?...** The official Fable ThreeJs / WebGL [demo](http://fable.io/samples/webGLTerrain/index.html)
is actually far superior to my effort here. This is a much cut-down version of that demo, showing how little code is needed to get F#
drawing 3D graphics in a browser.

### _**First things** first_ ###

To use Fable, your machine will need .NET Core installed - so you can use the `dotnet` command line tool. Assuming you have that, the quickest way to get started
is to clone the official Fable [samples-browser](https://github.com/fable-compiler/samples-browser) repository and then run the `restore` script
contained in its base directory, which will install the Fable extensions to the dotnet command line tool and will also then go on to run `yarn install` and
`dotnet restore` for you. This will pull down all the required npm (javascript) and paket (dotnet / nuget) dependencies. You can then replace the
code in the samples-browser/webGLTerrain/src/App.fs file with the below. _Note that you should comment out the first two lines of the below code_
(they are necessary only as I'm writing a fsx script file for this blog, whereas probably you would write a standard fs file, if not blogging). Once you've copied, pasted
and commented the code, you can then run `dotnet fable npm-run start` to compile the code to javascript and launch a dev server, then browse to
`http://localhost:8080/webGLTerrain` (which should actually now show you a plain spinning cube rather than fancy terrain). **Talking of the code,
the first lines are below**. Initially we just import the necessary namespaces and modules, then we define two functions to return the desired
size of the graphics canvas.

*)

// Comment out the below two lines if you are writing a .fs (compiled)
// file rather than a .fsx (script) file
#r "/home/hutch/.nuget/packages/fable.core/1.2.4/lib/netstandard1.6/Fable.Core.dll"
#r "/home/hutch/.nuget/packages/fable.elmish/1.0.0/lib/netstandard1.6/Fable.Elmish.dll"
#r "/home/hutch/.nuget/packages/fable.elmish.react/1.0.0/lib/netstandard1.6/Fable.Elmish.React.dll"
#r "/home/hutch/.nuget/packages/fable.import.browser/0.1.2/lib/netstandard1.6/Fable.Import.Browser.dll"
#r "/home/hutch/.nuget/packages/fable.react/1.2.2/lib/netstandard1.6/Fable.React.dll"

#load "/home/hutch/d/real/codehutch.github.io/Other/Fable2/paket-files/codehutch/fairfoil/Sample.fs"

//module App

open System
open Fable.Import.Browser
open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.React.Props
open Elmish
open Elmish.React

open AirfoilF.Core

// Types

type Digit = 
| Dgt of int

type Position = 
| First | Second | Third | Forth

type Model = Digit * Digit * Digit * Digit

type Message = 
| Increment of Position
| Decrement of Position

let increment (m:Digit) =
  match m with
  | Dgt n when n > -1 && n < 9 -> Dgt (n + 1)
  | Dgt _ -> Dgt 0  

let decrement =
  increment >> increment >> increment >> increment >> increment >>
  increment >> increment >> increment >> increment
  
let applyToPosition f ((a, b, c, d):Model) (p:Position) =
  match p with
  | First ->  (f a, b, c, d) 
  | Second -> (a, f b, c, d)
  | Third ->  (a, b, f c, d)
  | Forth ->  (a, b, c, f d)

// State
let initialState () = (Dgt 3, Dgt 4, Dgt 1, Dgt 2), Cmd.none

let update (msg:Message) (model:Model) = 
  match msg with
  | Increment pos -> applyToPosition increment model pos, Cmd.none
  | Decrement pos -> applyToPosition decrement model pos, Cmd.none
    
open Fable.Helpers.React
open Fable.Helpers.React.Props

let example2412 = myNaca2412

module D = Fable.Helpers.React

let view (Dgt a, Dgt b, Dgt c, Dgt d) dispatch =
  let m = (float a) / 100.0
  let p = (float b) / 10.0
  let tt = ((float c) * 10.0 + (float d)) / 100.0
  let coords = naca4 100 m p tt
  let pairs = List.pairwise coords
  let makeLine x1 y1 x2 y2 = line 
                               [ X1 (!! (string x1)); Y1 (!! (string (y1 * -1.0))); 
                                 X2 (!! (string x2)); Y2 (!! (string (y2 * -1.0))); 
                                 Stroke "orange"; !! ("stroke-width", string 0.005) ] 
                               []
  let lines = (pairs |> List.map (fun ((x1, y1),(x2, y2)) -> makeLine x1 y1 x2 y2))
              //needs end! append [makeLine List.first pairs]

  let digitButtons v p =
    D.div [ Style [ Display "inline-block" ] ]
      [ D.button [ OnClick (fun _ -> dispatch <| Increment p)  ] [ D.str "+" ] 
        D.div [ Style [ TextAlign "center" ] ] [ D.str (sprintf "%d" v) ]
        D.button [ OnClick (fun _ -> dispatch <| Decrement p)  ] [ D.str "-" ] ]             
                                           
  D.div []
    [ digitButtons a First
      digitButtons b Second
      digitButtons c Third
      digitButtons d Forth
           
      svg 
        [ ViewBox "-0.1 -0.6 1.2 1.2"; unbox ("width", "40%") ]
        lines 

    (* 
    D.div []
      [ D.button [ OnClick (fun _ -> dispatch <| Increment First)  ] [ D.str "+" ]
        D.button [ OnClick (fun _ -> dispatch <| Increment Second) ] [ D.str "+" ]
        D.button [ OnClick (fun _ -> dispatch <| Increment Third)  ] [ D.str "+" ]
        D.button [ OnClick (fun _ -> dispatch <| Increment Forth)  ] [ D.str "+" ]
        D.div [] [ D.str (sprintf "%A %A %A %A" a b c d) ]
        D.button [ OnClick (fun _ -> dispatch <| Decrement First)  ] [ D.str "-" ]
        D.button [ OnClick (fun _ -> dispatch <| Decrement Second) ] [ D.str "-" ]
        D.button [ OnClick (fun _ -> dispatch <| Decrement Third)  ] [ D.str "-" ]
        D.button [ OnClick (fun _ -> dispatch <| Decrement Forth)  ] [ D.str "-" ]        
      ]
    *)

        // circle in the center
        //circle 
        //  [ Cx (!! "50"); Cy (!! "50"); R (!! "3"); !! ("fill", "#0B79CE") ; Stroke "#023963"; !! ("stroke-width", 1.0) ] 
        //  []
      
   ]  


(**

### _**Making it** move_ ###

So, as we're using the movies as an analogy, we actually ought to add some movement to the scene,
a spinning cube is going to be much more impressive than a static one. Each frame we rotate the
cube a little about each of its axes to make it appear to spin. The use of requestAnimationFrame
(rather than a loop) ensures that the animation is paused if the render's target element isn't
on screen.

*)   

Program.mkProgram initialState update view
|> Program.withConsoleTrace
|> Program.withReact "elmish-app"
|> Program.run
