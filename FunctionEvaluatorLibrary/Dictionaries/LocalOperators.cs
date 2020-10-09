using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FunctionEvaluatorLibrary.Dictionaries
{
    static class LocalOperators
    {
        private static Dictionary<string, Func<double, double, double>> operators =
            new Dictionary<string, Func<double, double, double>>(5)
            {
                ["^"] = Math.Pow,
                ["/"] = (a, b) => a / b,
                ["*"] = (a, b) => a * b,
                ["-"] = (a, b) => a - b,
                ["+"] = (a, b) => a + b,
            };

        public static Dictionary<string, Func<double, double, double>> getOperators()
        {
            return operators;
        }              
        
        public static bool containsOperator(string operatorName)
        {
            return operators.ContainsKey(operatorName);
        }

        public static Func<double, double, double> getOperator(string operatorName)
        {
            return operators[operatorName];
        }
    }
}
