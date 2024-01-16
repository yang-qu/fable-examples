module Program

open Elmish
open Elmish.React
open Fable.Form.Simple
open Fable.Form.Simple.Bulma

type Values =
    { Email: string
      Password: string
      RememberMe: bool }

type Model = Form.View.Model<Values>


type Msg =
    | FormChanged of Model
    | LogIn of string * string * bool



let init () =
    { Email = ""
      Password = ""
      RememberMe = false }
    |> Form.View.idle,
    Cmd.none

let update (msg: Msg) (model: Model) =
    match msg with
    // We received a new form model, store it
    | FormChanged newModel -> newModel, Cmd.none

    // The form has been submitted
    // Here, we have access to the value submitted from the from
    | LogIn(_email, _password, _rememberMe) ->
        // For this example, we just set a message in the Form view
        { model with
            State = Form.View.Success "You have been logged in successfully" },
        Cmd.none


let form: Form.Form<Values, Msg, _> =
    let emailField =
        Form.textField
            { Parser =
                fun value ->
                    if value.Contains("@") then
                        Ok value
                    else
                        Error "The e-mail address must contain a '@' symbol"
              Value = fun values -> values.Email
              Update = fun newValue values -> { values with Email = newValue }
              Error = fun _ -> None
              Attributes =
                { Label = "Email"
                  Placeholder = "some@email.com"
                  HtmlAttributes = [] } }

    let passwordField =
        Form.passwordField
            { Parser = Ok
              Value = fun values -> values.Password
              Update = fun newValue values -> { values with Password = newValue }
              Error = fun _ -> None
              Attributes =
                { Label = "Password"
                  Placeholder = "Your password"
                  HtmlAttributes = [] } }

    let rememberMe =
        Form.checkboxField
            { Parser = Ok
              Value = fun values -> values.RememberMe
              Update = fun newValue values -> { values with RememberMe = newValue }
              Error = fun _ -> None
              Attributes = { Text = "Remember me" } }

    let onSubmit = fun email password rememberMe -> LogIn(email, password, rememberMe)

    Form.succeed onSubmit
    |> Form.append emailField
    |> Form.append passwordField
    |> Form.append rememberMe


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
|> Program.run
