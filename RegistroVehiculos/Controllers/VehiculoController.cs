using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Conexion.BD;
using Conexion.MODEL;
using Microsoft.Ajax.Utilities;

namespace RegistroVehiculos.Controllers
{
    public class VehiculoController : Controller
    {
        private static FormCollection formVehiculo;
        private static int id_tipo;
        private static int id_entidad;
        private static string datosvehiculo, datostitular, numeromotor;
        private static string CURP;

        //Get: crear titular
        public ActionResult CreateTitular()
        {
            return View(TempData["titular"]);
        }

        //Post guardar titular y vehículo
        [HttpPost]
        public ActionResult CreateTitular(FormCollection form, String command)
        {
            var modelVehiculo = new Vehiculos
            {
                id_entidad = int.Parse(formVehiculo["id_entidad"]),
                id_tipo = int.Parse(formVehiculo["id_tipo"]),
                marca = formVehiculo["marca"],
                año = int.Parse(formVehiculo["año"]),
                modelo = formVehiculo["modelo"],
                numeroSerie = formVehiculo["numeroSerie"],
                id_curp = form["id_curp"]
                };

          

            if (command.Equals("Anterior"))
            {
                var modelTitular = new Titular
                {
                    id_curp = form["id_curp"],
                    nombre = form["nombre"],
                    apellido = form["apellido"],
                    calle = form["calle"],
                    colonia = form["colonia"],
                    id_entidad = int.Parse(form["id_entidad"]),
                    id_ciudad = int.Parse(form["Ciudad"]),
                    numero = form["numero"]


                };
               
                TempData["idCiudad"] = modelTitular.id_ciudad;

                TempData["vehiculo"] = modelVehiculo;
                TempData["titular"] = modelTitular;

                return RedirectToAction("Create");
            }
            if (command.Equals("Guardar"))
            {
                var modelTitular = new Titular
                {
                    id_curp = form["id_curp"],
                    nombre = form["nombre"],
                    apellido = form["apellido"],
                    calle = form["calle"],
                    colonia = form["colonia"],
                    id_entidad = int.Parse(form["id_entidad"]),
                    id_ciudad = int.Parse(form["Ciudad"]),
                    numero = form["numero"]


                };



                VehiculoBD.Create(modelVehiculo);
                    VehiculoBD.CreateTitular(modelTitular);
                var x =VehiculoBD.ObtenerID(modelVehiculo.numeroSerie);

                return RedirectToAction("Asignar", new { id = x });
                
            }

            if (command.Equals("Buscar"))
            {
               
                var model = VehiculoBD.Buscar(form["id_curp"]);
                TempData["idCiudad"] = model.id_ciudad;
                
                return View(model);
                //asignar ese valor al return View()

            }
            else
            {
                return RedirectToAction("Index");
            }

          

        }



        // GET: Vehiculo
        public ActionResult Index()
        {
            var model = VehiculoBD.List();
            return View(model);
        }

        /// asignar placa
        public ActionResult Asignar(int id)
        {
            var model = PlacaBD.ObtenerDatosTitularVehiculo(id);
            id_entidad = model.id_entidad;
            id_tipo = model.id_tipo;
            datostitular = model.NombreTitular;
            datosvehiculo = model.datosVehiculo;
            numeromotor = model.numeroSerie;
            
            return View(model);
        }


        [HttpPost]
        public ActionResult Asignar(int id, FormCollection form)
        {

            var placaBase = PlacaBD.UltimaPlaca(id_entidad,id_tipo);   
            var placa = PlacaBD.GenerarPlaca(placaBase);
            if (placa.Contains("oficina"))
            {
                ModelState.AddModelError(string.Empty, "La oficina donde se registro este vehículo aun no ha establecido un patron de placas o un rango para este tipo de vehiculo");
                return View(new Placa {numeroSerie = numeromotor,datosVehiculo = datosvehiculo,NombreTitular = datostitular});
            }else if (PlacaBD.validarFinal(id_tipo, id_entidad))
            {
                ModelState.AddModelError(string.Empty, "Las placas para esta oficina se han terminado por favor asigne nuevas");
                return View(new Placa { numeroSerie = numeromotor, datosVehiculo = datosvehiculo, NombreTitular = datostitular });
            }


            else { 

            var model = new Placa
            {
                id_vehiculo = id,
                placa = placa,
                id_entidad = id_entidad,
                id_tipo = id_tipo,
                 asignada = true,
                datosVehiculo = datosvehiculo,
                NombreTitular = datostitular,
                numeroSerie = numeromotor,
                IsDisabled = "disabled"
                

            };
                ViewData["IsEnabled"] = false;

                PlacaBD.AsignarFinal(placa);
            PlacaBD.CrearPlaca(model);
                string path = @"C:\Users\VENTUS_DGO01\Desktop\PlacasGeneradas.txt";
                using (StreamWriter w = System.IO.File.AppendText(path))
                {
                    Log(model.id_vehiculo.ToString(),model.placa, w);
                    w.Close();
                }
                return View(model);
            }
        }


        // GET: Vehiculo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Vehiculo/Create
        public ActionResult Create()
        {
            
            return View(TempData["vehiculo"]);
        }

        // POST: Vehiculo/Create
        [HttpPost]
        public ActionResult Create(FormCollection form,String command)
        {


            if (command.Equals("Siguiente"))
            {
                if (VehiculoBD.ValidarMotor(form["numeroSerie"]).Select(o => new SelectListItem {Text = o.numeroSerie})
                        .ToList().Count > 0)
                {
                    ModelState.AddModelError(string.Empty, "Este vehículo ya fue dado de alta");
                    var modelVehiculo = new Vehiculos
                    {
                        id_entidad = int.Parse(form["id_entidad"]),
                        id_tipo = int.Parse(form["id_tipo"]),
                        marca = form["marca"],
                        año = int.Parse(form["año"]),
                        modelo = form["modelo"],
                        numeroSerie = form["numeroSerie"]
                    };

                    return View(modelVehiculo);
                }
                else
                {
                    formVehiculo = form;
                    return RedirectToAction("CreateTitular");

                }
            }
          
            else
            {
                return View();

            }




        }

        // GET: Vehiculo/Edit/5
        public ActionResult Edit(int id)
        {
            var model = VehiculoBD.getVehiculoTitular(id);
            TempData["idCiudad"] = model.id_ciudad;
            CURP = model.id_curp;
            return View(model);
        }

        // POST: Vehiculo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection dr)
        {



            var model = new EditarVehiculoTitular
            {
                CURP = CURP,
                id_curp =dr["id_curp"],
                nombre = dr["nombre"],
                apellido = dr["apellido"],
                calle = dr["calle"],
                numero = dr["numero"],
                colonia = dr["colonia"],
                id_entidad = int.Parse(dr["id_entidad"]),
                id_ciudad = int.Parse(dr["Ciudad"]),
                año = int.Parse(dr["año"]),
                marca = dr["marca"],
                modelo = dr["modelo"],
                numeroSerie = dr["numeroSerie"],
                id_tipo = int.Parse(dr["id_tipo"]),
                id_entidad2 = int.Parse(dr["id_entidad2"]),
                id_vehiculo = id
            };
            VehiculoBD.Update(model);
            return RedirectToAction("Index");
        }

        // GET: Vehiculo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Vehiculo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public static List<SelectListItem> ListaEntidades()
        {
            return VehiculoBD.Entidades().Select(o => new SelectListItem { Text = o.entidad.ToString(), Value = o.id_entidad.ToString() }).ToList();
        }

        public static IEnumerable<SelectListItem> ListaTipos()
        {
            return VehiculoBD.Tipos().Select(o => new SelectListItem { Text = o.tipo.ToString(), Value = o.id_tipo.ToString() }).ToList();
        }

        public static IEnumerable<SelectListItem> ListaCiudades()
        {
            return VehiculoBD.Ciudades().Select(o => new SelectListItem { Text = o.ciudad.ToString(), Value = o.id_ciudad.ToString() }).ToList();
        }

        
        [HttpPost]
        public JsonResult getCiudad(int id_entidad)
        {
            List<Ciudad> ciudads = new List<Ciudad>();
            ciudads= VehiculoBD.Ciudades();

            Ciudad[] selCiudad = ciudads.Where(e => e.id_entidad == id_entidad).OrderBy(x => x.ciudad).ToArray();

            return Json(selCiudad, JsonRequestBehavior.AllowGet);
        }



        public List<Titular> GetTitular(int curp)
        {
            return VehiculoBD.titular(curp);
        }

        public ActionResult AutocompModelo(string bus)
        {
            return Json(VehiculoBD.AutocompModelo(bus).Select(o => new SelectListItem { Text = o.modelo }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutocompMarca(string bus)
        {
            return Json(VehiculoBD.AutocompMarca(bus).Select(o => new SelectListItem { Text = o.marca }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutocompCol(string bus)
        {
            return Json(VehiculoBD.AutocompColonia(bus).Select(o => new SelectListItem { Text = o.colonia }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ValidarNumSerie(string serie)
        {
            var numero = VehiculoBD.ValidarMotor(serie).Select(o => new SelectListItem { Text = o.numeroSerie }).ToList();
            bool result = numero.Count > 0 ? false : true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public static void Log(string id,string placa, TextWriter w)
        {
            w.Write("\r\nFecha : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine($"ID del vehículo:  {id}");
            w.WriteLine($"Placa:            {placa}");
            w.WriteLine("------------------------------------------------");
        }


    }
}
