using System;
using System.Collections.Generic;
using System.Text;
using ReadFunction;

namespace NumericalMethods.RootFinding
{
    public class Secant
    {
        private double[] lastResult = new double[3];
        /// <summary>
        /// This value save the last result [0] x, [1] fx, [2] error
        /// </summary>
        public double[] LastResult { get => lastResult;}

        /// <summary>
        /// This class allows you to use the secant method for root finding
        /// </summary>
        private MathParser mathParser = new MathParser();        

        /// <summary>
        /// (Open Method) This method calculates the root of a f(x) function using Modified Secant method.
        /// </summary>
        /// <param name="fx">This is the function wich you want to find the root</param>
        /// <param name="x0">This is the initial value</param>      
        /// <param name="h">This is a small pertubation fraction that its really used to calculate numerical differentiation</param>
        /// <param name="maxIte">Maximum number of iterations, default = 100</param>
        /// <param name="tolerance">This is the tolerance you want to use, default = 1e-6</param>
        public double FindRoot(string fx, double x0, double x1, int maxIte = 100, double tolerance = 1e-6)
        {
            double error = 1, xr = 0, fx0 = 0, fx1 = 0, temp = 0;

            for (int i = 0; i < maxIte; i++)
            {
                fx0 = mathParser.Eval_function(fx, x0);
                fx1 = mathParser.Eval_function(fx, x1);
                temp = xr;

                xr = x1 - (x1 - x0) / (fx1 - fx0) * fx1;

                if (xr != 0)
                    error = Math.Abs((xr - temp) / xr);
                if (fx1 == 0) { break; }
                if (error < tolerance) { break; }
                if (xr == temp) { break; }

                x0 = x1; x1 = xr;
            }
            lastResult[0] = xr;
            lastResult[1] = fx1;
            lastResult[2] = error;
            return xr;
        }
        /// <summary>
        /// (Open Method) This method calculates the root of a f(x) function using Modified Secant method.        
        /// </summary>
        /// <param name="fx">This is the function wich you want to find the root</param>
        /// <param name="x0">This is the initial value, please don't use 0 as initial value it will not vonverge</param>        
        /// <param name="h">This value is used to calculate numerical derivative if you don't want to use it instead of symbolic derivative, default = 0.001. The algorith uses a centered finite-divided difference formula with O(h^4) of error. (high-accurate)</param>        
        /// <param name="maxIte">Maximum number of iterations, default = 100</param>
        /// <param name="tolerance">This is the tolerance you want to use, default = 1e-6</param>
        /// <param name="log">This is the list that will contain all the iterations info</param>
        public double FindRoot(string fx, double x0, double x1,out List<string> log, int maxIte = 100, double tolerance = 1e-6)
        {
            double error = 1, xr = 0, fx0 = 0, fx1 = 0, temp = 0;
            log = new List<string>();
            log.Add("Iteration,xr,f(xr),error");
            for (int i = 0; i < maxIte; i++)
            {
                fx0 = mathParser.Eval_function(fx, x0);
                fx1 = mathParser.Eval_function(fx, x1);
                temp = xr;

                xr = x1 - (x1 - x0) / (fx1 - fx0) * fx1;

                if (xr != 0)
                    error = Math.Abs((xr - temp) / xr);
                log.Add(string.Format($"{i + 1},{x1},{fx1},{error}"));
                if (fx1 == 0) { log.Add("Exact root found in the last iteration"); break; }
                if (error < tolerance) { log.Add(string.Format("Root found in the iteration #{0} with {1}% of tolerance", i + 1, tolerance * 100)); break; }
                if (xr == temp) { log.Add("Infinite loop"); break; }

                x0 = x1; x1 = xr;
            }
            lastResult[0] = x1;
            lastResult[1] = fx1;
            lastResult[2] = error;
            return xr;           
        }
    }
}
