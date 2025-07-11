using CapaEntidad;
using CapaLogica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace VentaMueble.Controllers
{
    public class PagoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult InsertarPago(int idCita)
        {
            return View();
        }

        [HttpPost]
        public IActionResult InsertarPago(entPago p)
        {
            bool inserta = logPago.Instancia.InsertarPago(p);
            if (inserta)
            {
                TempData["RegistroExitoso"] = "¡Pago Exitoso!";
                return RedirectToAction("ListarCita", "MantenedorCita");
            }
            else
            {
                //ViewBag.MetodoPago = 
                return View(p);
            }
        }
    }
}
