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
    public partial class FormBusquedadVuelo : Form
    {
        public FormBusquedadVuelo()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Desea Salir?", "Alerta", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int VueloDes = 0;

            try
            {
                VueloDes = (int)dataGridView2.CurrentRow.Cells[0].Value;
            }
            catch (Exception)
            {

              
            }
       
            Form r = new FormReserva()
            {
                VueloOrigen = (int)dataGridView1.CurrentRow.Cells[0].Value,
                VueloDestino = VueloDes,
                TipoCabina =  (int)comboBox3.SelectedValue
            };
            r.Show();
            this.Hide();
            r.FormClosed += (object s, FormClosedEventArgs e1) =>  {  this.Show();  };
        }

        private void FormBusquedadVuelo_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        public void LlenarAeropuerto(ComboBox combo)
        {
            using (Session3Entities model = new Session3Entities())
            {
                List<Airports> airports = model.Airports.ToList();
                airports.Insert(0, new Airports { IATACode = "Seleccione", ID = 0 });
                combo.DataSource = airports;
                combo.DisplayMember = "IATACode";
                combo.ValueMember = "ID";
            }
        }

         public void LlenarTipoCabina(ComboBox combo)
        {
            using (Session3Entities model = new Session3Entities())
            {
                List<CabinTypes> cabinTypes = model.CabinTypes.ToList();
                cabinTypes.Insert(0, new CabinTypes { Name = "Seleccione",ID = 0 });
                combo.DataSource = cabinTypes;
                combo.DisplayMember = "Name";
                combo.ValueMember = "ID";
            }
        }
        private void FormBusquedadVuelo_Load(object sender, EventArgs e)
        {
            LlenarAeropuerto(comboBox1);
            LlenarAeropuerto(comboBox2);
            LlenarTipoCabina(comboBox3);
        }
        private void llenarVueloDestino()
        {
            int.TryParse(comboBox3.SelectedValue.ToString(), out int tipocabina);
            int.TryParse(comboBox1.SelectedValue.ToString(), out int origen);
            int.TryParse(comboBox2.SelectedValue.ToString(), out int destino);

            using (Session3Entities model = new Session3Entities())
            {
                List<Vuelo> vuelos = (from x in model.Schedules
                                      where x.Routes.DepartureAirportID == destino && x.Routes.ArrivalAirportID == origen && x.Date == dateTimePicker2.Value.Date
                                      select new Vuelo
                                      {
                                          Origen = x.Routes.Airports.IATACode,
                                          Destino = x.Routes.Airports1.IATACode,
                                          ID = x.ID,
                                          Fecha = x.Date.ToString(),
                                          Hora = x.Time.ToString(),
                                          NumeroVuelo = x.FlightNumber,
                                          PrecioCabina = ((tipocabina == 1) ? x.EconomyPrice : ((tipocabina == 2) ? (x.EconomyPrice + (x.EconomyPrice * (decimal)0.30)) : x.EconomyPrice + ((x.EconomyPrice + (x.EconomyPrice * (decimal)0.30)) * (decimal)0.35))).ToString()

                                      }).ToList();
                dataGridView2.DataSource = vuelos;
            }

        }
    
        private void llenarVueloOrigen()
        {
            int.TryParse(comboBox3.SelectedValue.ToString(), out int tipocabina);
            int.TryParse(comboBox1.SelectedValue.ToString(), out int origen);
            int.TryParse(comboBox2.SelectedValue.ToString(), out int destino);
            using (Session3Entities model = new Session3Entities())
            {

                if (tipocabina == 0 && origen == 0 && destino == 0)
                {
                    MessageBox.Show("selecciona todos los datos");
                    return;
                }
                List<Vuelo> vuelos = (from x in model.Schedules
                                      where x.Routes.DepartureAirportID == origen && x.Routes.ArrivalAirportID == destino && x.Date == dateTimePicker1.Value.Date
                                      select new Vuelo
                                      {
                                          Origen = x.Routes.Airports.IATACode,
                                          Destino = x.Routes.Airports1.IATACode,
                                          ID = x.ID,
                                          Fecha = x.Date.ToString(),
                                          Hora = x.Time.ToString(),
                                          NumeroVuelo = x.FlightNumber,
                                          PrecioCabina = ((tipocabina == 1) ? x.EconomyPrice : ((tipocabina == 2) ? (x.EconomyPrice + (x.EconomyPrice * (decimal)0.30)) : x.EconomyPrice + ((x.EconomyPrice + (x.EconomyPrice * (decimal)0.30)) * (decimal)0.35))).ToString()

                                      }).ToList();
                dataGridView1.DataSource = vuelos;

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
          
            groupBox3.Visible = true;
            groupBox2.Dock = DockStyle.Top;
            groupBox4.Dock = DockStyle.None;
            llenarVueloOrigen();

            if (radioButton1.Checked)
            {
                llenarVueloDestino();
            }
            else
            {
                groupBox3.Visible = false;
                groupBox2.Dock = DockStyle.Fill;
                groupBox4.Dock = DockStyle.Bottom;
            }
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            int.TryParse(comboBox3.SelectedValue.ToString(), out int tipocabina);
            int.TryParse(comboBox1.SelectedValue.ToString(), out int origen);
            int.TryParse(comboBox2.SelectedValue.ToString(), out int destino);
            if (checkBox1.Checked)
            {
                using (Session3Entities model = new Session3Entities())
                {

                    DateTime f1 = dateTimePicker1.Value.Date.AddDays(-3);
                    DateTime f2 = dateTimePicker1.Value.Date.AddDays(3);
                    List<Vuelo> vuelos = (from x in model.Schedules
                                          where x.Routes.DepartureAirportID == origen && x.Routes.ArrivalAirportID == destino && x.Date >= f1.Date && x.Date <= f2.Date
                                          select new Vuelo
                                          {
                                              Origen = x.Routes.Airports.IATACode,
                                              Destino = x.Routes.Airports1.IATACode,
                                              ID = x.ID,
                                              Fecha = x.Date.ToString(),
                                              Hora = x.Time.ToString(),
                                              NumeroVuelo = x.FlightNumber,
                                              PrecioCabina = ((tipocabina == 1) ? x.EconomyPrice : ((tipocabina == 2) ? (x.EconomyPrice + (x.EconomyPrice * (decimal)0.30)) : x.EconomyPrice + ((x.EconomyPrice + (x.EconomyPrice * (decimal)0.30)) * (decimal)0.35))).ToString()

                                          }).ToList();
                    dataGridView1.DataSource = vuelos;

                }
            }
            else
            {
                llenarVueloOrigen();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            int.TryParse(comboBox3.SelectedValue.ToString(), out int tipocabina);
            int.TryParse(comboBox1.SelectedValue.ToString(), out int origen);
            int.TryParse(comboBox2.SelectedValue.ToString(), out int destino);
            if (checkBox2.Checked)
            {
                using (Session3Entities model = new Session3Entities())
                {

                    DateTime f1 = dateTimePicker1.Value.Date.AddDays(-3);
                    DateTime f2 = dateTimePicker1.Value.Date.AddDays(3);
                    List<Vuelo> vuelos = (from x in model.Schedules
                                          where x.Routes.DepartureAirportID == destino && x.Routes.ArrivalAirportID == origen && x.Date >= f1.Date && x.Date <= f2.Date
                                          select new Vuelo
                                          {
                                              Origen = x.Routes.Airports.IATACode,
                                              Destino = x.Routes.Airports1.IATACode,
                                              ID = x.ID,
                                              Fecha = x.Date.ToString(),
                                              Hora = x.Time.ToString(),
                                              NumeroVuelo = x.FlightNumber,
                                              PrecioCabina = ((tipocabina == 1) ? x.EconomyPrice : ((tipocabina == 2) ? (x.EconomyPrice + (x.EconomyPrice * (decimal)0.30)) : x.EconomyPrice + ((x.EconomyPrice + (x.EconomyPrice * (decimal)0.30)) * (decimal)0.35))).ToString()

                                          }).ToList();
                    dataGridView2.DataSource = vuelos;

                }
            }
            else
            {
                llenarVueloDestino();
            }
        }
    }
}
