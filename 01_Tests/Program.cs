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
            
            Console.WriteLine("Testing NDD");
            LinearRegression lr = new LinearRegression();
            PolynomialRegression pr = new PolynomialRegression();

            NewtonsDividedDifference ndd = new NewtonsDividedDifference();
;           double[] testX = new double[] { -3, -2, 0, 4 };
            double[] testY = new double[] { 5, 8, 4, 2 };

            Polynomial polynomial = ndd.Fit(testX, testY);
            
            Console.WriteLine(polynomial);                       
        }       
    }
}