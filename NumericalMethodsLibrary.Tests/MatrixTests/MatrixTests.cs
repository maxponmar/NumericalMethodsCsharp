using System;
using NumericalMethodsLibrary.MathObjects.MatrixClass;
using Xunit;

namespace NumericalMethodsLibrary.Tests.MatrixTests
{
    public class MatrixTests
    {
        [Fact]
        public void CreateMatrixAndAccessMatrixTest()
        {
            double[,] matrixValues =
                {
                    { 1, 2, 3 },
                    { 4, 5, 6 },
                    { 7, 8, 9 }
                };

            double[,] expectedValues =
                {
                    { 1, 2, 3 },
                    { 4, 5, 6 },
                    { 7, 8, 9 }
                };

            Matrix matrix = new Matrix(matrixValues);

            Assert.Equal(expectedValues, matrix.Data);
        }
    }
}
