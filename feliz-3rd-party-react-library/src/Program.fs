module FelizMuiButton

open Feliz
open Fable.Core.JsInterop

// https://safe-stack.github.io/docs/recipes/javascript/third-party-react-package/
// https://www.compositional-it.com/news-blog/f-wrappers-for-react-components/

let muiButton: obj = importDefault "@mui/material/Button"

type MuiButton =
    static member inline Variant (v: string) = "variant" ==> v
    static member inline Children (c: #seq<ReactElement>) = "children" ==>  (c:> ReactElement seq)

    static member inline create props = Interop.reactApi.createElement (muiButton, createObj !!props)

[<ReactComponent>]
let ButtonView() =
    MuiButton.create [
        MuiButton.Variant "contained"
        MuiButton.Children [Html.text "Next"]
    ]

open Browser.Dom

let root = ReactDOM.createRoot(document.getElementById "root")
root.render(ButtonView())