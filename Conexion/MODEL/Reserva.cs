using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexion.MODEL
{
   public class Reserva
   {
       public static string pattern { get; set; }


       public int id_reserva { get; set; }

       [Display(Name = "Entidad federativa")]
       [Required(ErrorMessage = "Seleccione una entidad")]
        public int id_entidad { get; set; }

       [Display(Name = "Tipo de vehículo")]
       [Required(ErrorMessage = "Seleccione un tipo de vehículo")]
        public int id_tipo { get; set; }

       [Display(Name = "Patron asignado")]
       [Required(ErrorMessage = "Verifique que el estado seleccionado tenga vehículos con patrones asignados")]
        public string id_patron { get; set; }

       [Display(Name = "Inicio")]
       [Required(ErrorMessage = "Este campo es requerido")]
        public string inicio { get; set; }

       [Display(Name = "Fin")]
       [Required(ErrorMessage = "Este campo es requerido")]
        public string fin { get; set; }
        public int cantidadActual { get; set; }
       [Display(Name = "Cantidad")]
        public int cantidad { get; set; }

       public bool terminado { get; set; }
   }
}
