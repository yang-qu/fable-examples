// For more information see https://aka.ms/fsharp-console-apps
open Browser

let div = document.createElement "div"
div.innerHTML <- "Hello world!"
document.body.appendChild div |> ignore
