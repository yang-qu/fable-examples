module MyForm

open Elmish
open Elmish.React
open Fable.Form.Simple
open Fable.Form.Simple.My

type Values = { RememberMe: bool }

type Model = Form.View.Model<Values>

type Msg =
    | FormChanged of Model
    | LogIn of bool

let init () =
    { RememberMe = false } |> Form.View.idle, Cmd.none

let update (msg: Msg) (model: Model) =
    match msg with
    // We received a new form model, store it
    | FormChanged newModel -> newModel, Cmd.none
    | LogIn(_rememberMe) ->
        // For this example, we just set a message in the Form view
        { model with
            State = Form.View.Success "You have been logged in successfully" },
        Cmd.none


let form: Form.Form<Values, Msg, _> =
    let rememberMe =
        Form.checkboxField
            { Parser = Ok
              Value = fun values -> values.RememberMe
              Update = fun newValue values -> { values with RememberMe = newValue }
              Error = fun _ -> None
              Attributes = { Text = "Remember me" } }

    let onSubmit = fun rememberMe -> LogIn(rememberMe)

    Form.succeed onSubmit |> Form.append rememberMe


let view (model: Model) (dispatch: Dispatch<Msg>) =
    Form.View.asHtml
        { Dispatch = dispatch
          OnChange = FormChanged
          Action = Form.View.Action.SubmitOnly "Sign in"
          Validation = Form.View.ValidateOnSubmit }
        form
        model

Program.mkProgram init update view
|> Program.withReactBatched "root"
|> Program.withConsoleTrace
|> Program.run
