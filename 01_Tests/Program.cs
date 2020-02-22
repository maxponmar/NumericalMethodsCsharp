using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using NumericalMethods;
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
            // REMAINDER
            // WRITE AN EXAMPLE FOR EVERY SINGLE METHOD
            // OPTIMIZE POLYNOMIAL TOSTRING METHOD
            List<string> log = new List<string>();
            
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