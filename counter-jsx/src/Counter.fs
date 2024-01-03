module Counter

open Feliz
open Feliz.UseElmish
open Elmish
open Fable.Core

module private CounterElmish = 
    type Msg =
        | Increment
        | Decrement

    type State = { Count : int }

    let init() = { Count = 0 }, Cmd.none

    let update msg state =
        match msg with
        | Increment -> { state with Count = state.Count + 1 }, Cmd.none
        | Decrement -> { state with Count = state.Count - 1 }, Cmd.none

open CounterElmish
[<JSX.Component>]
let Counter() =
    let state, dispatch = React.useElmish(init, update, [| |])
    JSX.jsx
        $"""
        <div>
            <button onClick={fun _ -> dispatch Decrement} >-</button>
            <div>{string state.Count}</div>
            <button onClick={fun _ -> dispatch Increment} >+</button>
        </div>
        """