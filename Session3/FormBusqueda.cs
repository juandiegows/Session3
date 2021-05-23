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
            Form r = new FormReserva();
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

        private void button2_Click(object sender, EventArgs e)
        {

            using (Session3Entities model = new Session3Entities())
            {
                
               
            }

            if (radioButton1.Checked)
            {

            }
           
        }
    }
}
