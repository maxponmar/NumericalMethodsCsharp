using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionEvaluatorLibrary.Token
{
    static class Tokenizer
    {
        private static Tokens tokens = new Tokens();

        public static Tokens tokenize(string expression)
        {
            tokens = new Tokens();
            string token = "";

            SignedDigit.solveSignRules(ref expression);

            for (int i = 0; i < expression.Length; i++)
            {
                char character = expression[i];

                if (char.IsWhiteSpace(character))
                {
                    continue;
                }

                if (char.IsLetter(character))
                {
                    if (i != 0 && (char.IsDigit(expression[i - 1]) || expression[i - 1] == ')'))
                        tokens.addToken("*");

                    token += character;

                    while (i + 1 < expression.Length && char.IsLetterOrDigit(expression[i + 1]))
                        token += expression[++i];

                    tokens.addToken(token);
                    token = "";

                    continue;
                }

                if (char.IsDigit(character))
                {
                    token += character;

                    while (i + 1 < expression.Length && (char.IsDigit(expression[i + 1]) || expression[i + 1] == '.'))
                        token += expression[++i];

                    tokens.addToken(token);
                    token = "";

                    continue;
                }

                if (character == '.')
                {
                    token += character;

                    while (i + 1 < expression.Length && char.IsDigit(expression[i + 1]))
                        token += expression[++i];

                    tokens.addToken(token);
                    token = "";

                    continue;
                }

                if (SignedDigit.isSigned(expression, character, i))
                {
                    token += character;

                    while (i + 1 < expression.Length && (char.IsDigit(expression[i + 1]) || expression[i + 1] == '.'))
                        token += expression[++i];

                    tokens.addToken(token);
                    token = "";

                    continue;
                }

                if (character == '(')
                {
                    if (i != 0 && (char.IsDigit(expression[i - 1]) || char.IsDigit(expression[i - 1]) || expression[i - 1] == ')'))
                    {
                        tokens.addToken("*");
                        tokens.addToken("(");
                    }
                    else
                        tokens.addToken("(");
                }
                else
                    tokens.addToken(character.ToString());
            }

            return tokens;
        }
    }
}
