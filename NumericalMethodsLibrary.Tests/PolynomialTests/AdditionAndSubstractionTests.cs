using System;
using NumericalMethodsLibrary.MathObjects.PolynomialClass;
using Xunit;

namespace NumericalMethodsLibrary.Tests.PolynomialTests
{
    public class AdditionAndSubstractionTests
    {
        [Fact]
        public void PlusSignOperatorTest()
        {
            double[] coefficients = { 1, 2, 3 };

            Polynomial polynomial = new Polynomial(coefficients);
            Polynomial actualResult = +polynomial;
            Polynomial expectedResult = new Polynomial(coefficients);

            Assert.Equal(expectedResult.Coefficients, actualResult.Coefficients);
        }

        [Fact]
        public void MinusSignOperatorTest()
        {
            double[] coefficients = { 1, 2, 3 };
            double[] expectedCoefficients = { -1, -2, -3 };
            
            Polynomial polynomial = new Polynomial(coefficients);
            Polynomial actualResult = -polynomial;
            Polynomial expectedResult = new Polynomial(expectedCoefficients);

            Assert.Equal(expectedResult.Coefficients, actualResult.Coefficients);
        }

        [Fact]
        public void ValuePlusPolynomialOperationTest()
        {
            double[] coefficients = { 1, 2, 3 };
            double[] expectedCoefficients = { 3, 2, 3 };

            Polynomial polynomial = new Polynomial(coefficients);
            Polynomial actualResult = 2 + polynomial;
            Polynomial expectedResult = new Polynomial(expectedCoefficients);

            Assert.Equal(expectedResult.Coefficients, actualResult.Coefficients);
        }

        [Fact]
        public void PolynomialPlusValueOperationTest()
        {
            double[] coefficients = { 1, 2, 3 };
            double[] expectedCoefficients = { 6, 2, 3 };

            Polynomial polynomial = new Polynomial(coefficients);
            Polynomial actualResult = polynomial + 5;
            Polynomial expectedResult = new Polynomial(expectedCoefficients);

            Assert.Equal(expectedResult.Coefficients, actualResult.Coefficients);
        }

        [Fact]
        public void ValueMinusPolynomialOperationTest()
        {
            double[] coefficients = { 1, 2, 3 };
            double[] expectedCoefficients = { 0, 2, 3 };

            Polynomial polynomial = new Polynomial(coefficients);
            Polynomial actualResult = 1 - polynomial;
            Polynomial expectedResult = new Polynomial(expectedCoefficients);

            Assert.Equal(expectedResult.Coefficients, actualResult.Coefficients);
        }

        [Fact]
        public void PolynomialMinusValueOperationTest()
        {
            double[] coefficients = { 1, 2, 3 };
            double[] expectedCoefficients = { -3, 2, 3 };

            Polynomial polynomial = new Polynomial(coefficients);
            Polynomial actualResult = polynomial - 4;
            Polynomial expectedResult = new Polynomial(expectedCoefficients);

            Assert.Equal(expectedResult.Coefficients, actualResult.Coefficients);
        }


        [Fact]
        public void PolynomialPlusPolynomialOperationTest()
        {
            double[] firstPolynomialCoefficients = { 1, 2, 3 };
            double[] secondPolynomialCoefficients = { 2, -1, 7 };            
            double[] expectedCoefficients = { 3, 1, 10 };

            Polynomial firstPolynomial = new Polynomial(firstPolynomialCoefficients);
            Polynomial secondPolynomial = new Polynomial(secondPolynomialCoefficients);  
            
            Polynomial actualResult = firstPolynomial + secondPolynomial;

            Polynomial expectedResult = new Polynomial(expectedCoefficients);

            Assert.Equal(expectedResult.Coefficients, actualResult.Coefficients);
        }

        [Fact]
        public void PolynomialMinusPolynomialOperationTest()
        {
            double[] firstPolynomialCoefficients = { 1, 2, 3 };
            double[] secondPolynomialCoefficients = { 3, -2, 1 };
            double[] expectedCoefficients = { -2, 4, 2 };

            Polynomial firstPolynomial = new Polynomial(firstPolynomialCoefficients);
            Polynomial secondPolynomial = new Polynomial(secondPolynomialCoefficients);

            Polynomial actualResult = firstPolynomial - secondPolynomial;

            Polynomial expectedResult = new Polynomial(expectedCoefficients);

            Assert.Equal(expectedResult.Coefficients, actualResult.Coefficients);
        }

    }
}
