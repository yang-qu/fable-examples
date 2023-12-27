open Feliz
open Elmish
open Elmish.React
open Fable.Core.JsInterop

importSideEffects "./styles.css"

type Move =
    | X
    | O
    | Empty

type State =
    { History: Move array list
      XIsNext: bool }

type Msg = Play of position: int

let init () =
    { History = [ Array.create 9 Empty ]
      XIsNext = true }

let update (msg: Msg) (state: State) =
    match msg with
    | Play(i) ->
        let current = state.History |> List.head |> Array.copy
        current[i] <- (if state.XIsNext then X else O)

        { state with
            History = current :: state.History
            XIsNext = (List.length state.History % 2 = 0) }

let render (state: State) (dispatch: Msg -> unit) =
    let squares n =
        let get = state.History |> List.head |> Array.get

        match (get n) with
        | X -> X.ToString()
        | O -> O.ToString()
        | Empty -> ""

    let nextPlayer xIsNext = 
      let m = if state.XIsNext then X else O
      sprintf "Next player: %s" (m.ToString())


    Html.div
        [ prop.className "game"
          prop.children
              [ Html.div
                    [ prop.className "game-board"
                      prop.children
                          [ Html.div [ prop.className "status"; prop.text (nextPlayer state.XIsNext) ]
                            Html.div
                                [ prop.className "board-row"
                                  prop.children
                                      [ Html.button
                                            [ prop.className "square"
                                              prop.text (squares 0)
                                              prop.onClick (fun _ -> dispatch (Play(0))) ]
                                        Html.button
                                            [ prop.className "square"
                                              prop.text (squares 1)
                                              prop.onClick (fun _ -> dispatch (Play(1))) ]

                                        Html.button
                                            [ prop.className "square"
                                              prop.text (squares 2)
                                              prop.onClick (fun _ -> dispatch (Play(2))) ] ] ]
                            Html.div
                                [ prop.className "board-row"

                                  prop.children
                                      [ Html.button
                                            [ prop.className "square"
                                              prop.text (squares 3)
                                              prop.onClick (fun _ -> dispatch (Play(3))) ]

                                        Html.button
                                            [ prop.className "square"
                                              prop.text (squares 4)
                                              prop.onClick (fun _ -> dispatch (Play(4))) ]

                                        Html.button
                                            [ prop.className "square"
                                              prop.text (squares 5)
                                              prop.onClick (fun _ -> dispatch (Play(5))) ]

                                        ] ]
                            Html.div
                                [ prop.className "board-row"

                                  prop.children
                                      [ Html.button
                                            [ prop.className "square"
                                              prop.text (squares 6)
                                              prop.onClick (fun _ -> dispatch (Play(6))) ]

                                        Html.button
                                            [ prop.className "square"
                                              prop.text (squares 7)
                                              prop.onClick (fun _ -> dispatch (Play(7))) ]

                                        Html.button
                                            [ prop.className "square"
                                              prop.text (squares 8)
                                              prop.onClick (fun _ -> dispatch (Play(8))) ]

                                        ] ] ] ]
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
