using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunctionEvaluatorLibrary.Dictionaries
{
    static class LocalMathFunctions
    {
        private static Dictionary<string, Func<double[], double>> functions =
            new Dictionary<string, Func<double[], double>>(18)
            {
                ["cos"] = inputs => Math.Cos(inputs[0]),
                ["cosh"] = inputs => Math.Cosh(inputs[0]),
                ["acos"] = inputs => Math.Acos(inputs[0]),
                ["arccos"] = inputs => Math.Acos(inputs[0]),

                ["sin"] = inputs => Math.Sin(inputs[0]),
                ["sinh"] = inputs => Math.Sinh(inputs[0]),
                ["asin"] = inputs => Math.Asin(inputs[0]),
                ["arcsin"] = inputs => Math.Asin(inputs[0]),

                ["tan"] = inputs => Math.Tan(inputs[0]),
                ["tanh"] = inputs => Math.Tanh(inputs[0]),
                ["atan"] = inputs => Math.Atan(inputs[0]),
                ["arctan"] = inputs => Math.Atan(inputs[0]),

                ["sqrt"] = inputs => Math.Sqrt(inputs[0]),
                ["pow"] = inputs => Math.Pow(inputs[0], inputs[1]),
                ["root"] = inputs => Math.Pow(inputs[0], 1 / inputs[1]),

                ["exp"] = inputs => Math.Exp(inputs[0]),


                ["log"] = inputs =>
                {
                    switch (inputs.Length)
                    {
                        case 1:
                            return Math.Log10(inputs[0]);
                        case 2:
                            return Math.Log(inputs[0], inputs[1]);
                        default:
                            return 0;
                    }
                },

                ["ln"] = inputs => Math.Log(inputs[0])
            };

        public static Dictionary<string, Func<double[], double>> getMathFunctions()
        {
            return functions;
        }

        public static bool containsFunction(string functionName)
        {
            return functions.Keys.Contains(functionName);
        }

        public static Func<double[], double> getFunction(string functionName)
        {
            return functions[functionName];
        }
    }
}
