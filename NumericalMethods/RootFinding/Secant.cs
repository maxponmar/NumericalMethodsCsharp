using System;
using System.Collections.Generic;
using System.Text;
using ReadFunction;

namespace NumericalMethods.RootFinding
{
    public class Secant
    {
        private double lastResult;
        /// <summary>
        /// This value save the last result
        /// </summary>
        public double LastResult { get => lastResult;}

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
        /// <param name="tolerance">This is the tolerance you want to use, default = 0.01 (1%)</param>
        public double FindRoot(string fx, double x0, double h = 0.001, int maxIte = 100, double tolerance = 0.01)
        {
            double error = 1, temp, xr = x0, fxr, z;
            for (int i = 0; i < maxIte; i++)
            {
                temp = xr;
                fxr = mathParser.Eval_function(fx, temp);
                z = mathParser.Eval_function(fx, temp + h * temp);
                xr = (h * temp * fxr) / (z - fxr);
                if (xr != 0)
                    error = Math.Abs((xr - temp) / xr);
                if (fxr == 0) { break; }
                if (error < tolerance) { break; }
                if (xr == temp) { break; }
            }
            lastResult = xr;
            return xr;
        }
        /// <summary>
        /// (Open Method) This method calculates the root of a f(x) function using Modified Secant method.        
        /// </summary>
        /// <param name="fx">This is the function wich you want to find the root</param>
        /// <param name="x0">This is the initial value, please don't use 0 as initial value it will not vonverge</param>        
        /// <param name="h">This value is used to calculate numerical derivative if you don't want to use it instead of symbolic derivative, default = 0.001. The algorith uses a centered finite-divided difference formula with O(h^4) of error. (high-accurate)</param>        
        /// <param name="maxIte">Maximum number of iterations, default = 100</param>
        /// <param name="tolerance">This is the tolerance you want to use, default = 0.01 (1%)</param>
        /// <param name="log">This is the list that will contain all the iterations info</param>
        public double FindRoot(string fx, double x0, out List<string> log, double h = 0.001, int maxIte = 100, double tolerance = 0.01)
        {
            double error = 1, temp, xr = x0, fxr, z;
            log = new List<string>();
            log.Add("Iteration,xr,f(xr),error");            
            for (int i = 0; i < maxIte; i++)
            {
                temp = xr;
                fxr = mathParser.Eval_function(fx, temp);
                z = mathParser.Eval_function(fx, temp + h * temp);
                xr = (h * temp * fxr) / (z - fxr);
                if (xr != 0)
                    error = Math.Abs((xr - temp) / xr);
                log.Add(string.Format($"{i + 1},{xr},{fxr},{error}"));
                if (fxr == 0) { log.Add("Exact root found in the last iteration"); break; }
                if (error < tolerance) { log.Add(string.Format("Root found in the iteration #{0} with {1}% of tolerance", i + 1, tolerance * 100)); break; }
                if (xr == temp) { log.Add("Infinite loop"); break; }
            }
            lastResult = xr;
            return xr;
        }
    }
}
