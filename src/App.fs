module App

open Fable.Core

JS.console.log "Hello from Fable!"

(*type Window =
    abstract alert: ?message: string -> unit

let [<Global>] window: Window = jsNative

window.alert ("Global Fable window.alert")
window.alert "Global Fable window.alert without parentheses"

[<Emit("window.alert($0)")>]
let alert (message: string): unit = jsNative

alert ("Emit form Fable window.alert")
alert "Emit from Fable without parentheses"
"Emit form fable window.alert with F# style" |> alert

// interface
type Math =
    abstract random: unit -> float
    
let [<Global>] Math: Math = jsNative

JS.console.log (Math.random())*)

// interfaces
type Node =
    abstract appendChild: child: Node -> Node
    abstract insertBefore: node: Node * ?child: Node -> Node
    
type Document =
    abstract createElement: tageName: string -> Node
    abstract createTextNode: date: string -> Node
    abstract getElementById: elementId: string -> Node
    abstract body: Node with get,set
    
let [<Global>] document: Document = jsNative

// client code
let newDiv = document.createElement("div")

"Good news everyone! Generated dynamically by Fable"
|> document.createTextNode
|> newDiv.appendChild
|> ignore

let currentDiv = document.getElementById("app")
document.body.insertBefore (newDiv, currentDiv) |> ignore

// p5 interface
[<StringEnum>]
type Render =
    | [<CompiledName("p2d")>] P2D
    | [<CompiledName("webgl")>] WebGL
    
type [<Import("*", "p5/lib/p5.js")>] p5(?sketch: p5 -> unit, ?id: string) =
    member __.setup with set(v: unit -> unit): unit = jsNative
    member __.draw with set(v: unit -> unit): unit = jsNative
    member __.createCanvas(w: float, h: float, ?renderer: Render): unit = jsNative
    member __.background(value: int): unit = jsNative
    member __.millis(): float = jsNative
    member __.rotateX(angle: float): unit = jsNative
    member __.rotateY(angle: float): unit = jsNative
    member __.box(): unit = jsNative
let sketch (it: p5) =
    it.setup <- fun () -> it.createCanvas(300., 300., WebGL)
    it.draw <- fun () ->
        it.background(243)
        it.rotateX(it.millis() / 1000.)
        it.rotateY(it.millis() / 1000.)
        it.box()
 // draw
p5(sketch) |> ignore        