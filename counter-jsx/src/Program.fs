module Program

open Browser
open Fable.Core
open Feliz

// Entry point must be in a separate file
// for Vite Hot Reload to work

[<JSX.Component>]
let Counter () = Counter.Counter()

let root = ReactDOM.createRoot (document.getElementById "root")
root.render(Counter() |> toReact)

