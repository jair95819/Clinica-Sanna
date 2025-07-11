using CapaEntidad;
using CapaLogica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VentaMueble.Controllers
{
    public class MantenedorCitaController : Controller
    {
        [HttpGet]
        public IActionResult InsertarCita()
        {
            var tiposCita = logTipoCita.Instancia.ListarTipoCita();
            ViewBag.TipoCita = tiposCita?
                .Where(t => t != null && t.nombre != null)
                .Select(t => new SelectListItem
                {
                    Value = t.id.ToString(),
                    Text = t.nombre
                }).ToList();

            var medicos = logMedico.Instancia.ListarMedico();
            ViewBag.Medico = medicos?
                .Where(m => m != null && m.Nombres != null)
                .Select(m => new SelectListItem
                {
                    Value = m.MedicoID.ToString(),
                    Text = m.Nombres + " " + m.Apellidos
                }).ToList();

            var especialidades = logEspecialidad.Instancia.ListarEspecialidad();
            ViewBag.Especialidad = especialidades?
                .Where(e => e != null && e.Nombre != null)
                .Select(e => new SelectListItem
                {
                    Value = e.EspecialidadID.ToString(),
                    Text = e.Nombre
                }).ToList();

            /*var sedes = logSede.Instancia.ListarSede();
            ViewBag.Sedes = sedes?
                .Where(s => s != null && s.nombre != null)
                .Select(s => new SelectListItem
                {
                    Value = s.id.ToString(),
                    Text = s.nombre + " - " + s.direccion
                }).ToList();*/

            return View();
        }

        // POST: MantenedorCita/InsertarCita
        [HttpPost]
        public IActionResult InsertarCita(entCita cita)
        {
            if (ModelState.IsValid)
            {
                //logCita.Instancia.InsertarCita(cita);

                TempData["mensaje"] = "Cita reservada correctamente.";
                return RedirectToAction("ListarCita");
            }

            // Si hay error de validación, volver a cargar combos y devolver la vista
            var tiposCita = logTipoCita.Instancia.ListarTipoCita();
            ViewBag.TipoCita = tiposCita?
                .Where(t => t != null && t.nombre != null)
                .Select(t => new SelectListItem
                {
                    Value = t.id.ToString(),
                    Text = t.nombre
                }).ToList();

            var medicos = logMedico.Instancia.ListarMedico();
            ViewBag.Medicos = medicos?
                .Where(m => m != null && m.Nombres != null)
                .Select(m => new SelectListItem
                {
                    Value = m.MedicoID.ToString(),
                    Text = m.Nombres + " " + m.Apellidos
                }).ToList();

            var especialidades = logEspecialidad.Instancia.ListarEspecialidad();
            ViewBag.Especialidad = especialidades?
                .Where(e => e != null && e.Nombre != null)
                .Select(e => new SelectListItem
                {
                    Value = e.EspecialidadID.ToString(),
                    Text = e.Nombre
                }).ToList();

            InsertarCita(); // para cargar los combos
            return View(cita);
        }

        public IActionResult Index()
        {
            return View(); // Vista de inicio o menú
        }

        [HttpGet]
        public IActionResult ListarCita(string tipo)
        {
            if (tipo == "Medico")
            {
                ViewBag.Layout = "~/Views/Shared/_LayoutMedico.cshtml";
            } 

            if (tipo == "Paciente")
            {
                ViewBag.Layout = "~/Views/Shared/_LayoutPaciente.cshtml";
            }

            ViewBag.Tipo = tipo;
            var listaCitas = logCita.Instancia.ListarCita();
            return View(listaCitas);
        }
    }
}
