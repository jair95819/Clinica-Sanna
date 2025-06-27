using Microsoft.AspNetCore.Mvc;

namespace VentaMueble.Controllers
{
    public class HomeController : Controller
    {
        // Acci�n para la vista de Login
        public IActionResult Login()
        {
            return View();
        }

        // Acci�n para procesar el login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Simulaci�n de validaci�n (esto debe ser reemplazado con autenticaci�n real)
            if (username == "Julio" && password == "1234")
            {
                return RedirectToAction("Index", "Home"); // Redirigir al Home si el login es correcto
            }

            // Si la validaci�n falla, mostrar un mensaje de error
            ViewBag.ErrorMessage = "Usuario o contrase�a incorrectos";
            return View();
        }

        // Acci�n para la vista de registro
        public IActionResult Register()
        {
            return View();
        }

        // Acci�n para procesar el registro
        [HttpPost]
        public IActionResult Register(string username, string password)
        {
            // Aqu� va la l�gica de registro (guardar el usuario en la base de datos)
            return RedirectToAction("Login"); // Redirigir a Login despu�s del registro
        }

        // Acci�n para el Home
        public IActionResult Index()
        {
            return View(); // P�gina principal de la Cl�nica SANNA
        }
    }
}
