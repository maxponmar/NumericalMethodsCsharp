using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethods.CurveFitting.FourierApproximation
{
    public class FourierSerie : ICloneable
    {
        // Number of armonics
        private int n;
        // First Fourier Coefficient
        private double a0;
        // Ohter Fourier Coefficients (an and bn)
        private double[] an;
        private double[] bn;
        // Frequency wn (rad/s)
        private double wn;
        public double Wn { get => wn; set => wn = value; }
        public double[] An { get => an; set => an = value; }
        public double[] Bn { get => bn; set => bn = value; }
        public double A0 { get => a0; set => a0 = value; }
        public int N { get => n; set => n = value; }

        public FourierSerie(double wn, int n)
        {
            an = new double[n];
            bn = new double[n];
            this.wn = wn;
            this.n = n;
        }

        /// <summary>
        /// This method evaluate the serie on a specific t time
        /// </summary>
        /// <param name="t">time parameter</param>
        /// <returns></returns>
        public double Eval(double t)
        {
            return 0.0;
        }
        public override string ToString()
        {
            string serie = a0.ToString() + " +";
            for (int i = 0; i < n; i++)
            {
                serie += string.Format(" ({0}*cos({1}*{2}*x)) + ({3}*sin({1}*{2}*x)) + ", Convert.ToDecimal(an[i]), Convert.ToDecimal(wn), i+1, Convert.ToDecimal(bn[i]));
            }
            return serie;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
