using Microsoft.AspNetCore.Mvc;

namespace VentaMueble.Controllers
{
    public class HomeController : Controller
    {
        // Acción para la vista de Login
        public IActionResult Login()
        {
            return View();
        }

        // Acción para procesar el login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Simulación de validación (esto debe ser reemplazado con autenticación real)
            if (username == "Julio" && password == "1234")
            {
                return RedirectToAction("Index", "Home"); // Redirigir al Home si el login es correcto
            }

            // Si la validación falla, mostrar un mensaje de error
            ViewBag.ErrorMessage = "Usuario o contraseña incorrectos";
            return View();
        }

        // Acción para la vista de registro
        public IActionResult Register()
        {
            return View();
        }

        // Acción para procesar el registro
        [HttpPost]
        public IActionResult Register(string username, string password)
        {
            // Aquí va la lógica de registro (guardar el usuario en la base de datos)
            return RedirectToAction("Login"); // Redirigir a Login después del registro
        }

        // Acción para el Home
        public IActionResult Index()
        {
            return View(); // Página principal de la Clínica SANNA
        }
    }
}
