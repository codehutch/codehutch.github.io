(*@
    Layout = "post";
    Title = "F# / Fable / Elmish / SVG - Fairfoil";
    Date = "2017-12-12T07:19:37";
    Tags = "fsharp fable f# svg airfoil naca fairfoil functional";
    Description = "";
*)
(*** more ***)
(**

** F#, _Elmish_, SVG - Airfoils! **
-----------------------------------

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

// Comment out the below six lines if you are writing a .fs (compiled)
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

type DigitPosition = 
| First | Second | Third | Forth

type NacaNum = Digit * Digit * Digit * Digit

type AirfoilColour = string
type AirfoilSpecifier = NacaNum * AirfoilColour 

type InterpolationDirection = 
| Up
| Down

type InterpolationFactor = float

type Model = AirfoilSpecifier * AirfoilSpecifier * InterpolationDirection * InterpolationFactor

type SubModelIndicator = 
| A
| B

type Message = 
| Increment of SubModelIndicator * DigitPosition
| Decrement of SubModelIndicator * DigitPosition
| Tick

let increment (m:Digit) =
  match m with
  | Dgt n when n > -1 && n < 9 -> Dgt (n + 1)
  | Dgt _ -> Dgt 0  

let decrement =
  increment >> increment >> increment >> increment >> increment >>
  increment >> increment >> increment >> increment
  
let applyToPosition f ((a, b, c, d):NacaNum, col) (p:DigitPosition) =
  match p with
  | First ->  (f a, b, c, d), col 
  | Second -> (a, f b, c, d), col
  | Third ->  (a, b, f c, d), col
  | Forth ->  (a, b, c, f d), col

// State
let initialState () = (((Dgt 1, Dgt 4, Dgt 1, Dgt 2), "orange"),
                       ((Dgt 9, Dgt 1, Dgt 9, Dgt 2), "purple"),
                       Up, 0.0), Cmd.none

let update (msg:Message) ((a, b, inpDir, inpFac):Model) = 
  match msg with
  | Increment (m, pos) ->
      match m with 
      | A ->  (applyToPosition increment a pos, b, inpDir, inpFac), Cmd.none
      | B ->  (a, applyToPosition increment b pos, inpDir, inpFac), Cmd.none
  | Decrement (m, pos) -> 
      match m with 
      | A -> (applyToPosition decrement a pos, b, inpDir, inpFac), Cmd.none
      | B -> (a, applyToPosition decrement b pos, inpDir, inpFac), Cmd.none
  | Tick ->
      let (Dgt aa, Dgt bb, Dgt cc, Dgt dd), colA = a 
      let (Dgt ee, Dgt ff, Dgt gg, Dgt hh), colB = b 
      (a, 
       b, 
       (if inpDir = Up && inpFac > 1.0 then Down 
        elif inpDir = Down && inpFac < 0.0 then Up
        else inpDir),
       if inpDir = Up then inpFac + 0.025 else inpFac - 0.025), Cmd.none       

let timerTick dispatch =
    window.setInterval(fun _ -> 
        dispatch Tick
    , 50) |> ignore

let subscription _ = Cmd.ofSub timerTick    

open Fable.Helpers.React
open Fable.Helpers.React.Props

let example2412 = myNaca2412

module D = Fable.Helpers.React

let makeLines a b c d col =
  let m = a / 100.0
  let p = b / 10.0
  let tt = (c * 10.0 + d) / 100.0
  let coords = naca4 100 m p tt
  let pairs = List.pairwise coords
  let makeLine x1 y1 x2 y2 = line 
                               [ X1 (!! (string x1)); Y1 (!! (string (y1 * -1.0))); 
                                 X2 (!! (string x2)); Y2 (!! (string (y2 * -1.0))); 
                                 Stroke col; !! ("stroke-width", string 0.005) ] 
                               []
  pairs |> List.map (fun ((x1, y1),(x2, y2)) -> makeLine x1 y1 x2 y2)
  //needs end! append [makeLine List.first pairs]
let makeLinesInt a b c d =
  makeLines (float a) (float b) (float c) (float d)                    
let view (afa, afb, inpDir, inpFac) dispatch = 

  let (Dgt a, Dgt b, Dgt c, Dgt d), colA = afa 
  let (Dgt e, Dgt f, Dgt g, Dgt h), colB = afb 

  let linesA = makeLinesInt a b c d colA
  let linesB = makeLinesInt e f g h colB

  let i = (double a) + ((double (e - a)) * inpFac) 
  let j = (double b) + ((double (f - b)) * inpFac) 
  let k = (double c) + ((double (g - c)) * inpFac) 
  let l = (double d) + ((double (h - d)) * inpFac)

  let linesC = makeLines i j k l "green"

  let digitButtons v p =
    D.div [ Style [ Display "inline-block" ] ]
      [ D.button [ OnClick (fun _ -> dispatch <| Increment p)  ] [ D.str "+" ] 
        D.div [ Style [ TextAlign "center" ] ] [ D.str (sprintf "%d" v) ]
        D.button [ OnClick (fun _ -> dispatch <| Decrement p)  ] [ D.str "-" ] ]             

  D.div [ Id "graphicsWrapper" ]
    [ D.div [ Id "graphicsContainer" ]
        [ D.div [ Style [ Display "inline-block" ]
                  ClassName "controls" ]
            [ digitButtons a (A, First)
              digitButtons b (A, Second)
              digitButtons c (A, Third)
              digitButtons d (A, Forth) ]

          D.div [ Style [ Display "inline-block" ]
                  ClassName "controls" ]
            [ digitButtons e (B, First)
              digitButtons f (B, Second)
              digitButtons g (B, Third)
              digitButtons h (B, Forth) ]
        ]

      svg 
        [ ViewBox "-0.15 -0.65 1.3 1.3"; unbox ("width", "40%") ]
        (List.append (List.append linesA linesB) linesC)  

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
|> Program.withSubscription subscription 
//|> Program.withConsoleTrace
|> Program.withReact "elmish-app"
|> Program.run
