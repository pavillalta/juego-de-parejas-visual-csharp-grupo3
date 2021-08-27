using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Juego_2_grupo3_parejas
{
    public partial class Form1 : Form
    {
        // firstClicked apunta al primer control Label
        // que el jugador hace clic, pero será nulo
        // si el jugador aún no ha hecho clic en una etiqueta
        Label firstClicked = null;

        // secondClicked apunta al segundo control Label
        // que el jugador hace clic
        Label secondClicked = null;



        // Utilice este objeto aleatorio para elegir iconos aleatorios para los cuadrados
        Random random = new Random();

        // Cada una de estas letras es un icono interesante
        // en la fuente Webdings,
        // y cada icono aparece dos veces en esta lista
        List<string> icons = new List<string>()
    {
        "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
    };
        /// <summary>
        /// Asignar cada icono de la lista de iconos a un cuadrado aleatorio  
        /// </summary>
        private void AssignIconsToSquares()
        {
            // El TableLayoutPanel tiene 16 etiquetas,
            // y la lista de iconos tiene 16 iconos,
            // por lo que un icono se extrae al azar de la lista
            // y agregado a cada etiqueta
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        /// <resumen>
        /// El evento Click de cada etiqueta es manejado por este controlador de eventos
        /// </summary>
        /// <param name = "sender"> La etiqueta en la que se hizo clic </param>
        /// <param name = "e"></param>
        private void label_Click(object sender, EventArgs e)
        {
            {
                // El temporizador solo se activa después de dos no coincidentes
                // Se han mostrado iconos al jugador,
                // así que ignora cualquier clic si el temporizador está funcionando
                if (timer1.Enabled == true)
                    return;

                Label clickedLabel = sender as Label;

                if (clickedLabel != null)
                {
                    // Si la etiqueta en la que se hizo clic es negra, el jugador hizo clic
                    // un icono que ya ha sido revelado --
                    // ignora el clic
                    if (clickedLabel.ForeColor == Color.Black)
                        return;

                    // Si firstClicked es nulo, este es el primer icono
                    // en el par en el que el jugador hizo clic,
                    // así que establezca firstClicked en la etiqueta que el jugador
                    // hace clic, cambia su color a negro y regresa
                    if (firstClicked == null)
                    {
                        firstClicked = clickedLabel;
                        firstClicked.ForeColor = Color.Black;
                        return;
                    }

                    // Si el jugador llega tan lejos, el temporizador no
                    // en ejecución y firstClicked no es nulo,
                    // por lo que este debe ser el segundo icono en el que el jugador hizo clic
                    // Establecer su color en negro
                    secondClicked = clickedLabel;
                    secondClicked.ForeColor = Color.Black;

                    // Verifica si el jugador ganó
                    CheckForWinner();

                    // Si el jugador hizo clic en dos iconos coincidentes, manténgalos
                    // ennegrezca y reinicie firstClicked y secondClicked
                    // para que el jugador pueda hacer clic en otro icono
                    if (firstClicked.Text == secondClicked.Text)
                    {
                        firstClicked = null;
                        secondClicked = null;
                        return;
                    }

                    // Si el jugador llega tan lejos, el jugador
                    // hizo clic en dos iconos diferentes, así que inicie el
                    // temporizador (que esperará tres cuartos de
                    // un segundo, y luego esconde los íconos)
                    timer1.Start();
                }
            }

        }

        /// <resumen>
        /// Este temporizador se inicia cuando el jugador hace clic
        /// dos iconos que no coinciden,
        /// por lo que cuenta tres cuartos de segundo
        /// y luego se apaga y oculta ambos íconos
        /// </summary>
        /// <param name = "sender"> </param>
        /// <param name = "e"> </param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Detén el cronómetro
            timer1.Stop();

            // Ocultar ambos iconos
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Restablecer firstClicked y secondClicked
            // así que la próxima vez que una etiqueta sea
            // se hace clic, el programa sabe que es el primer clic
            firstClicked = null;
            secondClicked = null;
        }


        /// <resumen>
        /// Verifique cada ícono para ver si coincide, por
        /// comparando su color de primer plano con su color de fondo.
        /// Si todos los iconos coinciden, el jugador gana
        /// </summary>
        private void CheckForWinner()
        {
            // Revise todas las etiquetas en TableLayoutPanel,
            // comprobando cada uno para ver si su icono coincide
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // Si el bucle no regresó, no encontró
            // cualquier ícono incomparable
            // Eso significa que el usuario ganó. Muestre un mensaje y cierre el formulario
            MessageBox.Show("¡Has coincidido con todos los íconos!", "Felicitaciones");
            Close();
        }
    }
}