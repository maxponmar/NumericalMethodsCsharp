using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionEvaluatorLibrary.Dictionaries
{
    static class LocalOperators
    {
        public static Dictionary<string, Func<double, double, double>> getOperators()
        {
            return new Dictionary<string, Func<double, double, double>>(5)
            {
                ["^"] = Math.Pow,
                ["/"] = (a, b) => a / b,
                ["*"] = (a, b) => a * b,
                ["-"] = (a, b) => a - b,
                ["+"] = (a, b) => a + b,
            };
        }                    
    }
}
