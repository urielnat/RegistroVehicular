using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conexion.MODEL;

namespace Conexion.BD
{
   public class VehiculoBD
    {
        public static void Create(Vehiculos m)
        {
            var p = new SqlParameter[]
            {

         
                new SqlParameter("@id_entidad",m.id_entidad),
                new SqlParameter("@marca",m.marca),
                new SqlParameter("@modelo",m.modelo),
                new SqlParameter("@año",m.año),
                new SqlParameter("@numeroSerie",m.numeroSerie),
                new SqlParameter("@id_tipo",m.id_tipo),
                new SqlParameter("@id_curp",m.id_curp),

            };
            Util.Execute("CrearVehiculo", p);
        }

        public static List<EntidadFederativa> Entidades()
        {
            var dt = Util.Query("SELECT DISTINCT id_entidad, entidad FROM entidades");
            var list = new List<EntidadFederativa>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new EntidadFederativa
                {
                    id_entidad = (int)dr["id_entidad"],
                    entidad = (string)dr["entidad"]


                });
            }
            return list;
        }

        public static List<TipoVehiculo> Tipos()
        {
            var dt = Util.Query("SELECT DISTINCT id_tipo, tipo FROM tipos");
            var list = new List<TipoVehiculo>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new TipoVehiculo
                {
                    id_tipo = (int)dr["id_tipo"],
                    tipo = (string)dr["tipo"]


                });
            }
            return list;
        }

        public static List<Ciudad> Ciudades()
        {
            
            var dt = Util.Query("listaCiudades");
            var list = new List<Ciudad>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Ciudad
                {
                    id_ciudad = (int)dr["id_ciudad"],
                    ciudad = (string)dr["ciudad"],
                    id_entidad = (int)dr["id_entidad"]


                });
            }
            return list;
        }

        public static void CreateTitular(Titular m)
        {
            var p = new SqlParameter[]
            {

                new SqlParameter("@id_curp",m.id_curp),
                new SqlParameter("@nombre",m.nombre),
                new SqlParameter("@apellido",m.apellido),
                new SqlParameter("@colonia",m.colonia),
                new SqlParameter("@calle",m.calle),
                new SqlParameter("@numero",m.numero),
                new SqlParameter("@id_entidad",m.id_entidad),
                new SqlParameter("@id_ciudad",m.id_ciudad),
              
            };
            Util.Execute("CrearTitular", p);

        }

   

        public static List<Titular> titular (int curp)
        {
            var list = new List<Titular>();
            return list;
        }

        public static Titular Buscar(string curp)
        {
            var dt = Util.Query("SELECT * FROM titular where id_curp="+"\'"+curp+"\'");

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                return new Titular
                {
                    id_curp = (string) dr["id_curp"],
                    nombre = (string) dr["nombre"],
                    apellido = (string) dr["apellido"],
                    calle = (string) dr["calle"],
                    numero = (string) dr["numero"],
                    colonia = (string) dr["colonia"],
                    id_entidad = (int) dr["id_entidad"],
                    id_ciudad = (int) dr["id_ciudad"],



                };
            }
            else return new Titular {id_curp = curp};

        }

        public static List<Vehiculos> AutocompModelo(string l)
        {
            var p = new[]
            {
                new SqlParameter("@l",l)
            };
            var dt = Util.Query("ModAuto", p);
            var list = new List<Vehiculos>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Vehiculos
                {
                    modelo = (string)dr["modelo"]
                });
            }
            
            return list;
        }

        public static List<Vehiculos> AutocompMarca(string l)
        {
            var p = new[]
            {
                new SqlParameter("@l",l)
            };
            var dt = Util.Query("MarcAuto", p);
            var list = new List<Vehiculos>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Vehiculos
                {
                    marca = (string)dr["marca"]
                });
            }
            return list;
        }

        public static List<Titular> AutocompColonia(string l)
        {
            var p = new[]
            {
                new SqlParameter("@l",l)
            };
            var dt = Util.Query("ColAuto", p);
            var list = new List<Titular>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Titular
                {
                   colonia = (string)dr["colonia"]
                });
            }
            return list;
        }

        public static List<Vehiculos> ValidarMotor(string serie)
        {
            var dt = Util.Query($"SELECT * FROM vehiculos WHERE numeroSerie= '{serie}'");
            var list = new List<Vehiculos>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Vehiculos
                {
                   numeroSerie = (string)dr["numeroSerie"]


                });
            }
            return list;
        }

        public static object List()
        {
            var dt = Util.Query("ListaEntidadVehiculo");
            var list = new List<Tabla>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Tabla
                {
                    id_curp = (string)dr["id_curp"],
                    id_vehiculo = (int)dr["id_vehiculo"],
                    año=(int)dr["año"],
                    nombre=(string)dr["nombre"]+ "\t"+ (string)dr["apellido"],
                    apellido = (string)dr["apellido"],
                    marca = (string)dr["marca"],
                    modelo = (string)dr["modelo"],
                    numeroSerie = (string)dr["numeroSerie"],
                    Estatus =  (dr["asignada"].ToString().Equals(string.Empty))?"Inactiva":((bool)dr["asignada"])? "Activa":"Inactiva",
                    Placa = (dr["placa"]).ToString().Equals(string.Empty)?"Sin placa asignada": (string)dr["placa"]

                });
            }
            return list;
        }

        public static EditarVehiculoTitular getVehiculoTitular(int id)
        {
            var p = new[]
            {
                new SqlParameter("@ID",id)
            };
            var dt = Util.Query("ObtenerVehiculoTitutular",p);


            DataRow dr = dt.Rows[0];
                return new EditarVehiculoTitular()
                {
                    id_curp = (string)dr["id_curp"],
                    nombre = (string)dr["nombre"],
                    apellido = (string)dr["apellido"],
                    calle = (string)dr["calle"],
                    numero = (string)dr["numero"],
                    colonia = (string)dr["colonia"],
                    id_entidad = (int)dr["id_entidad"],
                    id_ciudad = (int)dr["id_ciudad"],
                    año = (int)dr["año"],
                    marca = (string)dr["marca"],
                    modelo = (string)dr["modelo"],
                    numeroSerie = (string)dr["numeroSerie"],
                    id_tipo = (int)dr["id_tpo"],
                    id_entidad2 = (int)dr["id_entidad2"],
                    


                };
            
           
        }

        public static void Update(EditarVehiculoTitular m)
        {
            var p = new SqlParameter[]
            {


                new SqlParameter("@id_entidad2",m.id_entidad2),
                new SqlParameter("@marca",m.marca),
                new SqlParameter("@modelo",m.modelo),
                new SqlParameter("@año",m.año),
                new SqlParameter("@numeroSerie",m.numeroSerie),
                new SqlParameter("@id_tipo",m.id_tipo),
                new SqlParameter("@id_curp",m.id_curp),
                new SqlParameter("@nombre",m.nombre),
                new SqlParameter("@apellido",m.apellido),
                new SqlParameter("@colonia",m.colonia),
                new SqlParameter("@calle",m.calle),
                new SqlParameter("@numero",m.numero),
                new SqlParameter("@id_entidad",m.id_entidad),
                new SqlParameter("@id_ciudad",m.id_ciudad),
                new SqlParameter("@id_vehiculo",m.id_vehiculo),
                new SqlParameter("@CURP",m.CURP)
            };
            Util.Execute("ActualizarVehiculoTitular", p);

        }

        public static int ObtenerID(string modelVehiculoNumeroSerie)
        {

            var dt = Util.Query($"SELECT id_vehiculo FROM vehiculos WHERE numeroSerie= '{modelVehiculoNumeroSerie}'");
            var list = new List<Vehiculos>();
            DataRow dr = dt.Rows[0];
            return  (int)dr["id_vehiculo"];


        }
    }

}
