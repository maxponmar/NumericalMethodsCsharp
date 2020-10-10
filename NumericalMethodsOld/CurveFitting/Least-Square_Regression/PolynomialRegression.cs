using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethods.CurveFitting.Least_Square_Regression
{
    public class PolynomialRegression
    {
        private Polynomial lastResult;
        /// <summary>
        /// This value save the last result
        /// </summary>
        public Polynomial LastResult { get => lastResult; }

        /// <summary>
        /// This method fits the given data with polynomial regression method, limited to a 2nd degree polynomial (Return a Polynomial object)
        /// </summary>
        /// <param name="x">Independent data</param>
        /// <param name="y">Dependent data</param>
        /// <returns>Returns a polynomial of degree 2</returns>
        public Polynomial Fit(double[] x, double[] y)
        {
            double a0 = 0, a1 = 0, a2 = 0;
            if (x.Length == y.Length && x.Length > 1)
            {
                // system to solve
                double[,] a = new double[3, 3];
                double[] b = new double[3];

                // fill the system
                a[0, 0] = x.Length;
                for (int i = 0; i < a[0, 0]; i++)
                {

                    b[0] += y[i];
                    a[0, 1] += x[i];
                    a[0, 2] += Math.Pow(x[i], 2);
                    a[1, 2] += Math.Pow(x[i], 3);
                    a[2, 2] += Math.Pow(x[i], 4);
                    b[1] += (x[i] * y[i]);
                    b[2] += (Math.Pow(x[i], 2) * y[i]);
                }
                a[1, 0] = a[0, 1];
                a[1, 1] = a[2, 0] = a[0, 2];
                a[2, 1] = a[1, 2];

                // solve it
                double[] coeff;
                LinearAlgebraicEquations.GaussElimination ge = new LinearAlgebraicEquations.GaussElimination();
                ge.Solve(a, b, out coeff);

                a0 = coeff[0];
                a1 = coeff[1];
                a2 = coeff[2];
            }
            else
            {
                Console.WriteLine("The data set isn't compatible -> x.Length and y.Length must be the same and greater than 1");
            }
            lastResult = new Polynomial(new double[] { a0, a1, a2 });
            return (Polynomial)lastResult.Clone();
        }
    }
}
