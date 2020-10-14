using System;
using NumericalMethodsLibrary.MathObjects.PolynomialClass;
using Xunit;

namespace NumericalMethodsLibrary.Tests.PolynomialTests
{
    public class PrintPolynomialTests
    {
        [Fact]
        public void PrintPolynomialFirstTest()
        {
            double[] coefficients = { -12, -4, 3, 1 };
            string expectedResult = "-12 - 4x + 3x^2 + x^3";

            Polynomial polynomial = new Polynomial(coefficients);

            string actualResult = polynomial.ToString();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void PrintPolynomialSecondTest()
        {
            double[] coefficients = { 2, -1, 3, -1 };
            string expectedResult = "2 - x + 3x^2 - x^3";

            Polynomial polynomial = new Polynomial(coefficients);

            string actualResult = polynomial.ToString();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void PrintPolynomialThirdTest()
        {
            double[] coefficients = { 0, 1, 3, -2 };
            string expectedResult = "x + 3x^2 - 2x^3";

            Polynomial polynomial = new Polynomial(coefficients);

            string actualResult = polynomial.ToString();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void PrintPolynomialFourthTest()
        {
            double[] coefficients = { 0, -1, 3, -2, 0, 1 };
            string expectedResult = "-x + 3x^2 - 2x^3 + x^5";

            Polynomial polynomial = new Polynomial(coefficients);

            string actualResult = polynomial.ToString();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void PrintPolynomialFifthTest()
        {
            double[] coefficients = { 0, 0, 3 };
            string expectedResult = "3x^2";

            Polynomial polynomial = new Polynomial(coefficients);

            string actualResult = polynomial.ToString();

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
