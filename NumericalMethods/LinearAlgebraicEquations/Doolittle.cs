using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethods.LinearAlgebraicEquations
{
    public class Doolittle
    {
        /// <summary>
        /// This function uses LU Decomposition to solve a system of linear equations in the form A*x = B
        /// </summary>
        /// <param name="a">Coefficient matrix</param>
        /// <param name="b">Constant terms matrix</param>
        /// <param name="x">Variable terms matrix</param>
        /// <param name="tolerance">This is the tolerance you want to use, default = 0.001 (0.1%)</param>
        public void LUDoolittle(double[,] a, double[] b, out double[] x, double tolerance = 0.001)
        {
            x = new double[b.Length];
            int n = b.Length-1;
            double[] o = new double[n + 1];
            double[] s = new double[n + 1];
            int er = 0;
            Decompose(a, n, tolerance, o, s, er);
            if (er != -1)
                SubstituteLU(a, o, n, b, x);
        }

        private void Decompose(double[,] a, int n, double tol, double[] o, double[] s, int er)
        {
            double factor;
            for (int i = 0; i <= n; i++)
            {
                o[i] = i;
                s[i] = Math.Abs(a[i, i]);
                for (int j = 1; j <= n; j++)
                {
                    if (Math.Abs(a[i, j]) < s[i])
                        s[i] = Math.Abs(a[i, j]);
                }
            }

            for (int k = 0; k <= n - 1; k++)
            {
                PivotLU(a, o, s, n, k);
                if (Math.Abs(a[Convert.ToInt32(o[k]), k] / s[Convert.ToInt32(o[k])]) < tol)
                {
                    er = -1;
                    //txtResultado.AppendText(Convert.ToString(a[Convert.ToInt32(o[k]), k] / s[Convert.ToInt32(o[k])]));
                    break;
                }
                for (int i = k + 1; i <= n; i++)
                {
                    factor = a[Convert.ToInt32(o[i]), k] / a[Convert.ToInt32(o[k]), k];
                    a[Convert.ToInt32(o[i]), k] = factor;
                    for (int j = k + 1; j <= n; j++)
                    {
                        a[Convert.ToInt32(o[i]), j] -= factor * a[Convert.ToInt32(o[k]), j];
                    }
                }
                if (Math.Abs(a[Convert.ToInt32(o[k]), k] / s[Convert.ToInt32(o[k])]) < tol)
                {
                    er = -1;
                    //txtResultado.AppendText(Convert.ToString(a[Convert.ToInt32(o[k]), k] / s[Convert.ToInt32(o[k])]));
                    break;
                }
            }
        }

        private void PivotLU(double[,] a, double[] o, double[] s, int n, int k)
        {
            int p = k;
            double big = Math.Abs(a[Convert.ToInt32(o[k]), k] / s[Convert.ToInt32(o[k])]), dummy;

            for (int i = k + 1; i <= n; i++)
            {
                dummy = Math.Abs(a[Convert.ToInt32(o[i]), k] / s[Convert.ToInt32(o[i])]);
                if (dummy > big)
                {
                    big = dummy;
                    p = i;
                }
            }
            dummy = o[p];
            o[p] = o[k];
            o[k] = dummy;
        }

        private void SubstituteLU(double[,] a, double[] o, int n, double[] b, double[] x)
        {
            double sum;
            for (int i = 1; i <= n; i++)
            {
                sum = b[Convert.ToInt32(o[i])];
                for (int j = 0; j <= i - 1; j++)
                {
                    sum -= a[Convert.ToInt32(o[i]), j] * b[Convert.ToInt32(o[j])];
                }
                b[Convert.ToInt32(o[i])] = sum;
            }
            x[n] = b[Convert.ToInt32(o[n])] / a[Convert.ToInt32(o[n]), n];

            for (int i = n - 1; i >= 0; i--)
            {
                sum = 0;
                for (int j = i + 1; j <= n; j++)
                {
                    sum += a[Convert.ToInt32(o[i]), j] * x[j];
                }
                x[i] = (b[Convert.ToInt32(o[i])] - sum) / a[Convert.ToInt32(o[i]), i];
            }
        }
    }
}
