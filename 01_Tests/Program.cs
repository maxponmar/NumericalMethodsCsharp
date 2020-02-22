using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


using NumericalMethods;
using NumericalMethods.RootFinding;
using NumericalMethods.LinearAlgebraicEquations;
using NumericalMethods.CurveFitting.Least_Square_Regression;
using NumericalMethods.CurveFitting.Interpolation;
using NumericalMethods.CurveFitting.FourierApproximation;

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
            fxpt.FindRoot(fx, "(1+x)^(1/3)",1);
            Console.WriteLine("      x = " + fxpt.LastResult[0]);
            Console.WriteLine("      f(x) = " + fxpt.LastResult[1]);
            Console.WriteLine("      error = " + fxpt.LastResult[2]);
            Console.WriteLine();

            // Secant
            Console.WriteLine("   -> Secant");
            Secant secant = new Secant();
            secant.FindRoot(fx, 1,2);
            Console.WriteLine("      x = "+ secant.LastResult[0]);
            Console.WriteLine("      f(x) = " + secant.LastResult[1]);
            Console.WriteLine("      error = " + secant.LastResult[2]);
            Console.WriteLine();

            // Brent
            Console.WriteLine("   -> Brent");
            Brent brent = new Brent();
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



            // REMAINDER
            // WRITE AN EXAMPLE FOR EVERY SINGLE METHOD
            // OPTIMIZE POLYNOMIAL TOSTRING METHOD


            Console.WriteLine("Testing NDD");
            LinearRegression lr = new LinearRegression();
            PolynomialRegression pr = new PolynomialRegression();

            NewtonsDividedDifference ndd = new NewtonsDividedDifference();
            Lagrange lagrange = new Lagrange();

;           double[] testX = new double[] { 1,2,3,4 };
            double[] testY = new double[] { 1,5,3,1 };

            //Polynomial polynomial = lagrange.Fit(testX, testY);

            //Console.WriteLine(polynomial);                       

            NumericalMethods.CurveFitting.FourierApproximation.FourierSeries fft = new NumericalMethods.CurveFitting.FourierApproximation.FourierSeries();
            fft.Fit(testX, testY, n:3);
            Console.WriteLine(fft.FourierSerie.ToString());

            Console.WriteLine(" Eval at 2");
            Console.WriteLine(fft.FourierSerie.Eval(2));
        }       
    }
}