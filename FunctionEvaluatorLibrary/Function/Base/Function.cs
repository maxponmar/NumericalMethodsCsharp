using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionEvaluatorLibrary.Function.Base
{
    abstract class Function<T>
    {
        protected string function;        

        public Function(string function)
        {
            this.function = function;
        }

        public abstract double Evaluate(T variables);
    }
}
