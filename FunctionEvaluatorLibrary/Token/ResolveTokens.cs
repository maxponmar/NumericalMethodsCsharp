using FunctionEvaluatorLibrary.Dictionaries;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FunctionEvaluatorLibrary.Token
{
    static class ResolveTokens
    {
        private static int openParenthesesIndex;
        private static int closeParenthesesIndex;

        private static Tokens tokensToResolve;

        // Complex expressions are those that use parenthesis like sin(x)
        private static string complexExpressionName;
        private static double resultOfComplexExpressions;        
        private static Tokens complexExpressions;

        private static List<double> argumentsInsideParenthesis;

        public static double Resolve(Tokens tokens)
        {
            tokensToResolve = tokens;

            substituteVariables();

            convertComplexExpressionsToBasicOnes();

            return basicArithmeticalExpressions(tokensToResolve);
        }

        private static void substituteVariables()
        {            
            for (var i = 0; i < tokensToResolve.Count(); i++)
            {
                if (LocalVariables.containsVariable(tokensToResolve[i]))
                {
                    tokensToResolve[i] = LocalVariables.getVariable(tokensToResolve[i]).ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        private static void convertComplexExpressionsToBasicOnes()
        {
            while (tokensToResolve.indexOf("(") != -1)
            {
                openParenthesesIndex = tokensToResolve.lastIndexOf("(");
                closeParenthesesIndex = tokensToResolve.indexOf(")", openParenthesesIndex);

                parenthesesMatch(openParenthesesIndex, closeParenthesesIndex);
                
                resolveComplexExpressions();

                tokensToResolve[openParenthesesIndex] = resultOfComplexExpressions.ToString(CultureInfo.InvariantCulture);
                tokensToResolve.removeRange(openParenthesesIndex + 1, closeParenthesesIndex - openParenthesesIndex);

                if (LocalMathFunctions.containsFunction(complexExpressionName))
                {
                    tokensToResolve.removeAt(openParenthesesIndex - 1);
                }
            }
        }

        private static void parenthesesMatch(int openParenthesesIndex, int closeParenthesesIndex)
        {
            if (openParenthesesIndex >= closeParenthesesIndex)
            {
                throw new ArithmeticException(
                    string.Format($"No closing bracket/parenthesis. Token: ", openParenthesesIndex.ToString(CultureInfo.InvariantCulture)
                    ));
            }
        }

        private static void resolveComplexExpressions()
        {
            complexExpressions = new Tokens();

            for (int i = openParenthesesIndex + 1; i < closeParenthesesIndex; i++)
            {
                complexExpressions.addToken(tokensToResolve[i]);
            }
            
            argumentsInsideParenthesis = new List<double>();
            complexExpressionName = tokensToResolve[openParenthesesIndex == 0 ? 0 : openParenthesesIndex - 1];

            if (LocalMathFunctions.containsFunction(complexExpressionName))
            {                
                if (complexExpressions.contains(","))
                {
                    resolveTwoArgumentsExpression();
                }
                else                
                {
                    resolveOneArgumentExpression();
                }
            }
            else            
            {                
                resultOfComplexExpressions = basicArithmeticalExpressions(complexExpressions);
            }
        }

        private static void resolveTwoArgumentsExpression()
        {
            for (int i = 0; i < complexExpressions.Count(); i++)
            {
                Tokens defaultExpression = new Tokens();
                int firstCommaOrEndOfExpression =
                    complexExpressions.indexOf(",", i) != -1
                        ? complexExpressions.indexOf(",", i)
                        : complexExpressions.Count();

                while (i < firstCommaOrEndOfExpression)
                {
                    defaultExpression.addToken(complexExpressions[i++]);
                }

                argumentsInsideParenthesis.Add(defaultExpression.Count() == 0 ? 0 : basicArithmeticalExpressions(defaultExpression));
            }

            Func<double[], double> mathFunction = LocalMathFunctions.getFunction(complexExpressionName);
            resultOfComplexExpressions = double.Parse(mathFunction(argumentsInsideParenthesis.ToArray()).ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        }

        private static void resolveOneArgumentExpression()
        {
            resultOfComplexExpressions = double.Parse(LocalMathFunctions.getFunction(complexExpressionName)(new[]
            {
                            basicArithmeticalExpressions(complexExpressions)
                        }).ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        }

        private static double basicArithmeticalExpressions(Tokens tokensToResolve)
        {
            switch (tokensToResolve.Count())
            {
                case 1:
                    return double.Parse(tokensToResolve[0], CultureInfo.InvariantCulture);
                case 2:
                    var op = tokensToResolve[0];

                    if (op == "-" || op == "+")
                    {
                        var first = op == "+" ? "" : (tokensToResolve[1].Substring(0, 1) == "-" ? "" : "-");

                        return double.Parse(first + tokensToResolve[1], CultureInfo.InvariantCulture);
                    }

                    return LocalOperators.getOperator(op)(0, double.Parse(tokensToResolve[1], CultureInfo.InvariantCulture));
                case 0:
                    return 0;
            }

            foreach (var op in LocalOperators.getOperators())
            {
                int opPlace;

                while ((opPlace = tokensToResolve.indexOf(op.Key)) != -1)
                {
                    var rhs = double.Parse(tokensToResolve[opPlace + 1], CultureInfo.InvariantCulture);

                    if (op.Key == "-" && opPlace == 0)
                    {
                        var result = op.Value(0.0, rhs);
                        tokensToResolve[0] = result.ToString(CultureInfo.InvariantCulture);
                        tokensToResolve.removeRange(opPlace + 1, 1);
                    }
                    else
                    {
                        var result = op.Value(double.Parse(tokensToResolve[opPlace - 1], CultureInfo.InvariantCulture), rhs);
                        tokensToResolve[opPlace - 1] = result.ToString(CultureInfo.InvariantCulture);
                        tokensToResolve.removeRange(opPlace, 2);
                    }
                }
            }

            return double.Parse(tokensToResolve[0], CultureInfo.InvariantCulture);
        }
    }
}
