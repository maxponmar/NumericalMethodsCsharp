using NumericalMethodsLibrary.MathObjects.MatrixClass.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethodsLibrary.MathObjects.MatrixClass
{
    public partial class Matrix
    {
        public static Matrix operator +(Matrix matrix) => matrix;

        public static Matrix operator -(Matrix matrix)
        {
            Matrix result = matrix.Clone();
            for (int i = 0; i < result.rowsCount + 1; i++)
            {
                for (int j = 0; j < result.columnsCount; j++)
                {
                    result[i, j] *= -1;
                }
            }
            return result;
        }

        public static Matrix operator +(Matrix firstMatrix, Matrix secondMatrix)
        {
            Matrix resultMatrix = addMatricesWithMultiplier(firstMatrix, secondMatrix, 1);
            return resultMatrix;
        }

        public static Matrix operator -(Matrix firstMatrix, Matrix secondMatrix)
        {
            Matrix resultMatrix = addMatricesWithMultiplier(firstMatrix, secondMatrix, -1);
            return resultMatrix;
        }

        public bool isTheSameSize(Matrix comparedMatrix)
        {
            bool rowsMatched = this.rowsCount == comparedMatrix.rowsCount;
            bool columnsMatched = this.columnsCount == comparedMatrix.columnsCount;

            return rowsMatched && columnsMatched;
        }

        private static Matrix addMatricesWithMultiplier(Matrix firstMatrix, Matrix secondMatrix, int multiplier)
        {
            Matrix resultMatrix = new Matrix();
            try
            {
                if (firstMatrix.isTheSameSize(secondMatrix))
                {
                    for (int i = 0; i < firstMatrix.rowsCount + 1; i++)
                    {
                        for (int j = 0; j < firstMatrix.columnsCount; j++)
                        {
                            resultMatrix[i, j] = firstMatrix[i, j] + (multiplier * secondMatrix[i, j]);
                        }
                    }
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    string matricesSizeMessage = String.Format($"First matrix is ({firstMatrix.RowsCount},{firstMatrix.ColumnsCount}) and Second matrix is ({secondMatrix.RowsCount},{secondMatrix.ColumnsCount})\n");
                    string possibleCorrentionMessage = String.Format($"Ensure that first matrix and second matrix are the same size");
                    string errorMessage = stringBuilder.ToString();

                    throw new MatricesDoesNotMatchInSize(errorMessage);
                }
            }
            catch (MatricesDoesNotMatchInSize exception)
            {
                Console.WriteLine(exception.Message);
            }
            return resultMatrix;
        }
    }
}
