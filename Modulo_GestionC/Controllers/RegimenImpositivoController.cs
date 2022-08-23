using Modulo_GestionC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Modulo_GestionC.Controllers
{
    //Authorize(Roles = "CPN")]
    public class RegimenImpositivoController : Controller
    {
        //[Authorize(Roles = "CPN,Empleado")]
        public ActionResult Index(string searchString)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                var RegimenImpositivos = from s in db.RegimenImpositivo
                                         select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    RegimenImpositivos = RegimenImpositivos.Where(s => s.NombreRegimenImpositivo.Contains(searchString));
                }

                return View(RegimenImpositivos.ToList());
            }

        }
        //crearRegimenImpositivo
        public ActionResult Crear()
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                return View();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(RegimenImpositivo ri)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        ri.FechaRgistro = DateTime.Now;
                        db.RegimenImpositivo.Add(ri);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {

                        ModelState.AddModelError("ERROR AL AGREGARRegimenImpositivo", ex);
                        return View();
                    }
                }
                return View();
            }

        }
        //EditarRegimenImpositivo
        public ActionResult Edit(int id)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                RegimenImpositivo reg = db.RegimenImpositivo.Find(id);

                return View(reg);
            }
        }

        // POST:/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RegimenImpositivo p)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                RegimenImpositivo reg = db.RegimenImpositivo.Find(id);

                reg.NombreRegimenImpositivo = p.NombreRegimenImpositivo;
                reg.Categoria = p.Categoria;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        //detallarRegimenImpositivo
        public ActionResult Details(int id)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                RegimenImpositivo reg = db.RegimenImpositivo.Find(id);
                return View(reg);

            }
        }
        //eliminarRegimenImpositivo
        public ActionResult Delete(int? id)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                RegimenImpositivo reg = db.RegimenImpositivo.Find(id);
                if (reg == null)
                {
                    return HttpNotFound();
                }
                return View(reg);
            }


        }

        // POST:RegimenImpositivo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                RegimenImpositivo reg = db.RegimenImpositivo.Find(id);
                db.RegimenImpositivo.Remove(reg);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
        }

    }
}