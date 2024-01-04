module Program

open Elmish
open Fable.Core
// MODEL

type Model = int

type Msg =
| Increment
| Decrement

let init() : Model = 0

// UPDATE

let update (msg:Msg) (model:Model) =
    match msg with
    | Increment -> model + 1
    | Decrement -> model - 1


// VIEW (rendered with React)

let view (model: Model) (dispatch: Msg -> Unit) =
    JSX.jsx
        $"""
        <div>
            <button onClick={fun _ -> dispatch Decrement} >-</button>
            <div>{string model}</div>
            <button onClick={fun _ -> dispatch Increment} >+</button>
        </div>
        """
    |> toReact

open Elmish.React
// App
Program.mkSimple init update view
|> Program.withReactBatched "root"
|> Program.withConsoleTrace
|> Program.run
