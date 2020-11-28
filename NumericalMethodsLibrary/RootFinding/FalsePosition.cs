using System;
using System.Collections.Generic;
using System.Text;
using FunctionEvaluatorLibrary.Function;


namespace NumericalMethodsLibrary.RootFinding
{
    class FalsePosition
    {
        public class False_Position
        {
            private double[] lastResult = new double[3];
            /// <summary>
            /// This value save the last result [0] x, [1] fx, [2] error
            /// </summary>
            public double[] LastResult { get => lastResult; }

            private SimpleFunction function;

            /// <summary>
            /// (Bracketing Method) This method calculate the root of the given function between x1 and x2 values, it return the result as dobule
            /// </summary>
            /// /// <param name="function">This is the function wich you want to find the root</param>
            /// <param name="xl">This is the lower bound of the root</param>
            /// <param name="xu">This is the upper bound of the root</param>
            /// <param name="maxIte">Maximum number of iterations, default = 100</param>
            /// <param name="tolerance">This is the tolerance you want to use, default = 1e-6</param>
            public double FindRoot(string function, double xl, double xu, int maxIte = 100, double tolerance = 1e-6)
            {
                double fxl, fxu, xr = 0, fxr = 0, error = 1, temp, test, iu = 0, il = 0;

                this.function = new SimpleFunction(function);

                fxl = this.function.Evaluate(xl);
                fxu = this.function.Evaluate(xu);
                
                for (int i = 0; i < maxIte; i++)
                {
                    temp = xr;
                    xr = xu - fxu * (xl - xu) / (fxl - fxu);
                    fxr = this.function.Evaluate(xr);
                    if (xr != 0)
                        error = Math.Abs((xr - temp) / xr);
                    test = fxl * fxr;
                    if (test < 0)
                    {
                        xu = xr;
                        fxu = this.function.Evaluate(xu);
                        iu = 0;
                        il++;
                        if (il >= 2) { fxl /= 2; }
                    }
                    if (test > 0)
                    {
                        xl = xr;
                        fxl = this.function.Evaluate(xl);
                        il = 0;
                        iu++;
                        if (iu >= 2) { fxu /= 2; }
                    }
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
            /// This method calculates the root of the given function between a and b values, it return the result as dobule and a string List with the information of all iterations and it could be converted to a .csv file with list2csb function
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
                double fxl, fxu, xr = 0, fxr = 0, error = 1, temp, test, iu = 0, il = 0;
                fxl = this.function.Evaluate(xl);
                fxu = this.function.Evaluate(xu);
                for (int i = 0; i < maxIte; i++)
                {
                    temp = xr;
                    xr = xu - fxu * (xl - xu) / (fxl - fxu);
                    fxr = this.function.Evaluate(xr);
                    if (xr != 0)
                        error = Math.Abs((xr - temp) / xr);
                    test = fxl * fxr;
                    if (test < 0)
                    {
                        xu = xr;
                        fxu = this.function.Evaluate(xu);
                        iu = 0;
                        il++;
                        if (il >= 2) { fxl /= 2; }
                    }
                    if (test > 0)
                    {
                        xl = xr;
                        fxl = this.function.Evaluate(xl);
                        il = 0;
                        iu++;
                        if (iu >= 2) { fxu /= 2; }
                    }
                    log.Add(string.Format($"{i + 1},{xl},{xu},{xr},{fxr},{error}"));
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
}
