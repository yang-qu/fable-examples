module MyComponents 

open Fable.Core

[<JSX.Component>]
let Square () = 
    JSX.jsx 
        $"""
        <button className="square">X</button>
        """