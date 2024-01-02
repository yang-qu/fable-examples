module Program

open Fable.Core
open Browser
open Feliz
open Fable.Core.JsInterop

importSideEffects "../styles.css"
// Entry point must be in a separate file
// for Vite Hot Reload to work
[<JSX.Component>]
let Game () = TicTacToe.Game()

let root = ReactDOM.createRoot (document.getElementById "root")
root.render(Game() |> toReact)