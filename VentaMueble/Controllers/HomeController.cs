using Microsoft.AspNetCore.Mvc;

namespace VentaMueble.Controllers
{
    public class HomeController : Controller
    {
        // Acci�n para la vista de Login (se configura como la p�gina principal)
        public IActionResult Index()
        {
            return RedirectToAction("Login"); // Redirige autom�ticamente a la vista de Login
        }

        // Acci�n para mostrar la vista de Login
        public IActionResult Login()
        {
            return View(); // Muestra la vista Login.cshtml
        }

        // Acci�n para procesar el login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "Julio" && password == "1234")
            {
                return RedirectToAction("Index", "Home");
            }

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
            return RedirectToAction("Login");
        }
    }
}
