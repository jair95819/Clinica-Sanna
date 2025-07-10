using System.Diagnostics;
using CapaEntidad;
using CapaLogica;
using Microsoft.AspNetCore.Mvc;
using VentaMueble.Models;

namespace VentaMueble.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public IActionResult VistaAmbulancia()
        {
            return View();
        }
        public IActionResult Login()// GET
        {
            return View();
        }

        // POST Login
        [HttpPost]
        public IActionResult Login(string email, string password, string Tipousuario)
        {
            try
            {
                // Validar las credenciales con el tipo de usuario (Paciente, Médico, Administrador)
                var usuario = logUsuario.Instancia.ValidarUsuario(email, password, Tipousuario);

                if (usuario != null)
                {
                    if (Tipousuario == "Paciente")
                    {
                        // Lógica para paciente
                        var paciente = logPaciente.Instancia.ObtenerPacientePorUsuarioID(usuario.UsuarioID);
                        if (paciente != null)
                        {
                            if (!paciente.Estado)
                            {
                                ViewBag.Error = "Tu cuenta está deshabilitada. Contacta al administrador.";
                                return View();
                            }

                            HttpContext.Session.SetString("NombrePaciente", paciente.Nombres);
                            HttpContext.Session.SetInt32("PacienteID", paciente.PacienteID);
                            return RedirectToAction("Index", "DashboardPaciente");
                        }
                    }
                    else if (Tipousuario == "Medico")
                    {
                        // Buscar al médico usando su usuarioID
                        var medico = logMedico.Instancia.ObtenerMedicoPorUsuarioID(usuario.UsuarioID);
                        if (medico != null)
                        {
                            if (!medico.Estado)
                            {
                                ViewBag.Error = "Tu cuenta está deshabilitada. Contacta al administrador.";
                                return View();
                            }
                            // Guardar el nombre y el ID del médico en la sesión
                            HttpContext.Session.SetString("NombreMedico", medico.Nombres);
                            HttpContext.Session.SetInt32("MedicoID", medico.MedicoID);

                            // Enviar el mensaje de éxito al dashboard
                            TempData["InicioExitoso"] = "¡Bienvenido, Dr(a). " + medico.Nombres + "!";

                            // Redirigir al Dashboard del Médico
                            return RedirectToAction("Index", "DashboardMedico");  // Asegúrate de que esta redirección sea al controlador correcto
                        }
                    }
                    else if (Tipousuario == "Administrador")
                    {
                        // Lógica para administrador
                        HttpContext.Session.SetString("NombreAdministrador", "Administrador");
                        return RedirectToAction("Index", "DashboardAdministrador");
                    }

                    ViewBag.Error = "Tipo de usuario inválido.";
                    return View();
                }
                else
                {
                    ViewBag.Error = "Credenciales incorrectas.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al iniciar sesión: " + ex.Message;
                return View();
            }
        }

        // GET MostrarRegistro -> muestra vista con tipo de usuario
        [HttpGet]
        public IActionResult MostrarRegistro(string tipo)
        {
            ViewBag.Tipousuario = tipo;
            return View();
        }

        // POST Registrar
        [HttpPost]
        public IActionResult Registrar(IFormCollection form)
        {
            var tipo = form["Tipousuario"]; // Tomamos el tipo de usuario del formulario
            var email = form["Email"];
            var password = form["Password"];

            try
            {
                var nuevoUsuario = new entUsuario
                {
                    Email = email,
                    Password = password,
                    TipousuarioID = tipo == "Medico" ? 2 : 1 // Si es Medico, el TipousuarioID es 2
                };

                // Insertar el usuario y obtener su ID
                int nuevoUsuarioID = logUsuario.Instancia.InsertarUsuarioYDevolverID(nuevoUsuario);
                if (nuevoUsuarioID <= 0)
                {
                    ViewBag.Error = "Error al crear el usuario.";
                    return View("MostrarRegistro");
                }

                if (tipo == "Medico")
                {
                    // Registrar médico
                    var medico = new entMedico
                    {
                        UsuarioID = nuevoUsuarioID,
                        Nombres = form["Nombres"],
                        Apellidos = form["Apellidos"],
                        Telefono = form["Telefono"],
                        EspecialidadID = int.Parse(form["EspecialidadID"]),  // Especialidad
                        Estado = form["Estado"] == "on" // Si está activado el checkbox
                    };

                    bool inserta = logMedico.Instancia.InsertarMedico(medico);
                    if (inserta)
                    {
                        // Recuperar médico para obtener ID
                        var medicoCreado = logMedico.Instancia.BuscarMedico(nuevoUsuarioID);
                        if (medicoCreado != null)
                        {
                            // Guardar en sesión
                            HttpContext.Session.SetString("NombreMedico", medicoCreado.Nombres);
                            HttpContext.Session.SetInt32("MedicoID", medicoCreado.MedicoID);
                        }

                        // Redirigir a la vista del médico
                        return RedirectToAction("ListarMedico", "MantenedorMedico");
                    }
                    else
                    {
                        ViewBag.Error = "No se pudo registrar el médico.";
                        return View("MostrarRegistro");
                    }
                }
                else if (tipo == "Paciente")
                {
                    var paciente = new entPaciente
                    {
                        UsuarioID = nuevoUsuarioID,
                        Nombres = form["Nombres"],
                        Apellidos = form["Apellidos"],
                        NumDoc = form["NumDoc"],
                        FechaNacimiento = DateTime.Parse(form["FechaNacimiento"]),
                        Telefono = form["Telefono"],
                        Sexo = form["Sexo"],
                        Estado = true // Si está activado el checkbox
                    };

                    bool inserta = logPaciente.Instancia.InsertarPaciente(paciente);
                    if (inserta)
                    {
                        // Recuperar paciente para obtener ID
                        var pacienteCreado = logPaciente.Instancia.ObtenerPacientePorUsuarioID(nuevoUsuarioID);
                        if (pacienteCreado != null)
                        {
                            HttpContext.Session.SetString("NombrePaciente", pacienteCreado.Nombres);
                            HttpContext.Session.SetInt32("PacienteID", pacienteCreado.PacienteID);
                        }

                        return RedirectToAction("Index", "DashboardPaciente");
                    }
                    else
                    {
                        ViewBag.Error = "No se pudo registrar el paciente.";
                        return View("MostrarRegistro");
                    }
                }

                ViewBag.Error = "Tipo de usuario no soportado.";
                return View("MostrarRegistro");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al registrar: " + ex.Message;
                return View("MostrarRegistro");
            }
        }
    }
}
