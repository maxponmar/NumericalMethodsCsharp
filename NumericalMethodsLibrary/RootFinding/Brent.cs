using FunctionEvaluatorLibrary.Function;
using System;
using System.Collections.Generic;
using System.Text;


namespace NumericalMethodsLibrary.RootFinding
{
    /// <summary>
    /// The main algorithm was writen by: John D. Cook on his article : Three Methods for Root-finding in C# link: https://www.codeproject.com/Articles/79541/Three-Methods-for-Root-finding-in-C
    /// licensed under The BSD License also called the "Simplified BSD License" 
    /// </summary>
    public class Brent
    {
        private double[] lastResult = new double[3];
        /// <summary>
        /// This value save the last result  [0] x, [1] fx, [2] error
        /// </summary>
        public double[] LastResult { get => lastResult; }

        private SimpleFunction function;

        /// <summary>
        /// (Hybrid) This method calculates the root of a f(x) function using Brent's method.       
        /// The main algorithm was writen by: John D. Cook on his article : Three Methods for Root-finding in C# link: https://www.codeproject.com/Articles/79541/Three-Methods-for-Root-finding-in-C
        /// licensed under The BSD License also called the "Simplified BSD License" 
        /// </summary>
        /// <param name="fx">This is the function wich you want to find the root</param>
        /// <param name="xl">This is the lower bound of the root</param>
        /// <param name="xr">This is the upper bound of the root</param>        
        /// <param name="maxIte">Maximum number of iterations, default = 100</param>
        /// <param name="tolerance">This is the tolerance you want to use, default = 1e-6 (0.0001%)</param>
        public double FindRoot(string fx, double xl, double xr, int maxIte = 100, double tolerance = 1e-6)
        {
            int i; double errorEstimate;
            errorEstimate = double.MaxValue;
            // Implementation and notation based on Chapter 4 in
            // "Algorithms for Minimization without Derivatives"
            // by Richard Brent.
            double c, d, e, fa, fb, fc, tol, m, p, q, r, s;
            // set up aliases to match Brent's notation                        
            double a = xl; double b = xr; double t = tolerance;
            i = 0;

            this.function = new SimpleFunction(fx);

            fa = this.function.Evaluate(a);
            fb = this.function.Evaluate(b);

            if (fa * fb > 0.0)
            {
                lastResult[0] = -1;
                lastResult[1] = 0;
                lastResult[2] = errorEstimate;
                return -1;
            }

        label_int:
            c = a; fc = fa; d = e = b - a;
        label_ext:
            if (Math.Abs(fc) < Math.Abs(fb))
            {
                a = b; b = c; c = a;
                fa = fb; fb = fc; fc = fa;
            }

            i++;
            tol = 2.0 * t * Math.Abs(b) + t;
            errorEstimate = m = 0.5 * (c - b);
            if (Math.Abs(m) > tol && fb != 0.0) // exact comparison with 0 is OK here
            {
                // See if bisection is forced
                if (Math.Abs(e) < tol || Math.Abs(fa) <= Math.Abs(fb))
                {
                    d = e = m;
                }
                else
                {
                    s = fb / fa;
                    if (a == c)
                    {
                        // linear interpolation
                        p = 2.0 * m * s; q = 1.0 - s;
                    }
                    else
                    {
                        // Inverse quadratic interpolation
                        q = fa / fc; r = fb / fc;
                        p = s * (2.0 * m * q * (q - r) - (b - a) * (r - 1.0));
                        q = (q - 1.0) * (r - 1.0) * (s - 1.0);
                    }
                    if (p > 0.0)
                        q = -q;
                    else
                        p = -p;
                    s = e; e = d;
                    if (2.0 * p < 3.0 * m * q - Math.Abs(tol * q) && p < Math.Abs(0.5 * s * q))
                        d = p / q;
                    else
                        d = e = m;
                }
                a = b; fa = fb;
                if (Math.Abs(d) > tol)
                    b += d;
                else if (m > 0.0)
                    b += tol;
                else
                    b -= tol;
                if (i == maxIte)
                {
                    lastResult[0] = b;
                    lastResult[1] = fb;
                    lastResult[2] = errorEstimate;
                    return b;
                }


                //convEqn.ProgrammaticallyParse("let x =" + b);
                //fb = convEqn.Parse(g);
                fb = this.function.Evaluate(b);
                if ((fb > 0.0 && fc > 0.0) || (fb <= 0.0 && fc <= 0.0))
                    goto label_int;
                else
                    goto label_ext;
            }
            else
            {
                lastResult[0] = b;
                lastResult[1] = fb;
                lastResult[2] = errorEstimate;
                return b;
            }

        }
        /// <summary>
        /// (Hybrid) This method calculates the root of a f(x) function using Brent's method.       
        /// The main algorithm was writen by: John D. Cook on his article : Three Methods for Root-finding in C# link: https://www.codeproject.com/Articles/79541/Three-Methods-for-Root-finding-in-C
        /// licensed under The BSD License also called the "Simplified BSD License" 
        /// </summary>
        /// <param name="fx">This is the function wich you want to find the root</param>
        /// <param name="xl">This is the lower bound of the root</param>
        /// <param name="xr">This is the upper bound of the root</param>        
        /// <param name="maxIte">Maximum number of iterations, default = 100</param>
        /// <param name="tolerance">This is the tolerance you want to use, default = 1e-6 (0.0001%)</param>
        /// <param name="log">This is the list that will contain all the iterations info</param>
        public double FindRoot(string fx, double xl, double xr, out List<string> log, int maxIte = 100, double tolerance = 1e-6)
        {
            log = new List<string>();
            int i; double errorEstimate;
            log.Add("Iteration,xr,f(xr),error");
            errorEstimate = double.MaxValue;
            // Implementation and notation based on Chapter 4 in
            // "Algorithms for Minimization without Derivatives"
            // by Richard Brent.
            double c, d, e, fa, fb, fc, tol, m, p, q, r, s;
            // set up aliases to match Brent's notation                        
            double a = xl; double b = xr; double t = tolerance;
            i = 0;

            fa = this.function.Evaluate(a);
            fb = this.function.Evaluate(b);

            if (fa * fb > 0.0)
            {
                lastResult[0] = -1;
                lastResult[1] = 0;
                lastResult[2] = errorEstimate;
                return -1;
                //MessageBox.Show("Lo límites no son adecuados, asegurate de en encierran a la raíz", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        label_int:
            c = a; fc = fa; d = e = b - a;
        label_ext:
            if (Math.Abs(fc) < Math.Abs(fb))
            {
                a = b; b = c; c = a;
                fa = fb; fb = fc; fc = fa;
            }

            i++;
            tol = 2.0 * t * Math.Abs(b) + t;
            errorEstimate = m = 0.5 * (c - b);
            log.Add(string.Format($"{i},{b},{fb},{errorEstimate}"));
            if (Math.Abs(m) > tol && fb != 0.0) // exact comparison with 0 is OK here
            {
                // See if bisection is forced
                if (Math.Abs(e) < tol || Math.Abs(fa) <= Math.Abs(fb))
                {
                    d = e = m;
                }
                else
                {
                    s = fb / fa;
                    if (a == c)
                    {
                        // linear interpolation
                        p = 2.0 * m * s; q = 1.0 - s;
                    }
                    else
                    {
                        // Inverse quadratic interpolation
                        q = fa / fc; r = fb / fc;
                        p = s * (2.0 * m * q * (q - r) - (b - a) * (r - 1.0));
                        q = (q - 1.0) * (r - 1.0) * (s - 1.0);
                    }
                    if (p > 0.0)
                        q = -q;
                    else
                        p = -p;
                    s = e; e = d;
                    if (2.0 * p < 3.0 * m * q - Math.Abs(tol * q) && p < Math.Abs(0.5 * s * q))
                        d = p / q;
                    else
                        d = e = m;
                }
                a = b; fa = fb;
                if (Math.Abs(d) > tol)
                    b += d;
                else if (m > 0.0)
                    b += tol;
                else
                    b -= tol;
                if (i == maxIte)
                {
                    lastResult[0] = b;
                    lastResult[1] = fb;
                    lastResult[2] = errorEstimate;
                    return b;
                }

                fb = this.function.Evaluate(b);
                if ((fb > 0.0 && fc > 0.0) || (fb <= 0.0 && fc <= 0.0))
                    goto label_int;
                else
                    goto label_ext;
            }
            else
            {
                lastResult[0] = b;
                lastResult[1] = fb;
                lastResult[2] = errorEstimate;
                return b;
            }
        }
    }
}
