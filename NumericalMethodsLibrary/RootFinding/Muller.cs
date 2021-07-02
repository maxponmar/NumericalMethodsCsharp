using FunctionEvaluatorLibrary.Function;
using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethodsLibrary.RootFinding
{
    /// <summary>
    /// This class allows you to use the Muller's method for root finding (only works with polinomials)
    /// </summary>
    public class Muller
    {
        private double[] lastResult = new double[3];
        /// <summary>
        /// This value save the last result [0] x, [1] fx, [2] error
        /// </summary>
        public double[] LastResult { get => lastResult; }

        private SimpleFunction function;

        /// <summary>
        /// The Muller's Method only works with polinomials, it finds real roots.
        /// </summary>
        /// <param name="fx">This is the function wich you want to find the root</param>
        /// <param name="xr">This is the initial value</param>
        /// <param name="h">This value is used to determine x1, x2 and x3, default 0.001</param>        
        /// <param name="maxIte">Maximum number of iterations, default = 10</param>     
        public double FindRoot(string fx, double xr, double h = 0.001, int maxIte = 10)
        {
            double x0, x1, x2, h0, h1, d0, d1, a, b, c, rad, den, dxr = 0, fx1, fx0, fx2, fxr = 0;
            this.function = new SimpleFunction(fx);
            x2 = xr;
            x1 = xr + h * xr;
            x0 = xr + h * xr;
            for (int i = 0; i < maxIte; i++)
            {
                h0 = x1 + x0;
                h1 = x2 - x1;
                fx0 = this.function.Evaluate(x0);
                fx1 = this.function.Evaluate(x1);
                fx2 = this.function.Evaluate(x2);
                d0 = (fx1 - fx0) / h0;
                d1 = (fx2 - fx1) / h1;
                a = (d1 - d0) / (h1 + h0);
                b = a * h1 + d1;
                c = fx2;
                rad = Math.Sqrt(b * b - 4 * a * c);
                if (Math.Abs(b + rad) > Math.Abs(b - rad))
                {
                    den = b + rad;
                }
                else
                {
                    den = b - rad;
                }
                dxr = -2 * c / den;
                xr = x2 + dxr;
                fxr = this.function.Evaluate(xr);
                if (fxr == 0) { break; }
                if (fxr <= 1e-13 && fxr >= -1e-13) { break; }
                if (Math.Abs(dxr) < double.Epsilon * xr) { break; }
                x0 = x1;
                x1 = x2;
                x2 = xr;
            }
            lastResult[0] = xr;
            lastResult[1] = fxr;
            lastResult[2] = dxr;
            return xr;
        }
        /// <summary>
        /// The Muller's Method only works with polinomials, it finds real roots. 
        /// </summary>
        /// <param name="fx">This is the function wich you want to find the root</param>
        /// <param name="xr">This is the initial value</param>
        /// <param name="h">This value is used to determine x1, x2 and x3, default 0.001</param>        
        /// <param name="maxIte">Maximum number of iterations, default = 10</param>       
        /// <param name="log">This is the list that will contain all the iterations info</param>
        public double FindRoot(string fx, double xr, out List<string> log, double h = 0.001, int maxIte = 10)
        {
            log = new List<string>();
            log.Add("Iteration,xr,f(xr)");
            double x0, x1, x2, h0, h1, d0, d1, a, b, c, rad, den, dxr = 0, fx1, fx0, fx2, fxr = 0;
            x2 = xr;
            x1 = xr + h * xr;
            x0 = xr + h * xr;
            for (int i = 0; i < maxIte; i++)
            {
                h0 = x1 + x0;
                h1 = x2 - x1;
                fx0 = this.function.Evaluate(x0);
                fx1 = this.function.Evaluate(x1);
                fx2 = this.function.Evaluate(x2);
                d0 = (fx1 - fx0) / h0;
                d1 = (fx2 - fx1) / h1;
                a = (d1 - d0) / (h1 + h0);
                b = a * h1 + d1;
                c = fx2;
                rad = Math.Sqrt(b * b - 4 * a * c);
                if (Math.Abs(b + rad) > Math.Abs(b - rad))
                {
                    den = b + rad;
                }
                else
                {
                    den = b - rad;
                }
                dxr = -2 * c / den;
                xr = x2 + dxr;
                fxr = this.function.Evaluate(xr);
                log.Add(string.Format($"{i + 1},{xr},{fxr}"));
                if (fxr == 0)
                {
                    log.Add("Exact root found in the last iteration");
                    break;
                }
                if (fxr <= 1e-13 && fxr >= -1e-13)
                {
                    log.Add("fxr <= 1e-13 and fxr >= -1e-13");
                    break;
                }
                if (Math.Abs(dxr) < double.Epsilon * xr)
                {
                    log.Add(string.Format("The rule |dxr| < epsilon*xr was true"));
                    break;
                }
                x0 = x1;
                x1 = x2;
                x2 = xr;
            }
            lastResult[0] = xr;
            lastResult[1] = fxr;
            lastResult[2] = dxr;
            return xr;
        }
    }
}
