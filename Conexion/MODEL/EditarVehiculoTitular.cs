﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexion.MODEL
{
   public class EditarVehiculoTitular
    {
        [Required(ErrorMessage = "Ingrese el CURP")]
        [RegularExpression("[A-Z][A,E,I,O,U,X][A-Z]{2}[0-9]{2}[0-1][0-9][0-3][0-9][M,H][A-Z]{2}[B,C,D,F,G,H,J,K,L,M,N,Ñ,P,Q,R,S,T,V,W,X,Y,Z]{3}[0-9,A-Z][0-9]", ErrorMessage = "Ingrese un CURP valido")]
        [DisplayName("CURP")]
        public string id_curp { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        [DisplayName("Apellido")]
        public string apellido { get; set; }
        [DisplayName("Calle")]
        public string calle { get; set; }
        [DisplayName("Número")]
        public string numero { get; set; }
        [DisplayName("Colonia")]
        public string colonia { get; set; }
        [DisplayName("Entidad federativa")]
        [Required(ErrorMessage = "Seleccione una entidad")]
        public int id_entidad { get; set; }

        [Required(ErrorMessage = "Seleccione una ciudad")]
        [DisplayName("Ciudad")]
        public int id_ciudad { get; set; }


        public int id_vehiculo { get; set; }

        [Display(Name = "Entidad federativa donde el vehículo fue dado de alta")]
        [Required(ErrorMessage = "Seleccione una entidad")]
        public int id_entidad2 { get; set; }
        [Display(Name = "Tipo de vehiculo")]
        [Required(ErrorMessage = "Seleccione un tipo de vehículo")]
        public int id_tipo { get; set; }
        [DisplayName("Marca")]
        [Required(ErrorMessage = "Este compo es obligatorio")]
        public string marca { get; set; }
        [DisplayName("Año")]
        [Required(ErrorMessage = "Este compo es obligatorio")]
        public int año { get; set; }
        [DisplayName("Modelo")]
        [Required(ErrorMessage = "Este compo es obligatorio")]
        public string modelo { get; set; }
        [DisplayName("Número de serie del motor")]
        [Required(ErrorMessage = "Este compo es obligatorio")]
        public string numeroSerie { get; set; }

        public string CURP { get; set; }

    }
}
