using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace NumericalMethodsLibrary.RootFinding
{
    /// <summary>
    /// The original algorithm was writen by lebourhisgilles : https://workshop.numworks.com/python/lebourhisgilles
    /// I only take it from pytho to C#, here is the link of the code: https://workshop.numworks.com/python/lebourhisgilles/bairstow
    /// </summary>
    public class Bairstow
    {
        private List<string> results;
        /// <summary>
        /// This List save the last result
        /// </summary>
        public List<string> Results { get => results; }        

        /// <summary>
        /// The Bairstow methos only works with polinomials, it return all (real or complex) roots. This function returns a List(string) wiht the roots
        /// </summary>
        /// <param name="a">This is the polinomial "a0 + a1*x + a2 * x^2 + .. + a(n+1) * x^n" (n = polinomial's degree) given as a c# array [a0,a1,a2,...,a(n-1)]</param>
        public List<string> FindRoot(double[] a)
        {
            results = new List<string>();
            int g = a.Length - 1;
            Random random = new Random(Environment.TickCount);
            double r = random.NextDouble();
            double s = random.NextDouble();
            Bairstow_algorithm(a, r, s, g);
            return Results;
        }

        private int Bairstow_algorithm(double[] a, double r, double s, int g)
        {
            Complex x1, x2 = new Complex();
            if (g < 1)
                return 0;

            if (g == 1 && a[1] != 0)
            {
                Results.Add((-a[0] / a[1]).ToString());
                return 0;
            }

            if (g == 2)
            {
                double d = (a[1] * a[1]) - (4 * a[2] * a[0]);
                if (d < 0)
                {
                    x1 = new Complex(-a[1] / (2 * a[2]), -1 * Math.Sqrt(-d) / (2 * a[2]));
                    x2 = new Complex(-a[1] / (2 * a[2]), Math.Sqrt(-d) / (2 * a[2]));
                }
                else
                {
                    x1 = (-a[1] - Math.Sqrt(d)) / (2 * a[2]);
                    x2 = (-a[1] + Math.Sqrt(d)) / (2 * a[2]);
                }
                Results.Add(x1.ToString());
                Results.Add(x2.ToString());
                return 0;
            }
            int n = a.Length; int i = n - 3;
            double[] b = new double[a.Length];
            double[] c = new double[a.Length];
            b[n - 1] = a[n - 1];
            b[n - 2] = a[n - 2] + r * b[n - 1];
            while (i >= 0)
            {
                b[i] = a[i] + r * b[i + 1] + s * b[i + 2];
                i = i - 1;
            }
            c[n - 1] = b[n - 1];
            c[n - 2] = b[n - 2] + r * c[n - 1];
            i = n - 3;
            while (i >= 0)
            {
                c[i] = b[i] + r * c[i + 1] + s * c[i + 2];
                i = i - 1;
            }
            double Din = Math.Pow((c[2] * c[2]) - (c[3] * c[1]), -1.0);
            r += Din * ((c[2]) * (-b[1]) + (-c[3]) * (-b[0]));
            s += Din * ((-c[1]) * (-b[1]) + (c[2]) * (-b[0]));
            if (Math.Abs(b[0]) > 1e-14 || Math.Abs(b[1]) > 1e-14)
            {
                return Bairstow_algorithm(a, r, s, g);
            }
            if (g >= 3)
            {
                double dis = (Math.Pow(-r, 2.0)) - (4 * 1 * -s);
                if (dis < 0)
                {
                    x1 = new Complex(r / 2, -1 * Math.Sqrt(-dis) / 2);
                    x2 = new Complex(r / 2, Math.Sqrt(-dis) / 2);
                }
                else
                {
                    x1 = (r - Math.Sqrt(dis)) / 2;
                    x2 = (r + Math.Sqrt(dis)) / 2;
                }
                Results.Add(x1.ToString());
                Results.Add(x2.ToString());
                double[] temp = new double[b.Length - 2];

                for (int j = 2; j < b.Length; j++)
                {
                    temp[j - 2] = b[j];
                }

                return Bairstow_algorithm(temp, r, s, g - 2);
            }
            return 0;
        }
    }
}
