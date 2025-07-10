using CapaEntidad;
using CapaLogica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VentaMueble.Controllers
{
    public class MantenedorMedicoController : Controller
    {
        private readonly ILogger<MantenedorMedicoController> _logger;

        public MantenedorMedicoController(ILogger<MantenedorMedicoController> logger)
        {
            _logger = logger;
        }

        // Lista de médicos
        public IActionResult ListarMedico()
        {
            var lista = logMedico.Instancia.ListarMedico();
            ViewBag.Lista = lista;
            return View(lista);
        }

        // Vista para insertar un nuevo médico
        [HttpGet]
        public IActionResult InsertarMedico()
        {
            // Obtener las especialidades activas desde la lógica de negocio
            var especialidades = logEspecialidad.Instancia.ListarEspecialidadesActivas();

            // Verificar si la lista de especialidades no es nula ni vacía
            if (especialidades == null || !especialidades.Any())
            {
                ViewBag.Error = "No se encontraron especialidades para seleccionar.";
                return View();
            }

            // Convertir las especialidades a SelectListItem
            ViewBag.Especialidades = especialidades
                .Select(e => new SelectListItem
                {
                    Value = e.EspecialidadID.ToString(),  // Asegúrate de que sea el ID de la especialidad
                    Text = e.Nombre                       // El nombre de la especialidad
                }).ToList();

            return View();
        }


        // Insertar un nuevo médico
        [HttpPost]
        public IActionResult InsertarMedico(entMedico m, string email, string password)
        {
            try
            {
                // Primero insertar el usuario
                var nuevoUsuario = new entUsuario
                {
                    Email = email,
                    Password = password,
                    TipousuarioID = 2 // Suponiendo que 2 es Médico
                };

                // Insertar el usuario y obtener su ID
                int nuevoUsuarioID = logUsuario.Instancia.InsertarUsuarioYDevolverID(nuevoUsuario);

                if (nuevoUsuarioID <= 0)
                {
                    ViewBag.Error = "Error al crear el usuario.";
                    return View(m);
                }

                // Luego insertar el médico con ese UsuarioID
                m.UsuarioID = nuevoUsuarioID;
                bool inserta = logMedico.Instancia.InsertarMedico(m);

                if (inserta)
                {
                    // Insertar especialidades para el dropdown
                    var especialidades = logEspecialidad.Instancia.ListarEspecialidadesActivas();
                    ViewBag.Especialidades = new SelectList(especialidades, "EspecialidadID", "Nombre");

                    // Mostrar mensaje de éxito
                    TempData["RegistroExitoso"] = "¡Registro exitoso! Ahora puede iniciar sesión.";

                    // Renderizar la vista actual con el mensaje de éxito
                    return RedirectToAction("ListarMedico", "MantenedorMedico");
                }
                else
                {
                    ViewBag.Error = "No se pudo registrar el médico.";
                    return View(m);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View(m);
            }
        }




        // Vista para deshabilitar un médico
        [HttpGet]
        public IActionResult DeshabilitarMedico(int id)
        {
            var medico = logMedico.Instancia.BuscarMedico(id);
            return View(medico);
        }

        // Confirmar deshabilitación de un médico
        [HttpPost]
        public IActionResult ConfirmarDeshabilitarMedico(int id)
        {
            try
            {
                bool deshabilita = logMedico.Instancia.DeshabilitarMedico(id);
                if (deshabilita)
                {
                    return RedirectToAction("ListarMedico");
                }
                else
                {
                    ViewBag.Error = "No se pudo deshabilitar el médico.";
                    return RedirectToAction("ListarMedico");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al deshabilitar: " + ex.Message;
                return RedirectToAction("ListarMedico");
            }
        }

        // Vista para el perfil del médico
        public IActionResult Perfil()
        {
            int? medicoId = HttpContext.Session.GetInt32("MedicoID");

            if (medicoId == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var medico = logMedico.Instancia.BuscarMedico(medicoId.Value);
            return View(medico);
        }

        // Actualizar el perfil del médico
        [HttpPost]
        public IActionResult Perfil(entMedico m)
        {
            try
            {
                var medicoActual = logMedico.Instancia.BuscarMedico(m.MedicoID);
                if (medicoActual == null)
                {
                    ViewBag.Error = "Médico no encontrado.";
                    return View(m);
                }

                // Actualizar los campos editables
                medicoActual.Nombres = m.Nombres;
                medicoActual.Apellidos = m.Apellidos;
                medicoActual.Telefono = m.Telefono;
                medicoActual.EspecialidadID = m.EspecialidadID;

                bool edita = logMedico.Instancia.EditarMedico(medicoActual);
                if (edita)
                {
                    ViewBag.Mensaje = "Datos actualizados correctamente.";
                }
                else
                {
                    ViewBag.Error = "No se pudo actualizar los datos.";
                }

                return View(medicoActual);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View(m);
            }
        }
    }
}
