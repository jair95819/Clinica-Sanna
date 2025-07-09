using Microsoft.AspNetCore.Mvc;

namespace VentaMueble.Controllers
{
    public class DashboardAdministradorController : Controller
    {
        public IActionResult Index()
        {
            var nombreAdmin = HttpContext.Session.GetString("NombreAdministrador") ?? "Administrador";
            ViewBag.NombreAdmin = nombreAdmin;
            return View(); 
        }

    }
}
