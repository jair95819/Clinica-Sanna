using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using System.Collections.Generic;
using System.Linq;

namespace VentaMueble.Controllers
{
    public class MantenedorMetodoPagoController : Controller
    {
        private static List<entMetodoPago> lista = new List<entMetodoPago>
        {
            new entMetodoPago { idMetodoPago = 1, descripcion = "Tarjeta de crédito" },
            new entMetodoPago { idMetodoPago = 2, descripcion = "Efectivo" }
        };
        private static int ultimoId = 2;

        [HttpGet]
        public IActionResult Index()
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(lista);
            return View(lista);
        }

        [HttpPost]
        public IActionResult Insertar(entMetodoPago metodo)
        {
            if (ModelState.IsValid)
            {
                metodo.idMetodoPago = ++ultimoId;
                lista.Add(metodo);
                return RedirectToAction("Index");
            }
            return View("Index", lista);
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var item = lista.FirstOrDefault(m => m.idMetodoPago == id);
            if (item != null)
                lista.Remove(item);
            return RedirectToAction("Index");
        }
    }
}