using System;
using System.Collections.Generic;
using System.Text;
using ReadFunction;


namespace NumericalMethods.RootFinding
{
    /// <summary>
    /// This class allows you to use the Newton-Raphson's method for root finding
    /// </summary>
    public class Newthon_Raphson
    {
        private MathParser mathParser = new MathParser();
        /// <summary>
        /// (Open Method) This method calculates the root of a f(x) function using Newton-Raphson's method, you could use the numerical derivative (default) or enter the symbolic derivative.
        /// Note that by using numerical differentiation the method becomes to the modified secant method but using more terms from the Taylor series in order to have more accuracy.
        /// </summary>        
        /// <param name="fx_function">This is the function wich you want to find the root</param>
        /// <param name="x0">This is the initial value</param>
        /// <param name="fx_derivative">The derivative of fx if you know it, default = "" (disabled)</param>
        /// <param name="h">This value is used to calculate numerical derivative if you don't want to use it instead of symbolic derivative, default = 0.001. The algorith uses a centered finite-divided difference formula with O(h^4) of error (high-accurate)</param>
        /// <param name="maxIte">Maximum number of iterations, default = 100</param>
        /// <param name="tolerance">This is the tolerance you want to use, default = 0.01 (1%)</param>
        public double FindRoot(string fx_function, double x0, string fx_derivative = "", double h = 0.001, int maxIte = 100, double tolerance = 0.01)
        {
            double error = 1, temp, xr = x0, fxr, fdxr;
            if (fx_derivative != "")
            {
                // When the user introduced a symbolic derivative
                for (int i = 0; i < maxIte; i++)
                {
                    temp = xr;
                    fxr = mathParser.Eval_function(fx_function, temp);
                    fdxr = mathParser.Eval_function(fx_derivative, temp);
                    if (fdxr == 0) { break; }
                    xr = temp - (fxr / fdxr);
                    if (xr != 0)
                        error = Math.Abs((xr - temp) / xr);
                    if (fxr == 0) { break; }
                    if (error < tolerance) { break; }
                    if (xr == temp) { break; }
                }
            }
            else
            {
                // When the user didn't introduce a symbolic derivate
                // In order to use centered finite-divided difference formula we need to evaluate 4 time the function
                double fx1, fx2, fx3, fx4;
                for (int i = 0; i < maxIte; i++)
                {
                    temp = xr;
                    fxr = mathParser.Eval_function(fx_function, temp);

                    fx1 = mathParser.Eval_function(fx_function, temp + 2 * h);   // f_x(i+2)
                    fx2 = mathParser.Eval_function(fx_function, temp + h);       // f_x(i+1)
                    fx3 = mathParser.Eval_function(fx_function, temp - h);       // f_x(i-1)
                    fx4 = mathParser.Eval_function(fx_function, temp - 2 * h);   // f_x(i-2)

                    fdxr = (-fx1 + 8 * fx2 - 8 * fx3 + fx4) / (12 * h);
                    if (fdxr == 0) { break; }
                    xr = temp - (fxr / fdxr);
                    if (xr != 0)
                        error = Math.Abs((xr - temp) / xr);
                    if (fxr == 0) { break; }
                    if (error < tolerance) { break; }
                    if (xr == temp) { break; }
                }
            }            
            return xr;
        }
        /// <summary>
        /// (Open Method) This method calculates the root of a f(x) function using Newton-Raphson's method, you could use the numerical derivative (default) or enter the symbolic derivative.
        /// Note that by using numerical differentiation the method becomes to the modified secant method but using more terms from the Taylor series in order to have more accuracy.
        /// </summary>        
        /// <param name="fx_function">This is the function wich you want to find the root</param>
        /// <param name="x0">This is the initial value</param>
        /// <param name="fx_derivative">The derivative of fx if you know it, default = "" (disabled)</param>
        /// <param name="h">This value is used to calculate numerical derivative if you don't want to use it instead of symbolic derivative, default = 0.001. The algorith uses a centered finite-divided difference formula with O(h^4) of error. (high-accurate)</param>        
        /// <param name="maxIte">Maximum number of iterations, default = 100</param>
        /// <param name="tolerance">This is the tolerance you want to use, default = 0.01 (1%)</param>
        /// <param name="log">This is the list that will contain all the iterations info</param>
        public double FindRoot(string fx_function, double x0, out List<string> log, string fx_derivative = "", double h = 0.001, int maxIte = 100, double tolerance = 0.01)
        {
            double error = 1, temp, xr = x0, fxr, fdxr;
            log = new List<string>();
            log.Add("Iteration,xr,f(xr),df(xr),error");
            if (fx_derivative != "")
            {
                // When the user introduced a symbolic derivative
                for (int i = 0; i < maxIte; i++)
                {
                    temp = xr;
                    fxr = mathParser.Eval_function(fx_function, temp);
                    fdxr = mathParser.Eval_function(fx_derivative, temp);
                    if (fdxr == 0) { log.Add("Derivative equal to zero"); break; }
                    xr = temp - (fxr / fdxr);
                    if (xr != 0)
                        error = Math.Abs((xr - temp) / xr);
                    log.Add(string.Format($"{i + 1},{xr},{fxr},{fdxr},{error}"));
                    if (fxr == 0) { log.Add("Exact root found in the last iteration"); break; }
                    if (error < tolerance) { log.Add(string.Format("Root found in the iteration #{0} with {1}% of tolerance", i + 1, tolerance * 100)); break; }
                    if (xr == temp) { log.Add("Infinite loop"); break; }
                }
            }
            else
            {
                // When the user didn't introduce a symbolic derivate
                // In order to use centered finite-divided difference formula we need to evaluate 4 time the function
                double fx1, fx2, fx3, fx4;
                fxr = mathParser.Eval_function(fx_function, xr);
                for (int i = 0; i < maxIte; i++)
                {
                    temp = xr;                    

                    fx1 = mathParser.Eval_function(fx_function, temp + 2 * h);   // f_x(i+2)
                    fx2 = mathParser.Eval_function(fx_function, temp + h);       // f_x(i+1)
                    fx3 = mathParser.Eval_function(fx_function, temp - h);       // f_x(i-1)
                    fx4 = mathParser.Eval_function(fx_function, temp - 2 * h);   // f_x(i-2)

                    fdxr = (-fx1 + 8 * fx2 - 8 * fx3 + fx4) / (12 * h);
                    if (fdxr == 0) { log.Add("Derivative equal to zero"); break; }
                    xr = temp - (fxr / fdxr);
                    fxr = mathParser.Eval_function(fx_function, temp);
                    if (xr != 0)
                        error = Math.Abs((xr - temp) / xr);
                    log.Add(string.Format($"{i + 1},{xr},{fxr},{fdxr},{error}"));
                    if (fxr == 0) { log.Add("Exact root found in the last iteration"); break; }
                    if (error < tolerance) { log.Add(string.Format("Root found in the iteration #{0} with {1}% of tolerance", i + 1, tolerance * 100)); break; }
                    if (xr == temp) { log.Add("Infinite loop"); break; }
                }
            }
            return xr;
        }
    }
}
