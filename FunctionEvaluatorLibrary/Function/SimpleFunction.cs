using FunctionEvaluatorLibrary.Function.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionEvaluatorLibrary.Function
{
    public class SimpleFunction : Function<double>
    {
        public SimpleFunction(string function): base(function)
        {

        }

        public override double Evaluate(double xValue)
        {
            return FunctionEvaluator.Evaluate(function, xValue);            
        }
    }
}
