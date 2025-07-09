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
            List<entPaciente> list = logPaciente.Instancia.ListarPaciente();

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

        // Acción para mostrar el formulario de registro
        [HttpGet]
        public IActionResult RegistrarPaciente()
        {
            return View();
        }

        // Acción para manejar el registro de un paciente
        [HttpPost]
        public IActionResult RegistrarPaciente(entPaciente paciente)
        {
            try
            {
                // Establecemos la fecha de creación del paciente
                paciente.fecCreacion = DateTime.Now;

                // Llamamos al servicio para insertar el paciente
                bool registrado = logPaciente.Instancia.InsertarPaciente(paciente);

                if (registrado)
                {
                    return RedirectToAction("ListarPaciente");  // Redirige al listado de pacientes
                }
                else
                {
                    ViewBag.ErrorMessage = "Hubo un error al registrar al paciente.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
                return View();
            }
        }

        // Acción para editar paciente
        [HttpGet]
        public IActionResult EditarPaciente(int id)
        {
            var paciente = logPaciente.Instancia.BuscarPaciente(id);
            return View(paciente);
        }

        [HttpPost]
        public IActionResult EditarPaciente(entPaciente paciente)
        {
            try
            {
                bool edita = logPaciente.Instancia.EditarPaciente(paciente);

                if (edita)
                {
                    return RedirectToAction("ListarPaciente");
                }
                else
                {
                    return View(paciente);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
                return View(paciente);
            }
        }

        // Acción para eliminar paciente
        [HttpGet]
        public IActionResult EliminarPaciente(int id)
        {
            var paciente = logPaciente.Instancia.BuscarPaciente(id);
            return View(paciente);
        }

        [HttpPost]
        public IActionResult ConfirmarEliminarPaciente(int id)
        {
            try
            {
                bool elimina = logPaciente.Instancia.EliminarPaciente(id);

                if (elimina)
                {
                    return RedirectToAction("ListarPaciente");
                }
                else
                {
                    return RedirectToAction("ListarPaciente");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
                return RedirectToAction("ListarPaciente");
            }
        }
    }
}
