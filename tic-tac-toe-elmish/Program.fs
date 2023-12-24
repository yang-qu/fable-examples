open Feliz
open Elmish
open Elmish.React
open Fable.Core.JsInterop

importSideEffects "./styles.css"

type Move =
    | X
    | O
    | Empty

type State = { History: Move array list }

type Msg = NextMove of Move * int

let init () = { History = [ Array.create 9 Empty ] }

let update (msg: Msg) (state: State) =
    match msg with
    | NextMove(m, i) ->
        let current = state.History |> List.head |> Array.copy
        current[i] <- m

        { state with
            History = current :: state.History }

let render (state: State) (dispatch: Msg -> unit) =
    let squares n = 
        let get = state.History |> List.head |> Array.get
        match (get n) with 
        | X -> X.ToString()
        | O -> O.ToString()
        | Empty -> ""


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
                                      [ Html.button
                                            [ prop.className "square"
                                              prop.text (squares 0)
                                              prop.onClick (fun _ -> dispatch (NextMove(X, 0))) ]
                                        Html.button
                                            [ prop.className "square"
                                              prop.text (squares 1)
                                              prop.onClick (fun _ -> dispatch (NextMove(X, 1))) ]

                                        Html.button 
                                            [ 
                                              prop.className "square"
                                              prop.text (squares 2)
                                              prop.onClick (fun _ -> dispatch (NextMove(X, 2)))] ] ]
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

Program.mkSimple init update render
|> Program.withReactBatched "root"
|> Program.run
