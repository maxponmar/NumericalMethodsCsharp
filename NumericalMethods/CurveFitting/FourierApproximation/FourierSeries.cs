using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethods.CurveFitting.FourierApproximation
{
    public class FourierSeries
    {
        private Dictionary<double[], Polynomial> Splines = new Dictionary<double[], Polynomial>();

        public Dictionary<double[], Polynomial> Splines1 { get => Splines; }

        /// <summary>
        /// This method fits the given data using Splines, you can choose 1st, 2nd or 3th degree
        /// The result will be stored in "Splines" Directory (object attribute)
        /// The values are polynomials of 1st, 2nd or 3th degree and the Keys are their respective limits
        /// You can print spline object to get an idea of the explanation above
        /// </summary>
        /// <param name="x">Independent data</param>
        /// <param name="y">Dependent data</param>
        /// <param name="type">Choose the degree (3 by default)</param>
        public void Fit(double[] x, double[] y, int type = 3)
        {
            if (x.Length == y.Length && x.Length > 1)
            {

            }
            else
            {
                Console.WriteLine("The dataset is not compatible -> x.Length and y.Length must be the same and greater than 1");
            }
        }
    }
}
