using Microsoft.AspNetCore.Mvc;

namespace VentaMueble.Controllers
{
    public class DashboardPacienteController : Controller
    {
        public IActionResult DashboardPaciente()
        {
            // Puedes enviar TempData para mostrar el mensaje si quieres
            if (TempData["InicioExitoso"] != null)
                ViewBag.Mensaje = TempData["InicioExitoso"].ToString();

            return View();
        }
    }
}
