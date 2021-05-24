using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session3.ViewClass
{
   public class Pasajero
    {

       
        public int ID { set; get; }
        public String Nombre { set; get; }
        public String Apellido { set; get; }
        [DisplayName("Fecha Nacimiento")]
        public String Fecha { set; get; }
        [DisplayName("N° pasaporte")]
        public string Pasaporte { set; get; }
        [DisplayName("País pasaporte")]
        public string PaisPasaporte { set; get; }

        [DisplayName("Telefono")]
        public String Telefono { set; get; }
      
    
    }
}
