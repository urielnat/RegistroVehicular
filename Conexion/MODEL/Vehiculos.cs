using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexion.MODEL
{
   public  class Vehiculos
    {
        public int id_vehiculo { get; set; }
 
        [Display(Name="Entidad federativa")]
        [Required(ErrorMessage = "Seleccione una entidad")]
        public int id_entidad { get; set; }
        [Display(Name = "Tipo de vehiculo")]
        [Required(ErrorMessage = "Seleccione un tipo de vehículo")]
        public int  id_tipo { get; set; }
        [DisplayName("Marca")]
        [Required(ErrorMessage = "Este compo es obligatorio")]
        public string marca { get; set; }
        [DisplayName("Año")]
        [Range(1920, 2020,
            ErrorMessage = "El año debe ser entre 1920 y 2020")]
        [Required(ErrorMessage = "Este compo es obligatorio")]
        public int año { get; set; }
        [DisplayName("Modelo")]
        [Required(ErrorMessage = "Este compo es obligatorio")]
        public string modelo { get; set; }
        [DisplayName("Número de serie del motor")]
        [Required(ErrorMessage = "Este compo es obligatorio")]
        public string numeroSerie { get; set; }
      
        public string id_curp { get; set; }
       
    }
}
