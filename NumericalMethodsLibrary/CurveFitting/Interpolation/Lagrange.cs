using NumericalMethodsLibrary.MathObjects.PolynomialClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethodsLibrary.CurveFitting.Interpolation
{
    public class Lagrange
    {
        private Polynomial lastResult;

        public Polynomial LastResult { get => lastResult; }

        /// <summary>
        /// This method fits the given data with Lagrage method, it return a Polynomial object
        /// </summary>
        /// <param name="x">Independent data</param>
        /// <param name="y">Dependent data</param>
        /// <returns>Returns a polynomial</returns>
        public Polynomial Fit(double[] x, double[] y)
        {
            Polynomial result = new Polynomial(new double[] { 0 });

            if (x.Length == y.Length && x.Length > 1)
            {
                // Polynomial array that stores In polynomials
                Polynomial[] In = new Polynomial[x.Length];

                // Polynomial array that stores sub-polynomials to calculate In polynomials
                Polynomial[] sub_poly = new Polynomial[x.Length];

                // Obtain sub polynomials and initializa In polynomials with 1 
                double[] coef;
                for (int i = 0; i < x.Length; i++)
                {
                    coef = new double[] { -x[i], 1 };
                    sub_poly[i] = new Polynomial(coef);
                    In[i] = new Polynomial(new double[] { 1 });
                }
                // Calculate In polynomials        
                double den;
                for (int i = 0; i < x.Length; i++)
                {
                    den = 1;
                    for (int j = 0; j < sub_poly.Length; j++)
                    {
                        if (i != j)
                        {
                            In[i] *= sub_poly[j];
                            den *= (x[i] - x[j]);
                        }
                    }
                    In[i] /= den;
                }
                // Calculatin Px (result)
                for (int i = 0; i < x.Length; i++)
                    result += (y[i] * In[i]);
            }
            else
            {
                Console.WriteLine("The dataset is not compatible -> x.Length and y.Length must be the same and greater than 1");
            }
            lastResult = (Polynomial)result.Clone();
            return result;
        }
    }
}
