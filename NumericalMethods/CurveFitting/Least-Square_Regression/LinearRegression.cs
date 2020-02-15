using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethods.CurveFitting.Least_Square_Regression
{
    public class LinearRegression
    {
        /// <summary>
        /// This method fits the given data with linear regression method (Return a Polynomial object)
        /// </summary>
        /// <param name="x">Independent data</param>
        /// <param name="y">Dependent data</param>
        /// <returns>Returns a polynomial of degree 1</returns>
        public Polynomial Fit(double[] x, double[] y)
        {
            double a = 0, b = 0;
            if (x.Length == y.Length && x.Length > 1)
            {
                double zy = 0, zx = 0, zx_square = 0, zxy = 0;
                double average_x = 0, average_y = 0;
                int n = x.Length;
                for (int i = 0; i < n; i++)
                {
                    zy += y[i];
                    zx += x[i];
                    zxy += (x[i] * y[i]);
                    zx_square += Math.Pow(x[i], 2);
                }
                average_x = zx / n;
                average_y = zy / n;
                b = (n * zxy - zx * zy) / (n * zx_square - zx * zx);
                a = average_y - b * average_x;
            }
            else
            {
                Console.WriteLine("The data set isn't compatible -> x.Length and y.Length must be the same and greater than 1");
            }
            return new Polynomial(new double[] { a, b });
        }
    }
}
