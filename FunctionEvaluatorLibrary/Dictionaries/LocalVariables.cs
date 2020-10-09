using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionEvaluatorLibrary.Dictionaries
{
    static class LocalVariables
    {
        public static Dictionary<string, double> getVariables()
        {
            return new Dictionary<string, double>(6)
            {
                ["pi"] = 3.14159265358979,
                ["tao"] = 6.28318530717959,
                ["e"] = 2.71828182845905,
                ["phi"] = 1.61803398874989,
                ["x"] = 0.0,
                ["y"] = 0.0
            };
        }
    }
}
