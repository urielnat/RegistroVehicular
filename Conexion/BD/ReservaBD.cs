using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Conexion.MODEL;

namespace Conexion.BD
{
   public class ReservaBD
    {
        public static List<EntidadPatron> Tipos()
        {
            var dt = Util.Query("listaEntidadPatron");
            var list = new List<EntidadPatron>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new EntidadPatron
                {
                    id_tipo = (int)dr["id_tipo"],
                    id_entidad = (int)dr["id_entidad"],
                    patron = (string)dr["patron"],
                    tipo = (string)dr["tipo"]


                });
            }
            return list;
        }

        public static void Create(Reserva m)
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("@id_entidad",m.id_entidad),
                new SqlParameter("@id_tipo", m.id_tipo),
                new SqlParameter("@id_patron", m.id_patron),
                new SqlParameter("@inicio", m.inicio),
                new SqlParameter("@fin", m.fin),
                new SqlParameter("@cantidad", m.cantidad),


            }; //TODO: Populate
            Util.Execute("CrearResevaPlacas", p);
        }

        public static List<Reserva> ValidarPatron(int id_tipo,int id_entidad,int terminado)
        {
            //1 si quieo saber si saber si terminaron
            var dt = Util.Query($"SELECT * FROM ReservaPlacas WHERE id_entidad= {id_entidad} AND id_tipo={id_tipo} AND terminado={terminado}");
            var list = new List<Reserva>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Reserva
                {
                    id_entidad = (int)dr["id_entidad"],
                    id_tipo = (int)dr["id_tipo"],
                    terminado = (bool)dr["terminado"]

                });
            }

         
             
            return list;
        }



       
        public static bool Validarsecuencia(string inicio,string fin,int id_tipo, int id_entidad)
        {
            var autorizado = true;
            var INICIO = ObtenerValorNumerico(inicio);
            var FIN = ObtenerValorNumerico(fin);
            var list = new List<Reserva>();
            var dt = Util.Query($"SELECT * FROM ReservaPlacas WHERE id_entidad= {id_entidad} AND id_tipo={id_tipo}");
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Reserva
                {
                    inicio = ObtenerValorNumerico((string)dr["inicio"]).ToString(),
                    fin = ObtenerValorNumerico((string)dr["fin"]).ToString(),
                


                });
            }


            foreach (var item in list)
            {
                if (INICIO>long.Parse(item.fin))
                {
                    autorizado = true;
                }
                else if (INICIO < long.Parse(item.inicio) && FIN < long.Parse(item.inicio))
                {
                    autorizado = true;
                }
                else
                    autorizado = false;
            }

            return autorizado;
        }


        public static long ObtenerValorNumerico(string valor)
        {
            //char search = '-';
            //var AZ = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ".ToCharArray();


            //valor = valor.Replace("-", string.Empty);
            //char[] array = valor.ToCharArray();
            //var match = false;
            //string auxval = "";

            //for (var i = 0; i < array.Length; i++)
            //{

            //    for (var j = 0; j < AZ.Length; j++)
            //    {
            //        if (array[i] == AZ[j])
            //        {
            //            auxval += j.ToString();
            //            match = true;

            //            break;
            //        }
            //        else
            //        {
            //            match = false;
            //        }

            //    }
            //    if (!match)
            //    {

            //        auxval+=array[i].ToString();

            //    }


            //}
            //return int.Parse(auxval);

           var  placa = valor;
            placa = placa.Replace("-", string.Empty);
            placa = placa.Trim();
            char[] array = placa.ToCharArray();
            Array.Reverse(array);


            string numero = new string(array);
            int y = numero.Length;
            long suma = 0;
            int x;
            string aux = "";




       

           
                var rever = numero.ToCharArray();
                Array.Reverse(rever);
                
                numero = new string(rever);





            for (int i = 0; i < numero.Length; i++)
            {

                string n = numero.Substring(i, 1);

                switch (n)
                {
                    case "A":

                        x = 10;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "B":

                        x = 11;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "C":

                        x = 12;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "D":

                        x = 13;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "E":

                        x = 14;
                        suma = (long)(long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "F":

                        x = 15;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;


                    case "G":

                        x = 16;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "H":

                        x = 17;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "I":

                        x = 18;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "J":

                        x = 19;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "K":

                        x = 20;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "L":

                        x = 21;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "M":

                        x = 22;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "N":

                        x = 23;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "Ñ":

                        x = 24;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "O":

                        x = 25;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "P":

                        x = 26;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "Q":

                        x = 27;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "R":

                        x = 28;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "S":

                        x = 29;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "T":

                        x = 30;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "U":

                        x = 31;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "V":

                        x = 32;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "W":

                        x = 33;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "X":

                        x = 34;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "Y":

                        x = 35;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    case "Z":

                        x = 36;
                        suma = (long)(Math.Pow(36, i) * x) + suma;
                        break;

                    default:

                        x = int.Parse(n);

                        suma =(long)(Math.Pow(36, i) * x) + suma;

                        break;

                }



            }

            return suma;

        }

    }
}
