using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            String line = "";
            char[] b;
            int[,] c = new int[11,11];
            try
            {
                StreamReader sr = new StreamReader("C:\\Users\\Meteorok\\Desktop\\ProyectoIA\\respuesta.txt");
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
                                int num = Int32.Parse(b[i], NumberStyles.Integer, NumberStyles.AllowDecimalPoint)
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
                    if(line != null)
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
        }
    }
}
