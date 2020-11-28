using NumericalMethodsLibrary.MathObjects.PolynomialClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethodsLibrary.CurveFitting.Interpolation
{
    public class SplinesInterpolation
    {
        private Dictionary<double[], Polynomial> _Splines = new Dictionary<double[], Polynomial>();

        public Dictionary<double[], Polynomial> Splines { get => Splines; }

        /// <summary>
        /// This method fits the given data using Splines, you can choose 1st, 2nd or 3th degree
        /// The result will be stored in "Splines" Directory (object attribute)
        /// The values are polynomials of 1st, 2nd or 3th degree and the Keys are their respective limits
        /// You can print spline object to get an idea of the explanation above
        /// </summary>
        /// <param name="x">Independent data</param>
        /// <param name="y">Dependent data</param>
        /// <param name="type">Choose the degree (3 by default)</param>
        public void Fit(double[] x, double[] y, int type = 3)
        {
            if (x.Length == y.Length && x.Length > 1)
            {
                // First, clear splines list
                this._Splines.Clear();
                switch (type)
                {
                    case 1:
                        #region 1st degree
                        // It only interpolate linearly 2 values
                        int nsplines = x.Length - 1;
                        double[] _x, _y;
                        for (int i = 0; i < nsplines; i++)
                        {
                            _x = new double[] { x[i], x[i + 1] };
                            _y = new double[] { y[i], y[i + 1] };
                            Lagrange lagrange = new Lagrange();
                            // Adding splines
                            this._Splines.Add(new double[] { x[i], x[i + 1] }, lagrange.Fit(_x, _y));
                        }
                        break;
                    #endregion
                    case 2:
                        #region 2nd degree                        
                        int nSplines = x.Length - 1, variables = 3 * (x.Length - 1), nEqn1 = 2 * (x.Length - 1), nEqn2 = x.Length - 2;
                        int index1 = 0, Pot, temp1, temp2, step, column;
                        double[,] SELs2, eqn1s2, eqn2s2;
                        SELs2 = new double[variables, variables + 1];
                        eqn1s2 = new double[nEqn1, variables];
                        eqn2s2 = new double[nEqn2, variables];
                        // Get first equation
                        temp1 = 0; step = 0; Pot = 2; temp2 = 0; column = 0;
                        for (int i = 0; i < nEqn1; i++)
                        {
                            if (i == 0) { index1 = 0; }
                            if (i == (nEqn1 - 1)) { index1 = x.Length - 1; temp1 = -1; }
                            if (step == 2) { step = 0; temp1++; }
                            if (i > 0 && i < nEqn1 - 1) { index1 = 1 + temp1; step++; }
                            for (int j = 0; j < 3; j++)
                            {
                                eqn1s2[i, (j + column)] = Math.Pow((x[index1]), Pot);
                                Pot--;
                                if (j == 2) { temp2++; Pot = 2; }
                                if (temp2 == 2) { column += 3; temp2 = 0; }
                            }
                            index1++;
                        }
                        // Get 2nd equation
                        Pot = 1; column = 0; index1 = 1;
                        for (int i = 0; i < nEqn2; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                eqn2s2[i, (j + column)] = (Pot + 1) * Math.Pow((x[index1]), Pot);
                                eqn2s2[i, (j + column + 3)] = -1 * (Pot + 1) * Math.Pow((x[index1]), Pot);
                                Pot--;
                                if (j == 2) { Pot = 1; column += 3; }
                            }
                            index1++;
                        }
                        //==========CREATE LINEAR EQUATIONS SYSTEM====================//
                        for (int i = 0; i < variables; i++)
                        {
                            for (int j = 0; j < variables; j++)
                            {
                                if (i < nEqn1) { SELs2[i, j] = eqn1s2[i, j]; }
                                if (i >= nEqn1 && i < variables - 1) { SELs2[i, j] = eqn2s2[i - nEqn1, j]; }
                            }
                        }
                        SELs2[variables - 1, 0] = 1;
                        // Fill response column (y)
                        temp1 = 0; step = 0;
                        for (int i = 0; i < nEqn1; i++)
                        {
                            if (i == 0) { index1 = 0; }
                            if (i == (nEqn1 - 1)) { index1 = x.Length - 1; temp1 = -1; }
                            if (step == 2) { step = 0; temp1++; }
                            if (i > 0 && i < nEqn1 - 1) { index1 = 1 + temp1; step++; }
                            SELs2[i, variables] = y[index1];    //m2y[z, 0]
                        }
                        double[] rs2 = new double[variables];
                        if (GaussianElimination(SELs2, rs2))
                        {
                            rs2[0] = 0;
                            double[] coef = new double[3];
                            int count = 0;
                            for (int i = 0; i < rs2.Length / 3; i++)
                            {
                                for (int j = 0; j < 3; j++)
                                {                                    
                                    coef[j] = rs2[3 * (1 + i) - j - 1];                                    
                                    count++;
                                }                                
                                _Splines.Add(new double[] { x[i], x[i + 1] }, new Polynomial(coef));
                                coef = new double[3];
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: Can't solve for this dataset. System of linear equations can't be solved");
                        }
                        break;
                    #endregion
                    case 3:
                        #region 3th degree
                        int nsx, nsx1, q, k, w, t, z = 0, h, o;
                        double[,] eqn1, eqn2, eqn3, SEL1;
                        w = (x.Length - 1) * 4; nsx = (x.Length - 1) * 2; nsx1 = x.Length - 2;
                        eqn1 = new double[nsx, w]; eqn2 = new double[nsx1, w]; eqn3 = new double[nsx1, w];
                        SEL1 = new double[w, w + 1];
                        // Get first equation
                        h = 0; o = 0; k = 3; t = 0; q = 0;
                        for (int i = 0; i < nsx; i++)
                        {
                            if (i == 0) { z = 0; }
                            if (i == (nsx - 1)) { z = x.Length - 1; h = -1; }
                            if (o == 2) { o = 0; h++; }
                            if (i > 0 && i < nsx - 1) { z = 1 + h; o++; }
                            for (int j = 0; j < 4; j++)
                            {
                                eqn1[i, (j + q)] = Math.Pow((x[z]), k);     //m1x[z, 0]
                                k--;
                                if (j == 3) { t++; k = 3; }
                                if (t == 2) { q += 4; t = 0; }
                            }
                        }
                        // Get second equation
                        k = 2; q = 0; z = 1;
                        for (int i = 0; i < nsx1; i++)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                eqn2[i, (j + q)] = (k + 1) * Math.Pow((x[z]), k); //m1x[z, 0]
                                eqn2[i, (j + q + 4)] = -1 * (k + 1) * Math.Pow((x[z]), k); //m1x[z, 0]
                                k--;
                                if (j == 3) { k = 2; q += 4; }
                            }
                            z++;
                        }
                        // Get third equation 
                        k = 1; q = 0; z = 1; h = 5;
                        for (int i = 0; i < nsx1; i++)
                        {
                            for (int j = 0; j < 4; j++)
                            {

                                eqn3[i, (j + q)] = (k + h) * Math.Pow((x[z]), k); // m1x[z, 0]
                                eqn3[i, (j + q + 4)] = -1 * (k + h) * Math.Pow((x[z]), k); //m1x[z, 0]
                                k--;
                                if (k == 0) { h = 2; }
                                if (k < 0) { h = -1 * k; }
                                if (j == 3) { k = 1; q += 4; h = 5; }
                            }
                            z++;
                        }

                        //==========CREATE LINEAR EQUATIONS SYSTEM====================//
                        h = o;
                        for (int i = 0; i < w; i++)
                        {
                            for (int j = 0; j < w; j++)
                            {
                                if (i < nsx) { SEL1[i, j] = eqn1[i, j]; }

                                if (i >= nsx && i < (nsx + nsx1)) { h = nsx; SEL1[i, j] = eqn2[i - h, j]; }

                                if (i >= (nsx + nsx1) && i < (nsx + (nsx1 * 2))) { h = nsx + nsx1; SEL1[i, j] = eqn3[i - h, j]; }

                            }
                        }
                        SEL1[w - 2, 0] = 6 * x[0]; SEL1[w - 2, 1] = 2; //m1x[0, 0]
                        SEL1[w - 1, w - 4] = 6 * x[x.Length - 1]; SEL1[w - 1, w - 3] = 2;//m1x[n - 1, 0]
                        h = 0; o = 0; k = 3; t = 0;
                        for (int i = 0; i < nsx; i++)
                        {
                            if (i == 0) { z = 0; }
                            if (i == (nsx - 1)) { z = x.Length - 1; h = -1; }
                            if (o == 2) { o = 0; h++; }
                            if (i > 0 && i < nsx - 1) { z = 1 + h; o++; }
                            SEL1[i, w] = y[z];//m2y[z, 0]
                        }
                        double[] r = new double[w];
                        if (GaussianElimination(SEL1, r))
                        {
                            double[] coef = new double[4];
                            int count = 0;
                            for (int i = 0; i < r.Length / 4; i++)
                            {
                                for (int j = 0; j < 4; j++)
                                {                                    
                                    coef[j] = r[4 * (1 + i) - j - 1];
                                    count++;
                                }                                
                                _Splines.Add(new double[] { x[i], x[i + 1] }, new Polynomial(coef));
                                coef = new double[4];
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: Can't solve for this dataset. System of linear equations can't be solved");
                        }
                        break;
                        #endregion
                }
            }
            else
            {
                Console.WriteLine("The dataset is not compatible -> x.Length and y.Length must be the same and greater than 1");
            }
        }        

        public override string ToString()
        {
            string splines = "";

            foreach (KeyValuePair<double[], Polynomial> spl in _Splines)
            {
                splines += string.Format(spl.Value + $" x€[{spl.Key[0]},{spl.Key[1]}]\n");
            }
            return splines;
        }

        public double Evaluate(double x)
        {
            double result = 0;
            int count = 0;
            foreach (KeyValuePair<double[], Polynomial> spl in _Splines)
            {
                if (x >= spl.Key[0] && x <= spl.Key[1] || (count == 0 && x <= spl.Key[0]) || (count == _Splines.Count && x >= spl.Key[0]))
                {
                    result = spl.Value.Evaluate(x);
                }
                count++;
            }
            return result;
        }

        private static bool GaussianElimination(double[,] a, double[] r)
        {
            double t, s;
            int i, l, j, k, m, n;
            try
            {
                n = r.Length - 1;
                m = n + 1;
                for (l = 0; l <= n - 1; l++)
                {
                    j = l;
                    for (k = l + 1; k <= n; k++)
                    {
                        if (!(Math.Abs(a[j, l]) >= Math.Abs(a[k, l]))) j = k;
                    }
                    if (!(j == l))
                    {
                        for (i = 0; i <= m; i++)
                        {
                            t = a[l, i];
                            a[l, i] = a[j, i];
                            a[j, i] = t;
                        }
                    }
                    for (j = l + 1; j <= n; j++)
                    {
                        t = (a[j, l] / a[l, l]);
                        for (i = 0; i <= m; i++) a[j, i] -= t * a[l, i];
                    }
                }
                r[n] = a[n, m] / a[n, n];
                for (i = 0; i <= n - 1; i++)
                {
                    j = n - i - 1;
                    s = 0;
                    for (l = 0; l <= i; l++)
                    {
                        k = j + l + 1;
                        s += a[j, k] * r[k];
                    }
                    r[j] = ((a[j, m] - s) / a[j, j]);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
