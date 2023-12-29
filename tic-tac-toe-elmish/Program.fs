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

let slice history currentMove =
    let endIndex = List.length history - 1
    let startIndex = endIndex - currentMove
    (startIndex, endIndex)

let update (msg: Msg) (state: State) =
    match msg with
    | Play p ->
        let s, e = slice state.History state.CurrentMove
        let history = state.History[s..e]
        let next = history |> List.head |> Array.copy

        match next[p] with
        | Empty ->
            next[p] <- if state.XIsNext then X else O

            { state with
                History = next :: history
                XIsNext = List.length history % 2 = 0
                CurrentMove = List.length history }
        | _ -> state

    | JumpToMove m -> { state with CurrentMove = m }

let render (state: State) (dispatch: Msg -> unit) =
    let currentSquares =
        let at, _ = slice state.History state.CurrentMove
        state.History |> List.item at

    let squares n =
        let get = currentSquares |> Array.get

        match (get n) with
        | X -> string X
        | O -> string O
        | Empty -> ""

    let status =
        let m = if state.XIsNext then X else O
        sprintf "Next player: %O" m

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

    let square n =
        Html.button
            [ prop.className "square"
              prop.text (squares n)
              prop.onClick (fun _ -> dispatch (Play n)) ]

    Html.div
        [ prop.className "game"
          prop.children
              [ Html.div
                    [ prop.className "game-board"
                      prop.children
                          [ Html.div [ prop.className "status"; prop.text status ]
                            Html.div [ prop.className "board-row"; prop.children [ square 0; square 1; square 2 ] ]
                            Html.div [ prop.className "board-row"; prop.children [ square 3; square 4; square 5 ] ]
                            Html.div [ prop.className "board-row"; prop.children [ square 6; square 7; square 8 ] ] ] ]
                Html.div [ prop.className "game-info"; prop.children [ Html.ol moves ] ] ] ]

Program.mkSimple init update render
|> Program.withReactBatched "root"
|> Program.run
