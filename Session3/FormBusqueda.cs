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
    }
}
