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
                var usuario = logUsuario.Instancia.ValidarUsuario(email, password, Tipousuario);
                if (usuario != null)
                {
                    if (Tipousuario == "Paciente")
                    {
                        // Obtengo el paciente desde el usuarioID
                        var paciente = logPaciente.Instancia.ObtenerPacientePorUsuarioID(usuario.UsuarioID);

                        if (paciente != null)
                        {
                            // Verificar si el paciente está habilitado
                            if (!paciente.Estado)
                            {
                                ViewBag.Error = "Tu cuenta está deshabilitada. Contacta al administrador.";
                                return View();
                            }

                            // Si está habilitado, guardar datos en sesión
                            HttpContext.Session.SetString("NombrePaciente", paciente.Nombres);
                            HttpContext.Session.SetInt32("PacienteID", paciente.PacienteID);
                        }

                        return RedirectToAction("Index", "DashboardPaciente");
                    }
                    else if (Tipousuario == "Medico")
                        return RedirectToAction("ListarMedico", "MantenedorMedico");
                    else if (Tipousuario == "Administrador")
                    {
                        HttpContext.Session.SetString("NombreAdministrador", "Administrador");
                        return RedirectToAction("Index", "DashboardAdministrador");
                    }
                    else
                    {
                        ViewBag.Error = "Tipo de usuario inválido.";
                        return View();
                    }
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
            var tipo = form["Tipousuario"];
            var email = form["Email"];
            var password = form["Password"];

            try
            {
                // 1. Insertar en Usuario
                var nuevoUsuario = new entUsuario
                {
                    Email = email,
                    Password = password,
                    TipousuarioID = tipo == "Medico" ? 2 : 1 // Suponiendo 2 = Medico, 1 = Paciente
                };

                int nuevoUsuarioID = logUsuario.Instancia.InsertarUsuarioYDevolverID(nuevoUsuario);
                if (nuevoUsuarioID <= 0)
                {
                    ViewBag.Error = "Error al crear el usuario. ID no válido.";
                    ViewBag.Tipousuario = tipo;
                    return View("MostrarRegistro");
                }

                if (tipo == "Paciente")
                {
                    // 2. Insertar en Paciente
                    var paciente = new entPaciente
                    {
                        UsuarioID = nuevoUsuarioID,
                        Nombres = form["Nombres"],
                        Apellidos = form["Apellidos"],
                        NumDoc = form["NumDoc"],
                        FechaNacimiento = DateTime.Parse(form["FechaNacimiento"]),
                        Telefono = form["Telefono"],
                        Sexo = form["Sexo"],
                        Estado = form["Estado"] == "on" // checkbox devuelve "on"
                    };

                    bool inserta = logPaciente.Instancia.InsertarPaciente(paciente);
                    if (inserta)
                        return RedirectToAction("ListarPaciente", "MantenedorPaciente");
                }
                /*else if (tipo == "Medico")
                {
                    var medico = new entMedico
                    {
                        UsuarioID = nuevoUsuarioID,
                        Nombres = form["Nombres"],
                        Apellidos = form["Apellidos"],
                        Especialidad = form["Especialidad"],
                        NumColegio = form["NumColegio"],
                        Estado = form["Estado"] == "on"
                    };

                    bool inserta = logMedico.Instancia.InsertarMedico(medico);
                    if (inserta)
                        return RedirectToAction("ListarMedico", "MantenedorMedico");
                }*/

                ViewBag.Error = "No se pudo registrar el usuario.";
                ViewBag.Tipousuario = tipo;
                return View("MostrarRegistro");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al registrar: " + ex.Message;
                ViewBag.Tipousuario = tipo;
                return View("MostrarRegistro");
            }
        }




    }
}
