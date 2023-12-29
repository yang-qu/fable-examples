open Feliz
open Elmish
open Elmish.React
open Fable.Core.JsInterop

importSideEffects "./styles.css"

type Square =
    | X
    | O
    | Empty

type State =
    { History: Square array list
      XIsNext: bool
      CurrentMove: int }

type Msg =
    | Play of position: int
    | JumpToMove of move: int

let init () =
    { History = [ Array.create 9 Empty ]
      XIsNext = true
      CurrentMove = 0 }

let update (msg: Msg) (state: State) =
    match msg with
    | Play p ->
        let endIndex = List.length state.History - 1
        let history = state.History[endIndex - state.CurrentMove .. endIndex]
        let current = history |> List.head |> Array.copy
        current[p] <- if state.XIsNext then X else O

        { state with
            History = current :: history
            XIsNext = List.length history % 2 = 0
            CurrentMove = List.length history }
    | JumpToMove m -> { state with CurrentMove = m }

let render (state: State) (dispatch: Msg -> unit) =
    let currentSquares =
        let endIndex = List.length state.History - 1
        let at = (endIndex - state.CurrentMove)
        state.History |> List.item at

    let squares n =
        let get = currentSquares |> Array.get

        match (get n) with
        | X -> X.ToString()
        | O -> O.ToString()
        | Empty -> ""

    let status =
        let m = if state.XIsNext then X else O
        sprintf "Next player: %s" (m.ToString())

    let item (i: int) =
        let desc =
            if i > 0 then
                sprintf "Go to move %d" i
            else
                "Got to game start"

        Html.li
            [ prop.key i
              prop.children [ Html.button [ prop.text desc; prop.onClick (fun _ -> dispatch (JumpToMove i)) ] ] ]

    let moves = state.History |> List.mapi (fun i _ -> item i)

    Html.div
        [ prop.className "game"
          prop.children
              [ Html.div
                    [ prop.className "game-board"
                      prop.children
                          [ Html.div [ prop.className "status"; prop.text status ]
                            Html.div
                                [ prop.className "board-row"
                                  prop.children
                                      [ Html.button
                                            [ prop.className "square"
                                              prop.text (squares 0)
                                              prop.onClick (fun _ -> dispatch (Play 0)) ]
                                        Html.button
                                            [ prop.className "square"
                                              prop.text (squares 1)
                                              prop.onClick (fun _ -> dispatch (Play 1)) ]

                                        Html.button
                                            [ prop.className "square"
                                              prop.text (squares 2)
                                              prop.onClick (fun _ -> dispatch (Play 2)) ] ] ]
                            Html.div
                                [ prop.className "board-row"

                                  prop.children
                                      [ Html.button
                                            [ prop.className "square"
                                              prop.text (squares 3)
                                              prop.onClick (fun _ -> dispatch (Play 3)) ]

                                        Html.button
                                            [ prop.className "square"
                                              prop.text (squares 4)
                                              prop.onClick (fun _ -> dispatch (Play 4)) ]

                                        Html.button
                                            [ prop.className "square"
                                              prop.text (squares 5)
                                              prop.onClick (fun _ -> dispatch (Play 5)) ]

                                        ] ]
                            Html.div
                                [ prop.className "board-row"

                                  prop.children
                                      [ Html.button
                                            [ prop.className "square"
                                              prop.text (squares 6)
                                              prop.onClick (fun _ -> dispatch (Play 6)) ]

                                        Html.button
                                            [ prop.className "square"
                                              prop.text (squares 7)
                                              prop.onClick (fun _ -> dispatch (Play 7)) ]

                                        Html.button
                                            [ prop.className "square"
                                              prop.text (squares 8)
                                              prop.onClick (fun _ -> dispatch (Play 8)) ]

                                        ] ] ] ]
                Html.div [ prop.className "game-info"; prop.children [ Html.ol moves ] ] ] ]

Program.mkSimple init update render
|> Program.withReactBatched "root"
|> Program.run
