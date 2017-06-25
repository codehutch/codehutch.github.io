(*@
    Layout = "post";
    Title = "Fable / Three.js  - Hello Cube";
    Date = "2017-06-22T07:19:37";
    Tags = "";
    Description = "";
*)
let c = 1

(**
## Second-level heading
With some more documentation

yarn install
dotnet restore etc...

*)

(*** more ***)
//module FabTemplate

#r "../packages/Fable.Core/lib/netstandard1.6/Fable.Core.dll"
#load "../node_modules/fable-import-three/Fable.Import.Three.fs"

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
    renderer.setClearColor("#99ff99")
    (renderer :> Three.Renderer).setSize(width(), height())
    // Set antialias tbd...
    container.innerHTML <- ""
    container.appendChild((renderer :> Three.Renderer).domElement) |> ignore

    let ambientLight = Three.AmbientLight(U2.Case2 "#0C0C0C", 1.0)
    scene.add(ambientLight)

    let spotLight = Three.SpotLight(U2.Case2 "#FFFFFF")
    spotLight.position.set(-30., 60., 60.) |> ignore
    spotLight.castShadow <- true
    scene.add(spotLight)


    let cubeStart = Three.BoxGeometry(1., 1., 1.)

    let matProps = createEmpty<Three.MeshLambertMaterialParameters>
    matProps.color <- Some (U2.Case2 "#E443D1")

    let cube = Three.BufferGeometry().fromGeometry(cubeStart);

    let mesh = Three.Mesh(cube, Three.MeshLambertMaterial(matProps))
    scene.add(mesh)
    renderer, scene, camera, cube

let renderer, scene, camera, cube = init()

let mutable crx: float = 0.0
let mutable cry: float = 0.0

let render() =
    //controls.update(clock.getDelta())
    crx <- crx + 0.000007
    cry <- cry + 0.000003
    cube.rotateX ( crx ) |> ignore
    cube.rotateY ( cry ) |> ignore
    //cube.verticesNeedUpdate <- true
    renderer.render(scene, camera)

let rec animate (dt:float) =
    Browser.window.requestAnimationFrame(Func<_,_> animate)
    |> ignore
    render()

// kick it off
animate(0.0)

