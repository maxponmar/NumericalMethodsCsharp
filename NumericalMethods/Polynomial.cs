using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NumericalMethods
{
    public class Polynomial : ICloneable
    {
        private int degree;
        private double[] coeficients;

        public int Degree { get => degree; }
        public double[] Coeficients
        {
            get => coeficients;
            set
            {
                coeficients = value;
                degree = coeficients.Length - 1;
                //deleteZeros();
            }
        }

        /// <summary>
        /// Give the coefficients in ascending order a+bx+cx^2.../>
        /// </summary>
        /// <param name="coefficients">A double array with the coefficients in ascending order</param>
        public Polynomial(double[] coefficients)
        {
            this.coeficients = coefficients;
            degree = coeficients.Length - 1;
        }

        /// <summary>
        /// Enter a value to evaluate the polynomial
        /// </summary>
        /// <param name="x">variable</param>
        /// <returns>The poylnomial evaluated in x</returns>
        public double Eval(double x)
        {
            double res = coeficients[0];
            for (int i = 1; i < coeficients.Length; i++)
            {
                res += Math.Pow(x, i) * coeficients[i];
            }
            return res;
        }

        /// <summary>
        /// This method deletesd the last zeros of the polynomial
        /// </summary>
        public void deleteLastZeros()
        {
            if (coeficients.Length - 1 > -1)
            {
                if (coeficients[coeficients.Length - 1] == 0)
                {
                    Array.Resize(ref coeficients, coeficients.Length - 1);
                    degree -= 1;
                    //deleteZeros();
                }
            }
        }

        public object Clone()
        {
            //return (Polynomial)this;
            return this.MemberwiseClone();
        }
        public static Polynomial operator +(double x, Polynomial p)
        {
            double[] coef = (double[])p.coeficients.Clone();
            coef[0] += x;
            return new Polynomial(coef);
        }
        public static Polynomial operator +(Polynomial p, double x)
        {
            double[] coef = (double[])p.coeficients.Clone();
            coef[0] += x;
            return new Polynomial(coef);
        }
        public static Polynomial operator +(Polynomial p1, Polynomial p2)
        {
            double[] newCoef = new double[] { };
            if (p1.degree > p2.degree)
            {
                newCoef = new double[p1.degree + 1];
                for (int i = 0; i < p1.degree + 1; i++)
                {
                    if (i <= p2.degree)
                    {
                        newCoef[i] = p1.coeficients[i] + p2.coeficients[i];
                    }
                    else
                    {
                        newCoef[i] = p1.coeficients[i];
                    }
                }
            }
            if (p1.degree < p2.degree)
            {
                newCoef = new double[p2.degree + 1];
                for (int i = 0; i < p2.degree + 1; i++)
                {
                    if (i <= p1.degree)
                    {
                        newCoef[i] = p1.coeficients[i] + p2.coeficients[i];
                    }
                    else
                    {
                        newCoef[i] = p2.coeficients[i];
                    }
                }
            }
            if (p1.degree == p2.degree)
            {
                newCoef = new double[p1.degree + 1];
                for (int i = 0; i < p1.degree + 1; i++)
                {
                    newCoef[i] = p1.coeficients[i] + p2.coeficients[i];
                    //Console.WriteLine(newCoef[i]);
                }
            }
            return new Polynomial(newCoef);
        }
        public static Polynomial operator -(Polynomial p1, Polynomial p2)
        {
            double[] newCoef = new double[] { };
            if (p1.degree > p2.degree)
            {
                newCoef = new double[p1.degree + 1];
                for (int i = 0; i < p1.degree + 1; i++)
                {
                    if (i <= p2.degree)
                    {
                        newCoef[i] = p1.coeficients[i] - p2.coeficients[i];
                    }
                    else
                    {
                        newCoef[i] = -p1.coeficients[i];
                    }
                }
            }
            if (p1.degree < p2.degree)
            {
                newCoef = new double[p2.degree + 1];
                for (int i = 0; i < p2.degree + 1; i++)
                {
                    if (i <= p1.degree)
                    {
                        newCoef[i] = p1.coeficients[i] - p2.coeficients[i];
                    }
                    else
                    {
                        newCoef[i] = -p2.coeficients[i];
                    }
                }
            }
            if (p1.degree == p2.degree)
            {
                newCoef = new double[p1.degree + 1];
                for (int i = 0; i < p1.degree + 1; i++)
                {
                    newCoef[i] = p1.coeficients[i] - p2.coeficients[i];
                    //Console.WriteLine(newCoef[i]);
                }
            }

            return new Polynomial(newCoef);
        }
        public static Polynomial operator -(Polynomial p)
        {
            double[] newCoef = (double[])p.coeficients.Clone();
            for (int i = 0; i < newCoef.Length; i++)
            {
                newCoef[i] *= -1;
            }
            return new Polynomial(newCoef);
        }
        public static Polynomial operator *(Polynomial p1, Polynomial p2)
        {
            int len = p1.coeficients.Length + p2.coeficients.Length - 1;
            double[] newCoef = new double[len];

            for (int i = 0; i < p1.coeficients.Length; i++)
            {
                for (int j = 0; j < p2.coeficients.Length; j++)
                    newCoef[i + j] += p1.coeficients[i] * p2.coeficients[j];
            }
            return new Polynomial(newCoef);
        }
        public static Polynomial operator *(Polynomial p, double m)
        {
            double[] newCoef = (double[])p.coeficients.Clone();
            for (int i = 0; i < newCoef.Length; ++i)
            {
                newCoef[i] *= m;
            }
            return new Polynomial(newCoef);
        }
        public static Polynomial operator *(double m, Polynomial p)
        {
            double[] newCoef = (double[])p.coeficients.Clone();
            for (int i = 0; i < newCoef.Length; ++i)
            {
                newCoef[i] *= m;
            }
            return new Polynomial(newCoef);
        }
        public static Polynomial operator /(Polynomial p, double m)
        {
            return (1 / m) * p;
        }
        public static (Polynomial, Polynomial) operator /(Polynomial p1, Polynomial p2)
        {
            List<double> num = p1.coeficients.ToList<double>();
            List<double> den = p2.coeficients.ToList<double>();
            var (quotient, remainder) = extendedSyntheticDivision(num, den);
            double[] q = quotient.ToArray();
            double[] r = remainder.ToArray();
            return (new Polynomial(q), new Polynomial(r));
        }
        private static (List<double>, List<double>) extendedSyntheticDivision(List<double> dividend, List<double> divisor)
        {
            List<double> output = dividend.ToList();
            double normalizer = divisor[0];

            for (int i = 0; i < dividend.Count() - (divisor.Count() - 1); i++)
            {
                output[i] /= normalizer;

                double coef = output[i];
                if (coef != 0)
                {
                    for (int j = 1; j < divisor.Count(); j++)
                        output[i + j] += -divisor[j] * coef;
                }
            }

            int separator = output.Count() - (divisor.Count() - 1);
            List<double> r = output.GetRange(separator, output.Count() - separator);
            for (int i = 0; i < separator; i++)
            {
                r.Insert(0, 0);
            }
            //foreach (double x in r)
            //{
            //    Console.Write(x+" ");
            //}
            //Console.WriteLine("\nseparator = "+separator);
            //Console.WriteLine("o.count+sep = "+ (output.Count() - separator));
            return (
                output.GetRange(0, separator),
                r
            );
        }

        public override string ToString()
        {
            // If someone can help me to make this more clear and efficient I´ll apreciate it
            byte flag = 0;
            string polynomialStr = "";
            double temp = 0;
            if (coeficients.Length == 0)
            {
                return "Empty polynomial";
            }
            else
            {
                for (int i = 0; i < coeficients.Length; i++)
                {
                    if (coeficients[i] != 0)
                    {
                        if (i == 0)
                        {
                            flag = 2;
                            polynomialStr += string.Format($"{coeficients[0]}");
                        }
                        else
                        {
                            flag++;
                            if (flag == 1)
                            {
                                if (coeficients[i] < 0)
                                {
                                    if (coeficients[i] == -1)
                                        if (i == 1)
                                            polynomialStr += string.Format($"-x");
                                        else
                                            polynomialStr += string.Format($"-x^{i}");
                                    else
                                    {
                                        temp = Math.Abs(coeficients[i]);
                                        if (i == 1)
                                            polynomialStr += string.Format($"- {temp}x");
                                        else
                                            polynomialStr += string.Format($"- {temp}x^{i}");
                                    }
                                }
                                if (coeficients[i] > 0)
                                {
                                    if (coeficients[i] == 1)
                                        if (i == 1)
                                            polynomialStr += string.Format($"x");
                                        else
                                            polynomialStr += string.Format($"x^{i}");
                                    else
                                    {
                                        if (i == 1)
                                            polynomialStr += string.Format($"{coeficients[i]}x");
                                        else
                                            polynomialStr += string.Format($"{coeficients[i]}x^{i}");
                                    }
                                }
                            }
                            else
                            {
                                flag = 2;
                                if (coeficients[i] < 0)
                                {
                                    if (coeficients[i] == -1)
                                        if (i == 1)
                                            polynomialStr += string.Format($" - x");
                                        else
                                            polynomialStr += string.Format($" - x^{i}");
                                    else
                                    {
                                        temp = Math.Abs(coeficients[i]);
                                        if (i == 1)
                                            polynomialStr += string.Format($" - {temp}x");
                                        else
                                            polynomialStr += string.Format($" - {temp}x^{i}");
                                    }
                                }
                                if (coeficients[i] > 0)
                                {
                                    if (coeficients[i] == 1)
                                        if (i == 1)
                                            polynomialStr += string.Format($" + x");
                                        else
                                            polynomialStr += string.Format($" + x^{i}");
                                    else
                                    {
                                        if (i == 1)
                                            polynomialStr += string.Format($" + {coeficients[i]}x");
                                        else
                                            polynomialStr += string.Format($" + {coeficients[i]}x^{i}");
                                    }
                                }
                            }
                        }
                    }
                }
                return polynomialStr;
            }
        }
    }
}
