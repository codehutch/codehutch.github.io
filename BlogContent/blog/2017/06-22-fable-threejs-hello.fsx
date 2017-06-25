(*@
    Layout = "post";
    Title = "Fable / Three.js  - Hello Cube";
    Date = "2017-06-22T07:19:37";
    Tags = "";
    Description = "Showcasing Fable's power with a spinning cube";
*)
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

### _First things **first**_ ###

Your machine will need .NET Core installed so you can use the dotnet command line tool. Assuming you have that, the quickest way to get started 
is to clone the official Fable samples [repository](https://github.com/fable-compiler/samples-browser) and then run the `restore` script contained
within that repository, which will install the fable extensions to the dotnet command line tool and then go on to run `yarn install` and 
`dotnet restore` for you to pull down all the required npm (javascript) and paket (dotnet / nuget) dependencies.  

*)

(*** more ***)

#r "../../../packages/Fable.Core/lib/netstandard1.6/Fable.Core.dll"
#load "../../../node_modules/fable-import-three/Fable.Import.Three.fs"

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Three

let width () = Browser.window.innerWidth / 2.0;
let height () = Browser.window.innerHeight / 2.0

let init() =

    let container = Browser.document.getElementById("graphicsContainer")

    let camera = Three.PerspectiveCamera(75.0, width() / height(), 0.01, 1000.0)
    camera.matrixAutoUpdate <- true
    camera.rotationAutoUpdate <- true
    camera.position.z <- 2.0

    let scene = Three.Scene()
    scene.autoUpdate <- true

    let renderer = Three.WebGLRenderer()
    renderer.setClearColor("#0A1D2D")
    (renderer :> Three.Renderer).setSize(width(), height())
    // Set antialias tbd...
    container.innerHTML <- ""
    container.appendChild((renderer :> Three.Renderer).domElement) |> ignore

    let ambientLight = Three.AmbientLight(U2.Case2 "#3C3C3C", 1.0)
    scene.add(ambientLight)

    let spotLight = Three.SpotLight(U2.Case2 "#FFFFFF")
    spotLight.position.set(-30., 60., 60.) |> ignore
    spotLight.castShadow <- true
    scene.add(spotLight)

    let cubeStart = Three.BoxGeometry(1., 1., 1.)

    let matProps = createEmpty<Three.MeshLambertMaterialParameters>
    matProps.color <- Some (U2.Case2 "#9430B3")

    let cube = Three.BufferGeometry().fromGeometry(cubeStart);

    let mesh = Three.Mesh(cube, Three.MeshLambertMaterial(matProps))
    scene.add(mesh)
    renderer, scene, camera, cube

let renderer, scene, camera, cube = init()

let render() =
    cube.rotateX ( 0.003 ) |> ignore
    cube.rotateY ( 0.007 ) |> ignore
    cube.rotateZ ( 0.011 ) |> ignore
    renderer.render(scene, camera)

let rec animate (dt:float) =
    Browser.window.requestAnimationFrame(Func<_,_> animate) |> ignore
    render()

(** Start it up *)
animate(0.0)

