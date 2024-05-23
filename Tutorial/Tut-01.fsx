// Basic tutorial

// F# is an expression(not statement) based language
// Functions(expressions) are values
// The best way to code in F# is making the code point free and readable!
let add x y = x + y // Function with values (The same is variables, but is named values!)
let add' = fun x y -> x + y // Lambda expression (Same result as the function above). The 'fun' is an anonymous function
let add'' x = fun y -> x + y // Currying/Baking in. Baking in x in the fun y function. All functions are actually currying like this function

let add5 x =
    let five = 5
    x + five
    
// Partial application
let add5' = add 5 // Point free implementation (not specifying all parameters)

let operation number = (2. * (number + 3.)) ** 2. // ** is POW and the 2. is a flot annotation
let result = operation 5
// Can be wrote in separate expressions:
// Infix operator annotation 
let add3 number = number + 3 // Infix with +
let add3' number = (+) 3 number // prefix with +
let add3'' = (+) 3. // The same as above. Same can be done with +, -, *, /, **
let times2 = (*) 2.
let pow2 number = number ** 2.
 
let operation' number = // The same as (2. * (number + 3.)) ** 2.
    let x = add3' number
    let y = times2 x
    pow2 y
let result2 = operation' 5 // 256.0

// We can also use pipe operators to compose the functions
let example number =
    number |> add3' // Same as add3' number

let operation'' number = // Most readable! and can be point free
    number
    |> add3''
    |> times2
    |> pow2

let result3 = operation'' 5 // 256.0

// Composition operator
// Defining our own operators
let (>>) f g = // Function on the left and right
    fun x -> // Lambda expression
        x
        |> f// x pipes into f
        |> g // x pipes into g
        // This is the function g(f x): Doing f and x and getting the result to g
let operation''' = // Composition with >> operators (Also more readable and extendable)
    add3'' >> times2 >> pow2 

operation''' 5 // 256.0