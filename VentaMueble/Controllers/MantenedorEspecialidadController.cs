using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using System.Collections.Generic;
using System.Linq;

namespace VentaMueble.Controllers
{
    public class MantenedorEspecialidadController : Controller
    {
        private static List<entEspecialidad> lista = new List<entEspecialidad>
        {
            new entEspecialidad { idEspecialidad = 1, nombre = "Cardiología" },
            new entEspecialidad { idEspecialidad = 2, nombre = "Pediatría" }
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
        public IActionResult Insertar(entEspecialidad especialidad)
        {
            if (ModelState.IsValid)
            {
                especialidad.idEspecialidad = ++ultimoId;
                lista.Add(especialidad);
                return RedirectToAction("Index");
            }
            return View("Index", lista);
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var item = lista.FirstOrDefault(e => e.idEspecialidad == id);
            if (item != null)
                lista.Remove(item);
            return RedirectToAction("Index");
        }
    }
}
