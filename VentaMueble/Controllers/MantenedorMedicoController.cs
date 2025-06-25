using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using System.Collections.Generic;
using System.Linq;

namespace VentaMueble.Controllers
{
    public class MantenedorMedicoController : Controller
    {
        private static List<entMedico> listaMedicos = new List<entMedico>
        {
            new entMedico { idMedico = 1, nombre = "Dr. Juan Pérez", especialidad = "Cardiología" },
            new entMedico { idMedico = 2, nombre = "Dra. Ana Torres", especialidad = "Pediatría" }
        };

        private static int ultimoId = 2;

        [HttpGet]
        public IActionResult Index()
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(listaMedicos);

            return View(listaMedicos);
        }

        [HttpPost]
        public IActionResult Insertar(entMedico medico)
        {
            if (ModelState.IsValid)
            {
                ultimoId++;
                medico.idMedico = ultimoId;
                listaMedicos.Add(medico);
                return RedirectToAction("Index");
            }

            return View("Index", listaMedicos);
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var medico = listaMedicos.FirstOrDefault(m => m.idMedico == id);
            if (medico != null)
                listaMedicos.Remove(medico);

            return RedirectToAction("Index");
        }
    }
}
