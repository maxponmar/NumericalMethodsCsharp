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
            Polynomial polynomial = new Polynomial(coefficients);
            polynomial.deleteLastZeros();

            double[] expectedCoefficients = { 1, 9, 0, 4 };
            Assert.Equal(expectedCoefficients, polynomial.Coefficients);
        }

        [Fact]
        public void ClonePolynomialTest()
        {
            double[] coefficients = { 1, 2, 3};
            Polynomial polynomial = new Polynomial(coefficients);
            Polynomial actualResult = polynomial.Clone();

            Polynomial expectedResult = new Polynomial(coefficients);
            Assert.Equal(expectedResult.Coefficients, actualResult.Coefficients);
        }
    }
}
