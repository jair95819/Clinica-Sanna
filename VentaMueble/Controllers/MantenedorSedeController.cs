using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapaEntidad;
using CapaLogica;
using Microsoft.AspNetCore.Mvc;

namespace VentaMueble.Controllers
{
    public class MantenedorSedeController : Controller
    {
        private readonly ILogger<MantenedorSedeController> _logger;

        public MantenedorSedeController(ILogger<MantenedorSedeController> logger)
        {
            _logger = logger;
        }

        // Acción para listar todas las sedes
        public IActionResult ListarSede()
        {
            List<entSede> lista = logSede.Instancia.ListarSede();
            return View(lista);
        }

        // Acción para mostrar la vista de inserción de sede
        [HttpGet]
        public IActionResult InsertarSede()
        {
            return View();
        }

        // Acción para procesar la inserción de una nueva sede
        [HttpPost]
        public IActionResult InsertarSede(entSede sede)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool inserta = logSede.Instancia.InsertarSede(sede);
                    if (inserta)
                    {
                        TempData["Mensaje"] = "Sede insertada correctamente.";
                        return RedirectToAction("ListarSede");
                    }
                    else
                    {
                        ViewBag.Error = "No se pudo insertar la sede.";
                    }
                }
                else
                {
                    ViewBag.Error = "Datos inválidos.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
            }
            return View(sede);
        }

        // Acción para mostrar la vista de edición de una sede
        [HttpGet]
        public IActionResult EditarSede(int id)
        {
            var sede = logSede.Instancia.BuscarSede(id);
            if (sede == null)
            {
                return RedirectToAction("ListarSede");
            }
            return View(sede);
        }

        // Acción para procesar la edición de una sede
        [HttpPost]
        public IActionResult EditarSede(entSede sede)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool edita = logSede.Instancia.EditarSede(sede);
                    if (edita)
                    {
                        TempData["Mensaje"] = "Sede editada correctamente.";
                        return RedirectToAction("ListarSede");
                    }
                    else
                    {
                        ViewBag.Error = "No se pudo editar la sede.";
                    }
                }
                else
                {
                    ViewBag.Error = "Datos inválidos.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
            }
            return View(sede);
        }

        // Acción para mostrar la vista de deshabilitar sede
        [HttpGet]
        public IActionResult DeshabilitarSede(int id)
        {
            var sede = logSede.Instancia.BuscarSede(id);
            if (sede == null)
            {
                return RedirectToAction("ListarSede");
            }
            return View(sede);
        }

        // Acción para confirmar y procesar la deshabilitación de la sede
        [HttpPost]
        public IActionResult ConfirmarDeshabilitarSede(int id)
        {
            try
            {
                bool deshabilita = logSede.Instancia.DeshabilitarSede(id);
                if (deshabilita)
                {
                    TempData["Mensaje"] = "Sede deshabilitada correctamente.";
                }
                else
                {
                    TempData["Error"] = "No se pudo deshabilitar la sede.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al deshabilitar: " + ex.Message;
            }
            return RedirectToAction("ListarSede");
        }
    }
}
