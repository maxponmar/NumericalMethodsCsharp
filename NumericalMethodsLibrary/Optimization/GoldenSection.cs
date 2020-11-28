using System;
using System.Collections.Generic;
using System.Text;
using FunctionEvaluatorLibrary.Function;

namespace NumericalMethodsLibrary.Optimization
{
    class GoldenSection
    {
        private double[] lastResult = new double[2];
        /// <summary>
        /// This value save the last result [0] x, [1] fx, [2] error
        /// </summary>
        public double[] LastResult { get => lastResult; }

        private SimpleFunction function;

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
            double f1, f2, xopt = 0, fxx, ea = 0;

            this.function = new SimpleFunction(function);
            f1 = this.function.Evaluate(x1);            
            f2 = this.function.Evaluate(x2);

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
                            f1 = this.function.Evaluate(x1);
                        }
                        else
                        {
                            xu = x1;
                            x1 = x2;
                            x2 = xu - d;
                            f1 = f2;
                            f2 = this.function.Evaluate(x2);
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
                        fxx = this.function.Evaluate(xopt);
                        if (ea <= error)
                        {                            
                            px = xopt; pfx = fxx;
                            lastResult[0] = xopt;
                            lastResult[1] = pfx;
                            return xopt;                            
                        }                        
                        px = xopt; pfx = fxx;
                    }
                    lastResult[0] = xopt;
                    lastResult[1] = pfx;
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
                            f1 = this.function.Evaluate(x1);
                        }
                        else
                        {
                            xu = x1;
                            x1 = x2;
                            x2 = xu - d;
                            f1 = f2;
                            f2 = this.function.Evaluate(x2);
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
                        fxx = this.function.Evaluate(xopt);
                        if (ea <= error)
                        {                            
                            px = xopt; pfx = fxx;
                            lastResult[0] = xopt;
                            lastResult[1] = pfx;
                            return xopt;
                        }                        
                        px = xopt; pfx = fxx;
                    }
                    lastResult[0] = xopt;
                    lastResult[1] = pfx;
                    return xopt;
                }
            }
            else
            {
                lastResult[0] = -1;
                lastResult[1] = 0;
                return -1;
            }
            lastResult[0] = xopt;
            lastResult[1] = pfx;
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
            double f1, f2, xopt = 0, fxx, ea = 0;
            
            this.function = new SimpleFunction(function);
            f1 = this.function.Evaluate(x1);
            f2 = this.function.Evaluate(x2);

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
                            f1 = this.function.Evaluate(x1);
                        }
                        else
                        {
                            xu = x1;
                            x1 = x2;
                            x2 = xu - d;
                            f1 = f2;
                            f2 = this.function.Evaluate(x2);
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
                        fxx = this.function.Evaluate(xopt);
                        log.Add(string.Format($"{i + 1},{xopt},{fxx},{ea}"));
                        if (ea <= error)
                        {                            
                            px = xopt; pfx = fxx;
                            lastResult[0] = xopt;
                            lastResult[1] = pfx;
                            return xopt;                            
                        }                        
                        px = xopt; pfx = fxx;
                    }
                    lastResult[0] = xopt;
                    lastResult[1] = pfx;
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
                            f1 = this.function.Evaluate(x1);
                        }
                        else
                        {
                            xu = x1;
                            x1 = x2;
                            x2 = xu - d;
                            f1 = f2;
                            f2 = this.function.Evaluate(x2);
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
                        fxx = this.function.Evaluate(xopt);
                        log.Add(string.Format($"{i + 1},{xopt},{fxx},{ea}"));
                        if (ea <= error)
                        {                            
                            px = xopt; pfx = fxx;
                            lastResult[0] = xopt;
                            lastResult[1] = pfx;
                            return xopt;
                        }                        
                        px = xopt; pfx = fxx;
                    }
                    lastResult[0] = xopt;
                    lastResult[1] = pfx;
                    return xopt;
                }
            }
            else
            {
                lastResult[0] = -1;
                lastResult[1] = 0;
                return -1;
            }
            lastResult[0] = xopt;
            lastResult[1] = pfx;
            return xopt;
        }
    }
}
