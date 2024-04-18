using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Socket server;
        bool logeado = false;
        string UsuarioLog = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e) //boton de registro
        {

            string mensaje = "1/" + nombreReg.Text + "/"+ contraReg.Text + "/";
            // Enviamos al servidor el nombre y contrasenya tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split (',')[0];
            MessageBox.Show(mensaje);

        }
        private void consultaBoton_Click(object sender, EventArgs e)
        {
            if (mayorTurnos.Checked)
            {
                string mensaje = "3/Mayor/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split(',')[0];
                MessageBox.Show("La partida con mas turnos es la numero " + mensaje);
            }
            else if (victoriasDe.Checked)
            {
                string mensaje = "4/" + nombreConsulta.Text + "/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split(',')[0];
                MessageBox.Show("Las victorias de " + nombreConsulta.Text +" son: " + mensaje);
            }
            else if(mejorJugador.Checked)
            {
                string mensaje = "5/Mejor";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split(',')[0];
                MessageBox.Show("El mejor jugador es: " + mensaje);
            }
            else if(muertesDe.Checked)
            {
                string mensaje = "6/" + nombreConsulta.Text + "/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split(',')[0];
                MessageBox.Show("El jugador " + nombreConsulta.Text + " ha muerto " + mensaje + "veces");
            }
        }

        private void logeoBoton_Click(object sender, EventArgs e) //boton para loguearse
        {
            string mensaje = "2/" + nombreReg.Text + "/" + contraReg.Text + "/";
            // Enviamos al servidor el nombre y contrasenya tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split(',')[0];
            UsuarioLog = mensaje;
            if (UsuarioLog != "Error" && !logeado)
            {
                usuarioActual.Text = UsuarioLog;
                logeado = true;
            }
            else MessageBox.Show("Cierra sesión antes de logearte");
        }

        private void conectarBoton_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            usuarioActual.Text = "";
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9100);
            

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }

        }

        private void desconectarBoton_Click(object sender, EventArgs e)
        {
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Se terminó el servicio. 
            // Nos desconectamos
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        { //boton para cerrar sesion
            string mensaje = "7/" + UsuarioLog + "/";
            // Enviamos al servidor el nombre y contrasenya tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split(',')[0];
            UsuarioLog = "";
            usuarioActual.Text = "";
            logeado = false;
        }

        private void nServicios_Click(object sender, EventArgs e)
        {
            //Pedir
            string mensaje = "9/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            contLbl.Text =  "";
            mensaje = Encoding.ASCII.GetString(msg2).Split(',')[0];
            mensaje = mensaje.Substring(0, mensaje.Length - 1);
            contLbl.Text = mensaje;
        }
    }
}
