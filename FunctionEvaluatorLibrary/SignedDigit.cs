using FunctionEvaluatorLibrary.Dictionaries;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FunctionEvaluatorLibrary
{
    static class SignedDigit
    {
        public static void solveSignRules(ref string expression)
        {
            expression = expression.Replace("+-", "-");
            expression = expression.Replace("-+", "-");
            expression = expression.Replace("--", "+");
        }

        public static bool isSigned(string expression, char character, int index)
        {
            // if is true, then the token for that negative number will be "-1", not "-","1".
            // to sum up, the above will be true if the minus sign is in front of the number, but
            // at the beginning, for example, -1+2, or, when it is inside the brakets (-1).
            // NOTE: this works for + as well!

            bool isNotLastDigit = index + 1 < expression.Length;
            bool isSign = character == '-' || character == '+';
            bool isNumber = char.IsDigit(expression[index + 1]);
            bool isFirstDigit = index == 0 || LocalOperators.containsOperator(expression[index - 1].ToString(CultureInfo.InvariantCulture)) || index - 1 > 0 && expression[index - 1] == '(';
            return isNotLastDigit && isSign && isNumber && isFirstDigit;
        }
    }
}
