(*@
    Layout = "post";
    Title = "FSharp / Fable / ThreeJs - Maze";
    Date = "2017-07-03T08:48:31";
    Tags = "fsharp threejs fable maze functional";
    Description = "A graphical-code approach to maze generation, using F#, Fable and ThreeJs";
*)
(*** more ***)
(**

** F#, Fable & ThreeJs: _Maze_ **
---------------------------------

<div id="graphicsWrapper"><div id="graphicsContainer"></div></div>

<script src="http://cdnjs.cloudflare.com/ajax/libs/three.js/r77/three.js"></script>
<script src="/otherOutput/fable1/BlogFableThreeMazeBuild.js"></script>

**F# gives us considerable lee-way syntactically**. Let's see how far we can bend it to generate a maze from **code
that looks like diagrams** of what we want to generate.

### _**Decisions** decisions_ ###

The first question is... How do we want to represent a maze square? One way we could go is to use strings to draw
little box diagrams. We're helped in drawing our ascii mazes by F#'s rather nice triple quoted multi-line string
literals. However, whilst ascii art is undeniably masssively cool, using strings to draw mazes has some drawbacks...

  * There's nothing to stop me being lazy and drawing one that's incomplete 
  * There's nothing to stop me going crazy and using non-standard characters
  * I'm going to have to parse the strings into something meaningful
  * I'm going to have to handle potential extra white-space etc 

*)

// Option 1 - Use strings and ascii art - cool but not cool

let stringMazes = """ 

     +-+                      +-+                                   +-+-+
     | | <= 1x1 square box    |   <= very simple 1x1 square maze    |     <= really complicated
     +-+                      + +                                   + +-+    2x2 square maze
                                                                        |
                                                                    +-+-+
"""

(**

### _**Philosophical** rethink_ ###

So, ascii-art strings are looking like too much effort, how can we make it simpler? One way we could simplify
things is to try and identify the minimum-possible representation of a 1x1 maze. An interesting philosophical 
point here is that the 1x1 square box above required nine ascii characters to draw it (4 sides, 4 corners and
the space in the middle). But that was representing the maze square in terms of it's boundaries and walls (man). 
If we were to be all open minded and free thinking and glass-half-fullish then we could look at that square in 
terms of it's connectivity to other squares, and realise there's only 4 possible ways out of a square, north, 
south, east and west. That would require just 4 bools to represent it, which is lovely. Trouble is, how are we 
going to hold those bools? In an 4-tuple? In a list? A record-type? All would work, but I don't like such ideas 
as they seem like a 1-d flattening of what is really a 2-d problem. Also, when thinking about a 2x2 grid, the 
connectivity approach becomes a bit non-intuitive (to me at least). So, let's stick with 2-d-ish solutions, but 
see if we can get away from strings and introduce some type safety. What we can do is take inspiration from 
noughts and crosses and introduce two distinct values, X and O to draw our mazes with.

*)

#r "../../../packages/Fable.Core/lib/netstandard1.6/Fable.Core.dll"
#load "../../../node_modules/fable-import-three/Fable.Import.Three.fs"

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Three

// Here's our union-type for representing a maze.
type Cell = 
  | X // X is going to represent a wall
  | O // O is going to represent open space.
  | I // I represents intederminate / don't care

let o = O // Bit of a trick to get syntax-highlighting to show up better

let rnd = System.Random()
let r () = if (rnd.Next() &&& 0x1) = 0 then O else X

type SmallSquare = Cell * Cell * Cell
                 * Cell * Cell * Cell
                 * Cell * Cell * Cell 

let ss a b c
       d e f 
       g h i : SmallSquare = a, b, c,
                             d, e, f,
                             g, h, i
                             
let randomSS () =
    let b, d, f, h = r(), r(), r(), r() 
    ss  X b X
        d O f
        X h X

//let validSS                                

let ss1 = ss X X X
             o o o 
             X X X

let ss2 = ss X X X
             X o o 
             X o X

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

let ls1 = ls X X X X X 
             X o X o o
             X o X o X
             X o o o X
             X o X X X

let randomLS () =
    let b, d, f, h, j, l, n, p, r, t, v, x = r(), r(), r(), r(), r(), r(), r(), r(), r(), r(), r(), r() 
    ls  X b X d X
        f O h O j
        X l X n X
        p O r O t
        X v X x X
        
let adapt mf (a, b, c, d, e,
              f, g, h, i, j,
              k, l, m, n, o,
              p, q, r, s, t,
              u, v, w, x, y) = mf a b c d e
                                  f g h i j
                                  k l m n o
                                  p q r s t
                                  u v w x y

let (|>>|) = adapt

let toList a b c d e
           f g h i j
           k l m n o
           p q r s t
           u v w x y = [a; b; c; d; e; f; g; h; i; j; k; l; m; n; o; p; q; r; s; t; u; v; w; x; y]
        
type LargeRow = Cell * Cell * Cell * Cell * Cell
type LargeCol = LargeRow

let row [a; b; c; d; e] = a, b, c, d, e 

let topRow (ls:LargeSquare)      = adapt toList ls                 |> List.take 5 |> row
let upperMidRow (ls:LargeSquare) = adapt toList ls |> List.skip  5 |> List.take 5 |> row
let middleRow (ls:LargeSquare)   = adapt toList ls |> List.skip 10 |> List.take 5 |> row
let lowerMidRow (ls:LargeSquare) = adapt toList ls |> List.skip 15 |> List.take 5 |> row
let bottomRow (ls:LargeSquare)   = adapt toList ls |> List.skip 20 |> List.take 5 |> row

let rss a b c
        d e f
        g h i : SmallSquare = ss g d a
                                 h e b
                                 i f c

let rec a n = 
    match n with
    | 0 -> Seq.empty
    | _ -> seq { yield O; yield X; yield! a (n - 1) }

//let ss X X X
//       o o o 
//       X X X = ls X X X X X       ls X X X X X
//                  o o o o o          o o o o X
//                  X o X X X          X o X o X
//                  o o o o X          X o X o o
//                  X X X X X          X X X X X

//   X   X
// o s o s o 
//   o   o
// o s o s o 
//   X   X  


(*
let disallow a b c d e
             f g h i j
             k l m n o
             p q r s t
             u v w x y   a' b' c' d' e'
                         f' g' h' i' j'
                         k' l' m' n' o' 
                         p' q' r' s' t' 
                         u' v' w' x' y' = 
    match m with
    |
*)     

let (|=|) (a:Cell) (b:Cell) =
    match a, b with
    | X, I -> true
    | O, I -> true
    | I, X -> true
    | I, O -> true
    | _, _ -> a = b

let (|<>|) (a:Cell) (b:Cell) =
    match a, b with
    | X, I -> false
    | O, I -> false
    | I, X -> false
    | I, O -> false
    | I, I -> false
    | _, _ -> a <> b

let (||=||) (a:LargeSquare) (b:LargeSquare) =
    let al, bl = toList |>>| a, toList |>>| b
    List.fold2 (fun s x y -> s && (x |=| y)) true al bl

let (||<>||) (a:LargeSquare) (b:LargeSquare) =
    let al, bl = toList |>>| a, toList |>>| b
    List.fold2 (fun s x y -> s && (x |<>| y)) true al bl

let aa = ls X X X X X
            o o o o o 
            X o X o X
            X o o o X
            X X X X X

let at = ls I I I I I 
            o o o o o 
            I I I I I 
            I I I I I 
            I I I o I 

type Maze = 
  | Square of LargeSquare 
  | Grid of TopLeft:Maze    * TopRight:Maze 
          * BottomLeft:Maze * BottomRight:Maze

let renderCube (scene:Scene) xs xe ys ye =

    let size = xe - xs
    let cubeStart = Three.BoxGeometry(size, size, size)
    let matProps = createEmpty<Three.MeshLambertMaterialParameters>
    matProps.color <- Some (U2.Case2 "#94FFB3")
    let cube = Three.BufferGeometry().fromGeometry(cubeStart);
    let mesh = Three.Mesh(cube, Three.MeshLambertMaterial(matProps))
    mesh.translateX (xs - size / 2.0) |> ignore
    mesh.translateY (ys - size / 2.0) |> ignore
    mesh.translateZ 0.0 |> ignore
    scene.add(mesh)

let renderRow (scene:Scene) xs xe ys ye (a, b, c, d, e) = 
    let widthStep = (xe - xs) / 5.0
    if a = X then renderCube scene (xs + 0.0 * widthStep) (xs + 1.0 * widthStep) ys ye
    if b = X then renderCube scene (xs + 1.0 * widthStep) (xs + 2.0 * widthStep) ys ye
    if c = X then renderCube scene (xs + 2.0 * widthStep) (xs + 3.0 * widthStep) ys ye
    if d = X then renderCube scene (xs + 3.0 * widthStep) (xs + 4.0 * widthStep) ys ye
    if e = X then renderCube scene (xs + 4.0 * widthStep) (xs + 5.0 * widthStep) ys ye

let rec renderMaze (scene:Scene) tlx tly brx bry maze = 
    match maze with
    | Square s -> 
        let heightStep = (bry - tly) / 5.0
        renderRow scene tlx brx (tly + 0.0 * heightStep) (tly + 1.0 * heightStep) (topRow s)
        renderRow scene tlx brx (tly + 1.0 * heightStep) (tly + 2.0 * heightStep) (upperMidRow s)
        renderRow scene tlx brx (tly + 2.0 * heightStep) (tly + 3.0 * heightStep) (middleRow s)
        renderRow scene tlx brx (tly + 3.0 * heightStep) (tly + 4.0 * heightStep) (lowerMidRow s)
        renderRow scene tlx brx (tly + 4.0 * heightStep) (tly + 5.0 * heightStep) (bottomRow s)    
    | Grid (tl, tr, bl, br) -> 
        let heightStep, widthStep = (bry - tly) / 2.0, (brx - tlx) / 2.0
        renderMaze scene (tlx + 0.0 * widthStep) (tly + 0.0 * heightStep) (tlx + 1.0 * widthStep) (tly + 1.0 * heightStep) tl
        renderMaze scene (tlx + 1.0 * widthStep) (tly + 0.0 * heightStep) (tlx + 2.0 * widthStep) (tly + 1.0 * heightStep) tr
        renderMaze scene (tlx + 0.0 * widthStep) (tly + 1.0 * heightStep) (tlx + 1.0 * widthStep) (tly + 2.0 * heightStep) bl
        renderMaze scene (tlx + 1.0 * widthStep) (tly + 1.0 * heightStep) (tlx + 2.0 * widthStep) (tly + 2.0 * heightStep) br

let rec randomGrid n =
    let m = n - 1
    match n with 
    | n when n > 1 -> Grid (randomGrid m, randomGrid m,
                            randomGrid m, randomGrid m)
    | _ -> Square (randomLS())
               
    
let width () = Browser.window.innerWidth * 0.75;
let height () = Browser.window.innerHeight * 0.5

(**

#### _**1:** Lights_ ####

Our scene needs lights (so that we can see stuff).The below function adds a dim ambient light and
a bright spotlight to the scene. Ambient light defines a base-level of illumination within the
scene. It does not have a particular direction or position (hence it is easy to create). By contrast,
a spotlight has an exact location and shines in a particular direction, illuminating objects within
it's beam differently depending on the angle they meet it at. Here we just set the spotlight's
position and leave it shining at the origin (the center of our scene). Note that before we
specify a colour as a hex string, we have to use `U2.Case2` to create a union case. This is because
the underlying javascript libraries are weakly typed, but F# is very much a strongly typed language.
Therefore, where javascript functions are willing to accept various types of arguments, the F#
translation of them has to wrap each of the javascript-acceptable types in a union to keep the F#
type-system happy.

*)

let initLights (scene:Scene) =

    let ambientLight = Three.AmbientLight(U2.Case2 "#3C3C3C", 1.0)
    scene.add(ambientLight)

    let spotLight = Three.SpotLight(U2.Case2 "#FFFFFF")
    spotLight.position.set(-30., 60., 60.) |> ignore
    scene.add(spotLight)

(**

### _**2:** Camera_ ###

We also need a camera, which sets the location that the scene is viewed from. We need to set the
aspect ratio of the camer'a field of view. We use the `width()` and `height()` functions we defined above so that the
camera field's aspect will match the intended dimensions of our graphics output area. The last line of
the function is its return value; so the camera is returned back to the caller so that it can be used
during rendering.

*)

let initCamera () =

    let camera = Three.PerspectiveCamera(75.0, width() / height(), 0.01, 1000.0)
    camera.matrixAutoUpdate <- true
    camera.rotationAutoUpdate <- true
    camera.position.z <- 2.0
    camera

(**

#### _**3:** Renderer_ ####

OK, so not quite a case of _lights-camera-action_, as we now need a renderer. A renderer is sort of
analogous to a screen, and embodies the output area for our graphics. We have to tell Three which
DOM element within our page to put the render target into (and our HTML page must contain an element
called `graphicsContainer`. We also get to choose which kind of renderer to use. `WebGLRenderer` is
the fastest, but Three does support other renderers that could be used on devices without WebGL support.
The call to `setClearColour` sets the background colour for areas of the screen that are not otherwise
drawn on. Again we set the size of the output area using the `width()` and `height()` functions that we defined
above, so that the output dimensions tie up with the aspect ratio of the camera.

*)

let initRenderer (scene:Scene) =

    let renderer = Three.WebGLRenderer()
    renderer.setClearColor("#0A1D2D")
    (renderer :> Three.Renderer).setSize(width(), height())

    let container = if Browser.document.getElementById("graphicsContainer") <> null
                    then Browser.document.getElementById("graphicsContainer")
                    else Browser.document.body

    container.innerHTML <- ""
    container.appendChild((renderer :> Three.Renderer).domElement) |> ignore
    
    let buttonClick (b : Browser.MouseEvent) =
        while(scene.children.Count > 0) do 
            scene.remove(scene.children.Item(0)) 
        initLights scene
        renderMaze scene -1.025 1.15 1.275 -1.15 (randomGrid 3) 
        (Boolean() :> obj)
    
    let button = Browser.document.createElement("button")
    button.innerText <- "Click me"
    button.onclick <- Func<_,_> buttonClick
    container.appendChild(button) |> ignore

    renderer

(**

### _**4:** Geometry_ ###

Nearly there, but not quite like the movies. Now we have to create something to star in our scene.
We have only one cast member, a simple cube. Fortunately Three provides methods for defining most
standard geometric shapes, so we don't have to build the cube up out of individual triangles. Each object
also needs its surface properties defining (so that we know what it should look like). Here we say
that our cube's surface is made from a Lambert type material (which would allow for some shininess,
but we don't set that up here and just go for a plain purple matt surface). We buffer the cube's
geometry, which moves it to a more compact internal representation (for better performance) and then
combine it's shape and material definition together into a mesh. Finally we add the mesh to the scene
and also return the cube geometry for later use.

*)


(**

#### _**5:** Action_ ####

Finally we're there. We can create a Scene and initialise all required elements by calling
the functions we defined above. We return a 4-tuple of the 4 key graphics elements back to
the caller so that those elements can be used later on in rendering / animation. In-fact,
"the caller" is just the line of script at the bottom of the section, which creates top-level
bindings to each of the key graphics elements.

*)

let action() =

    let scene = Three.Scene()
    scene.autoUpdate <- true

    initLights scene
    let camera = initCamera ()
    let renderer = initRenderer scene
    renderMaze scene -1.025 1.15 1.275 -1.15 (randomGrid 3) 

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

let render() =
    renderer.render(scene, camera)

let rec reqFrame (dt:float) =
    Browser.window.requestAnimationFrame(Func<_,_> animate) |> ignore
    render()
and animate (dt:float) =
    Browser.window.setTimeout(Func<_,_> reqFrame, 1000.0 / 30.0) |> ignore // 30 fps

animate(0.0) // Start the animation going

