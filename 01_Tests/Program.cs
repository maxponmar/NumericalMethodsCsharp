using System;
using System.IO;
using System.Collections.Generic;

//using NumericalMethods.RootFinding;
using NumericalMethods.LinearAlgebraicEquations;
namespace _01_PruebasNumericalMethods
{
    class Program
    {
        static public void list2csv(List<string> list)
        {
            File.WriteAllLines(@"D:\Documentos\00_Pruebas\xd.csv", list.ToArray());
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<string> log = new List<string>();

            //Muller rf = new Muller();
            //double r = rf.FindRoot("x^3 - 10", 2, out log);
            //Console.WriteLine(r);

            //Bairstow b = new Bairstow();
            //log = b.FindRoot(new double[] { 1, 2, 3, 4 });

            ThomasLU sel = new ThomasLU();

            //double[,] a = new double[,] { {8, 9 }, { 1, 4 } };
            //double[] b = new double[] { 6, 2 };

            double[] x = new double[3];
            double[] f = new double[] { 1, 1, 1 };
            double[] g = new double[] { 5, 5, 0 };
            double[] e = new double[] { 0, 6, 6 };
            double[] r = new double[] { 1, 2, 3 };

            sel.SolveTridiagonalSystem(f, g, e, r, out x);

            foreach (double i in x)
            {
                Console.WriteLine(i);
            }

            //foreach (string x in log)
            //{
            //    Console.WriteLine(x);
            //}

            //list2csv(log);
            Console.ReadKey();
        }
    }
}
