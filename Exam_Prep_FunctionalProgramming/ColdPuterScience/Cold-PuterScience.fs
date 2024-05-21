module Exam_Prep_FunctionalProgramming.ColdPuterScience

open System

// Solution for:
// https://open.kattis.com/problems/cold
// [<EntryPoint>] ** Remove comment annotation to start **
let main argv =
    let amountInput = Console.ReadLine()
    let amount =
        match Int32.TryParse(amountInput) with
        | (true, value) -> value
        | (false, _) -> 0
    let tempInputs = Console.ReadLine().Split(' ')
    
    if tempInputs.Length <> amount then
        failwithf "Not the same amount! First input: %s, while amount of temperatures was: %d" amountInput tempInputs.Length
    
        
    let mutable counter = 0
 
    for i in 0..amount - 1 do
        let temp =
            match Int32.TryParse(tempInputs[i]) with
            | (true, value) -> value
            | (false, _) -> 0
        if temp < 0 then
            counter <- counter + 1
            
    printf "%d" counter
    
    0

