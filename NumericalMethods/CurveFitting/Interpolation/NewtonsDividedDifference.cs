using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethods.CurveFitting.Interpolation
{
    public class NewtonsDividedDifference
    {
        /// <summary>
        /// This method fits the given data with polynomial regression method, limited to a 2nd degree polynomial (Return a Polynomial object)
        /// </summary>
        /// <param name="x">Independent data</param>
        /// <param name="y">Dependent data</param>
        /// <returns>Returns a polynomial of degree 2</returns>
        public Polynomial Fit(double[] x, double[] y)
        {
            // The first difference is y[0] thats why it start with this value
            Polynomial result = new Polynomial(new double[] { y[0] });

            if (x.Length == y.Length && x.Length > 1)
            {
                int n = x.Length;

                // This poylnomial array store the polynomials that are related to the divded differences   

                Polynomial[] polynomials = new Polynomial[n - 1];

                // Divided differences table
                double[,] differences = new double[n - 1, n - 1];

                // Calculating differences table, it´s complicated to explain this algorithm to me here, but perhaps I will explain it on the future 
                // By seeing a difference table example on the internet, you should understand this algorithm
                for (int j = 0; j < n - 1; j++)  // Cols
                {
                    for (int i = 0; i < n - 1; i++) // Rows
                    {
                        if (j == 0)
                            differences[i, j] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]);
                        else
                        {
                            if (i >= j)
                                differences[i, j] = (differences[i, j - 1] - differences[i - 1, j - 1]) / (x[i + 1] - x[i - j]);
                        }
                    }
                }

                // Getting the polynomials 
                polynomials[0] = new Polynomial(new double[] { -x[0], 1 });
                for (int i = 1; i < n - 1; i++)
                {
                    polynomials[i] = new Polynomial(new double[] { -x[i], 1 });
                    polynomials[i] *= polynomials[i - 1];
                }

                for (int i = 0; i < n - 1; i++)
                {
                    Polynomial temp = differences[i, i] * polynomials[i];
                    result = result + temp;
                }
            }
            else
            {
                Console.WriteLine("The dataset is not compatible -> x.Length and y.Length must be the same and greater than 1");
            }
            return result;
        }
    }
}
