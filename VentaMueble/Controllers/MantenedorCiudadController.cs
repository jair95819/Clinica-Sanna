using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using System.Collections.Generic;
using System.Linq;

namespace VentaMueble.Controllers
{
    public class MantenedorCiudadController : Controller
    {
        private static List<entCiudad> lista = new List<entCiudad>
        {
            new entCiudad { idCiudad = 1, nombre = "Lima" },
            new entCiudad { idCiudad = 2, nombre = "Arequipa" }
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
        public IActionResult Insertar(entCiudad ciudad)
        {
            if (ModelState.IsValid)
            {
                ciudad.idCiudad = ++ultimoId;
                lista.Add(ciudad);
                return RedirectToAction("Index");
            }
            return View("Index", lista);
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var item = lista.FirstOrDefault(c => c.idCiudad == id);
            if (item != null)
                lista.Remove(item);
            return RedirectToAction("Index");
        }
    }
}