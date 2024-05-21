module Kattis.QuadrantSelection

open System
open Microsoft.FSharp.Core

// Solution to:
// https://open.kattis.com/problems/quadrant
// [<EntryPoint>] ** Remove comment annotation to start **
let main argv =
    let x = Console.ReadLine()
    let y = Console.ReadLine()
    let parseX =
        match Int32.TryParse(x) with
        | (true, value) -> value
        | (false, _) -> 1
    let parseY =
        match Int32.TryParse(y) with
        | (true, value) -> value
        | (false, _) -> 1
    
    if parseX < 0 && parseY < 0 then
        printf "%d" 3
    elif parseX > 0 && parseY < 0 then
        printf "%d" 4
    elif parseX > 0 && parseY > 0 then
        printf "%d" 1
    else
        printf "%d" 2
        
    0