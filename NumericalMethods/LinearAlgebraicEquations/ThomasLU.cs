using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethods.LinearAlgebraicEquations
{
    public class ThomasLU
    {
        /// <summary>
        /// This function uses LU Decomposition to solve a tridiagonal system of linear equations in the form A*x = B
        /// </summary>
        /// <param name="f">Principal diagonal</param>
        /// <param name="g">Upper diagonal (needs an extra 0 at the end)</param>
        /// <param name="e">Lower diagonal (needs an extra 0 at the start)</param>
        /// <param name="r">Constant terms matrix</param>
        /// <param name="x">Variable term matrix</param>
        public void SolveTridiagonalSystem(double[] f, double[] g, double[] e, double[] r, out double[] x)
        {
            x = new double[r.Length];            
            
            int n = r.Length-1;
            // Descomposicion
            for (int k = 1; k <= n; k++)
            {
                e[k] /= f[k - 1];
                f[k] -= e[k] * g[k - 1];
            }
            // Sustitucion hacia adelante
            for (int k = 1; k <= n; k++)
            {
                r[k] -= e[k] * r[k - 1];
            }
            //Sustitucion hacia atras
            x[n] = r[n] / f[n];
            for (int k = n - 1; k >= 0; k--)
            {
                x[k] = (r[k] - g[k] * x[k + 1]) / f[k];
            }
        }
    }
}