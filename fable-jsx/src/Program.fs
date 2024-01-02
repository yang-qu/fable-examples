module Program

open Fable.Core
open Browser
open Feliz

[<JSX.Component>]
let Square () = MyComponents.Square()

let root = ReactDOM.createRoot (document.getElementById "root")
root.render(Square() |> toReact)