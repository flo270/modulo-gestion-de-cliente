using Modulo_GestionC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Modulo_GestionC.Controllers
{
    [Authorize(Roles = "CPN,Empleado,Administrador")]
    public class MostrarTodoController : Controller
    {

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
        public ActionResult Impuesto(string searchString)
        {
            using (ContextoModuloG db = new ContextoModuloG())
            {
                var Impuestos = from s in db.Impuesto
                                select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    Impuestos = Impuestos.Where(s => s.NombreImpuesto.Contains(searchString)||
                    s.Cliente.CUIT.ToString().Contains(searchString)
                                           || s.Cliente.NombreContribuyente.Contains(searchString));
                }

                return View(Impuestos.ToList());
            }

        }
    }
}