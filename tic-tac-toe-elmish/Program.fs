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

let calculateWiner (squares: Square array) =
    let lines =
        [ (0, 1, 2)
          (3, 4, 5)
          (6, 7, 8)
          (0, 3, 6)
          (1, 4, 7)
          (2, 5, 8)
          (0, 4, 8)
          (2, 4, 6) ]

    let winLine = 
      List.filter
              (fun (a, b, c) ->
                  match squares[a], squares[b], squares[c] with
                  | X, X, X -> true
                  | O, O, O -> true
                  | _ -> false)
              lines
    match winLine with 
    | (a, _, _)::xs -> 
      Some squares[a]
    | _ -> None

let update (msg: Msg) (state: State) =
    match msg with
    | Play p ->
          let s, e = slice state.History state.CurrentMove
          let history = state.History[s..e]
          let currentSquares = history |> List.head 
          let winer = calculateWiner currentSquares
          match winer with 
          | Some _ -> state
          | None ->
            let next = currentSquares|> Array.copy

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
        let winer = calculateWiner currentSquares
        match winer with 
        | Some w -> sprintf "Winer: %O" w
        | None -> 
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
