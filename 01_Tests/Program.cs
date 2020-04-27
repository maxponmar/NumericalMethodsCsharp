using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


using NumericalMethods;
using NumericalMethods.RootFinding;
using NumericalMethods.LinearAlgebraicEquations;
using NumericalMethods.Optimization;
using NumericalMethods.CurveFitting.Least_Square_Regression;
using NumericalMethods.CurveFitting.Interpolation;
using NumericalMethods.CurveFitting.FourierApproximation;
using NumericalMethods.DifferentiationIntegration;
using NumericalMethods.OrdinayDifferentialEquations;

namespace _01_PruebasNumericalMethods
{
    class Program
    {
        static public void list2csv(List<string> list)
        {
            File.WriteAllLines(@"D:\Documentos\00_Pruebas\xd.csv", list.ToArray());
        }

        static void Main(string[] args)
        {
            #region Root Finding

            Console.WriteLine("===== Root Finding =====");

            List<string> log = new List<string>();
            // Test function
            string fx = "x^3 -x-1";

            Console.WriteLine();
            Console.WriteLine("Testing function: f(x) = " + fx);
            Console.WriteLine();

            // Bisection
            Console.WriteLine("   -> Bisection");
            Bisection bs = new Bisection();
            bs.FindRoot(fx, 1, 2);
            Console.WriteLine("      x = " + bs.LastResult[0]);
            Console.WriteLine("      f(x) = " + bs.LastResult[1]);
            Console.WriteLine("      error = " + bs.LastResult[2]);
            Console.WriteLine();

            // Newton-Raphson
            Console.WriteLine("   -> Newthon-Raphson");
            Newthon_Raphson nr = new Newthon_Raphson();
            nr.FindRoot(fx, 1);
            //nr.FindRoot(fx, 1, fx_derivative: "3x^2 -1");
            Console.WriteLine("      x = " + nr.LastResult[0]);
            Console.WriteLine("      f(x) = " + nr.LastResult[1]);
            Console.WriteLine("      error = " + nr.LastResult[2]);
            Console.WriteLine();

            // False position
            Console.WriteLine("   -> False Position");
            False_Position fp = new False_Position();
            fp.FindRoot(fx, 1, 2);
            Console.WriteLine("      x = " + fp.LastResult[0]);
            Console.WriteLine("      f(x) = " + fp.LastResult[1]);
            Console.WriteLine("      error = " + fp.LastResult[2]);
            Console.WriteLine();

            // Fixed point
            Console.WriteLine("   -> Fixed point");
            Fixed_Point fxpt = new Fixed_Point();
            fxpt.FindRoot(fx, "(1+x)^(1/3)", 1);
            Console.WriteLine("      x = " + fxpt.LastResult[0]);
            Console.WriteLine("      f(x) = " + fxpt.LastResult[1]);
            Console.WriteLine("      error = " + fxpt.LastResult[2]);
            Console.WriteLine();

            // Secant
            Console.WriteLine("   -> Secant");
            Secant secant = new Secant();
            secant.FindRoot(fx, 1, 2);
            Console.WriteLine("      x = " + secant.LastResult[0]);
            Console.WriteLine("      f(x) = " + secant.LastResult[1]);
            Console.WriteLine("      error = " + secant.LastResult[2]);
            Console.WriteLine();

            // Brent
            Console.WriteLine("   -> Brent");
            NumericalMethods.RootFinding.Brent brent = new NumericalMethods.RootFinding.Brent();
            brent.FindRoot(fx, 1, 2);
            Console.WriteLine("      x = " + brent.LastResult[0]);
            Console.WriteLine("      f(x) = " + brent.LastResult[1]);
            Console.WriteLine("      error = " + brent.LastResult[2]);
            Console.WriteLine();

            // Muller
            Console.WriteLine("   -> Muller");
            Muller muller = new Muller();
            muller.FindRoot(fx, 1, 2);
            Console.WriteLine("      x = " + muller.LastResult[0]);
            Console.WriteLine("      f(x) = " + muller.LastResult[1]);
            Console.WriteLine("      error = " + muller.LastResult[2]);
            Console.WriteLine();

            // Bairstow
            Console.WriteLine("   -> Bairstow");
            Bairstow bairstow = new Bairstow();
            // x^3 -x-1                      -1, -1x, 0x^2, 1x^3   
            bairstow.FindRoot(new double[] { -1, -1, 0, 1 });
            foreach (string root in bairstow.Results)
            {
                Console.WriteLine("      " + root);
            }

            #endregion

            #region Linear Algebraic Equations

            Console.WriteLine("\n===== Linear Algebraic Equations =====");

            // Test system
            double[,] a = new double[,] {{ 1, 3, 7},
                                         { 3, 0, 2 },
                                         { 6, 2, 1 }};
            double[] b = new double[] { -1, 0, 6 };

            Console.WriteLine();
            Console.WriteLine("Testing system: ");
            for (int i = 0; i < 3; i++)
                Console.WriteLine(string.Format($"{a[i, 0]}x1 + {a[i, 1]}x2 + {a[i, 2]}x3 = {b[i]}"));

            double[] x = new double[3];
            Console.WriteLine();

            // Gauss Elimination
            Console.WriteLine("   -> Gauss Elimination");
            GaussElimination ge = new GaussElimination();
            ge.Solve(a, b, out x);
            for (int i = 0; i < 3; i++)
                Console.WriteLine(string.Format($"      x1 = {x[i]}"));
            Console.WriteLine();

            // LU Decomposition
            // SOME ISSUES, DOESN'T WORK
            Console.WriteLine("   -> LU Decomposition");
            Console.WriteLine("      **DOESN'T WORK**");
            Doolittle doolittle = new Doolittle();
            x = new double[3];
            doolittle.Solve(a, b, out x);
            for (int i = 0; i < 3; i++)
                Console.WriteLine(string.Format($"      x1 = {x[i]}"));
            Console.WriteLine();

            #endregion

            #region Optimization

            Console.WriteLine("\n===== Optimization =====");

            // Test function
            string fun = "e^((x-0.7)^2)";

            Console.WriteLine();
            Console.WriteLine("Testing function: " + fun);
            Console.WriteLine();

            // Brent
            Console.WriteLine("   -> Brent");
            NumericalMethods.Optimization.Brent br = new NumericalMethods.Optimization.Brent();
            br.Solve(fun, 0, 2);
            Console.WriteLine("      x = " + br.LastResult[0]);
            Console.WriteLine("      f(x) = " + br.LastResult[1]);
            Console.WriteLine();

            // Golden section
            Console.WriteLine("   -> Golden section");
            Console.WriteLine("   -> **DOESN'T WORK**");
            GoldenSection gs = new GoldenSection();
            gs.Solve(fun, 0, 2);
            Console.WriteLine("      x = " + gs.LastResult[0]);
            Console.WriteLine("      f(x) = " + gs.LastResult[1]);
            Console.WriteLine();

            #endregion

            #region Curve fitting

            Console.WriteLine("\n===== Curve fitting =====");

            // Test data
            double[] testX = new double[] { 1, 2, 3, 4 };
            double[] testY = new double[] { 1, 3, 2, 5 };

            Console.WriteLine();
            Console.WriteLine("Testing dataset: ");
            Console.WriteLine(string.Format($"X: {testX[0]}-{testX[1]}-{testX[2]}-{testX[3]}"));
            Console.WriteLine(string.Format($"Y: {testY[0]}-{testY[1]}-{testY[2]}-{testY[3]}"));
            Console.WriteLine();

            // Linear regression
            Console.WriteLine("   -> Linear regression");
            LinearRegression lr = new LinearRegression();
            lr.Fit(testX, testY);
            Console.WriteLine("      " + lr.LastResult);
            Console.WriteLine();

            // Polynomial regression
            Console.WriteLine("   -> Polynomial regression");
            PolynomialRegression pr = new PolynomialRegression();
            pr.Fit(testX, testY);
            Console.WriteLine("      " + pr.LastResult);
            Console.WriteLine();

            // Newton Divided Difference
            Console.WriteLine("   -> Newton Divided Difference");
            NewtonsDividedDifference ndd = new NewtonsDividedDifference();
            ndd.Fit(testX, testY);
            Console.WriteLine("      " + ndd.LastResult);
            Console.WriteLine();

            // Lagrange
            Console.WriteLine("   -> Lagrange");
            Lagrange lagrange = new Lagrange();
            lagrange.Fit(testX, testY);
            Console.WriteLine("      " + lagrange.LastResult);
            Console.WriteLine();

            // Splines
            SplinesInterpolation splines = new SplinesInterpolation();
            Console.WriteLine("   -> Splines: 1st degree");
            splines.Fit(testX, testY, 1);
            Console.WriteLine(splines);
            Console.WriteLine();

            Console.WriteLine("   -> Splines: 2nd degree");
            splines.Fit(testX, testY, 2);
            Console.WriteLine(splines);
            Console.WriteLine();

            Console.WriteLine("   -> Splines: 3th degree");
            splines.Fit(testX, testY, 3);
            Console.WriteLine(splines);
            //Console.WriteLine("->" + splines.Eval(2.5));
            Console.WriteLine();

            // Fourier Series
            Console.WriteLine("   -> Fourier Series (3 armonics)");
            FourierSeries fft = new FourierSeries();
            fft.Fit(testX, testY, n: 3);
            Console.WriteLine(fft.FourierSerie.ToString());
            Console.WriteLine();

            #endregion

            #region Numerical Differentiation and Integration

            // Numerical Differentiation
            Console.WriteLine("===== Numerical differenciation =====\n");
            Console.WriteLine(" Function f(x) = -0.1x^4 - 0.15x^3 - 0.5x^2 - 0.25x + 1.2 when x = 3\n");

            Differentiation diff = new Differentiation();                        

            double res = 0;

            // Testing from 1 to 4th derivative, all 3 methods and both simple and improved versions
            string[] methods = new string[] { "forward", "backward", "centered" };
            string[] types = new string[] { "simple", "improved" };
            for (int grade = 1; grade <= 4; grade++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        //x = diff.derivative("-0.1x^4 - 0.15x^3 - 0.5x^2 - 0.25x + 1.2", 3, h: 0.01, grade, methods[i], types[j]);
                        //Console.WriteLine("   -> (grade = " + grade + " Derivative (" + methods[i] + " - " + types[j] + ") = " + x + "\n");
                        try
                        {
                            res = diff.Derivative("-0.1x^4 - 0.15x^3 - 0.5x^2 - 0.25x + 1.2", 3, h: 0.01, grade, methods[i], types[j]);
                            Console.WriteLine("   -> (grade = " + grade + " Derivative (" + methods[i] + " - " + types[j] + ") = " + res);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("   -> ERROR (grade-method-type): " + grade + " - " + methods[i] + " - " + types[j] + " xxxxxx ");
                        }
                    }
                }
                Console.WriteLine();
            }

            // Numerical Integration
            Console.WriteLine("===== Numerical Integration =====\n");
            Console.WriteLine(" Function f(x) = 0.2 + 25x - 200x^2 + 675x^3 - 900x^4 + 400x^5 from 0 to 0.8\n");

            Integration integration = new Integration();

            double integrate;

            integrate = integration.Integrate("0.2 + 25x - 200x^2 + 675x^3 - 900x^4 + 400x^5", 0, 0.8, method: "trapezoidal");
            Console.WriteLine("   -> Using trapezoidal method: " + integrate);

            integrate = integration.Integrate("0.2 + 25x - 200x^2 + 675x^3 - 900x^4 + 400x^5", 0, 0.8, method: "simpson12");
            Console.WriteLine("   -> Using simpson12 method: " + integrate);

            integrate = integration.Integrate("0.2 + 25x - 200x^2 + 675x^3 - 900x^4 + 400x^5", 0, 0.8, method: "simpson38");
            Console.WriteLine("   -> Using simpson38 method: " + integrate);

            #endregion

            #region Ordinary Differential Equations

            Console.WriteLine("\n===== Numerical Integration =====\n");
            Console.WriteLine(" Tets: y' = -2x^3 + 12x^2 - 20x + 8.5, from x=0 to x=4, initial conditions: y(0) = 1\n");

            ODE_Solver odeSolver = new ODE_Solver();
            
            double[] result_ode = odeSolver.Solve_Euler("-2x^3 + 12x^2 - 20x + 8.5", 1, 0, 0.5, new double[] { 0, 4 });
            Console.WriteLine("   -> Using Euler method: ");
            foreach (double value in result_ode)            
                Console.WriteLine("      " + value);

            result_ode = odeSolver.Solve_ImprovedEuler("-2x^3 + 12x^2 - 20x + 8.5", 1, 0, 0.5, 5, new double[] { 0, 4 });
            Console.WriteLine("   -> Using Improved Euler method (5 correctors): ");
            foreach (double value in result_ode)
                Console.WriteLine("      " + value);

            result_ode = odeSolver.Solve_RungeKutta4("-2x^3 + 12x^2 - 20x + 8.5", 1, 0, 0.5, new double[] { 0, 4 });
            Console.WriteLine("   -> Using Runge-Kutta (Fourth-Order) method: ");
            foreach (double value in result_ode)
                Console.WriteLine("      " + value);

            result_ode = odeSolver.Solve_ButcherRK("-2x^3 + 12x^2 - 20x + 8.5", 1, 0, 0.5, new double[] { 0, 4 });
            Console.WriteLine("   -> Using Butcher's (RK Fifth-Order) method: ");
            foreach (double value in result_ode)
                Console.WriteLine("      " + value);



            #endregion                 
        }
    }
}