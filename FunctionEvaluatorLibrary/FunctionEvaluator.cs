using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using FunctionEvaluatorLibrary.Dictionaries;
using FunctionEvaluatorLibrary.Token;

namespace FunctionEvaluatorLibrary
{
    static class FunctionEvaluator
    {
        
        /// <summary>
        /// Evaluate a f(x) function        
        /// </summary>
        /// <param name="function">f(x) function to evaluate</param>
        public static double Evaluate(string function, double value)
        {
            double response = 0;

            LocalVariables.setVariable("x", value);

            Tokens tokenizedFunction = Tokenizer.tokenize(function);

            response = ResolveTokens.Resolve(tokenizedFunction);

            return response;
        }

        /// <summary>
        /// Evaluate a f(x,y) function        
        /// </summary>
        /// <param name="function">A function of x and y => f(x,y)</param>
        public static double Evaluate(string function, double xValue, double yValue)
        {
            double response = 0;

            LocalVariables.setVariable("x", xValue);
            LocalVariables.setVariable("y", yValue);

            Tokens tokenizedFunction = Tokenizer.tokenize(function);

            response = ResolveTokens.Resolve(tokenizedFunction);

            return response;
        }                       
    }
}