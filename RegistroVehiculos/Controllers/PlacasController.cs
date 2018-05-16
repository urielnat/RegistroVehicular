using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Conexion.BD;
using Conexion.MODEL;

namespace RegistroVehiculos.Controllers
{
    public class PlacasController : Controller
    {
        // GET: Placas
        public ActionResult Index()
        {
            return View();
        }

        // GET: Placas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Placas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Placas/Create
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
           
                var model = new EntidadPatron
                {
                    id_entidad = int.Parse(form["id_entidad"]),
                    id_tipo = int.Parse(form["id_tipo"]),
                    patron = form["patron"]

                };
               
                if (PlacaBD.ValidarEntidad(model.id_entidad, model.id_tipo).Select(o => new SelectListItem { Text = o.patron })
                        .ToList().Count > 0)
                {
                    ModelState.AddModelError(string.Empty, "Esta entidad ya tiene placas asignadas para este vehiculo");
                
                   
                    return View(model);
                }
            if (PlacaBD.ValidarPatron(model.patron).Count > 0)
            {
                ModelState.AddModelError(string.Empty, "Esta patron ya esta siendo utilizado");


                return View(model);
            }
            else
                {
                    PlacaBD.Create(model);
                    return RedirectToAction("Index","Vehiculo");
                }


           
            
           
               
            
        }

        // GET: Placas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Placas/Edit/5
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

        // GET: Placas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Placas/Delete/5
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
    }
}
