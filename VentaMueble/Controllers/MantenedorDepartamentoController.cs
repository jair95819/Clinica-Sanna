using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using System.Collections.Generic;
using System.Linq;

namespace VentaMueble.Controllers
{
    public class MantenedorDepartamentoController : Controller
    {
        private static List<entDepartamento> lista = new List<entDepartamento>
        {
            new entDepartamento { idDepartamento = 1, nombre = "Lima" },
            new entDepartamento { idDepartamento = 2, nombre = "Cusco" }
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
        public IActionResult Insertar(entDepartamento departamento)
        {
            if (ModelState.IsValid)
            {
                departamento.idDepartamento = ++ultimoId;
                lista.Add(departamento);
                return RedirectToAction("Index");
            }
            return View("Index", lista);
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var item = lista.FirstOrDefault(d => d.idDepartamento == id);
            if (item != null)
                lista.Remove(item);
            return RedirectToAction("Index");
        }
    }
}