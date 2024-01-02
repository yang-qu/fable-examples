module Program

open Fable.Core
open Browser
open Feliz

[<JSX.Component>]
let DatePickers () = MuiDatePickers.DatePickers ()
let root = ReactDOM.createRoot (document.getElementById "root")
root.render(DatePickers() |> toReact)