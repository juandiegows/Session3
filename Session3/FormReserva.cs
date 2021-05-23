using Session3.Modelo;
using Session3.ViewClass;
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
        public int TipoCabina { get; set; }
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
            llenarPasajero();
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
          
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
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

        private void llenarPasajero()
        {
            using (Modelo.Session3Entities model = new Session3Entities())
            {
                List<Pasajero> pasajeros = (from x in model.Tickets
                                            join co in model.Countries
                                            on x.PassportCountryID equals co.ID
                                            select new Pasajero {
                                                Apellido = x.Lastname,
                                                Nombre = x.Firstname,
                                                Fecha = x.Schedules.Date.ToString(),
                                                PaisPasaporte = co.Name,
                                                Pasaporte = x.PassportNumber,
                                                Telefono = x.Phone,
                                                ID = x.ID
                                            }).ToList();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            using(Session3Entities model = new Session3Entities())
            {

            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
