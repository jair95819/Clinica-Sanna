using CapaEntidad;
using CapaLogica;
using Microsoft.AspNetCore.Mvc;
namespace VentaMueble.Controllers
{
    public class RecetasMedicasController : Controller
    {
        public IActionResult MisRecetas()
        {
            return View();
        }
    }
}
