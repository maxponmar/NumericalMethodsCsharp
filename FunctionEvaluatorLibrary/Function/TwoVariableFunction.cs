using FunctionEvaluatorLibrary.Function.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionEvaluatorLibrary.Function
{
    class TwoVariableFunction : Function<Point>
    {
        public TwoVariableFunction(string function): base(function)
        {

        }
        public override double Evaluate(Point point)
        {
            return FunctionEvaluator.Evaluate(function, point.getX(), point.getY());
        }
    }
}
