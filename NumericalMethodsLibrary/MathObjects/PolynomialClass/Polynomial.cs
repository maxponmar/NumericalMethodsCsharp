using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalMethodsLibrary.MathObjects.PolynomialClass
{
    public partial class Polynomial
    {
        private int degree;
        private List<double> coefficients;
        private StringBuilder polynomialStringBuilder;

        public int Degree { get => degree; }
        public double[] Coefficients
        {
            get => coefficients.ToArray();
            set
            {
                coefficients = value.ToList<double>();
                degree = value.Length - 1;
            }
        }

        /// <summary>
        /// Give the coefficients in ascending order/>
        /// </summary>
        /// <param name="coefficients">a+bx+cx^2+dx^3 => [a, b, c, d]</param>
        public Polynomial(double[] coefficients)
        {
            this.coefficients = coefficients.ToList<double>();
            degree = coefficients.Length - 1;
            polynomialStringBuilder = new StringBuilder();
        }

        public double Evaluate(double xValue)
        {
            double result = coefficients[degree];

            for (int i = degree - 1; i >= 0; i--)
            {
                result = result * xValue + coefficients[i];
            }

            return result;
        }

        public void deleteLastZeros()
        {            
            while (coefficients[degree] == 0)
            {                
                coefficients.RemoveAt(degree);
                degree--;                
            }           
        }

        public Polynomial Clone()
        {
            return new Polynomial(this.coefficients.ToArray());
        }
             
    }
}
