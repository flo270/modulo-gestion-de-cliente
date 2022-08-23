using Modulo_GestionC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Modulo_GestionC.Controllers
{
   [Authorize (Roles="CPN,Empleado")]
    public class ClienteController : Controller
    {
        // GET: Cliente
        [Authorize(Roles = "CPN,Empleado,Administrador")]
        public ActionResult Index(string searchString, string sortOrder)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                var Clientes = from s in db.Cliente
                               select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    Clientes = Clientes.Where(s => s.CUIT.ToString().Contains(searchString)
                                           || s.NombreContribuyente.Contains(searchString) ||
                                           s.TipoCliente.Contains(searchString) ||
                                           s.RegimenImpositivo.NombreRegimenImpositivo.Contains(searchString) ||
                                           s.RegimenImpositivo.Categoria.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        Clientes = Clientes.OrderByDescending(s => s.NombreContribuyente);
                        break;

                    default:
                        Clientes = Clientes.OrderBy(s => s.NombreContribuyente);
                        break;
                }
                return View(Clientes.ToList());
            }

        }
        //crear cliente
        public ActionResult Crear()
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                return View();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Cliente cl)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                if (db.Cliente.Any(p => p.CUIT == cl.CUIT))
                {
                    ModelState.AddModelError("CUITI", "Ya existe CUIT ingresado");
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        cl.FechaRegistro = DateTime.Now;
                        db.Cliente.Add(cl);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {

                        ModelState.AddModelError("ERROR AL AGREGAR Cliente", ex);
                        return View();
                    }
                }
                return View();
            }

        }
        //Editar cliente
        public ActionResult Edit(int id)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                Cliente pac = db.Cliente.Where(p => p.IdCliente == id).FirstOrDefault();

                return View(pac);
            }
        }

        // POST:/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Cliente p)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                Cliente pac = db.Cliente.Where(p1 => p1.IdCliente== id).FirstOrDefault();

                pac.NombreContribuyente = p.NombreContribuyente;
                pac.CUIT = p.CUIT;

                pac.Direccion = p.Direccion;
                pac.Telefono = p.Telefono;
                pac.Mail = p.Mail;
                pac.TipoCliente = p.TipoCliente;
                pac.RegimenImpositivoPerteneciente = p.RegimenImpositivoPerteneciente;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        //detallar cliente
        public ActionResult Details(int id)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                Cliente pac = db.Cliente.Find(id);
                return View(pac);

            }
        }
        //eliminar cliente
        public ActionResult Delete(int? id)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                Cliente pac = db.Cliente.Find(id);
                if (pac == null)
                {
                    return HttpNotFound();
                }
                return View(pac);
            }


        }

        // POST: Cliente/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                Cliente pac = db.Cliente.Find(id);
                db.Cliente.Remove(pac);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
        }
        //combo
        public ActionResult ListarRI()
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                return PartialView(db.RegimenImpositivo.ToList());
            }
        }
        // mostrar datos ri
        public static string RI(int? cRI)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {

                string pacientes = db.RegimenImpositivo.Find(cRI).NombreRegimenImpositivo +
                    " " + db.RegimenImpositivo.Find(cRI).Categoria + ".";
                return pacientes;

            }
        }
        // fin

    }
}