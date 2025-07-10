using Microsoft.AspNetCore.Mvc;

namespace VentaMueble.Controllers
{
    public class DashboardMedicoController : Controller
    {
        public IActionResult Index()
        {
            // Puedes enviar TempData para mostrar el mensaje si quieres
            if (TempData["InicioExitoso"] != null)
                ViewBag.Mensaje = TempData["InicioExitoso"].ToString();

            return View();
        }
    }
}
