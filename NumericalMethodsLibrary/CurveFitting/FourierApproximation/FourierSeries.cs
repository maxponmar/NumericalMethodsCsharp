using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethodsLibrary.CurveFitting.FourierApproximation
{
    class FourierSeries
    {
        private FourierSerie fourierserie;
        /// <summary>
        /// This value save the last result
        /// </summary>
        public FourierSerie FourierSerie { get => fourierserie; }

        /// <summary>
        /// This method calculates the Fourier serie given a dataset
        /// The result is saves as an FourierSerie Object
        /// The time samples need to be equally separated (e.g. 0.1-0.2-0.3, or 1-2-3)
        /// </summary>
        /// <param name="x">Independent data</param>
        /// <param name="y">Dependent data</param>
        /// <param name="n">Number of armonics to be calculated</param>
        public FourierSerie Fit(double[] x, double[] y, int n = 3)
        {
            FourierSerie result = new FourierSerie(0, n);
            if (x.Length == y.Length && x.Length > 1)
            {
                //int N = 0, n = 0;                
                double tao, deltaT;
                deltaT = x[1] - x[0];
                // N = dataset size
                double N = x.Length;
                tao = deltaT * N;
                double Wn = 2 * Math.PI / tao;
                result.Wn = Wn;
                double zx = 0, zxcos = 0, zxsen = 0, a0;
                // X ZUM
                for (int i = 0; i < N; i++)
                    zx += x[i];

                a0 = (2.0 / N) * zx;
                // XCOS Y XSEN ZUMS                        
                for (int i = 1; i < n + 1; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        zxcos += (y[j] * Math.Cos((2.0 * Math.PI * i * x[j]) / tao));
                        zxsen += (y[j] * Math.Sin((2.0 * Math.PI * i * x[j]) / tao));
                    }
                    result.An[i - 1] = (2 / N) * zxcos;
                    result.Bn[i - 1] = (2 / N) * zxsen;
                    zxcos = 0; zxsen = 0;
                }
                result.A0 = a0 / 2;
                fourierserie = (FourierSerie)result.Clone();
                return result;
            }
            else
            {
                Console.WriteLine("The dataset is not compatible -> x.Length and y.Length must be the same and greater than 1");
                fourierserie = (FourierSerie)result.Clone();
                return result;
            }
        }
    }
}
