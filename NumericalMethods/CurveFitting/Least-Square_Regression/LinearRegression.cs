using System;
using System.Collections.Generic;
using System.Text;

namespace NumericalMethods.CurveFitting.Least_Square_Regression
{
    public class LinearRegression
    {
        //public double[] Solve()
    }

    //public static void AjCurMinCua(ComboBox cbxMetodos, double[,] SEL, double[,] xy, int n, TextBox txtResultado, TextBox txtDatosf)
    //{
    //    double zx2, zx, zy, zxy, zx3, zx4, zx2y; int k = 0;
    //    zx2 = 0; zx = 0; zy = 0; zxy = 0; zx3 = 0; zx4 = 0; zx2y = 0; k = 0;

    //    for (int i = 0; i < n; i++)
    //    {
    //        zx += xy[0, i]; zx2 += Math.Pow(xy[0, i], 2); zx3 += Math.Pow(xy[0, i], 3); zx4 += Math.Pow(xy[0, i], 4);
    //        zy += xy[1, i]; zxy += (xy[0, i] * xy[1, i]); zx2y += (Math.Pow(xy[0, i], 2) * xy[1, i]);
    //    }
    //    switch (cbxMetodos.SelectedIndex)
    //    {
    //        case 0:
    //            // Lineal
    //            SEL = new double[2, 3]; k = 2;
    //            SEL[1, 1] = n;
    //            SEL[0, 0] = zx2; SEL[0, 1] = zx; SEL[1, 0] = zx; SEL[0, 2] = zxy; SEL[1, 2] = zy;
    //            break;
    //        case 1:
    //            // Cuadratica
    //            SEL = new double[3, 4]; k = 3;
    //            SEL[0, 0] = n;
    //            SEL[0, 1] = zx; SEL[1, 0] = zx; SEL[0, 1] = zx; SEL[0, 2] = zx2; SEL[1, 1] = zx2; SEL[2, 0] = zx2; SEL[1, 2] = zx3; SEL[2, 1] = zx3; SEL[2, 2] = zx4;
    //            SEL[0, 3] = zy; SEL[1, 3] = zxy; SEL[2, 3] = zx2y;
    //            break;
    //    }

    //    // Invoca el método para resolver el SEL
    //    double[] r = new double[k]; // Arreglo donde se guardarán los resultados
    //    if (CResolverSEL.EliminacionGaussiana(SEL, r))
    //    {
    //        switch (cbxMetodos.SelectedIndex)
    //        {
    //            case 0:
    //                // Lineal
    //                txtResultado.Text = r[0].ToString() + "x + (" + r[1].ToString() + ")";
    //                double sr = 0, st = 0, r2, syx;
    //                for (int i = 0; i < n; i++)
    //                {
    //                    st += Math.Pow(xy[1, i] - zy / n, 2);
    //                    sr += Math.Pow(xy[1, i] - r[0] * xy[0, i] - r[1], 2);
    //                }
    //                syx = Math.Sqrt(sr / (n - 2));
    //                r2 = (st - sr) / st;
    //                txtDatosf.AppendText(string.Format("{0}  |  {1}  |  {2}", syx, r2, Math.Sqrt(r2)));
    //                break;
    //            case 1:
    //                // Cuadratica
    //                txtResultado.Text = r[2].ToString() + "x^2 + (" + r[1].ToString() + ")x + (" + r[0].ToString() + ")";
    //                break;
    //        }
    //    }
    //    else
    //    {
    //        txtResultado.Clear();
    //        txtResultado.AppendText("No es un sistema de ecuaciones lineales");
    //    }
    //}

}
