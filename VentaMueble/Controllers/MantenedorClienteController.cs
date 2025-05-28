using System.Diagnostics;
using CapaEntidad;
using CapaLogica;
using Microsoft.AspNetCore.Mvc;

namespace VentaMueble.Controllers
{
    public class MantenedorClienteController : Controller
    {
        private readonly ILogger<MantenedorClienteController> _logger;

        // Inyección de dependencias para ILogger
        public MantenedorClienteController(ILogger<MantenedorClienteController> logger)
        {
            _logger = logger;
        }

        public IActionResult ListarCliente()
        {
            List<entCliente> list = new List<entCliente>();
           
            list = logCliente.Instancia.ListarCliente();
            if (list.Count == 0)
            {
                _logger.LogWarning("No se encontraron clientes en la base de datos.");
            }
            else
            {
                _logger.LogInformation($"Se encontraron {list.Count} clientes.");
            }
            ViewBag.Lista = list;
            return View(list);
        }

        [HttpGet]
        public IActionResult InsertarCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertarCliente(entCliente c)
        {
            try
            {
                Boolean inserta = logCliente.Instancia.InsertarCliente(c);

                if (inserta)
                {
                    return RedirectToAction("ListarCliente");
                }

                else
                {
                    return View(c);
                }
            }


            catch (ApplicationException ex)
            {
                return RedirectToAction("InsertarCliente", new { mesjExceptio = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult EditarCliente(int id)
        {
            // Aquí llamas a tu lógica de negocio o acceso a datos
            var cliente = logCliente.Instancia.BuscarCliente(id); // Método de ejemplo
            return View(cliente);
        }

        [HttpPost]
        public ActionResult EditarCliente(entCliente c)
        {
            try
            {
                Boolean edita = logCliente.Instancia.EditarCliente(c);
                if (edita)
                {
                    return RedirectToAction("ListarCliente");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("EditarCliente", new { mesjExceptio = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult EliminarCliente(int id)
        {
            // Aquí llamas a tu lógica de negocio o acceso a datos
            var cliente = logCliente.Instancia.BuscarCliente(id); // Método de ejemplo
            return View(cliente);
        }

        [HttpPost]
        public IActionResult ConfirmarEliminarCliente(int id)
        {
            try
            {
                Boolean elimina = logCliente.Instancia.EliminarCliente(id);
                if (elimina)
                {
                    return RedirectToAction("ListarCliente");
                }
                else
                {
                    return RedirectToAction("ListarCliente");
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("ListarCliente", new { mesjExceptio = ex.Message });
            }
        }
    }
}
