using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Hnefataftl
{
    class Llamar_imprimir_LISP
    {
        static public void cargarCmd()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            //Cambiar los siguientes datos una vez obtenido todo
            cmd.StandardInput.WriteLine("clisp");
            cmd.StandardInput.WriteLine("(load " + '"' + "C:/Users/AndrewSauto/Desktop/Hnefataft/HnefataflOptimista" + '"' + ")");
            cmd.StandardInput.WriteLine("(escriberesp)");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }

        static public void imprimirMatriz(int[,] Tablero)
        {
            int i, j, dato;
            String Direccion = @"C:\Users\AndrewSauto\Desktop\Hnefataft\TableroInterfaz.txt";
            String cadena = "";
            try
            {
                if (File.Exists(Direccion))
                    File.Delete(Direccion);

                using (StreamWriter sw= File.CreateText(Direccion))
                {

                    for (j = 0; j < 11; j++)
                    {
                        for (i = 0; i < 11; i++)
                        {
                            dato = Tablero[i, j];
                            cadena = cadena + dato + " ";
                        }
                        sw.WriteLine(cadena);
                        cadena = "";

                    }
                }
                using (StreamReader sr = File.OpenText(Direccion))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine("ERROR EN LA ESCRITURA");
            }

        }
        
        static public int[,] leerArchivo()
        {
            String line = "";
            char[] b;
            int[,] c = new int[11, 11];
            try
            {
                StreamReader sr = new StreamReader(@"C:\Users\AndrewSauto\Desktop\Hnefataft\Resultados.txt");
                line = sr.ReadLine();
                b = line.ToCharArray();
                int i = 0;
                int x = 0;
                int y = 0;

                while (line != null)
                {
                    while (i < line.Length)
                    {
                        while (b[i] != ')')
                        {
                            if (b[i] != '(' && b[i] != ' ')
                            {
                                int num = Int32.Parse(b[i].ToString());
                                c[x, y] = num;
                                x++;
                            }
                            i++;
                        }
                        i++;
                        x = 0;
                        y++;
                    }
                    i = 0;
                    line = sr.ReadLine();
                    if (line != null)
                    {
                        b = line.ToCharArray();
                    }
                }
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return c;
    }

    }
}
