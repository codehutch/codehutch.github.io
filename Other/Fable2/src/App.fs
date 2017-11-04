module App

open System
open Fable.Import.Browser
open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.React.Props
open Elmish
open Elmish.React

// Types

type Digit = 
| D of int

type Position = 
| First | Second | Third | Forth

type Model = Digit * Digit * Digit * Digit

type Message = 
| Increment of Position
| Decrement of Position

let increment (m:Digit) =
  match m with
  | D n when n > -1 && n < 9 -> D (n + 1)
  | D _ -> D 0  

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
let initialState () = (D 2, D 4, D 1, D 2), Cmd.none

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
module D = Fable.Helpers.React

let view (a, b, c, d) dispatch =
  D.div []
      [ D.button [ OnClick (fun _ -> dispatch <| Decrement First) ] [ D.str "-" ]
        D.div [] [ D.str (sprintf "%A %A %A %A" a b c d) ]
        D.button [ OnClick (fun _ -> dispatch <| Increment First) ] [ D.str "+" ] ]
    (*
    svg 
      [ ViewBox "0 0 100 100"; unbox ("width", "350px") ]
      [ circle 
          [ Cx (!! "50"); Cy (!! "50"); R (!! "45"); !! ("fill", "#0B79CE") ] 
          []
        // Hours
        clockHand (Hour time.Hour) "orange" "2" 25.0
        handTop time.Hour "orange" 25.b, c0 12.0
        // Minutes
        clockHand (Minute time.Minute) "purple" "2" 35.0
        handTop time.Minute "purple" 35.0 60.0
        // Seconds
        clockHand (Second time.Second) "#023963" "1" 40.0 
        handTop time.Second "#023963" 40.0 60.0
        // circle in the center
        circle 
          [ Cx (!! "50"); Cy (!! "50"); R (!! "3"); !! ("fill", "#0B79CE") ; Stroke "#023963"; !! ("stroke-width", 1.0) ] 
          []
      ]
    *)  

// App
Program.mkProgram initialState update view
|> Program.withConsoleTrace
|> Program.withReact "elmish-app"
|> Program.run
