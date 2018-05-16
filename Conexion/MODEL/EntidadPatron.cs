using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Conexion.MODEL
{
    public class EntidadPatron
    {
        public int id_entidadPatron { get; set; }
        [DisplayName("Entidad federativa")]
        [Required(ErrorMessage = "Seleccione una entidad federativa")]
        public int id_entidad { get; set; }
        [DisplayName("Tipo de vehículo")]
        [Required(ErrorMessage = "Seleccione un tipo de vehículo")]
        public int id_tipo { get; set; }
        [RegularExpression(@"^(X+|0+)+(-X|-0|X|0)*$", ErrorMessage = "introduzca un valor valido")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [DisplayName("Patron de la placa")]
        public string patron { get; set; }

        public string tipo { get; set; }
    }
}