(*@
    Layout = "post";
    Title = "FSharp / Fable / ThreeJs - Maze";
    Date = "2017-07-03T08:48:31";
    Tags = "fsharp threejs fable maze functional";
    Description = "Interactive maze generation using f#, fable, threejs and NOT javascript";
*)
(*** more ***)
(**

** Fabled F# _Maze_ **
----------------------

<div id="graphicsWrapper"><div id="graphicsContainer"></div></div>

<script src="http://cdnjs.cloudflare.com/ajax/libs/three.js/r77/three.js"></script>
<script src="/otherOutput/fable1/BlogFableThreeMazeBuild.js"></script>

**I love mazes, and I wanted to try and generate them in a webpage. Trouble was, maze generation was way too complicated for my 
javascript skills. But I was sure I could do better in a more idiot-proof language like F#...** Fortunately, now that F# can be 
compiled to javascript by the wonderful [Fable](http://fable.io), maze building is (just about) achievable for my limited brain! 
The **demo above** has been _built entirely_ from the [F#](http://fsharp.org) _code in this webpage_, using [Fable](http://fable.io). 
Graphics are courtesy of Fable's bindings to [ThreeJs](https://threejs.org). (You can also view the complete code on my 
[GitHub repo](https://github.com/codehutch/codehutch.github.io/blob/source/Other/Fable1/src/app/main/07-03-fsharp-fable-three-maze.fsx) 
if you prefer)
   
*)

(*** hide ***)

#r "../../../packages/Fable.Core/lib/netstandard1.6/Fable.Core.dll"
#load "../../../node_modules/fable-import-three/Fable.Import.Three.fs"

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Three

(**

### _**Building** blocks_ ###

The first lines of code we need are below, which 

*)

// Here's our union-type for representing a maze.
type Cell = 
  | X // X is going to represent a wall
  | O // O is going to represent open space.
  | I // I represents intederminate / don't care

let o = O // Bit of a trick to get syntax-highlighting to show up better

type SmallSquare = Cell * Cell * Cell
                 * Cell * Cell * Cell
                 * Cell * Cell * Cell 

let ss a b c
       d e f 
       g h i : SmallSquare = a, b, c,
                             d, e, f,
                             g, h, i
                             
type LargeSquare = Cell * Cell * Cell * Cell * Cell
                 * Cell * Cell * Cell * Cell * Cell
                 * Cell * Cell * Cell * Cell * Cell
                 * Cell * Cell * Cell * Cell * Cell
                 * Cell * Cell * Cell * Cell * Cell

let ls a b c d e
       f g h i j
       k l m n o
       p q r s t
       u v w x y : LargeSquare = a, b, c, d, e,
                                 f, g, h, i, j,
                                 k, l, m, n, o,
                                 p, q, r, s, t, 
                                 u, v, w, x, y
        
let rotate a b c d e
           f g h i j
           k l m n o
           p q r s t
           u v w x y = ls u p k f a
                          v q l g b
                          w r m h c
                          x s n i d
                          y t o j e

let flip a b c d e
         f g h i j
         k l m n o
         p q r s t
         u v w x y = ls e d c b a
                        j i h g f
                        o n m l k
                        t s r q p
                        y x w v u

let toList a b c d e
           f g h i j
           k l m n o
           p q r s t
           u v w x y = [a; b; c; d; e; f; g; h; i; j; k; l; m; n; o; p; q; r; s; t; u; v; w; x; y]
       
let (|>>|) sf (a, b, c,
               d, e, f,
               g, h, i) = sf a b c
                             d e f
                             g h i
   
let (||>>||) mf (a, b, c, d, e,
                 f, g, h, i, j,
                 k, l, m, n, o,
                 p, q, r, s, t,
                 u, v, w, x, y) = mf a b c d e
                                     f g h i j
                                     k l m n o
                                     p q r s t
                                     u v w x y

let (|=|) (a:Cell) (b:Cell) =
    match a, b with
    | X, I -> true
    | O, I -> true
    | I, X -> true
    | I, O -> true
    | _, _ -> a = b

let (|<>|) (a:Cell) (b:Cell) =
    match a, b with
    | I, _ -> false
    | _, I -> false
    | _, _ -> a <> b

let (||=||) (a:LargeSquare) (b:LargeSquare) =
    let al = toList ||>>|| a
    let bl = toList ||>>|| b
    List.fold2 (fun s x y -> s && (x |=| y)) true al bl

let (||<>||) (a:LargeSquare) (b:LargeSquare) =
    let al = toList ||>>|| a
    let bl = toList ||>>|| b
    List.fold2 (fun s x y -> s || (x |<>| y)) false al bl

let allRotationsAndFlips (s:LargeSquare) =
  let r0   = s 
  let r90  = rotate ||>>|| r0
  let r180 = rotate ||>>|| r90
  let r270 = rotate ||>>|| r180
  let f0   = flip   ||>>|| s
  let f90  = rotate ||>>|| f0
  let f180 = rotate ||>>|| f90
  let f270 = rotate ||>>|| f180
  [r0; r90; r180; r270; f0; f90; f180; f270]

let all a b c d e 
        f g h i j
        k l m n o
        p q r s t
        u v w x y = allRotationsAndFlips <| ls a b c d e
                                               f g h i j
                                               k l m n o
                                               p q r s t
                                               u v w x y

let (|.|) x y = List.append x y

let neverValid = [ls  I I I I I 
                      I o o o I
                      I o I o I
                      I o o o I
                      I I I I I  ] // A loop isn't valid
                 |.|
                 all I I X I I 
                     I I X I I 
                     X X X I I 
                     I I I I I 
                     I I I I I     // Small closed section isn't valid
                 |.|    
                 all I I I I I
                     I I I I I 
                     X X X X X
                     I I I I I 
                     I I I I I     // Medium closed section isn't valid
                 |.|
                 [ls X X X X X
                     X I I I X
                     X I I I X
                     X I I I X
                     X X X X X  ]  // Full closed square isn't valid
                 |.|
                 all X o X o X
                     I I I I I 
                     I I I I I 
                     I I I I I 
                     I I I I I     // Two entry points on any side aren't valid
                 
let alwaysRequired = ls  X I X I X
                         I O I O I
                         X I X I X
                         I O I O I
                         X I X I X  // Standard structure boundary walls

let orthodoxLS [b; d; f; h; j; l; n; p; r; t; v; x] =
    ls  X b X d X
        f O h O j
        X l X n X
        p O r O t
        X v X x X

let allLargeSquares = 
  let rec als n b = 
    match n with
    | 0 -> b
    | n ->  als (n-1) <| ((List.map (fun a -> X :: a) b) |.| (List.map (fun a -> O :: a) b))
  let allInputs = als 12 [[]]
  List.map (fun l -> orthodoxLS l) allInputs
  |> List.filter (fun x -> List.fold (fun a y -> a && x ||<>|| y) true neverValid)
  |> List.filter (fun x -> x ||=|| alwaysRequired)
  |> Set.ofList
  |> Set.toArray

type SideReq = 
| TopReq of (Cell * Cell * Cell * Cell * Cell) option
| LeftReq of (Cell * Cell * Cell * Cell * Cell) option 

type Reqs = { Top : SideReq; Left : SideReq}

let random = new System.Random ();

let isMatch  a b c d e
             f g h i j
             k l m n o
             p q r s t
             u v w x y  a' b' c'
                        d' e' f'
                        g' h' i' 
            
            (topReq : SideReq)
            (leftReq : SideReq) =

  let l = ls a b c d e
             f g h i j
             k l m n o
             p q r s t
             u v w x y

  let sideOK ssCell lsCellA lsCellB = 
    if ssCell = X 
    then lsCellA = X && lsCellB = X 
    else lsCellA <> lsCellB

  let reqOK (req:SideReq) a'' b'' c'' d'' e'' = 
    match req with
    | TopReq None  
    | LeftReq None -> true
    | TopReq (Some (a''', b''', c''', d''', e'''))  
    | LeftReq (Some (a''', b''', c''', d''', e''')) -> 
        a'' = a''' && b'' = b''' && c'' = c''' && 
        d'' = d''' && e'' = e'''

  let topOK = sideOK b' b d
  let bottomOK = sideOK h' v x
  let leftOK = sideOK d' f p
  let rightOK = sideOK f' j t
  let topReqOK = reqOK topReq a b c d e
  let leftReqOK = reqOK leftReq a f k p u
  
  topOK && bottomOK && leftOK && rightOK && topReqOK && leftReqOK

let replace a b c
            d e f
            g h i topReq
                  leftReq = 
  
  let possibles = allLargeSquares 
                  |> Array.filter (fun s -> (isMatch ||>>|| s) a b c
                                                               d e f
                                                               g h i
                                                               topReq 
                                                               leftReq)
  let n = Array.length possibles
  let choice = int (double n * random.NextDouble ())

  Array.item choice possibles                                                                 

let getBottomAsTopReq a b c d e
                      f g h i j
                      k l m n o
                      p q r s t
                      u v w x y = TopReq (Some (u, v, w, x, y))

let getRightAsLeftReq a b c d e
                      f g h i j
                      k l m n o
                      p q r s t
                      u v w x y = TopReq (Some (e, j, o, t, y))



let decompose a b c d e 
              f g h i j
              k l m n o
              p q r s t
              u v w x y  = (ss a b c
                               f g h
                               k l m, ss c d e
                                         h i j
                                         m n o,
                            ss k l m
                               p q r
                               u v w, ss m n o
                                         r s t
                                         w x y)

type Maze = LargeSquare list list

let replaceSquare sq topReqL topReqR leftReqT leftReqB =
  let (tl, tr,
       bl, br) = decompose ||>>|| sq    
  let ntl = (replace |>>| tl) topReqL                        leftReqT
  let ntr = (replace |>>| tr) topReqR                        (getRightAsLeftReq ||>>|| ntl)
  let nbl = (replace |>>| bl) (getBottomAsTopReq ||>>|| ntl) leftReqB
  let nbr = (replace |>>| br) (getBottomAsTopReq ||>>|| ntr) (getRightAsLeftReq ||>>|| nbl)  
  (ntl, ntr,
   nbl, nbr)

let growMaze (lsll : Maze) =
  let rec grl output prevOutputRowReqs (lsll : LargeSquare list list) =
    match lsll with
    | [] -> output
    | row::tail ->  
      let folder ((upper, lower), leftReqT, leftReqB) s (topReqL, topReqR) =
        let (ntl, ntr,
             nbl, nbr) = replaceSquare s topReqL topReqR leftReqT leftReqB
        (upper |.| [ntl; ntr], lower |.| [nbl; nbr]), getRightAsLeftReq ||>>|| ntr, getRightAsLeftReq ||>>|| nbr
      let (newTop, newBottom), _, _ = List.fold2 folder (([],[]), LeftReq None, LeftReq None) row prevOutputRowReqs
      let prevRowReqs = newBottom 
                        |> List.mapi (fun i b -> i, getBottomAsTopReq ||>>|| b) 
                        |> List.pairwise 
                        |> List.filter (fun ((i, r), (j, s)) -> i % 2 = 0)
                        |> List.map (fun ((i, r), (j, s)) -> (r, s))
      let newOutput = output |.| [newTop; newBottom]
      grl newOutput prevRowReqs tail
  let dummyTopReqs = List.init (List.length <| List.item 0 lsll) (fun x -> (TopReq None, TopReq None))      
  grl [] dummyTopReqs lsll               
  
let renderCube (scene:Scene) xs xe ys ye =

    let size = xe - xs
    let cube = Three.BoxBufferGeometry(size, size, 0.25 * size)
    let matProps = createEmpty<Three.MeshLambertMaterialParameters>
    matProps.color <- Some (U2.Case2 "#9430B3")
    let mesh = Three.Mesh(cube, Three.MeshLambertMaterial(matProps))
    mesh.translateX (xs - size / 2.0) |> ignore
    mesh.translateY (ys - size / 2.0) |> ignore
    mesh.translateZ 0.0 |> ignore
    scene.add(mesh)

let renderSquareRow (scene:Scene) xs xe ys ye a b c d e = 
  let widthStep = (xe - xs) / 5.0
  if a = X then renderCube scene (xs + 0.0 * widthStep) (xs + 1.0 * widthStep) ys ye
  if b = X then renderCube scene (xs + 1.0 * widthStep) (xs + 2.0 * widthStep) ys ye
  if c = X then renderCube scene (xs + 2.0 * widthStep) (xs + 3.0 * widthStep) ys ye
  if d = X then renderCube scene (xs + 3.0 * widthStep) (xs + 4.0 * widthStep) ys ye
  if e = X then renderCube scene (xs + 4.0 * widthStep) (xs + 5.0 * widthStep) ys ye

let rec renderSquare (scene:Scene) tlx tly brx bry a b c d e
                                                   f g h i j
                                                   k l m n o
                                                   p q r s t
                                                   u v w x y = 
        let heightStep = (bry - tly) / 5.0
        renderSquareRow scene tlx brx (tly + 0.0 * heightStep) (tly + 1.0 * heightStep) a b c d e 
        renderSquareRow scene tlx brx (tly + 1.0 * heightStep) (tly + 2.0 * heightStep) f g h i j
        renderSquareRow scene tlx brx (tly + 2.0 * heightStep) (tly + 3.0 * heightStep) k l m n o
        renderSquareRow scene tlx brx (tly + 3.0 * heightStep) (tly + 4.0 * heightStep) p q r s t
        renderSquareRow scene tlx brx (tly + 4.0 * heightStep) (tly + 5.0 * heightStep) u v w x y    

let rec renderMazeRow (scene:Scene) tlx tly brx bry row = 
    let step = (brx - tlx) / (float <| List.length row)
    row |> 
    List.iteri (fun i sq -> 
      let fi = float i
      (renderSquare scene (tlx + fi * step) tly (tlx + (fi + 1.0) * step) bry) ||>>|| sq)

let rec renderMaze(scene:Scene) tlx tly brx bry maze = 
    let step = (bry - tly) / (float <| List.length maze)
    maze |> 
    List.iteri (fun i r -> 
      let fi = float i
      renderMazeRow scene tlx (tly + fi * step) brx (tly + (fi + 1.0) * step)  r)

let rec randomMaze n =
  match n with 
  | n when n > 1 -> growMaze (randomMaze (n - 1))
  | _ -> [[replace X X X
                   o o o 
                   X X X 
                   (TopReq None)
                   (LeftReq None)]]
               
(**

#### _**5:** Action_ ####

Finally we're there. We can create a Scene and initialise all required elements by calling
the functions we defined above. We return a 4-tuple of the 4 key graphics elements back to
the caller so that those elements can be used later on in rendering / animation. In-fact,
"the caller" is just the line of script at the bottom of the section, which creates top-level
bindings to each of the key graphics elements.

*)

let action() =

    let width () = Browser.window.innerWidth * 0.75;
    let height () = Browser.window.innerHeight * 0.5

    let scene = Three.Scene()
    scene.autoUpdate <- true

    let camera = Three.PerspectiveCamera(75.0, width() / height(), 0.01, 1000.0)

    camera.matrixAutoUpdate <- true
    camera.rotationAutoUpdate <- true

    let initLights () =
      
      scene.add(Three.AmbientLight(U2.Case2 "#3C3C3C", 1.0))

      let spotLight = Three.SpotLight(U2.Case2 "#FFFFFF")
      spotLight.position.set(-30., 60., 60.) |> ignore
      scene.add(spotLight)

    initLights ()

    let renderer = Three.WebGLRenderer()
    renderer.setClearColor("#0A1D2D")
    (renderer :> Three.Renderer).setSize(width(), height())

    let container = if Browser.document.getElementById("graphicsContainer") <> null
                    then Browser.document.getElementById("graphicsContainer")
                    else Browser.document.body

    container.innerHTML <- ""
    container.appendChild((renderer :> Three.Renderer).domElement) |> ignore
    
    let makeButton text difficulty cssClass =    

        let button = Browser.document.createElement("button")
        button.innerText <- text
        button.className <- cssClass

        let buttonClick (b : Browser.MouseEvent) =
          let maze = randomMaze difficulty  
          while(scene.children.Count > 0) do 
            scene.remove(scene.children.Item(0)) 
          initLights ()  
          camera.position.z <- 0.0
          renderMaze scene -1.025 1.15 1.275 -1.15  maze
          (Boolean() :> obj)

        button.onclick <- Func<_,_> buttonClick
        button

    let buttonContainer = Browser.document.createElement("div")
    buttonContainer.className <- "buttonContainer"
    container.appendChild(buttonContainer) |> ignore

    buttonContainer.appendChild(makeButton "Easy" 2 "yellowGreen") |> ignore
    buttonContainer.appendChild(makeButton "Medium" 3 "yellowOrange") |> ignore
    buttonContainer.appendChild(makeButton "Hard" 4 "blueViolet") |> ignore

    renderMaze scene -1.025 1.15 1.275 -1.15 (randomMaze 3) 
    renderer, scene, camera

let renderer, scene, camera = action()

(**

### _**Making it** move_ ###

So, as we're using the movies as an analogy, we actually ought to add some movement to the scene,
a spinning cube is going to be much more impressive than a static one. Each frame we rotate the
cube a little about each of its axes to make it appear to spin. The use of requestAnimationFrame
(rather than a loop) ensures that the animation is paused if the render's target element isn't
on screen.

*)

let cameraPositionZ = 2.0

let rec reqFrame (dt:float) =
    Browser.window.requestAnimationFrame(Func<_,_> animate) |> ignore
    if camera.position.z < cameraPositionZ 
    then camera.position.z <- camera.position.z + 0.25
    else camera.position.z <- cameraPositionZ
    renderer.render(scene, camera)
and animate (dt:float) =
    Browser.window.setTimeout(Func<_,_> reqFrame, 1000.0 / 20.0) |> ignore // aim for 20 fps

animate(0.0) // Start!

