[<AutoOpen>]
module Helpers

open Fable.Core
open Feliz

let inline toJsx (el: ReactElement) : JSX.Element = unbox el
let inline toReact (el: JSX.Element) : ReactElement = unbox el

