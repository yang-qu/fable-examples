module Program

open Fable.Core
open Browser
open Feliz

[<JSX.Component>]
let Square () = MyComponents.Square()

open Browser.Dom

let root = ReactDOM.createRoot (document.getElementById "root")
root.render(Square() |> toReact)