using System;
using System.Collections.Generic;
using System.Text;
using ReadFunction;

namespace NumericalMethods.Optimization
{
    /// <summary>
    /// This class allows you to use the Golden Section method to found the  maximum o minimum of a function between an internval
    /// </summary>
    public class GoldenSection
    {
        private double lastResult;
        /// <summary>
        /// This value save the last result
        /// </summary>
        public double LastResult { get => lastResult; }

        private MathParser mathParser = new MathParser();
        private double px, pfx;
        /// <summary>
        ///  Use "mode" parameter to choose minimization (0) or maximization(1)
        /// </summary>
        /// <param name="function">The function that you want to optimize</param>
        /// <param name="xlow">Lower limit</param>
        /// <param name="xhigh">Upper limit</param>
        /// <param name="mode">0-Minimization 1-Maximization, default 1. If you enter antoher numeri it will return -1</param>
        /// <param name="maxIte">Maximum number of iterations (defoult = 20)</param>
        /// <param name="error">Minimal error estimate (defoult = 0.001 - 0.01%)</param>
        public double Solve(string function, double xlow, double xhigh, int mode = 1, int maxIte = 20, double error = 0.001)
        {
            double R = (Math.Sqrt(5) - 1) / 2;
            double xl = xlow; double xu = xhigh;
            double d = R * (xu - xl);
            double x1 = xl + d; double x2 = xu - d;
            double f1, f2, xopt=0, fxx, ea = 0;
            //miParser.ProgrammaticallyParse("let x =" + x1);
            //f1 = miParser.Parse(fx);
            f1 = mathParser.Eval_function(function, x1);
            //miParser.ProgrammaticallyParse("let x =" + x2);
            //f2 = miParser.Parse(fx);
            f2 = mathParser.Eval_function(function, x2);

            if(mode == 0 || mode == 1)
            {
                if (mode == 1)
                {
                    if (f1 > f2)
                    {
                        xopt = x1;
                        fxx = f1;
                    }
                    else
                    {
                        xopt = x2;
                        fxx = f2;
                    }
                    for (int i = 0; i < maxIte; i++)
                    {
                        d *= R;
                        if (f1 > f2)
                        {
                            xl = x2;
                            x2 = x1;
                            x1 = xl + d;
                            f2 = f1;
                            //miParser.ProgrammaticallyParse("let x =" + x1);
                            //f1 = miParser.Parse(fx);
                            f1 = mathParser.Eval_function(function, x1);
                        }
                        else
                        {
                            xu = x1;
                            x1 = x2;
                            x2 = xu - d;
                            f1 = f2;
                            //miParser.ProgrammaticallyParse("let x =" + x2);
                            //f2 = miParser.Parse(fx);
                            f2 = mathParser.Eval_function(function, x2);
                        }
                        if (f1 > f2)
                        {
                            xopt = x1;
                            fxx = f1;
                        }
                        else
                        {
                            xopt = x2;
                            fxx = f2;
                        }
                        if (xopt != 0)
                        {
                            ea = (1 - R) * (Math.Abs(xu - xl) / xopt) * 100;
                        }
                        //miParser.ProgrammaticallyParse("let x =" + xopt);
                        //fxx = miParser.Parse(fx);
                        fxx = mathParser.Eval_function(function, xopt);
                        if (ea <= error)
                        {
                            //txtResultado.AppendText(string.Format("iteracion {0} - f({1}) = {2} \r\n", i + 1, xopt, fxx));
                            px = xopt; pfx = fxx;
                            lastResult = xopt;
                            return xopt;
                            //break;
                        }
                        //txtResultado.AppendText(string.Format("iteracion {0} - f({1}) = {2} \r\n", i + 1, xopt, fxx));
                        px = xopt; pfx = fxx;
                    }
                    lastResult = xopt;
                    return xopt;
                }
                if (mode == 0)
                {
                    if (f1 < f2)
                    {
                        xopt = x1;
                        fxx = f1;
                    }
                    else
                    {
                        xopt = x2;
                        fxx = f2;
                    }
                    for (int i = 0; i < maxIte; i++)
                    {
                        d *= R;
                        if (f1 < f2)
                        {
                            xl = x2;
                            x2 = x1;
                            x1 = xl + d;
                            f2 = f1;
                            //miParser.ProgrammaticallyParse("let x =" + x1);
                            //f1 = miParser.Parse(fx);
                            f1 = mathParser.Eval_function(function, x1);
                        }
                        else
                        {
                            xu = x1;
                            x1 = x2;
                            x2 = xu - d;
                            f1 = f2;
                            //miParser.ProgrammaticallyParse("let x =" + x2);
                            //f2 = miParser.Parse(fx);
                            f2 = mathParser.Eval_function(function, x2);
                        }
                        if (f1 < f2)
                        {
                            xopt = x1;
                            fxx = f1;
                        }
                        else
                        {
                            xopt = x2;
                            fxx = f2;
                        }
                        if (xopt != 0)
                        {
                            ea = (1 - R) * (Math.Abs(xu - xl) / xopt) * 100;
                        }
                        //miParser.ProgrammaticallyParse("let x =" + xopt);
                        //fxx = miParser.Parse(fx);
                        fxx = mathParser.Eval_function(function, xopt);
                        if (ea <= error)
                        {
                            //txtResultado.AppendText(string.Format("iteracion {0} - f({1}) = {2} \r\n", i + 1, xopt, fxx));
                            px = xopt; pfx = fxx;
                            lastResult = xopt;
                            return xopt;
                        }
                        //txtResultado.AppendText(string.Format("iteracion {0} - f({1}) = {2} \r\n", i + 1, xopt, fxx));
                        px = xopt; pfx = fxx;
                    }
                    lastResult = xopt;
                    return xopt;
                }
            }
            else
            {
                lastResult = -1;
                return -1;
            }
            lastResult = xopt;
            return xopt;           
        }

        /// <summary>
        /// Use "mode" parameter to choose minimization (0) or maximization(1)
        /// </summary>
        /// <param name="function">The function that you want to optimize (to found a minimun you need to invert it)</param>
        /// <param name="xlow">Lower limit</param>
        /// <param name="xhigh">Upper limit</param>
        /// <param name="mode">0-Minimization 1-Maximization, default 1. If you enter antoher numeri it will return -1</param>
        /// <param name="maxIte">Maximum number of iterations (defoult = 20)</param>
        /// <param name="error">Minimal error estimate (defoult = 0.001 - 0.01%)</param>
        /// <param name="log">This is the list that will contain all the iterations info</param>
        public double Solve(string function, double xlow, double xhigh, out List<string> log, int mode = 1, int maxIte = 20, double error = 0.001)
        {
            log = new List<string>();
            log.Add("Iteration,xr,f(xr),error");
            double R = (Math.Sqrt(5) - 1) / 2;
            double xl = xlow; double xu = xhigh;
            double d = R * (xu - xl);
            double x1 = xl + d; double x2 = xu - d;
            double f1, f2, xopt=0, fxx, ea = 0;
            //miParser.ProgrammaticallyParse("let x =" + x1);
            //f1 = miParser.Parse(fx);
            f1 = mathParser.Eval_function(function, x1);
            //miParser.ProgrammaticallyParse("let x =" + x2);
            //f2 = miParser.Parse(fx);
            f2 = mathParser.Eval_function(function, x2);
            
            if (mode == 0 || mode == 1)
            {
                if (mode == 1)
                {
                    if (f1 > f2)
                    {
                        xopt = x1;
                        fxx = f1;
                    }
                    else
                    {
                        xopt = x2;
                        fxx = f2;
                    }
                    for (int i = 0; i < maxIte; i++)
                    {
                        d *= R;
                        if (f1 > f2)
                        {
                            xl = x2;
                            x2 = x1;
                            x1 = xl + d;
                            f2 = f1;
                            //miParser.ProgrammaticallyParse("let x =" + x1);
                            //f1 = miParser.Parse(fx);
                            f1 = mathParser.Eval_function(function, x1);
                        }
                        else
                        {
                            xu = x1;
                            x1 = x2;
                            x2 = xu - d;
                            f1 = f2;
                            //miParser.ProgrammaticallyParse("let x =" + x2);
                            //f2 = miParser.Parse(fx);
                            f2 = mathParser.Eval_function(function, x2);
                        }
                        if (f1 > f2)
                        {
                            xopt = x1;
                            fxx = f1;
                        }
                        else
                        {
                            xopt = x2;
                            fxx = f2;
                        }
                        if (xopt != 0)
                        {
                            ea = (1 - R) * (Math.Abs(xu - xl) / xopt) * 100;
                        }
                        //miParser.ProgrammaticallyParse("let x =" + xopt);
                        //fxx = miParser.Parse(fx);
                        fxx = mathParser.Eval_function(function, xopt);
                        log.Add(string.Format($"{i + 1},{xopt},{fxx},{ea}"));
                        if (ea <= error)
                        {
                            //txtResultado.AppendText(string.Format("iteracion {0} - f({1}) = {2} \r\n", i + 1, xopt, fxx));
                            px = xopt; pfx = fxx;
                            lastResult = xopt;
                            return xopt;
                            //break;
                        }
                        //txtResultado.AppendText(string.Format("iteracion {0} - f({1}) = {2} \r\n", i + 1, xopt, fxx));
                        px = xopt; pfx = fxx;
                    }
                    lastResult = xopt;
                    return xopt;
                }
                if (mode == 0)
                {
                    if (f1 < f2)
                    {
                        xopt = x1;
                        fxx = f1;
                    }
                    else
                    {
                        xopt = x2;
                        fxx = f2;
                    }
                    for (int i = 0; i < maxIte; i++)
                    {
                        d *= R;
                        if (f1 < f2)
                        {
                            xl = x2;
                            x2 = x1;
                            x1 = xl + d;
                            f2 = f1;
                            //miParser.ProgrammaticallyParse("let x =" + x1);
                            //f1 = miParser.Parse(fx);
                            f1 = mathParser.Eval_function(function, x1);
                        }
                        else
                        {
                            xu = x1;
                            x1 = x2;
                            x2 = xu - d;
                            f1 = f2;
                            //miParser.ProgrammaticallyParse("let x =" + x2);
                            //f2 = miParser.Parse(fx);
                            f2 = mathParser.Eval_function(function, x2);
                        }
                        if (f1 < f2)
                        {
                            xopt = x1;
                            fxx = f1;
                        }
                        else
                        {
                            xopt = x2;
                            fxx = f2;
                        }
                        if (xopt != 0)
                        {
                            ea = (1 - R) * (Math.Abs(xu - xl) / xopt) * 100;
                        }
                        //miParser.ProgrammaticallyParse("let x =" + xopt);
                        //fxx = miParser.Parse(fx);
                        fxx = mathParser.Eval_function(function, xopt);
                        log.Add(string.Format($"{i + 1},{xopt},{fxx},{ea}"));
                        if (ea <= error)
                        {
                            //txtResultado.AppendText(string.Format("iteracion {0} - f({1}) = {2} \r\n", i + 1, xopt, fxx));
                            px = xopt; pfx = fxx;
                            lastResult = xopt;
                            return xopt;
                        }
                        //txtResultado.AppendText(string.Format("iteracion {0} - f({1}) = {2} \r\n", i + 1, xopt, fxx));
                        px = xopt; pfx = fxx;
                    }
                    lastResult = xopt;
                    return xopt;
                }
            }
            else
            {
                lastResult = -1;
                return -1;
            }
            lastResult = xopt;
            return xopt;
        }
    }
}
