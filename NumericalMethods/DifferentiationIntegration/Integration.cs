using System;
using System.Collections.Generic;
using System.Text;
using ReadFunction;

namespace NumericalMethods.DifferentiationIntegration
{
    public class Integration
    {
        private MathParser mathParser = new MathParser();

        private int n;
        private double x, zy1, zy2, t;
        private double[] fx;

        /// <summary>
        /// This method allows you to use simple integration methods like trapezoida, simpson 1/2 and simpson 3/8
        /// </summary>
        /// <param name="function">Target function</param>
        /// <param name="lower">Lower limit</param>
        /// <param name="upper">Upper limit</param>
        /// <param name="h">Step h, 0.001 by default</param>
        /// <param name="method">You can use "trapezoidal", "simpson12", or "simpson38" </param>
        /// <returns></returns>
        public double integrate(string function, double lower, double upper, double h = 0.001, string method = "trapezoidal")
        {
            method = method.ToLower();
            switch (method)
            {
                case "trapezoidal":
                    return trapezoidal(function, lower, upper, h);
                case "simpson12":
                    return simpson13(function, lower, upper, h);
                case "simpson38":
                    return simpson38(function, lower, upper, h);
                default:
                    break;
            }
            return -1;
        }

        private void initialize(string function, double a, double b, double h)
        {
            x = a; zy1 = 0; zy2 = 0; t = 1;
            n = Convert.ToInt32(((b - a) / h) + 1);
            fx = new double[n];

            for (int i = 0; i < n; i++)
            {
                fx[i] = mathParser.Eval_function(function, x);
                x += h;
            }
        }

        private void fill_zy(int start, int stop, int step, int zy)
        {
            for (int i = start; i < stop; i+=step)
            {
                if (zy == 1)
                    zy1 += fx[i];
                else if (zy == 2)
                    zy2 += fx[i];
            }          
        }

        private double trapezoidal(string function, double a, double b, double h)
        {
            initialize(function, a, b, h);

            fill_zy(1, n - 1, 1, 1);

            return (h / 2) * (fx[0] + (2 * zy1) + fx[n - 1]);             
        }
        
        private double simpson13(string function, double a, double b, double h)
        {
            initialize(function, a, b, h);

            fill_zy(2, n - 4, 2, 1);
            fill_zy(1, n - 3, 2, 2);

            return (h / 3) * (fx[0] + (2 * zy1) + (4 * zy2) + fx[n - 1]);            
        }

        private double simpson38(string function, double a, double b, double h)
        {
            initialize(function, a, b, h);

            fill_zy(3, n - 1, 3, 1);

            for (int i = 1; i <= n - 2; i++)
            {
                if (t < 3)
                {
                    zy2 += fx[i];
                    t++;
                }
                else
                {
                    t = 1;
                }
            }
           return ((3 * h) / 8) * (fx[0] + (2 * zy1) + (3 * zy2) + fx[n - 1]);            
        }
    }
}
