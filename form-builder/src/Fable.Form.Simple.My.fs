module Fable.Form.Simple.My

open Fable.Form.Simple.Field
open Feliz.Bulma
open Feliz.prop

[<RequireQualifiedAccess>]
module Form =

    module View =

        open Feliz
        open Fable.Form
        open Fable.Form.Simple
        open Fable.Form.Simple.Form.View

        let notYetImplemented field = failwith $"{field} Not yet implemented"

        let fieldLabel (label: string) =
            Html.label [ prop.text label; prop.className "form-label" ]

        let errorMessage (message: string) =
            Html.div [ prop.className "alert alert-warning"; prop.text message; prop.role "alert" ]
            
        let fieldInvalidFeedback (message: string) =
            Html.div [ prop.className "invalid-feedback"; prop.text message; ]

        let wrapInFieldContainer (children: ReactElement list) = Html.div [ prop.children children ]

        let errorMessageAsHtml (showError: bool) (error: Error.Error option) =
            match error with
            | Some(Error.External externalError) -> errorMessage externalError

            | _ ->
                if showError then
                    error
                    |> Option.map errorToString
                    |> Option.map errorMessage
                    |> Option.defaultValue Html.none

                else
                    Html.none

        let withLabelAndError
            (label: string)
            (showError: bool)
            (error: Error.Error option)
            (fieldAsHtml: ReactElement)
            : ReactElement =
            [ fieldLabel label; fieldAsHtml; errorMessageAsHtml showError error ]
            |> wrapInFieldContainer

        let form
            ({ Dispatch = dispatch
               OnSubmit = onSubmit
               State = state
               Action = action
               Fields = fields }: FormConfig<'Msg>)
            =

            Html.form
                [
                  prop.onSubmit (fun ev ->
                      ev.stopPropagation ()
                      ev.preventDefault ()

                      onSubmit |> Option.map dispatch |> Option.defaultWith ignore)

                  prop.children
                      [ yield! fields

                        match state with
                        | Error error -> errorMessage error
                        | Success success ->
                            Html.div [ prop.className "alert alert-success"; prop.text success; prop.role "alert" ]
                        | Loading
                        | Idle -> Html.none

                        match action with
                        | Action.SubmitOnly submitLabel ->
                            Html.button [ prop.type' "submit"; prop.text submitLabel; prop.className "btn btn-primary" ]

                        | Action.Custom func -> func state dispatch ] ]

        let inputField
            (typ: InputType)
            ({ Dispatch = dispatch
               OnChange = onChange
               OnBlur = onBlur
               Disabled = disabled
               Value = value
               Error = error
               ShowError = showError
               Attributes = attributes }: TextFieldConfig<'Msg, IReactProperty>)
            =

            let inputFunc (props: IReactProperty list) =
                match typ with
                | Text -> Html.input ([ prop.type' "text" ] @ props)

                | Password -> Html.input ([ prop.type' "password" ] @ props)

                | Email -> Html.input ([ prop.type' "email" ] @ props)

                | Color -> Html.input ([ prop.type' "color" ] @ props)

                | Date -> Html.input ([ prop.type' "date" ] @ props)

                | DateTimeLocal -> Html.input ([ prop.type' "datetime-local" ] @ props)

                | Number -> Html.input ([ prop.type' "number" ] @ props)

                | Search -> Html.input ([ prop.type' "search" ] @ props)

                | Tel -> Html.input ([ prop.type' "tel" ] @ props)

                | Time -> Html.input ([ prop.type' "time" ] @ props)


            inputFunc
                [ prop.className "form-control"
                  prop.onChange (onChange >> dispatch)

                  match onBlur with
                  | Some onBlur -> prop.onBlur (fun _ -> dispatch onBlur)
                  | None -> ()

                  prop.disabled disabled
                  prop.value value
                  prop.placeholder attributes.Placeholder

                  yield! attributes.HtmlAttributes ]
            |> withLabelAndError attributes.Label showError error

        let checkboxField
            ({ Dispatch = dispatch
               OnChange = onChange
               OnBlur = onBlur
               Disabled = disabled
               Value = value
               Attributes = attributes }: CheckboxFieldConfig<'Msg>)
            =

            Html.div
                [ prop.className "form-check"
                  prop.children
                      [ Html.input
                            [ prop.type' "checkbox"
                              prop.value value
                              prop.className "form-check-input"
                              prop.onChange (onChange >> dispatch)
                              prop.disabled disabled
                              match onBlur with
                              | Some onBlur -> prop.onBlur (fun _ -> dispatch onBlur)
                              | None -> () ]
                        Html.label
                            [ prop.className "form-check-label"
                              prop.children [ Html.text attributes.Text ] ] ] ]

        let selectField
            ({ Dispatch = dispatch
               OnChange = onChange
               OnBlur = onBlur
               Disabled = disabled
               Value = value
               Error = error
               ShowError = showError
               Attributes = attributes }: SelectFieldConfig<'Msg>)
            =

            let toOption (key: string, label: string) =
                Html.option [ prop.value key; prop.text label ]

            let placeholderOption =
                Html.option
                    [ prop.disabled true
                      prop.value ""

                      prop.text ("-- " + attributes.Placeholder + " --") ]

            Html.select
                [ prop.className "form-select"
                  prop.disabled disabled
                  prop.onChange (onChange >> dispatch)

                  match onBlur with
                  | Some onBlur -> prop.onBlur (fun _ -> dispatch onBlur)
                  | None -> ()

                  prop.value value

                  prop.children
                      [ placeholderOption

                        yield! attributes.Options |> List.map toOption ] ]
            |> withLabelAndError attributes.Label showError error
            
        let textareaField
            (
                {
                    Dispatch = dispatch
                    OnChange = onChange
                    OnBlur = onBlur
                    Disabled = disabled
                    Value = value
                    Error = error
                    ShowError = showError
                    Attributes = attributes
                } : TextFieldConfig<'Msg, IReactProperty>
            ) =

            Html.textarea [
                prop.className "form-control"
                prop.onChange (onChange >> dispatch)

                match onBlur with
                | Some onBlur ->
                    prop.onBlur (fun _ ->
                        dispatch onBlur
                    )

                | None ->
                    ()

                prop.disabled disabled
                prop.value value
                prop.placeholder attributes.Placeholder

                yield! attributes.HtmlAttributes
            ]
            |> withLabelAndError attributes.Label showError error
            
        let fileField
            (
                {
                    Dispatch = dispatch
                    OnChange = onChange
                    Disabled = disabled
                    Value = value
                    Error = error
                    ShowError = showError
                    Attributes = attributes
                } : FileFieldConfig<'Msg>
            ) =
            Html.input [
                prop.className "form-control"
                prop.type' "file"
            ]
            |> withLabelAndError attributes.Label showError error

        let htmlViewConfig<'Msg> : CustomConfig<'Msg, IReactProperty> =
            { Form = form
              TextField = inputField Text
              PasswordField = inputField Password
              EmailField = inputField Email
              TextAreaField = textareaField
              ColorField = inputField Color
              DateField = inputField Date
              DateTimeLocalField = inputField DateTimeLocal
              NumberField = inputField Number
              SearchField = inputField Search
              TelField = inputField Tel
              TimeField = inputField Time
              CheckboxField = checkboxField
              RadioField = (fun _ -> Html.none)
              SelectField = selectField
              FileField = fileField
              Group = (fun _ -> Html.none)
              Section = (fun _ _ -> Html.none)
              FormList = (fun _ -> Html.none)
              FormListItem = (fun _ -> Html.none) }

        let asHtml (config: ViewConfig<'Values, 'Msg>) = custom htmlViewConfig config
