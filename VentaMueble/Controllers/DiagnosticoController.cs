using CapaEntidad;
using CapaLogica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VentaMueble.Controllers
{
    public class DiagnosticoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListarDiagnostico()
        {
            var listarDiagnostico = logDiagnostico.Instancia.ListarDiagnostico();
            return View(listarDiagnostico);
        }

        [HttpGet]
        public IActionResult InsertarDiagnostico()
        {
            var medicos = logMedico.Instancia.ListarMedico();
            ViewBag.Medico = medicos?
                .Where(m => m != null && m.Nombres != null)
                .Select(m => new SelectListItem
                {
                    Value = m.MedicoID.ToString(),
                    Text = m.Nombres + " " + m.Apellidos
                }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult InsertarDiagnostico(entDiagnostico m)
        {
            bool inserta = logDiagnostico.Instancia.InsertarDiagnostico(m);
            if (inserta)
            {
                TempData["RegistroExitoso"] = "¡Registro Exitoso!";
                return RedirectToAction("ListarDiagnostico", "Diagnostico");
            }
            else
            {
                //ViewBag.MetodoPago = 
                return View(m);
            }
        }
    }
}
