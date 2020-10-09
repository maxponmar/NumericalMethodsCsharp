using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using FunctionEvaluatorLibrary.Dictionaries;

namespace FunctionEvaluatorLibrary
{
    static class FunctionEvaluator
    {
        private static Dictionary<string, Func<double, double, double>> Operators =
            LocalOperators.getOperators();

        private static Dictionary<string, Func<double[], double>> Functions =
            LocalMathFunctions.getMathFunctions();

        private static Dictionary<string, double> Variables =
            LocalVariables.getVariables();

        /// <summary>
        /// When converting the result from the Parse method or ProgrammaticallyParse method ToString(),
        /// please use this culture info.
        /// </summary>
        private static CultureInfo CultureInfo = CultureInfo.InvariantCulture;

        /// <summary>
        /// Evaluate a f(x) function        
        /// </summary>
        /// <param name="function">f(x) function to evaluate</param>
        public static double Evaluate(string function, double value)
        {
            double response = 0;
            Variables["x"] = value;
            List<string> tokenizedFunction = tokenizeFunction(function);
            response = Calculate(tokenizedFunction);
            return response;
        }

        /// <summary>
        /// Evaluate a f(x,y) function        
        /// </summary>
        /// <param name="function">A function of x and y => f(x,y)</param>
        public static double Evaluate(string function, double xValue, double yValue)
        {
            double response = 0;
            Variables["x"] = xValue;
            Variables["y"] = yValue;
            List<string> tokenizedFunction = tokenizeFunction(function);
            response = Calculate(tokenizedFunction);
            return response;
        }

        private static List<string> tokenizeFunction(string expression)
        {
            string token = "";
            List<string> tokens = new List<string>();

            solveSignRules(ref expression);

            for (int i = 0; i < expression.Length; i++)
            {
                var character = expression[i];

                if (char.IsWhiteSpace(character))
                {
                    continue;
                }

                if (char.IsLetter(character))
                {
                    if (i != 0 && (char.IsDigit(expression[i - 1]) || expression[i - 1] == ')'))
                        tokens.Add("*");

                    token += character;

                    while (i + 1 < expression.Length && char.IsLetterOrDigit(expression[i + 1]))
                        token += expression[++i];

                    tokens.Add(token);
                    token = "";

                    continue;
                }

                if (char.IsDigit(character))
                {
                    token += character;

                    while (i + 1 < expression.Length && (char.IsDigit(expression[i + 1]) || expression[i + 1] == '.'))
                        token += expression[++i];

                    tokens.Add(token);
                    token = "";

                    continue;
                }

                if (character == '.')
                {
                    token += character;

                    while (i + 1 < expression.Length && char.IsDigit(expression[i + 1]))
                        token += expression[++i];

                    tokens.Add(token);
                    token = "";

                    continue;
                }

                if (isSigned(expression, character, i))
                {
                    token += character;

                    while (i + 1 < expression.Length && (char.IsDigit(expression[i + 1]) || expression[i + 1] == '.'))
                        token += expression[++i];

                    tokens.Add(token);
                    token = "";

                    continue;
                }

                if (character == '(')
                {
                    if (i != 0 && (char.IsDigit(expression[i - 1]) || char.IsDigit(expression[i - 1]) || expression[i - 1] == ')'))
                    {
                        tokens.Add("*");
                        tokens.Add("(");
                    }
                    else
                        tokens.Add("(");
                }
                else
                    tokens.Add(character.ToString());
            }

            return tokens;
        }

        private static void solveSignRules(ref string expression)
        {
            expression = expression.Replace("+-", "-");
            expression = expression.Replace("-+", "-");
            expression = expression.Replace("--", "+");
        }

        private static bool isSigned(string expression, char character, int index)
        {
            // if is true, then the token for that negative number will be "-1", not "-","1".
            // to sum up, the above will be true if the minus sign is in front of the number, but
            // at the beginning, for example, -1+2, or, when it is inside the brakets (-1).
            // NOTE: this works for + as well!

            bool isNotLastDigit = index + 1 < expression.Length;
            bool isSign = character == '-' || character == '+';
            bool isNumber = char.IsDigit(expression[index + 1]);
            bool isFirstDigit = index == 0 || Operators.ContainsKey(expression[index - 1].ToString(CultureInfo.InvariantCulture)) || index - 1 > 0 && expression[index - 1] == '(';
            return isNotLastDigit && isSign && isNumber && isFirstDigit;
        }

        private static double Calculate(List<string> tokens)
        {
            // Variables replacement
            for (var i = 0; i < tokens.Count; i++)
            {
                if (Variables.Keys.Contains(tokens[i]))
                    tokens[i] = Variables[tokens[i]].ToString(CultureInfo);
            }

            while (tokens.IndexOf("(") != -1)
            {
                // getting data between "(" and ")"
                var open = tokens.LastIndexOf("(");
                var close = tokens.IndexOf(")", open); // in case open is -1, i.e. no "(" // , open == 0 ? 0 : open - 1

                if (open >= close)
                    throw new ArithmeticException("No closing bracket/parenthesis. Token: " + open.ToString(CultureInfo));

                var roughExpr = new List<string>();

                for (var i = open + 1; i < close; i++)
                    roughExpr.Add(tokens[i]);

                double tmpResult;

                var args = new List<double>();
                var functionName = tokens[open == 0 ? 0 : open - 1];

                if (Functions.Keys.Contains(functionName))
                {
                    if (roughExpr.Contains(","))
                    {
                        // converting all arguments into a decimal array
                        for (var i = 0; i < roughExpr.Count; i++)
                        {
                            var defaultExpr = new List<string>();
                            var firstCommaOrEndOfExpression =
                                roughExpr.IndexOf(",", i) != -1
                                    ? roughExpr.IndexOf(",", i)
                                    : roughExpr.Count;

                            while (i < firstCommaOrEndOfExpression)
                                defaultExpr.Add(roughExpr[i++]);

                            args.Add(defaultExpr.Count == 0 ? 0 : BasicArithmeticalExpression(defaultExpr));
                        }

                        // finally, passing the arguments to the given function
                        tmpResult = double.Parse(Functions[functionName](args.ToArray()).ToString(CultureInfo), CultureInfo);
                    }
                    else
                    {
                        // but if we only have one argument, then we pass it directly to the function
                        tmpResult = double.Parse(Functions[functionName](new[]
                        {
                            BasicArithmeticalExpression(roughExpr)
                        }).ToString(CultureInfo), CultureInfo);
                    }
                }
                else
                {
                    // if no function is need to execute following expression, pass it
                    // to the "BasicArithmeticalExpression" method.
                    tmpResult = BasicArithmeticalExpression(roughExpr);
                }

                // when all the calculations have been done
                // we replace the "opening bracket with the result"
                // and removing the rest.
                tokens[open] = tmpResult.ToString(CultureInfo);
                tokens.RemoveRange(open + 1, close - open);

                if (Functions.Keys.Contains(functionName))
                {
                    // if we also executed a function, removing
                    // the function name as well.
                    tokens.RemoveAt(open - 1);
                }
            }

            // at this point, we should have replaced all brackets
            // with the appropriate values, so we can simply
            // calculate the expression. it's not so complex
            // any more!
            return BasicArithmeticalExpression(tokens);
        }

        private static double BasicArithmeticalExpression(List<string> tokens)
        {
            // PERFORMING A BASIC ARITHMETICAL EXPRESSION CALCULATION
            // THIS METHOD CAN ONLY OPERATE WITH NUMBERS AND OPERATORS
            // AND WILL NOT UNDERSTAND ANYTHING BEYOND THAT.

            switch (tokens.Count)
            {
                case 1:
                    return double.Parse(tokens[0], CultureInfo);
                case 2:
                    var op = tokens[0];

                    if (op == "-" || op == "+")
                    {
                        var first = op == "+" ? "" : (tokens[1].Substring(0, 1) == "-" ? "" : "-");

                        return double.Parse(first + tokens[1], CultureInfo);
                    }

                    return Operators[op](0, double.Parse(tokens[1], CultureInfo));
                case 0:
                    return 0;
            }

            foreach (var op in Operators)
            {
                int opPlace;

                while ((opPlace = tokens.IndexOf(op.Key)) != -1)
                {
                    var rhs = double.Parse(tokens[opPlace + 1], CultureInfo);

                    if (op.Key == "-" && opPlace == 0)
                    {
                        var result = op.Value(0.0, rhs);
                        tokens[0] = result.ToString(CultureInfo);
                        tokens.RemoveRange(opPlace + 1, 1);
                    }
                    else
                    {
                        var result = op.Value(double.Parse(tokens[opPlace - 1], CultureInfo), rhs);
                        tokens[opPlace - 1] = result.ToString(CultureInfo);
                        tokens.RemoveRange(opPlace, 2);
                    }
                }
            }

            return double.Parse(tokens[0], CultureInfo);
        }
    }
}