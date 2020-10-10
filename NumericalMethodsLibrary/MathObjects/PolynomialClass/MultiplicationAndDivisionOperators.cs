using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethodsLibrary.MathObjects.PolynomialClass
{
    public partial class Polynomial
    {
        public static Polynomial operator *(Polynomial polynomial, double value)
        {
            return multiplyPolynomialByValue(polynomial, value);
        }

        public static Polynomial operator *(double value, Polynomial polynomial)
        {
            return multiplyPolynomialByValue(polynomial, value);
        }

        public static Polynomial operator *(Polynomial firstPolynomial, Polynomial secondPolynomial)
        {
            double[] firstPolynomialCoefficients = firstPolynomial.coefficients.ToArray();
            double[] secondPolynomialCoefficients = secondPolynomial.coefficients.ToArray();

            int len = firstPolynomialCoefficients.Length + secondPolynomialCoefficients.Length - 1;
            double[] newCoef = new double[len];

            for (int i = 0; i < firstPolynomialCoefficients.Length; i++)
            {
                for (int j = 0; j < secondPolynomialCoefficients.Length; j++)
                    newCoef[i + j] += firstPolynomialCoefficients[i] * secondPolynomialCoefficients[j];
            }
            return new Polynomial(newCoef);
        }
        
        public static Polynomial operator /(Polynomial polynomial, double value)
        {
            return (1 / value) * polynomial;
        }

        public static (Polynomial, Polynomial) operator /(Polynomial numeratorPolynomial, Polynomial denumeratorPolynomial)
        {
            List<double> numeratorCoefficients = numeratorPolynomial.coefficients;
            List<double> denumenatorCoefficients = denumeratorPolynomial.coefficients;

            numeratorCoefficients.Reverse();
            denumenatorCoefficients.Reverse();

            var (quotientDoubleListCoefficients, remainderDoubleListCoefficients) = extendedSyntheticDivision(numeratorCoefficients, denumenatorCoefficients);

            quotientDoubleListCoefficients.Reverse();
            remainderDoubleListCoefficients.Reverse();

            double[] quotientCoefficients = quotientDoubleListCoefficients.ToArray();
            double[] reminderCoefficients = remainderDoubleListCoefficients.ToArray();

            Polynomial quotientPolynomial = new Polynomial(quotientCoefficients);
            Polynomial reminderPolynomial = new Polynomial(reminderCoefficients);

            quotientPolynomial.deleteLastZeros();
            reminderPolynomial.deleteLastZeros();

            return (quotientPolynomial, reminderPolynomial);
        }

        private static Polynomial multiplyPolynomialByValue(Polynomial polynomial, double value)
        {
            double[] resultCoefficients = polynomial.coefficients.ToArray();
            for (int i = 0; i < resultCoefficients.Length; ++i)
            {
                resultCoefficients[i] *= value;
            }
            return new Polynomial(resultCoefficients);
        }

        private static (List<double>, List<double>) extendedSyntheticDivision(List<double> dividendCoefficients, List<double> divisorCoefficients)
        {
            List<double> quotientCoefficients = dividendCoefficients;

            double normalizer = divisorCoefficients[0];

            for (int i = 0; i < dividendCoefficients.Count - (divisorCoefficients.Count - 1); i++)
            {
                quotientCoefficients[i] /= normalizer;

                double coefficient = quotientCoefficients[i];
                if (coefficient != 0)
                {
                    for (int j = 1; j < divisorCoefficients.Count; j++)
                    {
                        quotientCoefficients[i + j] += -divisorCoefficients[j] * coefficient;
                    }
                }
            }

            int separator = quotientCoefficients.Count - (divisorCoefficients.Count - 1);

            List<double> remainderCoefficients = quotientCoefficients.GetRange(separator, quotientCoefficients.Count - separator);
            for (int i = 0; i < separator; i++)
            {
                remainderCoefficients.Insert(0, 0);
            }

            return (quotientCoefficients.GetRange(0, separator), remainderCoefficients);
        }
    }
}
