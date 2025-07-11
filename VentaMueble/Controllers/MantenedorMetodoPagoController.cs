using Microsoft.AspNetCore.Mvc;

namespace CapaWeb.Controllers
{
    public class MantenedorMetodoPagoController : Controller
    {
        public IActionResult ListarMetodoPago()
        {
            return View();
        }
    }
}
