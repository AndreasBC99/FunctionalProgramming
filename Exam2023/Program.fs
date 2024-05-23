
open System

// 1.1
type prop =
    | TT
    | FF
    | And of prop * prop
    | Or of prop * prop

let rec eval p =
    match p with
    | TT -> true
    | FF -> false
    | And(p1, p2) -> (eval p1) && (eval p2)
    | Or(p1, p2) -> (eval p1) || (eval p2)
    
// Examples:
let p1 = And(TT, FF)
let p2 = Or(TT, FF)
let p3 = And(Or(TT, And(TT, FF)), TT)
let p4 = And(Or(TT, And(TT, FF)), Or(FF, And(TT, FF)))

printfn "eval TT = %b" (eval TT)    // true
printfn "eval FF = %b" (eval FF)    // false
printfn "eval p1 = %b" (eval p1)    // false
printfn "eval p2 = %b" (eval p2)    // true
printfn "eval p3 = %b" (eval p3)    // true
printfn "eval p4 = %b" (eval p4)    // false

// 1.2
let rec negate (p: prop) : prop =
    match p with
    | TT -> FF
    | FF -> TT
    | And(p1, p2) -> Or(negate p1, negate p2)
    | Or(p1, p2) -> Or(negate p1, negate p2)
    
let rec implies p q = Or (negate p, q) // Could also be (negate p, negate q)
// Examples
let p1n = And(TT, FF)
let p2n = Or(TT, FF)
let p3n = And(Or(TT, And(TT, FF)), TT)
let p4n = And(Or(TT, And(TT, FF)), Or(FF, And(TT, FF)))

printfn "negate TT = %A" (negate TT)    // FF
printfn "negate FF = %A" (negate FF)    // TT
printfn "negate p1 = %A" (negate p1n)    // Or(FF, TT)
printfn "negate p2 = %A" (negate p2n)    // And(FF, TT)
printfn "negate p3 = %A" (negate p3n)    // Or(And(FF, Or(FF, TT)), FF)
printfn "negate p4 = %A" (negate p4n)    // Or(And(FF, Or(FF, TT)), And(TT, Or(FF, TT)))

// 1.3
// Tail-recursive function to create a conjunction of propositions
let rec forall (f : 'a -> prop) (lst : list<'a>) : prop =
    // Auxiliary function with an accumulator
    let rec aux acc = function
        // Base case: if the list is empty, return the accumulated proposition
        | [] -> acc
        // Recursive case: apply f to the head of the list (x),
        // and create a new conjunction with the accumulator
        // then continue with the tail of the list (xs)
        | x::xs -> aux (And(acc, f x)) xs
    // Initialize the accumulator with TT (true) and start the recursion
    aux TT lst
// Example function to test with
let mod2 x = if x % 2 = 0 then TT else FF

// Examples
let example1 = [0 .. 10] |> forall mod2 |> eval  // false
let example2 = [0 .. 2 .. 10] |> forall mod2 |> eval  // true

printfn "Example 1: %b" example1
printfn "Example 2: %b" example2

let rec exists (f : 'a -> prop) (lst : list<'a>) : prop = // Non-recursive
    List.fold (fun acc x -> Or(acc, f x)) FF lst

let example3 = [0 .. 10] |> exists mod2 |> eval
let example4 = [1; 3; 5] |> exists mod2 |> eval

// Function to check if exactly one element satisfies the proposition
let existsOne (f : 'a -> prop) (lst : list<'a>) : prop =
    // Helper function to create the conjunction
    let conj acc x = And(acc, negate (f x))
    // Helper function to create the proposition for one element being true and others being false
    let createProp (x, others) =
        List.fold conj (f x) others
    // Create the list of propositions
    let props =
        lst |> List.mapi (fun i x ->
            let others = List.mapi (fun j y -> if i = j then TT else f y) lst
            createProp (x, others))
    // Join all propositions with disjunction
    List.fold (fun acc p -> Or(acc, p)) FF props


// 2.1
let rec foo xs ys = // Recursive function
    match xs, ys with
    |_ , [] -> Some xs
    | x :: xs', y :: ys' when x = y -> foo xs' ys'
    |_ ,_ -> None  
// foo is a recursive function
// foo checks if the list ys is a prefix of the list xs.
// If ys is a prefix of xs, it returns the remaining part of xs after removing ys
// If not, foo returns None
let rec bar xs ys = // Recursive function
    match foo xs ys with
    | Some zs -> bar zs ys
    | None -> match xs with
              | [] -> []
              | x :: xs' -> x :: (bar xs' ys)
let baz (a : string) (b : string) = // Higher order function because it includes another function as an argument
    bar [for c in a -> c] [for c in b -> c] |>
    List.fold (fun acc c -> acc + string c) "" // 'fun'



// 3.1
