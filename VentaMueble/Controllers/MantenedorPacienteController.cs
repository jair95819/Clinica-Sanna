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
        public ActionResult InsertarPaciente(entPaciente c, string email, string password)
        {
            try
            {
                // Primero insertar el usuario
                var nuevoUsuario = new entUsuario
                {
                    Email = email,
                    Password = password,
                    TipousuarioID = 1 // Suponiendo que 1 es Paciente
                };

                int nuevoUsuarioID = logUsuario.Instancia.InsertarUsuarioYDevolverID(nuevoUsuario);

                // Luego insertar el paciente con ese UsuarioID
                c.UsuarioID = nuevoUsuarioID;
                bool inserta = logPaciente.Instancia.InsertarPaciente(c);

                if (inserta)
                {
                    TempData["RegistroExitoso"] = "¡Registro exitoso! Ahora puedes iniciar sesión.";
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ViewBag.Error = "No se pudo registrar.";
                    return View(c);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View(c);
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
                // Recupera el paciente actual desde la BD
                var pacienteActual = logPaciente.Instancia.BuscarPaciente(c.PacienteID);

                if (pacienteActual == null)
                {
                    ViewBag.Error = "Paciente no encontrado.";
                    return View(c);
                }

                // Actualiza solo los campos editables
                pacienteActual.Nombres = c.Nombres;
                pacienteActual.Apellidos = c.Apellidos;
                pacienteActual.NumDoc = c.NumDoc;
                pacienteActual.FechaNacimiento = c.FechaNacimiento;
                pacienteActual.Telefono = c.Telefono;
                pacienteActual.Sexo = c.Sexo;
                pacienteActual.Estado = c.Estado;

                bool edita = logPaciente.Instancia.EditarPaciente(pacienteActual);
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
        public IActionResult DeshabilitarPaciente(int id)
        {
            // Buscamos el paciente que queremos deshabilitar
            var paciente = logPaciente.Instancia.BuscarPaciente(id);
            return View(paciente); // Mostramos la vista con los datos del paciente
        }


        [HttpPost]
        public IActionResult ConfirmarDeshabilitarPaciente(int id)
        {
            try
            {
                bool deshabilita = logPaciente.Instancia.DeshabilitarPaciente(id);
                if (deshabilita)
                {
                    return RedirectToAction("ListarPaciente");
                }
                else
                {
                    ViewBag.Error = "No se pudo deshabilitar el paciente.";
                    return RedirectToAction("ListarPaciente");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al deshabilitar: " + ex.Message;
                return RedirectToAction("ListarPaciente");
            }
        }
        public IActionResult Perfil()
        {
            // Obtén el ID del paciente desde la sesión
            int? pacienteId = HttpContext.Session.GetInt32("PacienteID");

            if (pacienteId == null)
            {
                // No está logueado, redirige al login
                return RedirectToAction("Login", "Home");
            }

            var paciente = logPaciente.Instancia.BuscarPaciente(pacienteId.Value);
            return View(paciente);
        }

        [HttpPost]
        public IActionResult Perfil(entPaciente c)
        {
            try
            {
                var pacienteActual = logPaciente.Instancia.BuscarPaciente(c.PacienteID);
                if (pacienteActual == null)
                {
                    ViewBag.Error = "Paciente no encontrado.";
                    return View(c);
                }

                // Solo actualiza campos que puede cambiar
                pacienteActual.Nombres = c.Nombres;
                pacienteActual.Apellidos = c.Apellidos;
                pacienteActual.NumDoc = c.NumDoc;
                pacienteActual.FechaNacimiento = c.FechaNacimiento;
                pacienteActual.Telefono = c.Telefono;
                pacienteActual.Sexo = c.Sexo;

                bool edita = logPaciente.Instancia.EditarPaciente(pacienteActual);
                if (edita)
                {
                    ViewBag.Mensaje = "Datos actualizados correctamente.";
                }
                else
                {
                    ViewBag.Error = "No se pudo actualizar.";
                }

                return View(pacienteActual);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View(c);
            }
        }

    }
}
