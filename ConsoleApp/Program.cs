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
                    { 1, 2, 3 },
                    { 4, 5, 6 },
                    { 7, 8, 9 }
                };

            double[,] secondMatrixValues =
                {
                    { -1, 3, 3 },
                    { -4, 2, 6 },
                    { -2, 1, 2 }
                };

            double[,] expectedValues =
                {
                    { 0, 5, 6 },
                    { 0, 10, 12 },
                    { 14, 16, 11 }
                };


            Matrix firstMatrix = new Matrix(firstMatrixValues);
            Matrix secondMatrix = new Matrix(secondMatrixValues);

            Matrix actualResult = firstMatrix + secondMatrix;
        }
    }
}
