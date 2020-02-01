using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using NumericalMethods;
using NumericalMethods.CurveFitting.Least_Square_Regression;


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
            double[] testX = new double[] { 43, 21, 25, 42, 57, 59 };
            double[] testY = new double[] { 99, 65, 79, 75, 87, 81 };

            Polynomial polynomial = lr.Fit(testX, testY);
            Console.WriteLine(polynomial);
        }       
    }
}
