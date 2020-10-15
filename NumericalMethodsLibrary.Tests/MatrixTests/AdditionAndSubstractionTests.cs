using System;
using NumericalMethodsLibrary.MathObjects.MatrixClass;
using Xunit;

namespace NumericalMethodsLibrary.Tests.MatrixTests
{
    public class AdditionAndSubstractionTests
    {
        [Fact]
        public void AddTwoMatricesTest()
        {
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
                    { 0, 7, 12 },
                    { 5, 9, 11 }
                };


            Matrix firstMatrix = new Matrix(firstMatrixValues);
            Matrix secondMatrix = new Matrix(secondMatrixValues);

            Matrix actualResult = firstMatrix + secondMatrix;

            Assert.Equal(expectedValues, actualResult.Data);
        }

        [Fact]
        public void SubstractTwoMatricesTest()
        {
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
                    { 2, -1, 0 },
                    { 8, 3, 0 },
                    { 9, 7, 7 }
                };


            Matrix firstMatrix = new Matrix(firstMatrixValues);
            Matrix secondMatrix = new Matrix(secondMatrixValues);

            Matrix actualResult = firstMatrix - secondMatrix;

            Assert.Equal(expectedValues, actualResult.Data);
        }
    }
}
