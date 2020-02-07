using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethods.CurveFitting.Interpolation
{
    public class NewtonsDividedDifference
    {
        /// <summary>
        /// This method fits the given data with polynomial regression method, limited to a 2nd degree polynomial (Return a Polynomial object)
        /// </summary>
        /// <param name="x">Independent data</param>
        /// <param name="y">Dependent data</param>
        /// <returns>Returns a polynomial of degree 2</returns>
        public Polynomial Fit(double[] x, double[] y)
        {
            return new Polynomial(NDD(x,y));
        }

        private static double[] NDD(double[] X, double[] Y)
        {
            if (X.Length == Y.Length)
            {
                int m = X.Length;
                double[] FX, Bn;
                double[,] polinomio;
                double[,] T;
                int A = m, B = 0, C, D;
                C = A - 1;
                T = new double[(A - 1), (A - 1)]; //obtener diferencias divididas
                while (B <= (C - 1))
                {
                    T[B, 0] = ((Y[B + 1] - Y[B]) / (X[B + 1] - X[B]));
                    B += 1;
                }
                D = 1;
                while (D <= (C - 1))
                {
                    B = D;
                    while (B <= (C - 1))
                    {
                        T[B, D] = ((T[B, (D - 1)] - T[(B - 1), (D - 1)]) / (X[B + 1] - X[B - D]));
                        B += 1;
                    }
                    D += 1;
                }
                Bn = new double[m]; //acomodar difrencias divididas en matriz Bn
                for (int i = 0; i < m; i++)
                {
                    if (i == 0)
                    {
                        Bn[0] = Y[0];
                    }
                    else
                    {
                        Bn[i] = T[(i - 1), (i - 1)];
                    }
                }
                // crear matriz donde se guarden los polinomios

                polinomio = new double[m, m];// polinomio de Newton;
                double[] p = new double[m + 1];
                p[0] = -X[0]; p[1] = 1;//p = (x-x1)

                polinomio[0, 0] = Bn[0]; // primera fila
                polinomio[1, 0] = -X[0]; polinomio[1, 1] = 1;// segunda fila
                for (int i = 2; i < m; i++) // filas que quedan
                {
                    var b = new double[] { -X[i - 1], 1 };
                    p = Multiply(p, b);
                    for (int j = 0; j < m; j++)
                    {
                        polinomio[i, j] = p[j];
                    }
                }

                for (int i = 1; i < m; i++) // multiplicar las filas por bn
                {
                    for (int j = 0; j < m; j++)
                    {
                        polinomio[i, j] *= Bn[i];
                    }
                }
                FX = new double[m];
                for (int i = 0; i < m; i++) // sumar colmnas para obtener ecuacion final
                {
                    for (int j = 0; j < m; j++)
                    {
                        FX[i] += polinomio[j, i];
                    }
                }
                return FX;
            }
            return new double[0];
        }
        private static double[] Multiply(double[] a, double[] b)
        {
            var result = new double[a.Length + b.Length - 1];
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    result[i + j] += a[i] * b[j];
                }
            }
            return result;
        }
    }
}
