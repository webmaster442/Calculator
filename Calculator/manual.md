# Accepted number formats

1. Floating point numbers. E.g: `33.2`
2. Floating point numbers in scientific form. E.g: `1E-10`
3. Complex numbers with the cplx function: `Cplx(5, 2)`
4. Integers in decimal. E.g: `11000`
5. Integers in hexadecimal. E.g: `HxFF`
6. Integers in octal. E.g: `Ox77`
7. Integers in binary. E.g: `Bx1001`

# Recognized constants

* pi
* e 
* quetta
* ronna 
* yotta 
* zetta 
* exa 
* peta 
* tera 
* giga 
* mega 
* kilo 
* hecto 
* deca 
* deci 
* centi 
* milli 
* micro 
* nano 
* pico 
* femto 
* atto 
* zepto 
* yocto 
* ronto,
* kib,
* mib,
* gib,
* tib,
* pib,
* eib

# Commands

## bcddecode

Decode a number from binary coded decimal to decimal

**Parameters:**

1. The number in BCD, with digits seperated by space or _ 

Example: `bcddecode 1001 0010`

## bcdencode

Encode a number to binary coded decimal

**Parameters:**

1. The number in decimal representation 

Example: `bcddecode 92`

## cd

Changes the current working directory.

**Parameters:**

1. The working directory that you want to change to. If not specified,
   a folder slection dialog will be displayed

## clear, cls

Clears the terminal output

**Parameters:** No parameters are needed

## color

Converts between color formats.

**Parameters:**

1. A color that needs to be converted between the different color formarts

Recognized color formats:

#rrggbb
rgb(r,g,b)
cmyk(c,m,y,k)
hsl(h, s, l)
yuv(y,u,v)
xyz(x,y,z)

## commands, cmds

Prints out the list of available commands

**Parameters:** No parameters are needed

## convert

Converts between number systems

**Parameters:**

1. Number in source system
2. Source number system. Must be in range 2 to 35
1. Target number system. Must be in range 2 to 35

## currency

Converts between currency exchange rates.

Exchange rates are provided by Hungarian MNB. Requires internet connection to work

If started without parameters all exchange rates are printed.

**Parameters:**

1. Currency amount
2. Source currency code
3. Target currency code

## deg

Changes the angle mode to degrees, which divides the circle into 360 parts

**Parameters:** No parameters are needed

## eval, $

Evaluate an expression and writes out the result

**Parameters:**

1. The Expression.

An Expression

Example: `eval (3+4)*5`

## exec

Executes a file containing calculator commands

**Parameters:**

1. File name containing commands to execute

## expense-ballance

Gets the current month's ballance

**Parameters:** No parameters are needed

## expense

Add an expense to the expenses

**Parameters:**

1. Ammount to add

Arguments:

-n or --name: Expense name. If not specified `Unknown expense` is used
-c or --category: Expense category. If not specified `Not categorized` is used
-d or --date: epxense dat. If not specified, current date is used.

## expense-stat

Provides detailed statistics about the monthly expenses and incomes

**Parameters:** No parameters are needed

## flushcache

Invalidates & deletes cached web service results.

**Parameters:** No parameters are needed

## functions

Prints out the list of available functions in eval mode

**Parameters:** No parameters are needed

## grad

Changes the angle mode to gradians, which divides the circle into 400 parts

**Parameters:** No parameters are needed

## list

Lists currently set variables.

**Parameters:** No parameters are needed

## man

Opens the manual.

**Parameters:** No parameters are needed

## md5

Computes the MD5 hash of a file

**Parameters:**

1. File name. If not specified a file selection dialog is shown.

## options

Opens the calculator options editor

**Parameters:** No parameters are needed

## pwd

Returns the current working directory

**Parameters:** No parameters are needed

## rad

Changes the angle mode to radians, which divides the circle into 2π radians

**Parameters:** No parameters are needed

## set

Sets a variable.

**Parameters:**

1. Variable name
2. An Expression, that will be used to set the variable value

Example: `set x 8`

## sha-1

Computes the SHA-1 hash of a file

**Parameters:**

1. File name. If not specified a file selection dialog is shown.

## sha-256

Computes the SHA-256 hash of a file

**Parameters:**

1. File name. If not specified a file selection dialog is shown.

## sha-384

Computes the SHA-384 hash of a file

**Parameters:**

1. File name. If not specified a file selection dialog is shown.

## sha-512

Computes the SHA-384 hash of a file

**Parameters:**

1. File name. If not specified a file selection dialog is shown.

## simplify

Simplifies a logical expression.

**Parameters:**

1. The Expression.

Example: `simplify (a|b)&a`

The command can operate with minterms too. In this mode you can specify the minterms of your function.
In this mode the number of variables must be specified with the `-v` or `--variables` switch
as minterms.

Example: `simplify 1 3 -v 2`

## unitconvert

Converts a value given in a unit to an other unit

**Parameters:**

1. Ammount to convert
2. Source unit name of ammount to convert
3. Target unit name

Example: `unitconvert 33 meter foot`

## unset 

Unset a variable

**Parameters:**

1. Variable name, that will be unset.

Example: `unset x`

## version

prints out current program version

**Parameters:** No parameters are needed

# Functions 

The following functions can be used in expression, when using the `eval` command to evaluate expressions:

* `Abs(x)`

    Returns the absolute value of a number. Works on complex and numbers that can be converted to double.

* `Acos(x)`

    The inverse of the cosine function. Given a value between -1 and 1, the acos function returns an angle, whose cosine is equal to that value. The range of the acos function is 0 radians (0° or 0 gradians) to π radians (180° or 200 gradians)

    Works on numbers that can be converted to double. Result is affected by current angle system.

* `And(x, y)`

    Computes the bitwise-and of two values.

    Works on numbers that can be converted to integers.

* `Asin(x)`

    The inverse of the sine function. It takes as input a value between -1 and 1 and returns an angle, hose sine is equal to that value. The range of the arcsin function is limited to -π/2 radians (-90° or -100 gradians) to π/2 radians (90° or 100 gradians), ensuring a unique output for each input within that range.

    Works on numbers that can be converted to double. Result is affected by current angle system.

* `Atan(x)`

    the inverse of the tangent function. It takes as input a real number and returns an angle in radians whose tangent is equal to that number. The range of the arctan function is -π/2 radians (-90° or -100 gradians) to π/2 radians (90° or 100 gradians), making it suitable for determining angles in right-angled triangles.

    Works on numbers that can be converted to double. Result is affected by current angle system.

* `Ceil(x)` 

    Returns the smallest integral value that is greater than or equal to the specified number. Works on numbers that can be converted to double.

* `Cos(x)`

    Returns the cosine of the specified angle. Works on numbers that can be converted to double. Result is affected by current angle system.

* `Cplx(x; y)`

    Creates a complex number from two numbers that can be converted to double.

* `Factorial(x)`

    Computes the factorial of a given number. Works on numbers that can be converted to double.

* `Floor(x)`
    
    Returns the largest integral value less than or equal to the specified number. Works on numbers that can be converted to double.

* `IsPrime(x)`

    Returns 1, if the parameter number is a prime number. Returns 0, if it's not.

* `Ln(x)`

    Returns the natural (base e) logarithm of a specified number. Works on numbers that can be converted to double.

* `Log(x; y)`

    Returns the logarithm of a specified number in a specified base. Works on numbers that can be converted to double.

* `Not(x)`

    Computes the ones-complement representation of a given value.

    Works on numbers that can be converted to integers.

* `Or(x, y)`

    Computes the bitwise-or of two values.

    Works on numbers that can be converted to integers.

* `Percent(x; y)`

    Computes the y percent of x. Works on numbers that can be converted to double.

* `Root(x; y)`

    Returns the Nth root of a specified number. Works on numbers that can be converted to double.

* `Shl(x, y)`

    Shifts a value left by a given amount. y must be in range of 1 to 128

    Works on numbers that can be converted to integers.

* `Shr(x, y)`

    Shifts a value right by a given amount. y must be in range of 1 to 128

    Works on numbers that can be converted to integers.

* `Sign(x)`

    Returns an integer that indicates the sign of the parameter number. Works on numbers that can be converted to double.

* `Sin(x)`

    Returns the sine of the specified angle. Works on numbers that can be converted to double. Result is affected by current angle system.


* `Tan(x)`

    Returns the tangent of the specified angle. Works on numbers that can be converted to double. Result is affected by current angle system.

* `Xor(x, y)`

    Computes the exclusive-or of two values.

    Works on numbers that can be converted to integers.