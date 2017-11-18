module App

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
let initialState () = (Dgt 2, Dgt 4, Dgt 1, Dgt 2), Cmd.none

let update (msg:Message) (model:Model) = 
  match msg with
  | Increment pos -> applyToPosition increment model pos, Cmd.none
  | Decrement pos -> applyToPosition decrement model pos, Cmd.none
    
open Fable.Helpers.React
open Fable.Helpers.React.Props

(*
let clockHand time color width length = 
    let clockPercentage = 
        match time with 
        | Hour n -> (float n) / 12.0
        | Second n -> (float n) / 60.0
        | Minute n -> (float n) / 60.0
    let angle = 2.0 * Math.PI * clockPercentage
    let handX = (50.0 + length * cos (angle - Math.PI / 2.0))
    let handY = (50.0 + length * sin (angle - Math.PI / 2.0))
    line [ X1 (!! "50"); Y1 (!! "50"); X2 (!! (string handX)); Y2 (!! (string handY)); Stroke color; !! ("stroke-width", string width) ] []

let handTop n color length fullRound = 
    let revolution = float n
    let angle = 2.0 * Math.PI * (revolution / fullRound)
    let handX = (50.0 + length * cos (angle - Math.PI / 2.0))
    let handY = (50.0 + length * sin (angle - Math.PI / 2.0))
    circle [ Cx (!! (string handX)); Cy (!! (string handY)); R (!! "2"); !! ("fill", color) ] []

*)

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
              //needs end! @ [makeLine List.first pairs]
                                           
  D.div []
   [
    D.div []
      [ D.button [ OnClick (fun _ -> dispatch <| Increment First)  ] [ D.str "+" ]
        D.button [ OnClick (fun _ -> dispatch <| Increment Second) ] [ D.str "+" ]
        D.button [ OnClick (fun _ -> dispatch <| Increment Third)  ] [ D.str "+" ]
        D.button [ OnClick (fun _ -> dispatch <| Increment Forth)  ] [ D.str "+" ]
        D.div [] [ D.str (sprintf "%A %A %A %A 2303" a b c d) ]
        D.button [ OnClick (fun _ -> dispatch <| Decrement First)  ] [ D.str "-" ]
        D.button [ OnClick (fun _ -> dispatch <| Decrement Second) ] [ D.str "-" ]
        D.button [ OnClick (fun _ -> dispatch <| Decrement Third)  ] [ D.str "-" ]
        D.button [ OnClick (fun _ -> dispatch <| Decrement Forth)  ] [ D.str "-" ]        
        //D.div [] [ D.str (sprintf "%A" coords) ] 
      ]
    
    svg 
      [ ViewBox "-0.1 -0.6 1.2 1.2"; unbox ("width", "50%") ]
      lines 

        // circle in the center
        //circle 
        //  [ Cx (!! "50"); Cy (!! "50"); R (!! "3"); !! ("fill", "#0B79CE") ; Stroke "#023963"; !! ("stroke-width", 1.0) ] 
        //  []
      
   ]  

// App
Program.mkProgram initialState update view
|> Program.withConsoleTrace
|> Program.withReact "elmish-app"
|> Program.run
