module Program

open Fable.Core
open Browser
open Feliz
open Fable.Core.JsInterop

importSideEffects "../styles.css"

[<JSX.Component>]
let Square () = MyComponents.Square()

let root = ReactDOM.createRoot (document.getElementById "root")
root.render(Square() |> toReact)