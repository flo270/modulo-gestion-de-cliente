using Modulo_GestionC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Modulo_GestionC.Controllers
{
    [Authorize(Roles = "CPN,Empleado")]
    public class ImpuestoController : Controller
    {
        public ActionResult Index(string searchString)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                var Impuestos = from s in db.Impuesto
                                select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    Impuestos = Impuestos.Where(s => s.NombreImpuesto.Contains(searchString));
                }

                return View(Impuestos.ToList());
            }

        }
        //crearImpuesto
        public ActionResult Crear()
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                return View();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Impuesto imp)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        imp.FechaRegistro = DateTime.Now;
                        db.Impuesto.Add(imp);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {

                        ModelState.AddModelError("ERROR AL AGREGARImpuesto", ex);
                        return View();
                    }
                }
                return View();
            }

        }
        //EditarImpuesto
        public ActionResult Edit(int id)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                Impuesto pac = db.Impuesto.Where(p => p.IdImp == id).FirstOrDefault();

                return View(pac);
            }
        }

        // POST:/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Impuesto p)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                Impuesto pac = db.Impuesto.Where(p1 => p1.IdImp == id).FirstOrDefault();

                pac.NombreImpuesto= p.NombreImpuesto;
                pac.FechaVencimiento = p.FechaVencimiento;
                pac.Monto = p.Monto;
                pac.FechaPago = p.FechaPago;
                pac.TipoImpuesto = p.TipoImpuesto;
                pac.ClientePerteneciente = p.ClientePerteneciente;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        //detallarImpuesto
        public ActionResult Details(int id)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                Impuesto pac = db.Impuesto.Find(id);
                return View(pac);

            }
        }
        //eliminarImpuesto
        public ActionResult Delete(int? id)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                Impuesto pac = db.Impuesto.Find(id);
                if (pac == null)
                {
                    return HttpNotFound();
                }
                return View(pac);
            }


        }

        // POST:Impuesto/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                Impuesto pac = db.Impuesto.Find(id);
                db.Impuesto.Remove(pac);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
        }
        //combo
        public ActionResult ListarCliente()
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                return PartialView(db.Cliente.ToList());
            }
        }
        // mostrar datos ri
        public static string Cliente(int? cc)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {

                string Clientes = db.Cliente.Find(cc).NombreContribuyente +
                    " " + db.Cliente.Find(cc).CUIT.ToString() + " " + db.Cliente.Find(cc).TipoCliente + " " +
                    db.Cliente.Find(cc).RegimenImpositivo.NombreRegimenImpositivo+ " "
                    + db.Cliente.Find(cc).RegimenImpositivo.Categoria + ".";
                return Clientes;

            }
        }
        // fin

    }
}