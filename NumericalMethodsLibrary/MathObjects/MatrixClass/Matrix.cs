using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace NumericalMethodsLibrary.MathObjects.MatrixClass
{
    public partial class Matrix
    {
        private List<List<double>> data;
        private List<int> characterColumnLengths;
        private int rowsCount;
        private int columnsCount;

        public int RowsCount { get => rowsCount; }
        public int ColumnsCount { get => columnsCount; }

        public double[,] Data
        {
            get => convertDataTo2DArray();
            set => setData(value);
        }

        public double this[int row, int column]
        {
            get => data[row][column];
            set => data[row][column] = value;
        }

        public Matrix()
        {
            data = new List<List<double>>();
            characterColumnLengths = new List<int>();
            rowsCount = 0;
            columnsCount = 0;
        }

        public Matrix(double[,] data)
        {
            characterColumnLengths = new List<int>();
            setData(data);
        }

        public Matrix(List<List<double>> data)
        {
            this.data = data;
            rowsCount = data.Count;
            columnsCount = data[0].Count;
            characterColumnLengths = new List<int>();
        }

        private double[,] convertDataTo2DArray()
        {
            double[,] wantedData = new double[rowsCount, columnsCount];

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    wantedData[i, j] = data[i][j];
                }
            }

            return wantedData;
        }

        private void setData(double[,] data)
        {
            this.data = new List<List<double>>();

            rowsCount = data.GetLength(0);
            columnsCount = data.GetLength(1);

            for (int i = 0; i < rowsCount; i++)
            {
                double[] newRow = new double[columnsCount];
                for (int j = 0; j < columnsCount; j++)
                {
                    newRow[j] = data[i, j];
                }
                this.data.Add(newRow.ToList());
            }
        }

        public Matrix Clone()
        {
            return new Matrix(this.data);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < this.rowsCount; i++)
            {
                stringBuilder.Append("[ ");

                for (int j = 0; j < columnsCount; j++)
                {
                    // Get maximum lenght of every column only in the first row iteration
                    if (i == 0)
                        setCharacterMaximumLenght(j);

                    stringBuilder.Append($"{centerNumber(this.data[i][j], j)} ");
                }
                stringBuilder.Append("]\n");
            }

            return stringBuilder.ToString();
        }

        private void setCharacterMaximumLenght(int column)
        {
            int currentLenght = 1;


            for (int i = 0; i < this.rowsCount; i++)
            {
                for (int j = 0; j < this.columnsCount; j++)
                {
                    // Initiate with 1 to compare with
                    this.characterColumnLengths.Add(1);

                    currentLenght = this.data[i][j].ToString().Length;

                    if (currentLenght > characterColumnLengths[j])
                    {
                        characterColumnLengths[j] = currentLenght;
                    }
                }
            }
        }

        private string centerNumber(double number, int column)
        {
            return String.Format("{0,-" + this.characterColumnLengths[column] + "}", number);
        }
    }
}
