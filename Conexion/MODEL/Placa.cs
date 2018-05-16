using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexion.MODEL
{
   public class Placa
    {
        public int id_vehiculo { get; set; }
  
        public bool asignada { get; set; }
        [DisplayName("Número de serie")]
        public string numeroSerie { get; set; }
        [DisplayName("Datos del vehículo")]
        public string datosVehiculo { get; set; }
        [DisplayName("Nombre del titular")]
        public string NombreTitular { get; set; }
        [DisplayName("Placa")]
        public string placa { get; set; }
        public int id_tipo { get; set; }

        public int id_entidad { get; set; }
        public string IsDisabled { get; set; }

    }
}
