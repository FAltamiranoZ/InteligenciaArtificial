using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hnefataftl
{
    class Matriz
    {

        static public int[,] Iniciar(int[,] tab)
        {
            int i;
            int[,] tablero = tab; 
            tablero [5, 5] = 3; // se coloca al rey
            
            for (i = 3; i <=7 ; i++) //Colocamos piezas atacantes
            {   
                    tablero[i, 0] = 2;
                    tablero[0, i] = 2;
                    tablero[10, i] = 2;
                    tablero[i, 10] = 2;
                if(i==5)
                {
                    tablero[i, 1] = 2;
                    tablero[1, i] = 2;
                    tablero[9, i] = 2;
                    tablero[i, 9] = 2;
                }
            }

            for (i = 3; i <= 7; i++) //Colocamos piezas defensiva
            {
                switch (i)
                {
                    case 3:
                        tablero[5, i] = 1;
                        break;
                    case 7:
                        tablero[5, i] = 1;
                        break;
                    case 5:
                        tablero[3, i] = 1;
                        tablero[4, i] = 1;
                        tablero[6, i] = 1;
                        tablero[7, i] = 1;
                        break;
                    default:
                        tablero[4, i] = 1;
                        tablero[5, i] = 1;
                        tablero[6, i] = 1;
                        break;
                }
            }
            return tablero;
        }

        static public int[,] reiniciartablero()
        {
            int[,] PB = new int[11, 11];
            int i, j;
            for (i = 0; i < 11; i++)
            {
                for (j = 0; i < 11; i++)
                {
                    PB[i, j] = 0;
                }
            }
            return PB;
        }

        /*static public int[,] modificar(int i, int j, int tipo, int[,] tab)
        {
            tab[i, j] = tipo;
            return tab;
        }*/

    }
}
