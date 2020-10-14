using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethodsLibrary.MathObjects.MatrixClass.Exceptions
{
    class MatricesDoesNotMatchInSize : Exception
    {        
        public MatricesDoesNotMatchInSize(string message) : base(message)
        {

        }
    }
}
