using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethods.CurveFitting.Interpolation
{
    public class Lagrange
    {
        /// <summary>
        /// This method fits the given data with Lagrage method, it return a Polynomial object
        /// </summary>
        /// <param name="x">Independent data</param>
        /// <param name="y">Dependent data</param>
        /// <returns>Returns a polynomial</returns>
        public Polynomial Fit(double[] x, double[] y)
        {           
            Polynomial result = new Polynomial(new double[] { y[0] });

            if (x.Length == y.Length && x.Length > 1)
            {
                
            }
            else
            {
                Console.WriteLine("The dataset is not compatible -> x.Length and y.Length must be the same and greater than 1");
            }
            return result;
        }
    }
}
