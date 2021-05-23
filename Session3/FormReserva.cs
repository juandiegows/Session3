using Session3.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session3
{
    public partial class FormReserva : Form
    {
        public int VueloOrigen { get; set; }
        public int VueloDestino { get; set; }
        public FormReserva()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormReserva_Load(object sender, EventArgs e)
        {
            using(Session3Entities model = new Modelo.Session3Entities())
            {
                Schedules vuelo = model.Schedules.FirstOrDefault(x => x.ID == VueloOrigen);
                origen.Text = vuelo.Routes.Airports.IATACode;
                destino.Text = vuelo.Routes.Airports1.IATACode;
                fecha.Text = vuelo.Date.ToShortDateString();
                Nvuelo.Text = vuelo.FlightNumber.ToString();
            }

            if(VueloDestino == 0)
            {
                groupBox2.Visible = false;
            }
            else
            {
                using (Session3Entities model = new Modelo.Session3Entities())
                {
                    Schedules vuelo = model.Schedules.FirstOrDefault(x => x.ID == VueloDestino);
                    origen2.Text = vuelo.Routes.Airports.IATACode;
                    destino2.Text = vuelo.Routes.Airports1.IATACode;
                    fecha2.Text = vuelo.Date.ToShortDateString();
                    Nvuelo2.Text = vuelo.FlightNumber.ToString();
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.Yes)
            {
                lblRuta.Text = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form r = new OpcionesPago
            {
              
            };
            r.Show();
            this.Enabled = false;
            r.FormClosed += (object s, FormClosedEventArgs e1) => { this.Enabled = true; };
        }
    }
}
