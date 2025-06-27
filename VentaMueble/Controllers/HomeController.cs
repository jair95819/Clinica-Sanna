using Microsoft.AspNetCore.Mvc;

namespace VentaMueble.Controllers
{
    public class HomeController : Controller
    {
        // Acción para la vista de Login (se configura como la página principal)
        public IActionResult Index()
        {
            return RedirectToAction("Login"); // Redirige automáticamente a la vista de Login
        }

        // Acción para mostrar la vista de Login
        public IActionResult Login()
        {
            return View(); // Muestra la vista Login.cshtml
        }

        // Acción para procesar el login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "Julio" && password == "1234")
            {
                return RedirectToAction("Index", "Home");
            }

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
            return RedirectToAction("Login");
        }
    }
}
