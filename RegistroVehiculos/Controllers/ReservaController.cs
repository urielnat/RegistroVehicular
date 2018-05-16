using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Conexion.BD;
using Conexion.MODEL;
using WebGrease.Css.Ast.Selectors;

namespace RegistroVehiculos.Controllers
{
    public class ReservaController : Controller
    {
        // GET: Reserva
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reserva/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reserva/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reserva/Create
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {


            var model = new Reserva
            {
                id_entidad = int.Parse(form["id_entidad"]),
                id_tipo = int.Parse(form["id_tipo"]),
                id_patron = form["id_patron"],
                inicio = form["INICIO"],
                fin = form["FIN"],
                cantidad = int.Parse(form["TOTAL"]),
                cantidadActual = int.Parse(form["TOTAL"])

            };


            
            if (ReservaBD.ValidarPatron(model.id_tipo,model.id_entidad,0).Count > 0)
            {
                ModelState.AddModelError(String.Empty, "Esta entidad ya tiene placas asignadas para este vehículo, no podrá reservar nuevas hasta que todas las anteriores sean asignadas");


                return View(model);
            }
            else if(!ReservaBD.Validarsecuencia(form["INICIO"], form["FIN"], int.Parse(form["id_tipo"]), int.Parse(form["id_entidad"])))
                {
                    ModelState.AddModelError(String.Empty, "El patron colicina");


                    return View(model);
            }
            else
            {
                ReservaBD.Create(model);
                return RedirectToAction("Index","Vehiculo");
            }

          
        }

        // GET: Reserva/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reserva/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reserva/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reserva/Delete/5
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

        [HttpPost]
        public JsonResult getTipo(int id_entidad)
        {
           

            EntidadPatron[] selEntidadPatron = ReservaBD.Tipos().Where(e => e.id_entidad == id_entidad).ToArray();

            

         
            if (selEntidadPatron.Length < 1)
            {
                selEntidadPatron =(new EntidadPatron[]
                {
                   new EntidadPatron {id_tipo = -1,tipo = "Sin asignar"} 
                });
            }

            return Json(selEntidadPatron, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPatron(int id_tipo,int id_entidad)
        {

            EntidadPatron[] selPatron = ReservaBD.Tipos().Where(e => e.id_tipo == id_tipo && e.id_entidad==id_entidad  ).ToArray();
            return Json(selPatron, JsonRequestBehavior.AllowGet);
        }
    }
}
