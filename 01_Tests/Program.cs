using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using NumericalMethods;
using NumericalMethods.CurveFitting.Least_Square_Regression;
using NumericalMethods.CurveFitting.Interpolation;

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
            // REMAINDER
            // WRITE AN EXAMPLE FOR EVERY SINGLE METHOD
            // OPTIMIZE POLYNOMIAL TOSTRING METHOD
            List<string> log = new List<string>();
            
            Console.WriteLine("Testing Linear Regression");
            LinearRegression lr = new LinearRegression();
            PolynomialRegression pr = new PolynomialRegression();

            NewtonsDividedDifference ndd = new NewtonsDividedDifference();

            double[] testX = new double[] { 1,4,6 };
            double[] testY = new double[] { 0, 1.386294, 1.791759 };

            Polynomial polynomial = ndd.Fit(testX, testY);
            Console.WriteLine(polynomial);
        }       
    }
}