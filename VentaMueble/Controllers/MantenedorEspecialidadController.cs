using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CapaEntidad;
using CapaLogica;
using Microsoft.AspNetCore.Mvc;

namespace VentaMueble.Controllers
{
    public class MantenedorEspecialidadController : Controller
    {
        private readonly ILogger<MantenedorEspecialidadController> _logger;

        public MantenedorEspecialidadController(ILogger<MantenedorEspecialidadController> logger)
        {
            _logger = logger;
        }

        public IActionResult ListarEspecialidad()
        {
            List<entEspecialidad> lista = logEspecialidad.Instancia.ListarEspecialidad();
            ViewBag.Lista = lista;
            return View(lista);
        }
        [HttpGet]
        public IActionResult InsertarEspecialidad()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InsertarEspecialidad(entEspecialidad e)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool inserta = logEspecialidad.Instancia.InsertarEspecialidad(e);
                    if (inserta)
                    {
                        TempData["RegistroExitoso"] = "¡Especialidad registrada exitosamente!";
                        return RedirectToAction("ListarEspecialidad");
                    }
                    else
                    {
                        ViewBag.Error = "No se pudo registrar la especialidad.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al registrar la especialidad: " + ex.Message;
            }
            return View(e);
        }
        [HttpGet]
        public IActionResult DeshabilitarEspecialidad(int id)
        {
            var especialidad = logEspecialidad.Instancia.BuscarEspecialidad(id);
            if (especialidad == null)
            {
                ViewBag.Error = "Especialidad no encontrada.";
                return RedirectToAction("ListarEspecialidad");
            }

            return View(especialidad);
        }

        [HttpPost]
        public IActionResult ConfirmarDeshabilitarEspecialidad(int id)
        {
            try
            {
                bool deshabilita = logEspecialidad.Instancia.DeshabilitarEspecialidad(id);
                if (deshabilita)
                {
                    TempData["RegistroExitoso"] = "¡Especialidad deshabilitada correctamente!";
                    return RedirectToAction("ListarEspecialidad");
                }
                else
                {
                    ViewBag.Error = "No se pudo deshabilitar la especialidad.";
                    return RedirectToAction("ListarEspecialidad");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al deshabilitar: " + ex.Message;
                return RedirectToAction("ListarEspecialidad");
            }
        }
        [HttpGet]
        public IActionResult EditarEspecialidad(int id)
        {
            var especialidad = logEspecialidad.Instancia.BuscarEspecialidad(id);
            if (especialidad == null)
            {
                ViewBag.Error = "Especialidad no encontrada.";
                return RedirectToAction("ListarEspecialidad");
            }

            return View(especialidad);
        }

        [HttpPost]
        public IActionResult EditarEspecialidad(entEspecialidad e)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool edita = logEspecialidad.Instancia.EditarEspecialidad(e);
                    if (edita)
                    {
                        TempData["RegistroExitoso"] = "¡Especialidad editada correctamente!";
                        return RedirectToAction("ListarEspecialidad");
                    }
                    else
                    {
                        ViewBag.Error = "No se pudo editar la especialidad.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al editar: " + ex.Message;
            }
            return View(e);
        }
    }
}
