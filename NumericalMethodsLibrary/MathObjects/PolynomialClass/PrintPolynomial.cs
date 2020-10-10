using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethodsLibrary.MathObjects.PolynomialClass
{
    public partial class Polynomial
    {
        public override string ToString()
        {
            if (coefficients.Count == 0)
            {
                return "Empty polynomial";
            }
            else
            {
                buildStringPolynomial();
            }

            return polynomialStringBuilder.ToString();
        }

        private void buildStringPolynomial()
        {
            double coefficient = 0;
            int? emptyCoefficientsInARow = 0;

            for (int index = 0; index < degree + 1; index++)
            {
                coefficient = coefficients[index];
                if (coefficient != 0)
                {
                    switch (index)
                    {
                        case 0:
                            appendFirstCoefficient(coefficient);
                            break;
                        case 1:
                            appendX(emptyCoefficientsInARow, index, coefficient);
                            break;
                        default:
                            appendMonomial(emptyCoefficientsInARow, index, coefficient);
                            break;
                    }
                    emptyCoefficientsInARow = null;
                }
                else
                {
                    emptyCoefficientsInARow++;
                }
            }
        }

        private void appendFirstCoefficient(double coefficient)
        {
            bool isNegative = coefficient < 0;
            if (isNegative)
            {
                polynomialStringBuilder.Append($"-{-coefficient}");
            }
            else
            {
                polynomialStringBuilder.Append($"{coefficient}");
            }
        }

        private void appendMonomial(int? emptyCoefficientsInARow, int index, double coefficient)
        {
            if (emptyCoefficientsInARow > 0)
            {
                appendFirstCoefficient(coefficient);
            }
            else
            {
                appendX(emptyCoefficientsInARow, index, coefficient);
                appendDegree(index);
            }
        }

        private void appendX(int? emptyCoefficientsInARow, int index, double coefficient)
        {
            double coefficientWithoutSign = Math.Abs(coefficient);
            bool isNegative = coefficient < 0;

            if (emptyCoefficientsInARow > 0)
            {
                if (coefficientWithoutSign == 1)
                {
                    if (isNegative)
                    {
                        polynomialStringBuilder.Append("-x");
                    }
                    else
                    {
                        polynomialStringBuilder.Append("x");
                    }
                }
                else
                {
                    appendCoefficient(coefficient);
                }
            }
            else
            {
                if (coefficientWithoutSign == 1)
                {
                    if (isNegative)
                    {
                        polynomialStringBuilder.Append(" - x");
                    }
                    else
                    {
                        polynomialStringBuilder.Append(" + x");
                    }
                }
                else
                {
                    appendCoefficient(coefficient);
                }
            }

        }

        private void appendDegree(double degree)
        {
            polynomialStringBuilder.Append($"^{degree}");
        }

        private void appendCoefficient(double coefficient)
        {
            if (coefficient == 1)
            {
                polynomialStringBuilder.Append($" + ");
            }
            else
            {
                bool isNegative = coefficient < 0;
                if (isNegative)
                {
                    polynomialStringBuilder.Append($" - {-coefficient}");
                }
                else
                {
                    polynomialStringBuilder.Append($" + {coefficient}");
                }
            }
            polynomialStringBuilder.Append("x");
        }
    }
}
