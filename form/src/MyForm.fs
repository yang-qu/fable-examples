module MyForm

open Elmish
open Elmish.React
open Fable.Form.Simple
open Fable.Form.Simple.Field
open Fable.Form.Simple.My
open System

type Values =
    { Email: string
      Password: string
      RememberMe: bool
      Priority: string
      Description: string
      Files: Browser.Types.File array
      DueDate: string}

type Model = Form.View.Model<Values>

type Msg =
    | FormChanged of Model
    | LogIn of string * string * bool * string

let init () =
    { Email = ""
      Password = ""
      RememberMe = false
      Priority = ""
      Description = ""
      Files = Array.empty
      DueDate = "" } 
    |> Form.View.idle,
    Cmd.none

let update (msg: Msg) (model: Model) =
    match msg with
    // We received a new form model, store it
    | FormChanged newModel -> newModel, Cmd.none

    // The form has been submitted
    // Here, we have access to the value submitted from the from
    | LogIn(_email, _password, _rememberMe, _priority) ->
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

    let priority =
        Form.selectField
            { Parser = Ok
              Value = fun values -> values.Priority
              Update = fun newValue values -> { values with Priority = newValue }
              Error = fun _ -> None
              Attributes =
                { Label = "Priority"
                  Placeholder = "Select a priority"
                  Options =
                    [ "p1", "P1"
                      "p2", "P2"
                      "p3", "P3"
                      "p4", "P4"
                      "p5", "P5" ] } }
    
    let description =
        Form.textareaField
            { Parser = Ok
              Value = fun values -> values.Description
              Update = fun newValue values -> { values with Description = newValue }
              Error = fun _ -> None
              Attributes =
                { Label = "Description"
                  Placeholder = "Enter a description"
                  HtmlAttributes = [] } }
            
    let fileField =
        Form.fileField
            {
                Parser =
                    Ok
                Value =
                    fun values -> values.Files
                Update =
                    fun newValue values ->
                        { values with Files = newValue }
                Error =
                    fun _ -> None
                Attributes =
                    {
                        Label = "Invoices"
                        InputLabel = "Choose one or more PDF files"
                        Accept = FileField.FileType.Specific [".pdf"]
                        FileIconClassName = FileField.FileIconClassName.Default
                        Multiple = true
                    }
            }
            
    let dueDate =
        Form.dateField
            {
                Parser =
                    Ok
                Value =
                    fun values -> values.DueDate 
                Update =
                    fun newValue values ->
                        { values with DueDate = newValue }
                Error =
                    fun _ -> None
                Attributes =
                    {
                        Label = "Due date"
                        Placeholder = "Enter a due date"
                        HtmlAttributes = []
                    }
            }


    let onSubmit = fun email password rememberMe priority desc files dueDate -> LogIn(email, password, rememberMe, priority)

    Form.succeed onSubmit
    |> Form.append emailField
    |> Form.append passwordField
    |> Form.append rememberMe
    |> Form.append priority
    |> Form.append (Form.optional description)
    |> Form.append fileField
    |> Form.append dueDate


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
