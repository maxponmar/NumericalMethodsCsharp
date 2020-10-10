using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalMethodsLibrary.MathObjects.PolynomialClass
{
    public partial class Polynomial
    {
        public static Polynomial operator +(Polynomial polynomial) => polynomial;

        public static Polynomial operator -(Polynomial polynomial)
        {
            Polynomial result = polynomial.Clone();
            for (int i = 0; i < result.degree + 1; i++)
            {
                result.coefficients[i] *= -1;
            }
            return result;
        }

        public static Polynomial operator +(double value, Polynomial polynomial)
        {           
            return plusSignOperation(polynomial, value);
        }

        public static Polynomial operator +(Polynomial polynomial, double value)
        {
            return plusSignOperation(polynomial, value);
        }        

        public static Polynomial operator -(double value, Polynomial polynomial)
        {
            return minusSignOperation(polynomial, value);
        }

        public static Polynomial operator -(Polynomial polynomial, double value)
        {
            return minusSignOperation(polynomial, value);
        }

        public static Polynomial operator +(Polynomial firstPolynomial, Polynomial secondPolynomial)
        {
            return addTwoPolynomialsWithMultiplier(firstPolynomial, secondPolynomial, 1);
        }

        public static Polynomial operator -(Polynomial firstPolynomial, Polynomial secondPolynomial)
        {
            return addTwoPolynomialsWithMultiplier(firstPolynomial, secondPolynomial, -1);
        }

        private static Polynomial plusSignOperation(Polynomial polynomial, double value)
        {
            Polynomial result = polynomial.Clone();
            result.coefficients[0] += value;
            return result;
        }

        private static Polynomial minusSignOperation(Polynomial polynomial, double value)
        {
            Polynomial result = polynomial.Clone();
            result.coefficients[0] -= value;
            return result;
        }

        private static Polynomial addTwoPolynomialsWithMultiplier(Polynomial firstPolynomial, Polynomial secondPolynomial, int multiplier)
        {
            double[] resultCoefficients = new double[] { };
            double[] firstPolynomialCoefficients = firstPolynomial.coefficients.ToArray();
            double[] recondPolynomialCoefficients = secondPolynomial.coefficients.ToArray();

            if (firstPolynomial.degree > secondPolynomial.degree)
            {
                resultCoefficients = addDifferentSizePolynomials(firstPolynomial, secondPolynomial, multiplier);
            }
            if (firstPolynomial.degree < secondPolynomial.degree)
            {
                resultCoefficients = addDifferentSizePolynomials(secondPolynomial, firstPolynomial, multiplier);
            }
            if (firstPolynomial.degree == secondPolynomial.degree)
            {
                resultCoefficients = new double[firstPolynomial.degree + 1];
                for (int i = 0; i < firstPolynomial.degree + 1; i++)
                {
                    resultCoefficients[i] = firstPolynomialCoefficients[i] + (multiplier * recondPolynomialCoefficients[i]);
                }
            }
            return new Polynomial(resultCoefficients);
        }
        
        private static double[] addDifferentSizePolynomials(Polynomial biggestPolynomial, Polynomial smallestPolynomial, int multiplier)
        {
            double[] resultCoefficients = new double[biggestPolynomial.degree + 1];
            double[] biggerPolynomialCoefficients = biggestPolynomial.coefficients.ToArray();
            double[] smallestPolynomialCoefficients = smallestPolynomial.coefficients.ToArray();            
            
            for (int i = 0; i < biggestPolynomial.degree + 1; i++)
            {
                if (i <= smallestPolynomial.degree)
                {
                    resultCoefficients[i] = biggerPolynomialCoefficients[i] + (multiplier * smallestPolynomialCoefficients[i]);
                }
                else
                {
                    resultCoefficients[i] = biggerPolynomialCoefficients[i];
                }
            }

            return resultCoefficients;
        }
    }
}
