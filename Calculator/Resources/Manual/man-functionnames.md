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
   `
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