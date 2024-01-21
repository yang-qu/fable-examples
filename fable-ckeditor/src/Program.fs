module Program

open Elmish
open Fable.Core
open Fable.Core.JsInterop
open Browser

// MODEL

type Model = {
    DescInput: string
    Editor: obj
}

type Msg =
    | SetTextInput of string 
    | EditorReady of obj

let init () : Model = { DescInput = ""; Editor = createObj []}

// UPDATE

let update (msg: Msg) (model: Model) =
    match msg with
    | SetTextInput input -> { model with DescInput = input }
    | EditorReady ed -> { model with Editor = ed }


// VIEW (rendered with React)

let view (model: Model) (dispatch: Msg -> Unit) =
    let handleOnChange (ev: obj) (editor: obj) =
            console.log (editor?getData())
            let input = editor?getData()
            dispatch (SetTextInput input)
    JSX.jsx
        $"""
        import {{ CKEditor }} from '@ckeditor/ckeditor5-react';
        import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
        <CKEditor
            editor={{ ClassicEditor }}
            data="<p>Hello from CKEditor&nbsp;5!</p>"
            onReady={ fun ed -> dispatch (EditorReady ed)}
            onChange={ System.Func<_,_,_> handleOnChange }
        />
        """
    |> toReact

open Elmish.React
// App
Program.mkSimple init update view
|> Program.withReactBatched "root"
|> Program.withConsoleTrace
|> Program.run
