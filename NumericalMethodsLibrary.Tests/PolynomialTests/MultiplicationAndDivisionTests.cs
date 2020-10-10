using System;
using NumericalMethodsLibrary.MathObjects.PolynomialClass;
using Xunit;

namespace NumericalMethodsLibrary.Tests.PolynomialTests
{
    public class MultiplicationAndDivisionTests
    {
        [Fact]
        public void MultiplyPolynomialByValueOperationTest()
        {
            double[] coefficients = { 2, 4, 6 };
            double[] expectedCoefficients = { 6, 12, 18 };

            Polynomial polynomial = new Polynomial(coefficients);
            Polynomial actualResult = polynomial * 3;

            Assert.Equal(expectedCoefficients, actualResult.Coefficients);
        }

        [Fact]
        public void MultiplyValueValuePolynomialOperationTest()
        {
            double[] coefficients = { 2, -4, 6 };
            double[] expectedCoefficients = { 4, -8, 12 };

            Polynomial polynomial = new Polynomial(coefficients);
            Polynomial actualResult = 2 * polynomial;

            Assert.Equal(expectedCoefficients, actualResult.Coefficients);
        }

        [Fact]
        public void MultiplyTwoPolynomialsOperationTest()
        {
            double[] firstPolynomialCoefficients = { 2, 3 };
            double[] secondPolynomialCoefficients = { 5, -7, 4 };

            double[] expectedCoefficients = { 10, 1, -13, 12 };

            Polynomial firstPolynomial = new Polynomial(firstPolynomialCoefficients);
            Polynomial secondPolynomial = new Polynomial(secondPolynomialCoefficients);
            Polynomial actualResult = firstPolynomial * secondPolynomial;

            Assert.Equal(expectedCoefficients, actualResult.Coefficients);
        }

        [Fact]
        public void DividePolynomialByValueOperationTest()
        {
            double[] coefficients = { 10, 8, -7 };
            double[] expectedCoefficients = { 5, 4, -3.5 };

            Polynomial polynomial = new Polynomial(coefficients);
            Polynomial actualResult = polynomial / 2;

            Assert.Equal(expectedCoefficients, actualResult.Coefficients);
        }

        [Fact]
        public void DivideTwoPolynomialsOperationTest()
        {
            double[] dividendPolynomialCoefficients = { 9, 2, 7, 2 };
            double[] divisorPolynomialCoefficients = { 3, 2 };

            double[] quotientCoefficientsExpected = { -2, 2, 1 };
            double[] reminderCoefficientsExpected = { 15 };

            Polynomial dividendPolynomial = new Polynomial(dividendPolynomialCoefficients);
            Polynomial divisorPolynomial = new Polynomial(divisorPolynomialCoefficients);
            
            var (quotientPolynomial, reminderPolynomial) = dividendPolynomial / divisorPolynomial;

            Assert.Equal(quotientCoefficientsExpected, quotientPolynomial.Coefficients);
            Assert.Equal(reminderCoefficientsExpected, reminderPolynomial.Coefficients);
        }
    }
}
