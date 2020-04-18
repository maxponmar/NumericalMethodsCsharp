using System;
using System.Collections.Generic;
using System.Text;
using ReadFunction;

namespace NumericalMethods.DifferentiationIntegration
{
    public class Differentiation
    {
        private MathParser mathParser = new MathParser();
        /// <summary>
        /// This method calculate the derivative of the fiven function at point x, you can specify the step, grade, the method and the type
        /// If you want the first derivative you give gradue = 1 (default) or 2 if you want the second derivative (limited to fourth derivative)
        /// You can choose the "fordward" (default), "backward", or the "centered" method.
        /// Finaly you can use "simple" (default) or "improved" type, the difference is that the "improved" version incorporate more terms of the Taylor series expansion.
        /// </summary>
        /// <param name="function">Your function f(x) as string</param>
        /// <param name="x">Point where the derivate will be calculated</param>
        /// <param name="h">Step (0.001 by default)</param>
        /// <param name="grade">You can choose the 1st to 4th derivative</param>
        /// <param name="method">"forward" (default), "backward", or "centered"</param>
        /// <param name="type">"simple" or "improved"</param>
        /// <returns></returns>
        public double derivative(string function, double x, double h = 0.001, int grade = 1, string method = "forward" , string type = "simple")
        {
            switch (grade)
            {
                case 1:
                    return firstDerivative(function ,x, h, method, type);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    return -1;                    
            }
            return 1.1;
        }
        private double firstDerivative(string function, double x, double h, string method, string type)
        {
            switch (method)
            {
                case "forward":
                    return calc_derivative(function, x, h, 0, 2, new double[] { -1, 1 }, h);
                    break;

                case "backward":
                    break;

                case "center":
                    break;

                default:
                    return -1;                    
            }
            return -1;
        }
        // === In order to avoid repetition, this function can calculate directly any kind of derivative given the correct parameters
        // === To understand how it works you need to look at the book or other resource and found how a deriviative is calculated using Taylor series
        // === For example, for the first derivative using forward method and the improvec version you have the next formula:

        //     -f(x+2h) + 4*f(x+h) - 3*(fx)
        //      --------------------------
        //                 2*h

        // So to calculate the derivative using the formula above, you can get the f(x+nh) values giving where n starts, in this example
        // n start at 0 (start=0) and needs 3 evaluations (steps=3), the function will return a double array like this:

        //      [ f(x+0h), f(x+1h), f(x+2h) ]

        // Then you need to specify the coefficients that multiply every element, in this case you need to give a double array like this:

        //      [ -3, 4, -1]

        // So -3 will multiply  f(x+0h), 4  f(x+1h), and -1  f(x+2h), all these elements will be added    -3*f(x+0h) + 4*f(x+1h) + -1*f(x+2h) 
        // == The function actually do all these steps in the same line 

        // And finally you need to give the value that will divde the result of the operation above, in this case it is 2*h (denominator) so that the function will return:

        //  -3*f(x+0h) + 4*f(x+1h) + -1*f(x+2h) / 2*h

        // ==== Remember that there are some formular that need to start from -3 for example, so the first calculated is f(x-3h) so be carefull when you give
        // === the array that will multiply these values.
        private double calc_derivative(string function, double x, double h, int start,int steps, double[] multipliers, double denominator)
        {
            double numerator = 0;         
            for (int i = start, c = 0; i < steps; i++, c++)            
                numerator += multipliers[c] * mathParser.Eval_function(function, x + i * h);
            return numerator / denominator;
        }       
    }
}
