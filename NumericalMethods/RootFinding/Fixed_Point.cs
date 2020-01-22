using System;
using System.Collections.Generic;
using ReadFunction;

namespace NumericalMethods.RootFinding
{
    /// <summary>
    /// This class allows you to use the simple fixed-point method for root finding
    /// </summary>
    public class Fixed_Point
    {
        MathParser mathParser = new MathParser();
        /// <summary>
        /// (Open Method) This method calculates the root of a f(x) function using Simple Fixed Poitn method given g(x) function, you could find g(x) like this: g(x) = f(x) + x
        /// or isolating x in f(x)
        /// It return the result as dobule.
        /// </summary>     
        /// <param name="fx_function">This is the function wich you want to find the root</param>
        /// <param name="gx_function">This is the g(x) function that helps to find the root</param>
        /// <param name="x0">This is the initial value to start the algorithm</param>        
        /// <param name="maxIte">Maximum number of iterations, default = 100</param>
        /// <param name="tolerance">This is the tolerance you want to use, default = 0.01 (1%)</param>
        public double FindRoot(string fx_function, string gx_function, double x0, int maxIte = 100, double tolerance = 0.01)
        {
            double error = 1, temp, xr = x0, fxr;
            for (int i = 0; i < maxIte; i++)
            {
                temp = xr;
                fxr = mathParser.Eval_function(fx_function, temp);
                xr = mathParser.Eval_function(gx_function, temp);
                if (xr != 0)
                    error = Math.Abs((xr - temp) / xr);
                if (fxr == 0) { break; }
                if (error < tolerance) { break; }
                if (xr == temp) { break; }
            }
            return xr;
        }
        /// <summary>
        /// (Open Method) This method calculates the root of a f(x) function using Simple Fixed Poitn method given g(x) function, you could find g(x) like this: g(x) = f(x) + x
        /// or isolating x in f(x)
        /// It return the result as dobule and a string List with the information of all iterations and it could be converted to a .csv file with list2csb function.
        /// </summary>    
        /// <param name="fx_function">This is the function wich you want to find the root</param>
        /// <param name="gx_function">This is the g(x) function that helps to find the root</param>
        /// <param name="x0">This is the initial value to start the algorithm</param>        
        /// <param name="maxIte">Maximum number of iterations, default = 100</param>
        /// <param name="tolerance">This is the tolerance you want to use, default = 0.01 (1%)</param>
        /// <param name="log">This is the list that will contain all the iterations info</param>
        public double FindRoot(string fx_function, string gx_function, double x0, out List<string> log, int maxIte = 100, double tolerance = 0.01)
        {
            log = new List<string>();
            log.Add("Iteration,xr,f(xr),g(xr),error");
            double error = 1, temp, xr = x0, fxr;
            for (int i = 0; i < maxIte; i++)
            {
                temp = xr;
                fxr = mathParser.Eval_function(fx_function, temp);
                xr = mathParser.Eval_function(gx_function, temp);                
                if (xr != 0)
                    error = Math.Abs((xr - temp) / xr);
                log.Add(string.Format($"{i + 1},{temp},{fxr},{xr},{error}"));
                if (fxr == 0) { log.Add("Exact root found in the last iteration"); break; }
                if (error < tolerance) { log.Add(string.Format("Root found in the iteration #{0} with {1}% of tolerance", i + 1, tolerance * 100)); break; }
                if (xr == temp) { log.Add("Infinite loop"); break; }
            }
            return xr;
        }
    }
}
