using System.Diagnostics;
using CapaEntidad;
using CapaLogica;
using Microsoft.AspNetCore.Mvc;

namespace VentaMueble.Controllers
{
    public class MantenedorPacienteController : Controller
    {
        private readonly ILogger<MantenedorPacienteController> _logger;

        // Inyección de dependencias para ILogger
        public MantenedorPacienteController(ILogger<MantenedorPacienteController> logger)
        {
            _logger = logger;
        }

        public IActionResult ListarPaciente()
        {
            List<entPaciente> list = new List<entPaciente>();
           
            list = logPaciente.Instancia.ListarPaciente();
            if (list.Count == 0)
            {
                _logger.LogWarning("No se encontraron Pacientes en la base de datos.");
            }
            else
            {
                _logger.LogInformation($"Se encontraron {list.Count} Pacientes.");
            }
            ViewBag.Lista = list;
            return View(list);
        }

        [HttpGet]
        public IActionResult InsertarPaciente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertarPaciente(entPaciente c)
        {
            try
            {
                Boolean inserta = logPaciente.Instancia.InsertarPaciente(c);

                if (inserta)
                {
                    return RedirectToAction("ListarPaciente");
                }

                else
                {
                    return View(c);
                }
            }


            catch (ApplicationException ex)
            {
                return RedirectToAction("InsertarPaciente", new { mesjExceptio = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult EditarPaciente(int id)
        {
            // Aquí llamas a tu lógica de negocio o acceso a datos
            var Paciente = logPaciente.Instancia.BuscarPaciente(id); // Método de ejemplo
            return View(Paciente);
        }

        [HttpPost]
        public ActionResult EditarPaciente(entPaciente c)
        {
            try
            {
                Boolean edita = logPaciente.Instancia.EditarPaciente(c);
                if (edita)
                {
                    return RedirectToAction("ListarPaciente");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("EditarPaciente", new { mesjExceptio = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult EliminarPaciente(int id)
        {
            // Aquí llamas a tu lógica de negocio o acceso a datos
            var Paciente = logPaciente.Instancia.BuscarPaciente(id); // Método de ejemplo
            return View(Paciente);
        }

        [HttpPost]
        public IActionResult ConfirmarEliminarPaciente(int id)
        {
            try
            {
                Boolean elimina = logPaciente.Instancia.EliminarPaciente(id);
                if (elimina)
                {
                    return RedirectToAction("ListarPaciente");
                }
                else
                {
                    return RedirectToAction("ListarPaciente");
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("ListarPaciente", new { mesjExceptio = ex.Message });
            }
        }
    }
}
