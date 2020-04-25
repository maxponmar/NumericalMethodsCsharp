# -NumericalMethodsCsharp-
Numerical Methods Library for C# 

This library is based on the book "Numerical Method for Engineers - Steven C. Chapra, Raymond P. Cannale"
and in order to build it quickly or add more methods I added algoriths writen by other people so
their credits are published on that specific code and on the library description below:

To use this library you can add the .dll to your project and then make objects of the specific class to use a numerical method.
for example to use Bairstow add "using NumericalMethods.RootFinding:" and creat an object like this: "Bairstow b = new Bairstow();" and start using it. Note that some classes have two functions to calculate the result, the first one only returns the result and the second returns the result and needs a List<string> to create a log and save all the iterations info to analyze its behavior.

(Working-on)This library also has a method to convert the List<string> object to a .csv file on your computer given the path in order to open it with excel or other software like this and make graphs or simply be able to see the information better. (Actually, this method exists on the test folder. I'll update the library later and add it in its place)

## Root Finding 

To evaluate math funcion I used mathos-parser, here is its repository:
https://github.com/MathosProject/Mathos-Parser

* Bisection:
(Bracketing Method) Calculates the root of the given function between xl and xu values, it return the result as dobule

* Fixed-point:
(Open Method) This method calculates the root of a f(x) function using Simple Fixed Poitn method given g(x) function, you could find g(x) like this: g(x) = f(x) + x
  
* Newthon-Raphson:
(Open Method) This method calculates the root of a f(x) function using Newton-Raphson's method, you could use the numerical derivative (default) or enter the symbolic derivative.
  
* Secant:
(Open Method) This method calculates the root of a f(x) function using Modified Secant method.
  
* False-Position:
(Bracketing Method) This method calculate the root of the given function between x1 and x2 values, it return the result as dobule
  
* Brent:
(Hybrid) This method calculates the root of a f(x) function using Brent's method.       
This algorithm was writen by: John D. Cook on his article : Three Methods for Root-finding in C#,
link:              https://www.codeproject.com/Articles/79541/Three-Methods-for-Root-finding-in-C
licensed under The BSD License also called the "Simplified BSD License"
  
* Muller:
The Muller's Method only works with polinomials, it finds real roots.
  
* Bairstow:
This method only works with polinomials and returns either real or complex roots (All the roots)
The original algorithm was writen by lebourhisgilles : https://workshop.numworks.com/python/lebourhisgilles
I only converted it from python to C#, here is the link of the code: https://workshop.numworks.com/python/lebourhisgilles/bairstow

## Solve Linear Algebraic Equations
To use these methods you need to give the matrix as double array 
  
* LU Decomposition:
This function uses LU Decomposition to solve a system of linear equations in the form A*x = B
  
* Gauss-Elimination:
This function uses Gauss Elimination to solve a system of linear equations in the form A*x = B
  
* Thomas LU:
This method only works with tridiagonal systems, to use it you need to give the main diagonal (f), the diagonal above the main one (e) and the diagonal below the main diagonal (g) - f,g and e are single dimention double arrays.
 
## Optimization 
* Golden section:
Golden Section method is use to found the  maximun o minimun value of a function between an internval.
* Brent:
The brent's methos combines parabolic interpolation and golden-section to find the minimun value between an interval

## Curve fitting (only for 1 input - 1 output)

### Least Square Regression
* Linear regression:
This is the simpliest least-squares approximation, this method return a 1st degree polynomial that fits the given data X Y 
* Polynomial regression:
This method fits the given data with polynomial regression method, limited to a 2nd degree polynomial (Return a Polynomial object).
### Interpolation
* Newton Divided Differences:
This method fits the given data with Newton´s Divided Differences, return a polynomial object.
* Lagrange:
This method fits the given data with Lagrage method, it returns a Polynomial object.
* Splines:
This method fits the given data using Splines, you can choose 1st, 2nd or 3th degree. The result will be stored in "Splines" Dictionary (object attribute of the class SplinesInterpolation).
The values (of the dictionary) are polynomials of 1st, 2nd or 3th degree and the Keys are their respective limits.
You can print spline object to get an idea of the explanation above.
### Fourier Approximation
* This method calculates the Fourier serie given a dataset. The result is saved as a FourierSerie Object.
The time samples need to be equally separated (e.g. 0.1-0.2-0.3, or 1-2-3)

## Numerical Differentiation and Integration

### Differentiation
With the Derivative method, you can calculate the first, second, thirdh, or fourth derivative using multiple methods:
* Forward (Simple or improved)
* Centered (Simple or improved)
* Backward (Simple or improved)
Using Taylor's series, you can choose 'simple' or 'improved' that uses more terms of the Taylor's serios so that it will be more accurate but needs to evaluate the function more times.

### Integration
You can choose the below methods to integrate:
* Trapezoidal
* Simpson 1/2
* Simpson 3/8
All are simple implementations

## Ordinary Differential Equations
To solve ordinary differential equations ( y'=f(x,y) ) you can chose one of these methods:
* Euler
* Improved Euler
* Runge-Kutta
To used you need to use the specific method and give the function, initial conditions, the boundaries ( e.g. from x=0 to x=4 ) and the h (step)

## Extras
* Polynomial class:
This class alows you to create, print and make basic operations bewteen polynomials (+,-,*,/).
* CSV-Tools:
This static class alows you to work with csv files, you can convert a List<string> to a csv file, or creat a List<string> or double[,] array from a csv file.
