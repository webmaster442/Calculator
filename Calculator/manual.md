# Accepted number formats

1. Floating point numbers. E.g: `33.2`
2. Complex numbers with the cplx function: `Cplx(5, 2)`
3. Integers in decimal. E.g: `11000`
4. Integers in hexadecimal. E.g: `HxFF`
5. Integers in octal. E.g: `Ox77`
6. Integers in binary. E.g: `Bx1001`

# Commands

## clear, cls

Clears the terminal output

Parameters: No parameters are needed

## deg

Changes the angle mode to degrees, which divides the circle into 360 parts

Parameters: No parameters are needed

## eval, $

Evaluate an expression and writes out the result

Parameters:

1. The Expression.

An Expression

Example: `eval (3+4)*5`

## grad

Changes the angle mode to gradians, which divides the circle into 400 parts

Parameters: No parameters are needed

## rad

Changes the angle mode to radians, which divides the circle into 2π radians

Parameters: No parameters are needed

## set

Sets a variable.

Parameters:

1. Variable name
2. An Expression, that will be used to set the variable value

Example: `set x 8`

## simplify

Simplifies a logical expression.

Parameters:

1. The Expression.

Example: `simplify (a|b)&a`

The command can operate with minterms too. In this mode you can specify the minterms of your function.
In this mode, the first argument is the number of variables in the expression, the rest of the numbers are treated
as minterms.

Example: `simplify 2 1 3`

## unset 

Delete a variable

Parameters:

1. Variable name, that will be unset.

Example: `unset x`

# Functions 

* `Pow(x; y)` - Returns a specified number raised to the specified power. Works on complex and numbers that can be converted to double.
* `Abs(x)` - Returns the absolute value of a number. Works on complex and numbers that can be converted to double.
* `Floor(x)` -  Returns the largest integral value less than or equal to the specified number. Works on numbers that can be converted to double.
* `Ceiling(x)` -  Returns the smallest integral value that is greater than or equal to the specified number. Works on numbers that can be converted to double.
* `Ln(x)` - Returns the natural (base e) logarithm of a specified number. Works on numbers that can be converted to double.
* `Log(x; y)` - Returns the logarithm of a specified number in a specified base. Works on numbers that can be converted to double.
* `Root(x; y)` - Returns the Nth root of a specified number. Works on numbers that can be converted to double.
* `Sign(x)` - Returns an integer that indicates the sign of the parameter number. Works on numbers that can be converted to double.
* `Cplx(x; y)` - Creates a complex number from two numbers that can be converted to double.
* `Sin(x)` - Returns the sine of the specified angle. Works on numbers that can be converted to double. Result is affected by current angle system.
* `Cos(x)` - Returns the cosine of the specified angle. Works on numbers that can be converted to double. Result is affected by current angle system.
* `Tan(x)` - Returns the tangent of the specified angle. Works on numbers that can be converted to double. Result is affected by current angle system.
* `Factorial(x)` - Computes the factorial of a given number. Works on numbers that can be converted to double.
* `Percent(x; y)` -  Computes the y percent of x. Works on numbers that can be converted to double.