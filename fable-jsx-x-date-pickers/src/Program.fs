module JsxDatePickers

open Fable.Core
open Browser
open Feliz
open Fable.Core.JsInterop

[<JSX.Component>]
let DatePickers () =
    JSX.jsx 
        $"""
        <p>hello world</p>
        """

let root = ReactDOM.createRoot (document.getElementById "root")
root.render(DatePickers() |> toReact)