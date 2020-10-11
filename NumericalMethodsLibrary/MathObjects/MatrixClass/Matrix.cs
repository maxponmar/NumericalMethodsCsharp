using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalMethodsLibrary.MathObjects.MatrixClass
{
    public class Matrix
    {
        private List<List<double>> data;
        private int rowsCount;
        private int columnsCount;

        public int RowsCount { get => rowsCount; }
        public int ColumnsCount { get => columnsCount; }
        public double[,] Data
        {
            get => convertDataTo2DArray();
            set => setData(value);
        }

        public Matrix()
        {
            data = new List<List<double>>();
            rowsCount = 0;
            columnsCount = 0;
        }

        public Matrix(double[,] data)
        {
            setData(data);
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
            rowsCount = data.GetLength(0);
            columnsCount = data.GetLength(1);

            for (int i = 0; i < rowsCount; i++)
            {
                double[] newRow = new double[columnsCount];
                for (int n = 0; n < columnsCount; n++)
                {
                    newRow[n] = data[i, n];
                }
                this.data.Add(newRow.ToList());
            }
        }
    }
}
