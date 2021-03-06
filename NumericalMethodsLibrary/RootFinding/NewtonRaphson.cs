﻿using FunctionEvaluatorLibrary.Function;
using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethodsLibrary.RootFinding
{
    public class NewtonRaphson
    {
        private double[] lastResult = new double[3];
        /// <summary>
        /// This value save the last result [0] x, [1] fx, [2] error
        /// </summary>
        public double[] LastResult { get => lastResult; }

        private SimpleFunction function;
        private SimpleFunction derivative;

        /// <summary>
        /// (Open Method) This method calculates the root of a f(x) function using Newton-Raphson's method, you could use the numerical derivative (default) or enter the symbolic derivative.
        /// Note that by using numerical differentiation the method becomes to the modified secant method but using more terms from the Taylor series in order to have more accuracy.
        /// </summary>        
        /// <param name="fx_function">This is the function wich you want to find the root</param>
        /// <param name="x0">This is the initial value</param>
        /// <param name="fx_derivative">The derivative of fx if you know it, default = "" (disabled)</param>
        /// <param name="h">This value is used to calculate numerical derivative if you don't want to use it instead of symbolic derivative, default = 0.001. The algorith uses a centered finite-divided difference formula with O(h^4) of error (high-accurate)</param>
        /// <param name="maxIte">Maximum number of iterations, default = 100</param>
        /// <param name="tolerance">This is the tolerance you want to use, default = 1e-6</param>
        public double FindRoot(string fx_function, double x0, string fx_derivative = "", double h = 0.001, int maxIte = 100, double tolerance = 1e-6)
        {
            double error = 1, temp, xr = x0, fxr = 0, fdxr;

            this.function = new SimpleFunction(fx_function);
            this.derivative = new SimpleFunction(fx_derivative);

            if (fx_derivative != "")
            {
                // When the user introduced a symbolic derivative
                for (int i = 0; i < maxIte; i++)
                {
                    temp = xr;
                    fxr = this.function.Evaluate(temp);
                    fdxr = this.derivative.Evaluate(temp);
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
                    fxr = this.function.Evaluate(temp);

                    fx1 = this.function.Evaluate(temp + 2 * h);   // f_x(i+2)
                    fx2 = this.function.Evaluate(temp + h);       // f_x(i+1)
                    fx3 = this.function.Evaluate(temp - h);       // f_x(i-1)
                    fx4 = this.function.Evaluate(temp - 2 * h);   // f_x(i-2)

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
            lastResult[0] = xr;
            lastResult[1] = fxr;
            lastResult[2] = error;
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
        /// <param name="tolerance">This is the tolerance you want to use, default = 1e-6</param>
        /// <param name="log">This is the list that will contain all the iterations info</param>
        public double FindRoot(string fx_function, double x0, out List<string> log, string fx_derivative = "", double h = 0.001, int maxIte = 100, double tolerance = 1e-6)
        {
            double error = 1, temp, xr = x0, fxr = 0, fdxr;

            this.function = new SimpleFunction(fx_function);
            this.derivative = new SimpleFunction(fx_derivative);

            log = new List<string>();
            log.Add("Iteration,xr,f(xr),df(xr),error");
            if (fx_derivative != "")
            {
                // When the user introduced a symbolic derivative
                for (int i = 0; i < maxIte; i++)
                {
                    temp = xr;
                    fxr = this.function.Evaluate(temp);
                    fdxr = this.derivative.Evaluate(temp);
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
                fxr = this.function.Evaluate(xr);
                for (int i = 0; i < maxIte; i++)
                {
                    temp = xr;

                    fxr = this.function.Evaluate(temp);

                    fx1 = this.function.Evaluate(temp + 2 * h);   // f_x(i+2)
                    fx2 = this.function.Evaluate(temp + h);       // f_x(i+1)
                    fx3 = this.function.Evaluate(temp - h);       // f_x(i-1)
                    fx4 = this.function.Evaluate(temp - 2 * h);   // f_x(i-2)

                    fdxr = (-fx1 + 8 * fx2 - 8 * fx3 + fx4) / (12 * h);
                    if (fdxr == 0) { log.Add("Derivative equal to zero"); break; }
                    xr = temp - (fxr / fdxr);
                    fxr = this.function.Evaluate(temp);
                    if (xr != 0)
                        error = Math.Abs((xr - temp) / xr);
                    log.Add(string.Format($"{i + 1},{xr},{fxr},{fdxr},{error}"));
                    if (fxr == 0) { log.Add("Exact root found in the last iteration"); break; }
                    if (error < tolerance) { log.Add(string.Format("Root found in the iteration #{0} with {1}% of tolerance", i + 1, tolerance * 100)); break; }
                    if (xr == temp) { log.Add("Infinite loop"); break; }
                }
            }
            lastResult[0] = xr;
            lastResult[1] = fxr;
            lastResult[2] = error;
            return xr;
        }
    }
}
