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

let o = O // Sneaky trick to get syntax-highlighting to work better

type SmallSquare = Cell * Cell * Cell
                 * Cell * Cell * Cell
                 * Cell * Cell * Cell 

let width () = Browser.window.innerWidth / 2.0;
let height () = Browser.window.innerHeight / 2.0

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

let initRenderer () =

    let renderer = Three.WebGLRenderer()
    renderer.setClearColor("#0A1D2D")
    (renderer :> Three.Renderer).setSize(width(), height())

    let container = if Browser.document.getElementById("graphicsContainer") <> null
                    then Browser.document.getElementById("graphicsContainer")
                    else Browser.document.body

    container.innerHTML <- ""
    container.appendChild((renderer :> Three.Renderer).domElement) |> ignore

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

let initGeometry(scene:Scene) =

    let cubeStart = Three.BoxGeometry(1., 1., 1.)

    let matProps = createEmpty<Three.MeshLambertMaterialParameters>
    matProps.color <- Some (U2.Case2 "#94FFB3")

    let cube = Three.BufferGeometry().fromGeometry(cubeStart);
    let mesh = Three.Mesh(cube, Three.MeshLambertMaterial(matProps))

    scene.add(mesh)
    cube

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
    let renderer = initRenderer ()
    let cube = initGeometry scene

    renderer, scene, camera, cube

let renderer, scene, camera, cube = action()

(**

### _**Making it** move_ ###

So, as we're using the movies as an analogy, we actually ought to add some movement to the scene,
a spinning cube is going to be much more impressive than a static one. Each frame we rotate the
cube a little about each of its axes to make it appear to spin. The use of requestAnimationFrame
(rather than a loop) ensures that the animation is paused if the render's target element isn't
on screen.

*)

let render() =
    cube.rotateX ( 0.003 ) |> ignore
    cube.rotateY ( 0.007 ) |> ignore
    cube.rotateZ ( 0.011 ) |> ignore
    renderer.render(scene, camera)

let rec animate (dt:float) =
    Browser.window.requestAnimationFrame(Func<_,_> animate) |> ignore
    render()

animate(0.0) // Start the animation going

