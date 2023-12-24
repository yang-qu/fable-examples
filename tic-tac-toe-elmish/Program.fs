open Feliz
open Elmish
open Elmish.React
open Fable.Core.JsInterop

importSideEffects "./styles.css"

type State = { Count: int }

type Msg =
    | Increment
    | Decrement

let init () = { Count = 0 }, Cmd.none

let update (msg: Msg) (state: State) =
    match msg with
    | Increment -> { state with Count = state.Count + 1 }, Cmd.none
    | Decrement -> { state with Count = state.Count - 1 }, Cmd.none

let render (state: State) (dispatch: Msg -> unit) =
    Html.div
        [ prop.className "game"
          prop.children
              [ Html.div
                    [ prop.className "game-board"
                      prop.children
                          [ Html.div [ prop.className "status"; prop.text "Next player: X" ]
                            Html.div
                                [ prop.className "board-row"
                                  prop.children
                                      [ Html.button [ prop.className "square"; prop.text "" ]
                                        Html.button [ prop.className "square"; prop.text "" ]
                                        Html.button [ prop.className "square"; prop.text "" ] ] ]
                            Html.div
                                [ prop.className "board-row"

                                  prop.children
                                      [ Html.button [ prop.className "square"; prop.text "" ]
                                        Html.button [ prop.className "square"; prop.text "" ]
                                        Html.button [ prop.className "square"; prop.text "" ] ] ]
                            Html.div
                                [ prop.className "board-row"

                                  prop.children
                                      [ Html.button [ prop.className "square"; prop.text "" ]
                                        Html.button [ prop.className "square"; prop.text "" ]
                                        Html.button [ prop.className "square"; prop.text "" ] ] ] ] ]
                Html.div
                    [ prop.className "game-info"
                      prop.children
                          [ Html.ol
                                [ Html.li [ prop.children [ Html.button [ prop.text "Go to game start" ] ] ]
                                  Html.li [ prop.children [ Html.button [ prop.text "Go to move #1" ] ] ]
                                  Html.li [ prop.children [ Html.button [ prop.text "Go to move #2" ] ] ] ] ] ] ] ]

Program.mkProgram init update render
|> Program.withReactBatched "root"
|> Program.run
