using System;
using NumericalMethodsLibrary.MathObjects.PolynomialClass;
using Xunit;

namespace NumericalMethodsLibrary.Tests.PolynomialTests
{
    public class PolynomialTests
    {
        [Fact]
        public void FirstPolynomialEvaluationTest()
        {
            double[] coefficients = { -1, 2, -6, 2 };

            Polynomial polynomial = new Polynomial(coefficients);

            double actualResult = polynomial.Evaluate(3);

            Assert.Equal(5, actualResult);
        }
       
        [Fact]
        public void SecondPolynomialEvaluationTest()
        {
            double[] coefficients = { 1, 3, 0, 2 };

            Polynomial polynomial = new Polynomial(coefficients);

            double actualResult = polynomial.Evaluate(2);

            Assert.Equal(23, actualResult);
        }

        [Fact]
        public void DeleteLastZerosInPolynomialTest()
        {
            double[] coefficients = { 1, 9, 0, 4, 0, 0 };
            double[] expectedCoefficients = { 1, 9, 0, 4 };

            Polynomial polynomial = new Polynomial(coefficients);

            polynomial.deleteLastZeros();            

            Assert.Equal(expectedCoefficients, polynomial.Coefficients);
        }

        [Fact]
        public void ClonePolynomialTest()
        {
            double[] coefficients = { 1, 2, 3};
            double[] expectedCoefficients = { 1, 2, 3 };

            Polynomial polynomial = new Polynomial(coefficients);

            Polynomial actualResult = polynomial.Clone();            

            Assert.Equal(expectedCoefficients, actualResult.Coefficients);
        }

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
    }
}
