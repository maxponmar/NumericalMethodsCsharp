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
    }
}

//                        case 2: // Runge-Kutta
//                            for (int i = 0; i<n; i++)
//                            {
//                                eqnDiferencial.ProgrammaticallyParse("let x =" + x.ToString());
//                                eqnDiferencial.ProgrammaticallyParse("let y =" + y.ToString());
//                                k1 = h* eqnDiferencial.Parse(ecuacion);
//eqnDiferencial.ProgrammaticallyParse("let x =" + (x + (h / 2)).ToString());
//                                eqnDiferencial.ProgrammaticallyParse("let y =" + (y + (k1 / 2)).ToString());
//                                k2 = h* eqnDiferencial.Parse(ecuacion);
//eqnDiferencial.ProgrammaticallyParse("let x =" + (x + (h / 2)).ToString());
//                                eqnDiferencial.ProgrammaticallyParse("let y =" + (y + (k2 / 2)).ToString());
//                                k3 = h* eqnDiferencial.Parse(ecuacion);
//eqnDiferencial.ProgrammaticallyParse("let x =" + (x + (h / 2)).ToString());
//                                eqnDiferencial.ProgrammaticallyParse("let y =" + (y + (k3 / 2)).ToString());
//                                k4 = h* eqnDiferencial.Parse(ecuacion);
//k = (1.0 / 6.0) * (k1 + (2 * k2) + (2 * k3) + k4);
//                                y += k; x += h;
//                                txtResultado.AppendText("y" + (i + 1).ToString() + "= " + y.ToString() + Environment.NewLine);
//                            }
//                            break;
//                    }
