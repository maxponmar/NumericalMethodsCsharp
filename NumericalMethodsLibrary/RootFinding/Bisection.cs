using FunctionEvaluatorLibrary.Function;
using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethodsLibrary.RootFinding
{
    public class Bisection
    {
        private double[] lastResult = new double[3];
        /// <summary>
        /// This value save the last result [0] x, [1] fx, [2] error
        /// </summary>
        public double[] LastResult { get => lastResult; }

        private SimpleFunction function;

        /// <summary>
        /// (Bracketing Method) This method calculate the root of the given function between xl and xu values using the bisection method, it return the result as dobule
        /// </summary>
        /// <param name="function">This is the function wich you want to find the root</param>
        /// <param name="xl">This is the lower bound of the root</param>
        /// <param name="xu">This is the upper bound of the root</param>
        /// <param name="maxIte">Maximum number of iterations, default = 100</param>
        /// <param name="tolerance">This is the tolerance you want to use, default = 1e-6</param>
        public double FindRoot(string function, double xl, double xu, int maxIte = 100, double tolerance = 1e-6)
        {
            this.function = new SimpleFunction(function);

            double xr = 0, fxr = 0, temp, error = 1;
            for (int i = 0; i < maxIte; i++)
            {
                temp = xr;
                xr = (xl + xu) / 2.0;
                fxr = this.function.Evaluate(xr);
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
            lastResult[0] = xr;
            lastResult[1] = fxr;
            lastResult[2] = error;
            return xr;
        }

        /// <summary>
        /// This method calculates the root of the given function between xl and xu values using bisection method, it return the result as dobule and a string List with the information of all iterations and it could be converted to a .csv file with list2csb function
        /// </summary>
        /// <param name="function">This is the function wich you want to find the root</param>
        /// <param name="xl">This is the lower bound of the root</param>
        /// <param name="xu">This is the upper bound of the root</param>
        /// <param name="maxIte">Maximum number of iterations, default = 100</param>
        /// <param name="tolerance">This is the tolerance you want to use, default = 1e-6</param>
        /// <param name="log">This is the list that will contain all the iterations info</param>
        public double FindRoot(string function, double xl, double xu, out List<string> log, int maxIte = 100, double tolerance = 1e-6)
        {
            log = new List<string>();
            log.Add("Iteration,xl,xu,xr,f(xr),error");

            this.function = new SimpleFunction(function);

            double xr = 0, fxr = 0, temp, error = 1;
            for (int i = 0; i < maxIte; i++)
            {
                temp = xr;
                xr = (xl + xu) / 2.0;
                fxr = this.function.Evaluate(xr);
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
            lastResult[0] = xr;
            lastResult[1] = fxr;
            lastResult[2] = error;
            return xr;
        }
    }
}
