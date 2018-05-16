using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexion.MODEL
{
   public class Tabla
    {
        [DisplayName("CURP")]
        public string id_curp { get; set; }
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        [DisplayName("Apellido")]
        public string apellido { get; set; }
      


        public int id_vehiculo { get; set; }
        
     
        [DisplayName("Marca")]
        public string marca { get; set; }
        [DisplayName("Año")]
        public int año { get; set; }
        [DisplayName("Modelo")]
        public string modelo { get; set; }
        [DisplayName("Número de serie del motor")]
        public string numeroSerie { get; set; }

        public string Placa { get; set; }

        public string Estatus { get; set; }

    }
}
