using System;
using System.Collections.Generic;
using System.Text;
using ReadFunction;

namespace NumericalMethods.OrdinayDifferentialEquations
{
    public class ODE_Solver
    {
        private MathParser mathParser = new MathParser();

        private double[] y;
        private int size_y;
        private double fxy;

        /// <summary>
        /// Euler's method to calculate partial derivatives
        /// </summary>
        /// <param name="function">Target function</param>
        /// <param name="initial_y">Initial conditions (y)</param>
        /// <param name="initial_x">Initial conditions (x)</param>
        /// <param name="h">Step size</param>
        /// <param name="x_boundaries">Double array (2 items) that defines the boundaries to calculate the function, for exmaple from x=0 to x=4</param>
        /// <returns></returns>
        public double[] Solve_Euler(string function, double initial_y, double initial_x, double h, double[] x_boundaries)
        {
            if (x_boundaries[0] > x_boundaries[1])
            {
                Console.WriteLine("The first item in x_boundaries needs to be less than the second one");
                return new double[] { 0 };
            }
            else
            {
                size_y = (int)((x_boundaries[1] - x_boundaries[0]) / h) + 1;
                //Console.WriteLine("from "+ x_boundaries[0] + " to " + x_boundaries[1] + " h: " + h);
                //Console.WriteLine("size: " + size_y);
                y = new double[size_y];

                y[0] = initial_y;
                for (int i = 1; i < size_y; i++)
                {
                    fxy = mathParser.Eval_function_xy(function, (i-1)*h + initial_x, y[i-1]);
                    y[i] = y[i - 1] + fxy * h;
                }
                return y;
            }           
        }

        /// <summary>
        /// Improved Euler method, you can choose the improbe degree parameter to get more precise results
        /// </summary>
        /// <param name="function">Target function</param>
        /// <param name="initial_y">Initial conditions (y)</param>
        /// <param name="initial_x">Initial conditions (x)</param>
        /// <param name="h">Step size</param>
        /// <param name="Improve_Degree">Improve degree, higher better result but more computation</param>
        /// <param name="x_boundaries">Double array (2 items) that defines the boundaries to calculate the function, for exmaple from x=0 to x=4</param>
        /// <returns></returns>
        public double[] Solve_ImprovedEuler(string function, double initial_y, double initial_x, double h, int Improve_Degree, double[] x_boundaries)
        {
            if (x_boundaries[0] > x_boundaries[1])
            {
                Console.WriteLine("The first item in x_boundaries needs to be less than the second one");
                return new double[] { 0 };
            }
            else
            {
                double temp_fxy;
                size_y = (int)((x_boundaries[1] - x_boundaries[0]) / h) + 1;
                y = new double[size_y];                
                y[0] = initial_y;                
                for (int i = 1; i < size_y; i++)
                {
                    // Predict
                    fxy = mathParser.Eval_function_xy(function, (i - 1) * h + initial_x, y[i - 1]);                    
                    y[i] = y[i - 1] + (fxy * h);                    
                    for (int j = 0; j < Improve_Degree; j++)
                    {                      
                        // Corrector
                        temp_fxy = mathParser.Eval_function_xy(function, i * h + initial_x, y[i]);                        
                        y[i] = y[i - 1] + (((fxy + temp_fxy) / 2) * h);                        
                    }                   
                }
                return y;
            }
        }

        /// <summary>
        /// Fourth order Runge-Kutta method implementation
        /// </summary>
        /// <param name="function">Target function</param>
        /// <param name="initial_y">Initial conditions (y)</param>
        /// <param name="initial_x">Initial conditions (x)</param>
        /// <param name="h">Step size</param>        
        /// <param name="x_boundaries">Double array (2 items) that defines the boundaries to calculate the function, for exmaple from x=0 to x=4</param>
        /// <returns></returns>
        public double[] Solve_RungeKutta4(string function, double initial_y, double initial_x, double h, double[] x_boundaries)
        {
            double k, k1, k2, k3, k4, x;            

            size_y = (int)((x_boundaries[1] - x_boundaries[0]) / h) + 1;
            y = new double[size_y];
            y[0] = initial_y;
            for (int i = 1; i < size_y; i++)
            {
                x = (i - 1) * h + initial_x;
                k1 = mathParser.Eval_function_xy(function, x, y[i-1]);
                k2 = mathParser.Eval_function_xy(function, x + (h / 2), y[i - 1] + (k1 / 2) * h);
                k3 = mathParser.Eval_function_xy(function, x + (h / 2), y[i - 1] + (k2 / 2) * h);
                k4 = mathParser.Eval_function_xy(function, x + h, y[i - 1] + k3 * h);               
                k = (1.0 / 6.0) * (k1 + (2 * k2) + (2 * k3) + k4) * h;
                y[i] = y[i-1] + k;                
            }
            return y;
        }

        /// <summary>
        /// Butcher's fift order RK implementation 
        /// </summary>
        /// <param name="function">Target function</param>
        /// <param name="initial_y">Initial conditions (y)</param>
        /// <param name="initial_x">Initial conditions (x)</param>
        /// <param name="h">Step size</param>        
        /// <param name="x_boundaries">Double array (2 items) that defines the boundaries to calculate the function, for exmaple from x=0 to x=4</param>
        /// <returns></returns>
        public double[] Solve_ButcherRK(string function, double initial_y, double initial_x, double h, double[] x_boundaries)
        {
            double k, k1, k2, k3, k4, k5, k6, x;

            size_y = (int)((x_boundaries[1] - x_boundaries[0]) / h) + 1;
            y = new double[size_y];
            y[0] = initial_y;
            for (int i = 1; i < size_y; i++)
            {
                x = (i - 1) * h + initial_x;                
                k1 = mathParser.Eval_function_xy(function, x, y[i - 1]);                
                k2 = mathParser.Eval_function_xy(function, x + (h / 4), y[i - 1] + (k1 / 4) * h);                
                k3 = mathParser.Eval_function_xy(function, x + (h / 4), y[i - 1] + (k1 / 8) * h + (k2 / 8) * h);                
                k4 = mathParser.Eval_function_xy(function, x + (h / 2), y[i - 1] - (k2 / 2) * h + k3 * h);                
                k5 = mathParser.Eval_function_xy(function, x + (3 * h / 4), y[i - 1] - (3 * k1 / 16) * h + (9 * k4 / 16) * h);                
                k6 = mathParser.Eval_function_xy(function, x + h, y[i - 1] - (3 * k1 / 7) * h + (2 * k2 / 7) * h + (12 * k3 / 7) * h - (12 * k4 / 7) * h + (8 * k5 / 7) * h);                
                k = (1.0 / 90.0) * (7 * k1 + 32 * k3 + 12 * k4 + 32 * k5 + 7 * k6) * h;
                y[i] = y[i - 1] + k;
            }
            return y;
        }

    }
}