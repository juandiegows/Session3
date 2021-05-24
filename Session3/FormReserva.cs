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
            CargarPaises();
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
                dataGridView2.DataSource = pasajeros;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            int passID = 0;
            int.TryParse(comboBox1.SelectedValue.ToString(), out passID);
            using(Session3Entities model = new Session3Entities())
            {
                model.Tickets.Add(new Tickets {
                    Firstname = txtNombre.Text,
                    Lastname = txtApellido.Text,
                    PassportNumber = txtPasaporte.Text,
                    Phone = txtcel.Text,
                    CabinTypeID = TipoCabina,
                    PassportCountryID =passID,
                    ScheduleID = VueloOrigen,
                    PassportPhoto = openFileDialog1.FileName,
                    Confirmed = false,
                    BookingReference = "",
                    Email = null,
                    UserID = 1
                  
                    

                });

                if (VueloDestino != 0)
                {
                    model.Tickets.Add(new Tickets
                    {
                        Firstname = txtNombre.Text,
                        Lastname = txtApellido.Text,
                        PassportNumber = txtPasaporte.Text,
                        Phone = txtcel.Text,
                        CabinTypeID = TipoCabina,
                        PassportCountryID = passID,
                        ScheduleID = VueloDestino,
                        PassportPhoto = openFileDialog1.FileName,
                        Confirmed = false,
                        BookingReference = "",
                        Email = null,
                        UserID = 1



                    });
                }
                int result = model.SaveChanges();
                if (result == 1)
                {
                    MessageBox.Show("Se ha agregado el usuario el vuelo de ida");
                    llenarPasajero();
                }
                else if (result == 2)
                {
                    MessageBox.Show("Se ha agregado el usuario el vuelo de ida y retorno");
                    llenarPasajero();
                }
                else
                {
                    MessageBox.Show("No se puedo agregar el pasajero");
                }
            }
        }

        private void CargarPaises()
        {
            using(Session3Entities model = new Session3Entities())
            {

                comboBox1.DataSource = model.Countries.ToList();
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "ID";
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
