module TicTacToe

open Fable.Core
// For React Fast Refresh to work, the file must have **one single export**

module private GameComponents = 
    [<JSX.Component>]
    let Square (content: string) =
        JSX.jsx
            $"""
            <button className="square">{content}</button>
            """
    let Board () = 
        JSX.jsx
            $"""
            <>
                <div className="status">Next player: X</div>
                <div className="board-row">
                    { Square "X" }
                    { Square "X" }
                    { Square "X" }
                </div>
                <div className="board-row">
                    { Square "O" }
                    { Square "O" }
                    { Square "O" }
                </div>
                <div className="board-row">
                    { Square "X" }
                    { Square "" }
                    { Square "O" }
                </div>
            </>
            """
    let Move (desc: string) = 
        JSX.jsx
            $"""
            <li>
                <button>{desc}</button>
            </li>
            """
open GameComponents
[<JSX.Component>]
let Game () = 
    JSX.jsx 
        $"""
        <div className="game">
            <div className="game-board">
                {Board()}
            </div>
            <div className="game-info">
                <ol>
                    {Move "Go to game start"}
                    {Move "Go to game start"}
                    {Move "Go to game start"}
                    {Move "Go to game start"}
                </ol>
            </div>
        </div>
        """
    