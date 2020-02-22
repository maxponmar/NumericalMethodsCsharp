using System;

namespace NumericalMethods.LinearAlgebraicEquations
{
    public class GaussElimination
    {
        /// <summary>
        /// This function uses Gauss Elimination to solve a system of linear equations in the form A*x = B
        /// </summary>
        /// <param name="a">Coefficient matrix</param>
        /// <param name="b">Constant terms matrix</param>
        /// <param name="x">Variable terms matrix</param>
        /// <param name="tolerance">This is the tolerance you want to use, default = 0.001 (0.1%)</param>
        public void Solve(double[,]a, double[] b, out double[] x, double tolerance = 0.001)
        {
            x = new double[b.Length];
            GaussElimination_Algorithm(a, b, x, tolerance);
        }
        private void GaussElimination_Algorithm(double[,] a, double[] b,double[] x, double tol)
        {
            int n = b.Length - 1;
            double[] s = new double[n + 1];
            int er = 0;
            for (int i = 0; i <= n; i++)
            {
                s[i] = Math.Abs(a[i, 0]);
                for (int j = 1; j <= n; j++)
                {
                    if (Math.Abs(a[i, j]) > s[i])
                        s[i] = Math.Abs(a[i, j]);
                }
            }
            Eliminate(a, s, n, b, tol, er);
            if (er != -1)
            {
                Suvbstitute(a, n, b, x);
            }
        }

        private void Eliminate(double[,] a, double[] s, int n, double[] b, double tol, int er)
        {
            double factor;
            for (int k = 0; k <= n - 1; k++)
            {
                Pivot(a, b, s, n, k);
                if (Math.Abs(a[k, k] / s[k]) < tol)
                {
                    er = -1;
                    break;
                }

                for (int i = k + 1; i <= n; i++)
                {
                    factor = a[i, k] / a[k, k];
                    for (int j = k + 1; j <= n; j++)
                    {
                        a[i, j] -= factor * a[k, j];
                    }
                    b[i] -= factor * b[k];
                }
            }
            if (Math.Abs(a[n, n] / s[n]) < tol)
                er = -1;
        }

        private void Pivot(double[,] a, double[] b, double[] s, int n, int k)
        {
            int p = k;
            double big = Math.Abs(a[k, k] / s[k]), dummy;

            for (int i = k + 1; i <= n; i++)
            {
                dummy = Math.Abs(a[i, k] / s[i]);
                if (dummy > big)
                {
                    big = dummy;
                    p = i;
                }
            }

            if (p != k)
            {
                for (int j = k; j <= n; j++)
                {
                    dummy = a[p, j];
                    a[p, j] = a[k, j];
                    a[k, j] = dummy;
                }
                dummy = b[p];
                b[p] = b[k];
                b[k] = dummy;
                dummy = s[p];
                s[p] = s[k];
                s[k] = dummy;
            }
        }

        private void Suvbstitute(double[,] a, int n, double[] b, double[] x)
        {
            double sum;
            x[n] = b[n] / a[n, n];
            for (int i = n - 1; i >= 0; i--)
            {
                sum = 0;
                for (int j = i; j <= n; j++)
                {
                    sum += a[i, j] * x[j];
                }
                x[i] = (b[i] - sum) / a[i, i];
            }
        }
    }
}
