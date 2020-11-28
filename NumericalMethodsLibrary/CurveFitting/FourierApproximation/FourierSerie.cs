using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethodsLibrary.CurveFitting.FourierApproximation
{
    public class FourierSerie : ICloneable
    {        
        private int numberOfArmonincs;

        // First Fourier Coefficient
        private double a0;
        
        private double[] an;
        private double[] bn;
        
        private double wn;
        public double Wn { get => wn; set => wn = value; }
        public double[] An { get => an; set => an = value; }
        public double[] Bn { get => bn; set => bn = value; }
        public double A0 { get => a0; set => a0 = value; }
        public int N { get => numberOfArmonincs; set => numberOfArmonincs = value; }

        public FourierSerie(double wn, int n)
        {
            an = new double[n];
            bn = new double[n];
            this.wn = wn;
            this.numberOfArmonincs = n;
        }

        /// <summary>
        /// This method evaluate the serie on a specific t time
        /// </summary>
        /// <param name="t">time parameter</param>
        /// <returns></returns>
        public double Eval(double t)
        {
            double result = a0;
            for (int i = 0; i < numberOfArmonincs; i++)
            {
                result += (an[i] * Math.Cos(wn * (i + 1) * t) + bn[i] * Math.Sin(wn * (i + 1) * t));
            }
            return result;
        }

        /// <summary>
        /// Note that this method round number to 4 digits, if some value is 0.000000001 for example it will be 0 and it won't be printed
        /// But to make calculations those numbers will be considered
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            double anr = 0, bnr = 0, wr = 0;
            string serie = Math.Round(a0, 3) + " ";
            for (int i = 0; i < numberOfArmonincs; i++)
            {
                anr = Math.Round(an[i], 4);
                bnr = Math.Round(bn[i], 4);
                wr = Math.Round(wn * (i + 1), 4);

                // an's                
                if (anr < 0)
                {
                    if (anr == -1)
                        serie += string.Format("- cos({0}x) ", wr);
                    else
                        serie += string.Format("- {0}cos({1}x) ", Math.Abs(anr), wr);
                }
                if (anr > 0)
                {
                    if (anr == 1)
                        serie += string.Format("+ cos({0}x) ", wr);
                    else
                        serie += string.Format("+ {0}cos({1}x) ", anr, wr);
                }

                // bn's
                if (bnr < 0)
                {
                    if (bnr == -1)
                        serie += string.Format("- sin({0}x) ", wr);
                    else
                        serie += string.Format("- {0}sinX({1}x) ", Math.Abs(bnr), wr);
                }
                if (bnr > 0)
                {
                    if (bnr == 1)
                        serie += string.Format("+ sin({0}x) ", wr);
                    else
                        serie += string.Format("+ {0}sin({1}x) ", bnr, wr);
                }                
            }
            return serie;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
