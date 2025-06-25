using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using System.Collections.Generic;
using System.Linq;

namespace VentaMueble.Controllers
{
    public class MantenedorSedeController : Controller
    {
        private static List<entSede> lista = new List<entSede>
        {
            new entSede { idSede = 1, nombre = "Sede Central", direccion = "Av. Lima 123" },
            new entSede { idSede = 2, nombre = "Sede Norte", direccion = "Calle Norte 456" }
        };
        private static int ultimoId = 2;

        [HttpGet]
        public IActionResult Index()
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(lista);
            return View(lista);
        }

        [HttpPost]
        public IActionResult Insertar(entSede sede)
        {
            if (ModelState.IsValid)
            {
                sede.idSede = ++ultimoId;
                lista.Add(sede);
                return RedirectToAction("Index");
            }
            return View("Index", lista);
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var item = lista.FirstOrDefault(s => s.idSede == id);
            if (item != null)
                lista.Remove(item);
            return RedirectToAction("Index");
        }
    }
}
