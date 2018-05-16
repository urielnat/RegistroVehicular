using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conexion.MODEL;

namespace Conexion.BD
{
    public class PlacaBD
    {
        public static void Create(EntidadPatron m)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("@id_entidad", m.id_entidad),
                new SqlParameter("@id_tipo", m.id_tipo),
                new SqlParameter("@patron", m.patron)

            }; //TODO: Populate
            Util.Execute("CrearEntidadPatron", p);
        }

        public static List<EntidadPatron> ValidarEntidad(int id_entidad, int id_tipo)
        {
            var dt = Util.Query($"SELECT * FROM entidadPatron WHERE id_entidad= {id_entidad} AND id_tipo={id_tipo}");
            var list = new List<EntidadPatron>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new EntidadPatron
                {
                    id_entidad = (int) dr["id_entidad"],
                    id_tipo = (int) dr["id_tipo"],
                    patron = (string) dr["patron"]

                });
            }

            return list;
        }

        public static List<EntidadPatron> ValidarPatron(string patron)
        {
            var dt = Util.Query($"SELECT * FROM entidadPatron WHERE patron= '{patron}' ");
            var list = new List<EntidadPatron>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new EntidadPatron
                {
                    id_entidad = (int) dr["id_entidad"],
                    id_tipo = (int) dr["id_tipo"],
                    patron = (string) dr["patron"]

                });
            }

            return list;
        }

        public static Placa ObtenerDatosTitularVehiculo(int id)
        {
            var p = new[]
            {
                new SqlParameter("@ID", id)
            };
            var dt = Util.Query("ObtenerPatronVehiculo", p);
            DataRow dr = dt.Rows[0];
            return new Placa
            {
                id_vehiculo = (int) dr["id_vehiculo"],
                numeroSerie = (string) dr["numeroSerie"],
                datosVehiculo = (string) dr["marca"] + " / " + (string) dr["modelo"] + " / " + (int) dr["año"],
                NombreTitular = (string) dr["nombre"] + " " + (string) dr["apellido"],
                placa = "XXX-XX-XX",
                id_entidad = (int) dr["id_entidad"],
                id_tipo = (int) dr["id_tpo"]
            };
        }

        public static string GenerarPlaca(string s)
        {


            if (!s.Contains("$"))
            {
                string placa = s.Trim();
                placa = placa.Replace("$", string.Empty);

                char search = '-';

                var result = placa.Select((b,i) => b.Equals(search) ? i : -1).Where(i => i != -1);


                placa = placa.Replace("-", string.Empty);
                char[] array = placa.ToCharArray();
                Array.Reverse(array);
                var AZ = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ".ToCharArray();


                for (var i = 0; i < array.Length; i++)
                {

                    if (!Char.IsNumber(array[i]))
                    {


                        for (var j = 0; j < AZ.Length; j++)
                        {

                            if (j + 1 != AZ.Length && array[i] == AZ[j] && array[i] != 'Z')
                            {
                                array[i] = AZ[j + 1];
                                break;
                            }

                        }

                        if (array[i] == 'Z' && i + 1 != array.Length)
                        {
                            array[i] = 'A';


                        }
                        else break;


                    }
                    else if (array[i] != '9')
                    {

                        array[i] = (char) (array[i] + 1);
                        break;
                    }
                    else if (i + 1 != array.Length)
                    {
                        array[i] = '0';

                    }
                    else
                    {
                        break;
                    }

                }



                Array.Reverse(array);
                var placaGenerada = "";
                foreach (var x in array)
                {
                    placaGenerada += x;
                }

                foreach (var index in result)
                {

                    placaGenerada = placaGenerada.Insert(index, "-");

                }

                return placaGenerada;
            }
            else return s.Replace("$", string.Empty);
        }

        public static string UltimaPlaca(int idEntidad, int idTipo)
        {
            var placaBase = "Esta oficina aun no ha reservado placas";
            var dt = Util.Query($"SELECT inicio FROM ReservaPlacas WHERE id_entidad= {idEntidad} AND id_tipo={idTipo}");

            foreach (DataRow dr in dt.Rows)
            {
                placaBase = dr["inicio"].ToString().Trim();
            }

            if (dt.Rows.Count > 0)
            {
                placaBase += "$";
            }

            var dt2 = Util.Query($"SELECT placa FROM Placas WHERE id_entidad= {idEntidad} AND id_tipo={idTipo}");
            var list = new List<Placa>();

            if (dt2.Rows.Count > 0)
            {

                foreach (DataRow dr in dt2.Rows)
                {
                    list.Add(new Placa
                    {
                        placa = (string) dr["placa"]
                    });
                }


                return (list.Last() as Placa).placa;
            }

            return placaBase;
        }

        public static void CrearPlaca(Placa m)
        {
            var p = new SqlParameter[]
            {


                new SqlParameter("@id_entidad", m.id_entidad),
                new SqlParameter("@placa", m.placa),
                new SqlParameter("@id_tipo", m.id_tipo),
                new SqlParameter("@id_vehiculo", m.id_vehiculo),
                new SqlParameter("@asignada", m.asignada),


            };
            Util.Execute("CrearPlaca", p);
        }

        public static void AsignarFinal(string placa)
        {
            var dt = Util.Query($"SELECT id_reserva FROM ReservaPlacas WHERE fin= '{placa}'");
            

            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0];

                var id_reserva = (int) dr["id_reserva"];

                var p = new SqlParameter[]
                {


                    new SqlParameter("@id_reserva", id_reserva),
                    


                };
                Util.Execute("TerminaReserva", p);
            }

            
        }


        public static bool validarFinal( int id_tipo, int id_entidad)
        {
            var dt = Util.Query($"SELECT terminado FROM ReservaPlacas WHERE id_tipo= {id_tipo} AND id_entidad={id_entidad}");
            var list = new List<Reserva>();
            foreach (DataRow item in dt.Rows)
            {
                list.Add(new Reserva
                {
                    terminado = (bool)item["terminado"]
                });
            }

            return list.Last().terminado;
        }
    }
}
