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

        [Fact]
        public void GetMatrixValueTest()
        {
            double[,] matrixValues =
                {
                    { 1, 2, 3 },
                    { 4, 5, 6 },
                    { 7, 8, 9 }
                };            

            Matrix matrix = new Matrix(matrixValues);

            double firstRowSecondColumnValue = matrix[0, 1];

            Assert.Equal(2, matrix[0,1]);
        }

        [Fact]
        public void SetMatrixValueTest()
        {
            double[,] matrixValues =
                {
                    { 1, 2, 3 },
                    { 4, 5, 6 },
                    { 7, 8, 9 }
                };

            Matrix matrix = new Matrix(matrixValues);

            double firstRowSecondColumnValue = 9;

            matrix[0, 1] = firstRowSecondColumnValue;

            Assert.Equal(firstRowSecondColumnValue, matrix[0, 1]);
        }

        [Fact]
        public void CloneMatrixTest()
        {
            double[,] matrixValues =
                {
                    { 1, 2, 3 },
                    { 4, 5, 6 },
                    { 7, 8, 9 }
                };

            Matrix firstMatrix = new Matrix(matrixValues);

            Matrix secondMatrix = firstMatrix.Clone();

            Assert.Equal(secondMatrix.Data, firstMatrix.Data);
        }
    }
}
