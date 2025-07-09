using Microsoft.AspNetCore.Mvc;
using CapaLogica;
using CapaEntidad;

namespace VentaMueble.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Verificamos el tipo de usuario (Paciente, Médico, Administrador)
            entUsuario usuario = logUsuario.Instancia.ValidarLogin(email, password);

            if (usuario != null)
            {
                // Si el login es correcto, redirigimos a su área correspondiente
                if (usuario.TipoUsuario == "Paciente")
                {
                    return RedirectToAction("VistaPaciente", "Home");  // Redirigir al paciente
                }
                else if (usuario.TipoUsuario == "Médico")
                {
                    return RedirectToAction("VistaMedico", "Home");  // Redirigir al médico
                }
                else if (usuario.TipoUsuario == "Administrador")
                {
                    return RedirectToAction("VistaAdmin", "Home");  // Redirigir al administrador
                }
            }

            // Si el login falla, mostramos un mensaje de error
            ViewBag.ErrorMessage = "Email o contraseña incorrectos.";
            return View();
        }
    }
}
