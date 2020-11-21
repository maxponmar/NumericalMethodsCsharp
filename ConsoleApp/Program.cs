using NumericalMethodsLibrary.MathObjects.MatrixClass;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            double[,] firstMatrixValues =
                {
                    { 11, 2, 3 },
                    { 4, 5, 60 },
                    { 7, 8, 9 }
                };

            double[,] secondMatrixValues =
                {
                    { -1, 3, 3 },
                    { -4, 2, 60 },
                    { -2, 1, 2 }
                };

            double[,] expectedValues =
                {
                    { 10, 5, 6   },
                    { 0,  7, 120 },
                    { 5,  9, 11  }
                };


            Matrix firstMatrix = new Matrix(firstMatrixValues);
            Matrix secondMatrix = new Matrix(secondMatrixValues);

            Matrix actualResult = firstMatrix + secondMatrix;

            //

            Console.WriteLine(actualResult);
        }
    }
}
