using System;
using System.Collections.Generic;
using ReadFunction;

namespace NumericalMethods.RootFinding
{   
    // DONE 
    /// <summary>
    /// This class allows you to use the bisection method for root finding
    /// </summary>
    public class Bisection
    {
        private double lastResult;
        /// <summary>
        /// This value save the last result
        /// </summary>
        public double LastResult { get => lastResult; }

        private MathParser mathParser = new MathParser();
        /// <summary>
        /// (Bracketing Method) This method calculate the root of the given function between xl and xu values using the bisection method, it return the result as dobule
        /// </summary>
        /// <param name="function">This is the function wich you want to find the root</param>
        /// <param name="xl">This is the lower bound of the root</param>
        /// <param name="xu">This is the upper bound of the root</param>
        /// <param name="maxIte">Maximum number of iterations, default = 100</param>
        /// <param name="tolerance">This is the tolerance you want to use, default = 0.01 (1%)</param>
        public double FindRoot(string function, double xl, double xu, int maxIte = 100, double tolerance = 0.01)
        {
            double xr = 0, fxr, temp, error = 1;
            for (int i = 0; i < maxIte; i++)
            {
                temp = xr;
                xr = (xl + xu) / 2.0;
                fxr = mathParser.Eval_function(function, xr);
                if (xr != 0)
                    error = Math.Abs((xr - temp) / xr);
                if (fxr > 0)
                    xu = xr;
                if (fxr < 0)
                    xl = xr;
                if (fxr == 0) { break; }
                if (error < tolerance) { break; }
                if (xr == temp) { break; }
            }
            lastResult = xr;
            return xr;
        }
        /// <summary>
        /// This method calculates the root of the given function between xl and xu values using bisection method, it return the result as dobule and a string List with the information of all iterations and it could be converted to a .csv file with list2csb function
        /// </summary>
        /// <param name="function">This is the function wich you want to find the root</param>
        /// <param name="xl">This is the lower bound of the root</param>
        /// <param name="xu">This is the upper bound of the root</param>
        /// <param name="maxIte">Maximum number of iterations, default = 100</param>
        /// <param name="tolerance">This is the tolerance you want to use, default = 0.01 (1%)</param>
        /// <param name="log">This is the list that will contain all the iterations info</param>
        public double FindRoot(string function, double xl, double xu, out List<string> log, int maxIte = 100, double tolerance = 0.01)
        {
            log = new List<string>();
            log.Add("Iteration,xl,xu,xr,f(xr),error");
            double xr = 0, fxr, temp, error = 1;
            for (int i = 0; i < maxIte; i++)
            {
                temp = xr;
                xr = (xl + xu) / 2.0;
                fxr = mathParser.Eval_function(function, xr);
                if (xr != 0)
                    error = Math.Abs((xr - temp) / xr);
                log.Add(string.Format($"{i + 1},{xl},{xu},{xr},{fxr},{error}"));
                if (fxr > 0)
                    xu = xr;
                if (fxr < 0)
                    xl = xr;
                if (fxr == 0) { log.Add("Exact root found in the last iteration"); break; }
                if (error < tolerance) { log.Add(string.Format("Root found in the iteration #{0} with {1}% of tolerance", i + 1, tolerance * 100)); break; }
                if (xr == temp) { log.Add("Infinite loop"); break; }
            }
            lastResult = xr;
            return xr;
        }
    }
}
