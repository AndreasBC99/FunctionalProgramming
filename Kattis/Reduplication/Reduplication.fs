module Kattis.Reduplication

open System

// Solution to:
// https://open.kattis.com/problems/reduplikation?editresubmit=13670646
// [<EntryPoint>] ** Remove comment annotation to start **
let main argv =
    let inputString = Console.ReadLine()
    let inputNumber = Console.ReadLine()
    let multiplier =
        match Int32.TryParse(inputNumber) with
        | (true, value) -> value
        | (false, _) -> 1
    let numbers = [1..multiplier - 1]
    
    let mutable concatString = inputString
    for i in numbers do
        concatString <- concatString + inputString
        
    printfn "%s" concatString
    
    0
        
        


