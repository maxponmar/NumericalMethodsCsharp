using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NumericalMethodsLibrary
{
    public static class CSV_Tools
    {
        /// <summary>
        /// Convert a List(String) into a csv file given its path
        /// </summary>
        /// <param name="list">List you want to convert</param>
        /// <param name="path">Path to save csv file, include its name e.g. @"C:\Documents\test.csv" </param>
        static public void listToCSV(List<string> list, string path)
        {
            File.WriteAllLines(path, list.ToArray());
        }


        /// <summary>
        /// Convert a csv file into a List(string)
        /// </summary>
        /// <param name="path">Path of the csv file, include its name e.g. @"C:\Documents\test.csv"</param>
        /// <returns></returns>
        static public List<string> csvToList(string path)
        {
            try
            {
                List<string> result = new List<string>();
                string datos = File.ReadAllText(path).Replace("\r", "");
                string[] fila = datos.Split(Environment.NewLine.ToCharArray());
                for (int i = 0; i < fila.Length; i++)
                {
                    result.Add(fila[i]);
                }
                return result;
            }
            catch (Exception e)
            {
                // TODO: Implemebt thorw exception
                throw;
            }
        }

        /// <summary>
        /// Convert a csv file into a double[,] array
        /// </summary>
        /// <param name="path">Path of the csv file, including its name e.g. @"C:\Documents\test.csv"</param>
        /// <returns></returns>
        static public double[,] csvToDoubleArray(string path)
        {
            try
            {
                string datos = File.ReadAllText(path).Replace("\r", "");
                string[] fila = datos.Split(Environment.NewLine.ToCharArray());
                string[] filaDatos = fila[0].Split(',');
                double[,] result = new double[fila.Length, filaDatos.Length];
                for (int i = 0; i < fila.Length; i++)
                {
                    filaDatos = fila[i].Split(',');
                    for (int j = 0; j < filaDatos.Length; j++)
                    {
                        result[i, j] = Convert.ToDouble(filaDatos[j]);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                // TODO: Implement throw exception
                throw;
            }
        }

        /// <summary>
        /// Convert a csv file into a double[,] array, only returns square arrays (mxm)
        /// </summary>
        /// <param name="path">Path of the csv file, including its name e.g. @"C:\Documents\test.csv"</param>
        /// <param name="omitR">This value is used to omit the last N rows of the csv file</param>
        /// <param name="omitC">This value is used to omit the last N cols of the csv file</param>
        /// <returns></returns>
        static public double[,] csvToDoubleArray(string path, int omitR, int omitC)
        {
            try
            {
                string datos = File.ReadAllText(path).Replace("\r", "");
                string[] fila = datos.Split(Environment.NewLine.ToCharArray());
                string[] filaDatos = fila[0].Split(',');
                double[,] result = new double[fila.Length - omitR, filaDatos.Length - omitC];
                for (int i = 0; i < fila.Length - omitR; i++)
                {
                    filaDatos = fila[i].Split(',');
                    for (int j = 0; j < filaDatos.Length - omitC; j++)
                    {
                        result[i, j] = Convert.ToDouble(filaDatos[j]);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                // TODO: Implement throew exception
                throw;
            }
        }
    }
}
