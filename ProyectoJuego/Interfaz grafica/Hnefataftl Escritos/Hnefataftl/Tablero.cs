using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hnefataftl
{
    public partial class Tablero : Form
    {
        public Tablero()
        {
            InitializeComponent();
        }

        public static int[,] tab = new int[11, 11], TabAnterior = new int[11,11],TabNueva = new int[11,11];
        public static PictureBox[,] MImg = new PictureBox[11, 11];
        public static int posgx, posgy;
        public static Image img; //Variable para guardar un imagen y moverla
        public static Boolean Anterior = false, Selec = false; //Para saber si ya se habí seleccionado una imagen


        private void Form1_Load(object sender, EventArgs e)
        {
            int[,] Minicial = new int[11, 11]; 
            LIA.Text = "Atacante";
            PBJg.Image = global::Hnefataftl.Properties.Resources.Defensores;
            LJg.Text = "Defensor";
            PBIA.Image = global::Hnefataftl.Properties.Resources.atacante;
            tab = Matriz.Iniciar(Minicial);
            TabAnterior = tab;
            iniciarPB();
            matrizImagenes();
        }

        private void ActualizarMatrices()
        {
            TabNueva = Llamar_imprimir_LISP.leerArchivo();
            int i, j;
            i = 0;
            j = 0;
            Boolean cond = false;

            while (i < 11 && !cond)
            {
                while (j < 11 && !cond)
                {
                    if (TabNueva[i, j] == 3)
                    {
                        cond = true;
                    }
                    j++;
                }
                j = 0;
                i++;
            }

            i = 0;
            j = 0;

            if (cond)
            {
                for (j = 0; j < 11; j++)
                {
                    for (i = 0; i < 11; i++)
                    {
                        int valor = TabNueva[i, j];
                        switch (valor)
                        {
                            case 1:
                                MImg[i, j].Image = global::Hnefataftl.Properties.Resources.Defensores;
                                break;
                            case 2:
                                MImg[i, j].Image = global::Hnefataftl.Properties.Resources.atacante;
                                break;
                            case 3:
                                MImg[i, j].Image = global::Hnefataftl.Properties.Resources.ReyVikingo;
                                break;
                            default:
                                MImg[i, j].Image = null;
                                break;
                        }
                    }
                }

                tab = TabNueva;
            }
            else
            {
                MessageBox.Show("DERROTA");
            }
        }

        private void limpiar()
        {
            int i, j;
            for (j = 0; j < 11; j++)
            {
                for (i = 0; i < 11; i++)
                {
                    MImg[i, j].Image = null;
                }
            }
        }

        private void iniciarPB()
        {
            Image Ata, Def, Rey;
            Ata = global::Hnefataftl.Properties.Resources.atacante;
            Def = global::Hnefataftl.Properties.Resources.Defensores;
            Rey = global::Hnefataftl.Properties.Resources.ReyVikingo;
            PB1_4.Image = Ata;PB1_5.Image = Ata;PB1_6.Image = Ata;PB1_7.Image = Ata;PB1_8.Image = Ata;PB2_6.Image = Ata;
            PB4_1.Image = Ata; PB5_1.Image = Ata; PB6_1.Image = Ata; PB7_1.Image = Ata; PB8_1.Image = Ata; PB6_2.Image = Ata;
            PB4_11.Image = Ata; PB5_11.Image = Ata; PB6_11.Image = Ata; PB7_11.Image = Ata; PB8_11.Image = Ata;PB6_10.Image = Ata;
            PB11_4.Image = Ata; PB11_5.Image = Ata; PB11_6.Image = Ata; PB11_7.Image = Ata; PB11_8.Image = Ata; PB10_6.Image = Ata;

            PB6_6.Image = Rey;

            PB6_4.Image = Def; PB6_5.Image = Def; PB6_7.Image = Def; PB6_8.Image = Def;
            PB5_5.Image = Def; PB5_6.Image = Def; PB5_7.Image = Def; PB4_6.Image = Def;
            PB7_5.Image = Def; PB7_6.Image = Def; PB7_7.Image = Def; PB8_6.Image = Def;
        }

        private Image cambiarImagen(Image ImagenLocal,Boolean HayImagen,int posx,int posy)
        {

            if (!victoria())
            {
                if (esValido(posx, posy))
                {

                    if (HayImagen)
                    {
                        //Si hay imagen tenemos que preguntar si hay una imagen anterior guardada
                        //Si no hay imagen anterior, entonces podemos guardar la imagen
                        if (!Anterior)
                        {
                            img = ImagenLocal;
                            Anterior = true;
                            ImagenLocal = null;
                            posgx = posx;
                            posgy = posy;
                        }

                    }
                    else
                    {
                        if (Anterior && casillaVal(posx, posy))
                        {
                            ImagenLocal = img;
                            img = null;
                            Anterior = false;
                            tab[posx, posy] = tab[posgx, posgy];
                            tab[posgx, posgy] = 0;
                            TabAnterior = tab;
                            comer(posx,posy);
                        }
                        else
                        {
                            MessageBox.Show("Movimiento no valido");
                            MImg[posgx, posgy].Image = img;
                            img = null;
                            Anterior = false;
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("VICTORIA");
            }
            return ImagenLocal;
        }

        private void HneBoard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnFn_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

        private void PB1_1_Click(object sender, EventArgs e)
        {  
            Image local;
            
            local = PB1_1.Image; //Cambiar estos valores
            Boolean imagen = local!=null;//Se pregunta si hay imagen en la casilla
            int posx = 0, posy = 0;//Cambiar datos
            PB1_1.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB11_1_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB11_1.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 10, posy = 0;//Cambiar datos
            PB11_1.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB11_11_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB11_11.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 10, posy = 10;//Cambiar datos
            PB11_11.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB1_11_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB1_11.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
             int posx = 0, posy = 10;//Cambiar datos           
            PB1_11.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB6_7.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 5, posy = 6;//Cambiar datos
            PB6_7.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB2_1_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB2_1.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 1, posy = 0;//Cambiar datos
            PB2_1.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB3_1_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB3_1.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 2, posy = 0;//Cambiar datos
            PB3_1.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB4_1_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB4_1.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 3, posy = 0;//Cambiar datos
            PB4_1.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB5_1_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB5_1.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 4, posy = 0;//Cambiar datos
            PB5_1.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB6_1_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB6_1.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 5, posy = 0;//Cambiar datos
            PB6_1.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB7_1_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB7_1.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 6, posy = 0;//Cambiar datos
            PB7_1.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB8_1_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB8_1.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 7, posy = 0;//Cambiar datos
            PB8_1.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB9_1_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB9_1.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 8, posy = 0;//Cambiar datos
            PB9_1.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB10_1_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB10_1.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 9, posy = 0;//Cambiar datos
            PB10_1.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB1_2_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB1_2.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 0, posy = 1;//Cambiar datos
            PB1_2.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB2_2_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB2_2.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 1, posy = 1;//Cambiar datos
            PB2_2.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB3_2_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB3_2.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 2, posy = 1;//Cambiar datos
            PB3_2.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB4_2_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB4_2.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 3, posy = 1;//Cambiar datos
            PB4_2.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB5_2_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB5_2.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 4, posy = 1;//Cambiar datos
            PB5_2.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB6_2_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB6_2.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 5, posy = 1;//Cambiar datos
            PB6_2.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB7_2_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB7_2.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 6, posy = 1;//Cambiar datos
            PB7_2.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB8_2_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB8_2.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 7, posy = 1;//Cambiar datos
            PB8_2.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB9_2_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB9_2.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 8, posy = 1;//Cambiar datos
            PB9_2.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB10_2_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB10_2.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 9, posy = 1;//Cambiar datos
            PB10_2.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB11_2_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB11_2.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 10, posy = 1;//Cambiar datos
            PB11_2.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB1_3_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB1_3.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 0, posy = 2;//Cambiar datos
            PB1_3.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB2_3_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB2_3.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 1, posy = 2;//Cambiar datos
            PB2_3.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB3_3_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB3_3.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 2, posy = 2;//Cambiar datos
            PB3_3.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB4_3_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB4_3.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 3, posy = 2;//Cambiar datos
            PB4_3.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB5_3_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB5_3.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 4, posy = 2;//Cambiar datos
            PB5_3.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB6_3_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB6_3.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 5, posy = 2;//Cambiar datos
            PB6_3.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB7_3_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB7_3.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 6, posy = 2;//Cambiar datos
            PB7_3.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB8_3_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB8_3.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 7, posy = 2;//Cambiar datos
            PB8_3.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB9_3_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB9_3.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 8, posy = 2;//Cambiar datos
            PB9_3.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB10_3_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB10_3.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 9, posy = 2;//Cambiar datos
            PB10_3.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB11_3_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB11_3.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 10, posy = 2;//Cambiar datos
            PB11_3.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB1_4_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB1_4.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 0, posy = 3;//Cambiar datos
            PB1_4.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB2_4_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB2_4.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 1, posy = 3;//Cambiar datos
            PB2_4.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB3_4_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB3_4.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 2, posy = 3;//Cambiar datos
            PB3_4.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB4_4_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB4_4.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 3, posy = 3;//Cambiar datos
            PB4_4.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB5_4_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB5_4.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 4, posy = 3;//Cambiar datos
            PB5_4.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB6_4_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB6_4.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 5, posy = 3;//Cambiar datos
            PB6_4.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB7_4_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB7_4.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 6, posy = 3;//Cambiar datos
            PB7_4.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB8_4_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB8_4.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 7, posy = 3;//Cambiar datos
            PB8_4.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB9_4_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB9_4.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 8, posy = 3;//Cambiar datos
            PB9_4.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB10_4_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB10_4.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 9, posy = 3;//Cambiar datos
            PB10_4.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB11_4_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB11_4.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 10, posy = 3;//Cambiar datos
            PB11_4.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB1_5_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB1_5.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 0, posy = 4;//Cambiar datos
            PB1_5.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB2_5_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB2_5.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 1, posy = 4;//Cambiar datos
            PB2_5.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB3_5_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB3_5.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 2, posy = 4;//Cambiar datos
            PB3_5.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB4_5_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB4_5.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 3, posy = 4;//Cambiar datos
            PB4_5.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB5_5_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB5_5.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 4, posy = 4;//Cambiar datos
            PB5_5.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB6_5_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB6_5.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 5, posy = 4;//Cambiar datos
            PB6_5.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB7_5_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB7_5.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
           int posx = 6, posy = 4;//Cambiar datos
            PB7_5.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB8_5_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB8_5.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 7, posy = 4;//Cambiar datos
            PB8_5.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB9_5_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB9_5.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 8, posy = 4;//Cambiar datos
            PB9_5.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB10_5_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB10_5.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 9, posy = 4;//Cambiar datos
            PB10_5.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB11_5_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB11_5.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 10, posy = 4;//Cambiar datos
            PB11_5.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB1_6_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB1_6.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 0, posy = 5;//Cambiar datos
            PB1_6.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB2_6_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB2_6.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 1, posy = 5;//Cambiar datos
            PB2_6.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB3_6_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB3_6.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 2, posy = 5;//Cambiar datos
            PB3_6.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB4_6_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB4_6.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 3, posy = 5;//Cambiar datos
            PB4_6.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB5_6_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB5_6.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 4, posy = 5;//Cambiar datos
            PB5_6.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB7_6_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB7_6.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
           int posx = 6, posy = 5;//Cambiar datos
            PB7_6.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB8_6_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB8_6.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 7, posy = 5;//Cambiar datos
            PB8_6.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB9_6_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB9_6.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 8, posy = 5;//Cambiar datos
            PB9_6.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB10_6_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB10_6.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 9, posy = 5;//Cambiar datos
            PB10_6.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB11_6_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB11_6.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 10, posy = 5;//Cambiar datos
            PB11_6.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB1_7_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB1_7.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 0, posy = 6;//Cambiar datos
            PB1_7.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB2_7_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB2_7.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 1, posy = 6;//Cambiar datos
            PB2_7.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB3_7_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB3_7.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 2, posy = 6;//Cambiar datos
            PB3_7.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB4_7_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB4_7.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 3, posy = 6;//Cambiar datos
            PB4_7.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB5_7_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB5_7.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 4, posy = 6;//Cambiar datos
            PB5_7.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB7_7_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB7_7.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 6, posy = 6;//Cambiar datos
            PB7_7.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB8_7_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB8_7.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 7, posy = 6;//Cambiar datos
            PB8_7.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB9_7_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB9_7.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 8, posy = 6;//Cambiar datos
            PB9_7.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB10_7_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB10_7.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 9, posy = 6;//Cambiar datos
            PB10_7.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB11_7_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB11_7.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 10, posy = 6;//Cambiar datos
            PB11_7.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB1_8_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB1_8.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 0, posy = 7;//Cambiar datos
            PB1_8.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB2_8_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB2_8.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 1, posy = 7;//Cambiar datos
            PB2_8.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB3_8_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB3_8.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 2, posy = 7;//Cambiar datos
            PB3_8.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB4_8_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB4_8.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 3, posy = 7;//Cambiar datos
            PB4_8.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB5_8_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB5_8.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 4, posy = 7;//Cambiar datos
            PB5_8.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB6_8_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB6_8.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 5, posy = 7;//Cambiar datos
            PB6_8.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB7_8_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB7_8.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 6, posy = 7;//Cambiar datos
            PB7_8.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB8_8_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB8_8.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 7, posy = 7;
            PB8_8.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB9_8_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB9_8.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 8, posy = 7;//Cambiar datos
            PB9_8.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB10_8_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB10_8.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 9, posy = 7;//Cambiar datos
            PB10_8.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB11_8_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB11_8.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 10, posy = 7;//Cambiar datos
            PB11_8.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB1_9_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB1_9.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 0, posy = 8;//Cambiar datos
            PB1_9.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB2_9_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB2_9.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 1, posy = 8;//Cambiar datos
            PB2_9.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB3_9_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB3_9.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 2, posy = 8;//Cambiar datos
            PB3_9.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB4_9_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB4_9.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 3, posy = 8;//Cambiar datos
            PB4_9.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB5_9_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB5_9.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 4, posy = 8;//Cambiar datos
            PB5_9.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB6_9_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB6_9.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 5, posy = 8;//Cambiar datos
            PB6_9.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB7_9_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB7_9.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
int posx = 6, posy = 8;//Cambiar datos
            PB7_9.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB8_9_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB8_9.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 7, posy = 8;//Cambiar datos
            PB8_9.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB9_9_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB9_9.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
int posx = 8, posy = 8;//Cambiar datos
            PB9_9.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB10_9_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB10_9.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
 int posx = 9, posy = 8;//Cambiar datos
            PB10_9.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB11_9_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB11_9.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 10, posy = 8;//Cambiar datos
            PB11_9.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB1_10_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB1_10.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
int posx = 0, posy = 9;//Cambiar datos
            PB1_10.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato

        }

        private void PB2_10_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB2_10.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
int posx = 1, posy = 9;//Cambiar datos
            PB2_10.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB3_10_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB3_10.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
int posx = 2, posy = 9;//Cambiar datos
            PB3_10.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar 
        }

        private void PB4_10_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB4_10.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 3, posy = 9;//Cambiar datos
            PB4_10.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB5_10_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB5_10.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
int posx = 4, posy = 9;//Cambiar datos
            PB5_10.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB6_10_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB6_10.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
int posx = 5, posy = 9;//Cambiar datos
            PB6_10.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB7_10_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB7_10.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 6, posy = 9;//Cambiar datos
            PB7_10.Image = cambiarImagen(local, imagen,posx,posy);//Cambiar dato
        }

        private void PB8_10_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB8_10.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
int posx = 7, posy = 9;//Cambiar datos
            PB8_10.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB9_10_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB9_10.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
int posx = 8, posy = 9;//Cambiar datos
            PB9_10.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB10_10_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB10_10.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
int posx = 9, posy = 9;//Cambiar datos
            PB10_10.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB11_10_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB11_10.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 10, posy = 9;//Cambiar datos
            PB11_10.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB2_11_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB2_11.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 1, posy = 10;//Cambiar datos
            PB2_11.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato

        }

        private void PB3_11_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB3_11.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
int posx = 2, posy = 10;//Cambiar datos
            PB3_11.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB4_11_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB4_11.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
int posx = 3, posy = 10;//Cambiar datos
            PB4_11.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
            
        }

        private void PB5_11_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB5_11.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
int posx = 4, posy = 10;//Cambiar datos
            PB5_11.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato

        }

        private void PB6_11_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB6_11.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
int posx = 5, posy = 10;//Cambiar datos
            PB6_11.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB7_11_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB7_11.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 6, posy = 10;//Cambiar datos
            PB7_11.Image = cambiarImagen(local, imagen,posx,posy);//Cambiar dato
        }

        private void PB8_11_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB8_11.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 7, posy = 10;//Cambiar datos
            PB8_11.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB9_11_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB9_11.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
int posx = 8, posy = 10;//Cambiar datos
            PB9_11.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB10_11_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB10_11.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 9, posy = 10;//Cambiar datos
            PB10_11.Image = cambiarImagen(local, imagen, posx, posy);//Cambiar dato
        }

        private void PB6_6_Click(object sender, EventArgs e)
        {
            Image local;

            local = PB6_6.Image; //Cambiar estos valores
            Boolean imagen = local != null;//Se pregunta si hay imagen en la casilla
            int posx = 5, posy = 5;//Cambiar datos
            PB6_6.Image = cambiarImagen(local, imagen,posx, posy);//Cambiar dato
        }

        private void btnCompletar_Click(object sender, EventArgs e)
        {
            Llamar_imprimir_LISP.imprimirMatriz(tab);
            Llamar_imprimir_LISP.cargarCmd();
            Llamar_imprimir_LISP.leerArchivo();
            ActualizarMatrices();
        }

        private void btnReniciar_Click(object sender, EventArgs e)
        {
            limpiar();
            tab = Matriz.reiniciartablero();
            tab = Matriz.Iniciar(tab);
            iniciarPB();

            Llamar_imprimir_LISP.imprimirMatriz(tab);
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            btnReniciar.Visible = true;
            btnCompletar.Visible = true;
            btnInicio.Visible = false;
            Llamar_imprimir_LISP.imprimirMatriz(tab);
            Llamar_imprimir_LISP.cargarCmd();
            Llamar_imprimir_LISP.leerArchivo();
            ActualizarMatrices();
        }

        private void matrizImagenes()
        {
            MImg[0, 0] = PB1_1;MImg[0, 1] = PB1_2;MImg[0, 2] = PB1_3;MImg[0, 3] = PB1_4;MImg[0, 4] = PB1_5;MImg[0, 5] = PB1_6;MImg[0, 6] = PB1_7;MImg[0, 7] = PB1_8;MImg[0, 8] = PB1_9;MImg[0, 9] = PB1_10;MImg[0, 10] = PB1_11;
            MImg[1, 0] = PB2_1; MImg[1, 1] = PB2_2; MImg[1, 2] = PB2_3; MImg[1, 3] = PB2_4; MImg[1, 4] = PB2_5; MImg[1, 5] = PB2_6; MImg[1, 6] = PB2_7; MImg[1, 7] = PB2_8; MImg[1, 8] = PB2_9; MImg[1, 9] = PB2_10; MImg[1, 10] = PB2_11;
            MImg[2, 0] = PB3_1; MImg[2, 1] = PB3_2; MImg[2, 2] = PB3_3; MImg[2, 3] = PB3_4; MImg[2, 4] = PB3_5; MImg[2, 5] = PB3_6; MImg[2, 6] = PB3_7; MImg[2, 7] = PB3_8; MImg[2, 8] = PB3_9; MImg[2, 9] = PB3_10; MImg[2, 10] = PB3_11;
            MImg[3, 0] = PB4_1; MImg[3, 1] = PB4_2; MImg[3, 2] = PB4_3; MImg[3, 3] = PB4_4; MImg[3, 4] = PB4_5; MImg[3, 5] = PB4_6; MImg[3, 6] = PB4_7; MImg[3, 7] = PB4_8; MImg[3, 8] = PB4_9; MImg[3, 9] = PB4_10; MImg[3, 10] = PB4_11;
            MImg[4, 0] = PB5_1; MImg[4, 1] = PB5_2; MImg[4, 2] = PB5_3; MImg[4, 3] = PB5_4; MImg[4, 4] = PB5_5; MImg[4, 5] = PB5_6; MImg[4, 6] = PB5_7; MImg[4, 7] = PB5_8; MImg[4, 8] = PB5_9; MImg[4, 9] = PB5_10; MImg[4, 10] = PB5_11;
            MImg[5, 0] = PB6_1; MImg[5, 1] = PB6_2; MImg[5, 2] = PB6_3; MImg[5, 3] = PB6_4; MImg[5, 4] = PB6_5; MImg[5, 5] = PB6_6; MImg[5, 6] = PB6_7; MImg[5, 7] = PB6_8; MImg[5, 8] = PB6_9; MImg[5, 9] = PB6_10; MImg[5, 10] = PB6_11;
            MImg[6, 0] = PB7_1; MImg[6, 1] = PB7_2; MImg[6, 2] = PB7_3; MImg[6, 3] = PB7_4; MImg[6, 4] = PB7_5; MImg[6, 5] = PB7_6; MImg[6, 6] = PB7_7; MImg[6, 7] = PB7_8; MImg[6, 8] = PB7_9; MImg[6, 9] = PB7_10; MImg[6, 10] = PB7_11;
            MImg[7, 0] = PB8_1; MImg[7, 1] = PB8_2; MImg[7, 2] = PB8_3; MImg[7, 3] = PB8_4; MImg[7, 4] = PB8_5; MImg[7, 5] = PB8_6; MImg[7, 6] = PB8_7; MImg[7, 7] = PB8_8; MImg[7, 8] = PB8_9; MImg[7, 9] = PB8_10; MImg[7, 10] = PB8_11;
            MImg[8, 0] = PB9_1; MImg[8, 1] = PB9_2; MImg[8, 2] = PB9_3; MImg[8, 3] = PB9_4; MImg[8, 4] = PB9_5; MImg[8, 5] = PB9_6; MImg[8, 6] = PB9_7; MImg[8, 7] = PB9_8; MImg[8, 8] = PB9_9; MImg[8, 9] = PB9_10; MImg[8, 10] = PB9_11;
            MImg[9, 0] = PB10_1; MImg[9, 1] = PB10_2; MImg[9, 2] = PB10_3; MImg[9, 3] = PB10_4; MImg[9, 4] = PB10_5; MImg[9, 5] = PB10_6; MImg[9, 6] = PB10_7; MImg[9, 7] = PB10_8; MImg[9, 8] = PB10_9; MImg[9, 9] = PB10_10; MImg[9, 10] = PB10_11;
            MImg[10, 0] = PB11_1; MImg[10, 1] = PB11_2; MImg[10, 2] = PB11_3; MImg[10, 3] = PB11_4; MImg[10, 4] = PB11_5; MImg[10, 5] = PB11_6; MImg[10, 6] = PB11_7; MImg[10, 7] = PB11_8; MImg[10, 8] = PB11_9; MImg[10, 9] = PB11_10; MImg[10, 10] = PB11_11;
        }

        private Boolean esValido(int posx,int  posy)
        {
            Boolean resp;
            int valor = tab[posx, posy];
            if (valor != 2)
            {
                
                resp = true;
            }
            else
                resp = false;

            return resp;
        }

        private Boolean casillaVal(int posx, int posy)
        {
            Boolean resp;

            if (posx == posgx || posy == posgy)
            {
                if (libre(posx, posy))
                    resp = true;
                else
                    resp = false;
            }
            else
                resp = false;

            return resp;
        }

        public Boolean libre(int posx, int posy)
        {
            int x = posx, y = posy, xg = posgx, yg = posgy, i,cont;
            bool Mismox, Mismoy,resp;
            Mismox = x == xg;
            Mismoy = y == yg;
            if (Mismox)
            {
                if (y > yg)
                {
                    cont = 0;
                    for (i = 1; i+yg < y; i++)
                    {
                        cont = cont + tab[x, yg + i];
                    }
                    resp = cont == 0;
                }
                else
                {
                    cont = 0;
                    for (i = 1; yg-i > y; i++)
                    {
                        cont = cont + tab[x, yg - i];
                    }
                    resp = cont == 0;
                }
            }
            else
            {
                if (x > xg)
                {
                    cont = 0;
                    for (i = 1; i+xg < x; i++)
                    {
                        cont = cont + tab[xg+i, y];
                    }
                    resp = cont == 0;
                }
                else
                {
                    cont = 0;
                    for (i = 1; xg-i > x; i++)
                    {
                        cont = cont + tab[xg - i, y];
                    }
                    resp = cont == 0;
                }
            }
            return resp;
        }

        private Boolean victoria()
        {
            if (tab[0, 10] == 3 || tab[10, 0] == 3 || tab[0, 0] == 3 || tab[10, 10] == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void comer(int posx, int posy)
        {
            
            if (posx + 1 <= 10 && posx - 1 >= 0 && posy + 1 <= 10 && posy - 1 >= 0)// && posx + 2 <= 10 && posx - 2 >= 0 && posy + 2 <= 10 && posx - 2 >= 0)
            {
                if (tab[posx, posy + 1] == 2)
                {
                    if (posy + 2 < 10)
                    {
                        if (tab[posx, posy + 2] == 1)
                        {
                            tab[posx, posy + 1] = 0;
                            MImg[posx, posy + 1].Image = null;
                        }
                            
                    }
                       
                }

                if (tab[posx, posy - 1] == 2)
                {
                    if(posy - 2 > 0){
                        if (tab[posx, posy - 2] == 1)
                        {
                            tab[posx, posy - 1] = 0;
                            MImg[posx, posy - 1].Image = null;
                        }

                    }
                    
                }

                if (tab[posx + 1, posy] == 2)
                {
                    if (posx + 2 < 10)
                    {
                        if (tab[posx + 2, posy] == 1)
                        {
                            tab[posx + 1, posy] = 0;
                            MImg[posx+1, posy].Image = null;
                        }

                    }
                }

                if (tab[posx - 1, posy] == 2)
                {
                    if (posx - 2 > 0)
                    {
                        if (tab[posx - 2, posy] == 1)
                        {
                            tab[posx - 1, posy] = 0;
                            MImg[posx - 1, posy].Image = null;
                        }
                    }  
                }

                if(posx+2 ==10 && (posy == 0||posy==10))
                {
                    tab[posx + 1, posy] = 0;
                    MImg[posx + 1, posy].Image = null;
                }
                if(posx-2==0 && (posy == 0||posy==10))
                {
                    tab[posx - 1, posy] = 0;
                    MImg[posx - 1, posy].Image = null;
                }
                if(posy-2 == 10 &&(posx==0 || posx == 10))
                {
                    tab[posx, posy - 1] = 0;
                    MImg[posx, posy-1].Image = null;
                }
                if(posy+2 ==10 && (posx == 0 || posx == 10))
                {
                    tab[posx, posy + 1] = 0;
                    MImg[posx, posy + 1].Image = null;
                }
            }
        }

    }
}
