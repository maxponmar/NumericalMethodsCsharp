using System;
using NumericalMethodsLibrary.MathObjects.PolynomialClass;
using Xunit;

namespace NumericalMethodsLibrary.Tests.PolynomialTests
{
    public class PolynomialTests
    {
        [Fact]
        public void GetCoefficientFromPolynomialTest()
        {
            double[] coefficients = { 1, 9, 4, 7 };

            Polynomial polynomial = new Polynomial(coefficients);

            double secondCoefficient = polynomial[1];

            Assert.Equal(9, secondCoefficient);
        }

        [Fact]
        public void SetCoefficientFromPolynomialTest()
        {
            double[] coefficients = { 1, 9, 4, 7 };

            Polynomial polynomial = new Polynomial(coefficients);

            double secondCoefficient = 13;

            polynomial[1] = 13;

            Assert.Equal(13, secondCoefficient);
        }

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
    }
}
