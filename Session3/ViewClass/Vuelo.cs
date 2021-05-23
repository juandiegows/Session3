using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session3.ViewClass
{
    class Vuelo
    {
        public int ID { set; get; }
        public String Origen { set; get; }
        public String Destino { set; get; }
        public String Fecha { set; get; }
        public String Hora { set; get; }
        [DisplayName("N° Vuelo")]
        public String NumeroVuelo { set; get; }
        [DisplayName("Precio de cabina")]
        public String PrecioCabina { set; get; }
    }
}
