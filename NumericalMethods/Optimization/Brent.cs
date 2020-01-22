using System;
using System.Collections.Generic;
using System.Text;
using ReadFunction;

namespace NumericalMethods.Optimization
{
    /// <summary>
    /// This class allows you to use the Brent's method to found the minimum of a function between an internval
    /// </summary>
    public class Brent
    {
        private MathParser mathParser = new MathParser();
        private double px, pfx;
        
        /// <summary>
        /// This function return the result as double
        /// </summary>
        /// <param name="function">The function that you want to optimize (to found a maximun you need to invert it)</param>
        /// <param name="xlow">Lower limit</param>
        /// <param name="xhigh">Upper limit</param>
        public double Solve(string function, double xlow, double xhigh)
        {
            double tol = 0.000001; double phi = (1 + Math.Sqrt(5)) / 2; double rho = 2 - phi;
            double u = xlow + rho * (xhigh - xlow); double v = u; double w = u; double x = u;
           
            //miParser.ProgrammaticallyParse("let x =" + u);
            //double fu = miParser.Parse(function);
            double fu = mathParser.Eval_function(function, u);

            double fv = fu; double fw = fu; double fxx = fu;            
            double xm = 0.5 * (xlow + xhigh); double d = 0; double e = 0;
            bool para = true; double r, p, q, s; int i = 0;
            do
            {
                i++;
                if (Math.Abs(x - xm) <= tol)
                    para = Math.Abs(e) > tol;

                if (para)
                {
                    r = (x - w) * (fxx - fv); q = (x - v) * (fxx - fw);
                    p = (x - v) * q - (x - w) * r; s = 2 * (q - r);
                    if (s > 0)
                        p = -p;

                    s = Math.Abs(s);
                    para = Math.Abs(p) < Math.Abs(0.5 * s * e) && p > s * (xlow - x) && p < s * (xhigh - x);
                    if (para)
                    {
                        e = d; d = p / s;
                    }
                }
                if (!para)
                {
                    if (x >= xm)
                        e = xlow - x;
                    else
                        e = xhigh - x;

                    d = rho * e;
                }
                u = x + d;

                //miParser.ProgrammaticallyParse("let x =" + u);
                //fu = miParser.Parse(function);
                fu = mathParser.Eval_function(function, u);

                if (fu <= fxx)
                {
                    if (u >= x)
                        xlow = x;
                    else
                        xhigh = x;

                    v = w; fv = fw; w = x; fw = fxx; x = u; fxx = fu;
                }
                else
                {
                    if (u < x)
                        xlow = u;
                    else
                        xhigh = u;

                    if (fu <= fw || w == x)
                    {
                        v = w; fv = fw; w = u; fw = fu;
                    }
                    else
                    {
                        if (fu <= fv || v == x || v == w)
                        {
                            v = u; fv = fu;
                        }
                    }
                }
                xm = 0.5 * (xlow + xhigh);
                //txtResultado.AppendText(string.Format("iteracion {0} - f({1}) = {2} \r\n", i, u, fu));
                px = u; pfx = fu;                
            } while (Math.Abs(e) > tol);
            return px;
        }

        /// <summary>
        /// This method returns the result as double and write all the iteration info on a list"
        /// </summary>
        /// <param name="function">The function that you want to optimize (to found a maximun you need to invert it)</param>
        /// <param name="xlow">Lower limit</param>
        /// <param name="xhigh">Upper limit</param>
        /// <param name="log">This is the list that will contain all the iterations info</param>
        /// <returns></returns>
        public double Solve(string function, double xlow, double xhigh, out List<string> log)
        {
            log = new List<string>();
            log.Add("Iteration,xr,f(xr)");

            double tol = 0.000001; double phi = (1 + Math.Sqrt(5)) / 2; double rho = 2 - phi;
            double u = xlow + rho * (xhigh - xlow); double v = u; double w = u; double x = u;

            //miParser.ProgrammaticallyParse("let x =" + u);
            //double fu = miParser.Parse(function);
            double fu = mathParser.Eval_function(function, u);

            double fv = fu; double fw = fu; double fxx = fu;
            double xm = 0.5 * (xlow + xhigh); double d = 0; double e = 0;
            bool para = true; double r, p, q, s; int i = 0;
            do
            {
                i++;
                if (Math.Abs(x - xm) <= tol)
                    para = Math.Abs(e) > tol;

                if (para)
                {
                    r = (x - w) * (fxx - fv); q = (x - v) * (fxx - fw);
                    p = (x - v) * q - (x - w) * r; s = 2 * (q - r);
                    if (s > 0)
                        p = -p;

                    s = Math.Abs(s);
                    para = Math.Abs(p) < Math.Abs(0.5 * s * e) && p > s * (xlow - x) && p < s * (xhigh - x);
                    if (para)
                    {
                        e = d; d = p / s;
                    }
                }
                if (!para)
                {
                    if (x >= xm)
                        e = xlow - x;
                    else
                        e = xhigh - x;

                    d = rho * e;
                }
                u = x + d;

                //miParser.ProgrammaticallyParse("let x =" + u);
                //fu = miParser.Parse(function);
                fu = mathParser.Eval_function(function, u);

                if (fu <= fxx)
                {
                    if (u >= x)
                        xlow = x;
                    else
                        xhigh = x;

                    v = w; fv = fw; w = x; fw = fxx; x = u; fxx = fu;
                }
                else
                {
                    if (u < x)
                        xlow = u;
                    else
                        xhigh = u;

                    if (fu <= fw || w == x)
                    {
                        v = w; fv = fw; w = u; fw = fu;
                    }
                    else
                    {
                        if (fu <= fv || v == x || v == w)
                        {
                            v = u; fv = fu;
                        }
                    }
                }
                xm = 0.5 * (xlow + xhigh);
                //txtResultado.AppendText(string.Format("iteracion {0} - f({1}) = {2} \r\n", i, u, fu));
                log.Add(string.Format($"{i},{u},{fu}"));
                px = u; pfx = fu;
            } while (Math.Abs(e) > tol);
            return px;
        }
    }
}
