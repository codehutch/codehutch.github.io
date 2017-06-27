(*@
    Layout = "post";
    Title = "Fable / Three.js  - Hello Cube";
    Date = "2017-06-22T07:19:37";
    Tags = "";
    Description = "Showcasing Fable's power with a spinning cube";
*)
(*** more ***)
(**

** Fable & ThreeJs: _Hello Cube_ **
---------------------------------

<div id="graphicsWrapper"><div id="graphicsContainer"></div></div>

<script src="http://cdnjs.cloudflare.com/ajax/libs/three.js/r77/three.js"></script>
<script src="/otherOutput/fable1/BlogFableThreeHelloWorldBuild.js"></script>

**The F# to JS compiler [Fable](http://fable.io/) is looking ever more impressive. What better way to showcase its abilities than 
by putting a spinning a cube on your screen?...** The official Fable ThreeJs / WebGL [demo](http://fable.io/samples/webGLTerrain/index.html) 
is actually far superiour to my effort here. This is a much cut-down version of that, to show how little code is needed to get F#
drawing 3D graphics in a browser.     

### _**First things** first_ ###

Your machine will need .NET Core installed so you can use the dotnet command line tool. Assuming you have that, the quickest way to get started 
is to clone the official Fable [samples-browser](https://github.com/fable-compiler/samples-browser) repository and then run the `restore` script 
contained within that repository, which will install the fable extensions to the dotnet command line tool and then go on to run `yarn install` and 
`dotnet restore` for you. This will pull down all the required npm (javascript) and paket (dotnet / nuget) dependencies. You can then replace the
code in the samples-browser/webGLTerrain/src/App.fs file with the below. _Note that you should comment out the first two lines of the below code 
(which are necessary only as I'm writing an fsx script file for this blog, and you'll be writing a standard fs file)_. Once you've copied, pasted 
and commented the code, you can then run `dotnet fable npm-run start` to compile the code to javascript and launch a dev server, then browse to
`http://localhost:8080/webGLTerrain` (which should actually now show you a plain spinning cube rather than fancy terrain). Talking of the code,
the first lines are below. Initially we just import the necessary namespaces and modules, then we define two functions to return the desired
size of the graphics canvas. 

*)

#r "../../../packages/Fable.Core/lib/netstandard1.6/Fable.Core.dll"     // Comment out if you are writing an fs file
#load "../../../node_modules/fable-import-three/Fable.Import.Three.fs"  // Comment out if you are writing an fs file

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Three

let width () = Browser.window.innerWidth / 2.0;
let height () = Browser.window.innerHeight / 2.0

(**

#### _**1:** Lights_ ####

Our scene needs lights (so we can see stuff).The below function add a dim ambient light and 
a bright spotslight to the scene.

*)

let initLights (scene:Scene) =

    let ambientLight = Three.AmbientLight(U2.Case2 "#3C3C3C", 1.0)
    scene.add(ambientLight)

    let spotLight = Three.SpotLight(U2.Case2 "#FFFFFF")
    spotLight.position.set(-30., 60., 60.) |> ignore
    //spotLight.castShadow <- true
    scene.add(spotLight)

(**

### _**2:** Camera_ ###

We also need a camera, which sets the location that the scene is viewed from. We also need to set the 
aspect ration of the field of view. We use the width and height functions we defined above so that the
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

OK, so not quite a case of lights-camera-action, as we now need a renderer. A renderer is kind of 
analagous to a screen, and embodies the output area for our graphics. We have to tell Three which 
DOM element of our page to put the render target into. We also get to choose which kind of renderer
to use. WebGLRenderer is the fastest, but Three does support other renderers that could be used on
devices without WebGL support. The call to setClearColour sets the background colour for areas of
the screen that are not otherwise drawn on. Again we set the size of the output area using the
width and height functions that we defined above, so that the output dimensions tie up with the
aspect ratio of the camera.

*)

let initRenderer () = 

    let container = Browser.document.getElementById("graphicsContainer")
    let renderer = Three.WebGLRenderer()

    renderer.setClearColor("#0A1D2D")
    (renderer :> Three.Renderer).setSize(width(), height())
    // Set antialias tbd...
    container.innerHTML <- ""
    container.appendChild((renderer :> Three.Renderer).domElement) |> ignore
    
    renderer

(**

### _**4:** Geometry_ ###

Nearly there, but not quite like the movies. Now we have to create something to star in our scene.
We have only one cast member, a simple cube. Fortunately Three provides methods for defining most
standard geometric shapes, so we don't have to build it up out of individual triangles. Each object
also needs its surface properties defining (so that we know what it should look like). Here we say
that our cube's surface is made from a Lambert type material (which would allow for some shinyness, 
but we don't set that up here and just go for a plain purple matt surface). We buffer the cube's
geometry, which moves it to a more compact internal representation (for better performance) and then
combine it's shape and material definition together into a mesh. Finally we add the mesh to the scene
and also return the cube geometry for later use.

*)

let initGeometry(scene:Scene) =

    let cubeStart = Three.BoxGeometry(1., 1., 1.)

    let matProps = createEmpty<Three.MeshLambertMaterialParameters>
    matProps.color <- Some (U2.Case2 "#9430B3")

    let cube = Three.BufferGeometry().fromGeometry(cubeStart);
    let mesh = Three.Mesh(cube, Three.MeshLambertMaterial(matProps))

    scene.add(mesh)
    cube

(**

#### _**5:** Action_ ####

Finally we're there. We can create a Scene and initialise all required elements. We return a 
4-tuple of the 4 key graphics elements back to the caller so that those elements can be used
later on in rendering / animation.

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

### _**Make it** move_ ###

Soo, as we're using the movies as an analogy, we actuall ought to add some movement to the scene, 
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

