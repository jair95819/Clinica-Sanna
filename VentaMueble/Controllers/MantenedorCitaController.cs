using CapaEntidad;
using CapaLogica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VentaMueble.Controllers
{
    public class MantenedorCitaController : Controller
    {
        // Acción para mostrar la vista de inserción de cita
        [HttpGet]
        public IActionResult InsertarCita()
        {
            // Cargar tipos de cita desde la capa lógica
            var tiposCita = logTipoCita.Instancia.ListarTipoCita();
            ViewBag.TipoCita = tiposCita?
                .Where(t => t != null && t.nombre != null)
                .Select(t => new SelectListItem
                {
                    Value = t.id.ToString(),
                    Text = t.nombre
                }).ToList();

            // Cargar médicos desde la capa lógica
            var medicos = logMedico.Instancia.ListarMedico();
            ViewBag.Medico = medicos?
                .Where(m => m != null && m.Nombres != null)
                .Select(m => new SelectListItem
                {
                    Value = m.MedicoID.ToString(),
                    Text = m.Nombres + " " + m.Apellidos
                }).ToList();

            // Cargar especialidades desde la capa lógica
            var especialidades = logEspecialidad.Instancia.ListarEspecialidad();
            ViewBag.Especialidad = especialidades?
                .Where(e => e != null && e.Nombre != null)
                .Select(e => new SelectListItem
                {
                    Value = e.EspecialidadID.ToString(),
                    Text = e.Nombre
                }).ToList();

            // Cargar sedes desde la capa lógica
            var sedes = logSede.Instancia.ListarSede(); // Método para listar sedes
            ViewBag.Sedes = sedes?
                .Where(s => s != null && s.nombre != null)
                .Select(s => new SelectListItem
                {
                    Value = s.idSede.ToString(),
                    Text = s.nombre + " - " + s.direccion
                }).ToList();

            return View();
        }

        // Acción para procesar la inserción de una nueva cita
        [HttpPost]
        public IActionResult InsertarCita(entCita cita)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insertar la cita en la base de datos
                    logCita.Instancia.InsertarCita(cita);  // Asegúrate de tener este método en la capa de lógica

                    TempData["mensaje"] = "Cita reservada correctamente.";
                    return RedirectToAction("ListarCita");
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Error al reservar la cita: " + ex.Message;
                }
            }

            // Si hay error de validación, volver a cargar los combos y devolver la vista
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

            // Cargar las sedes
            var sedes = logSede.Instancia.ListarSede();
            ViewBag.Sedes = sedes?
                .Where(s => s != null && s.nombre != null)
                .Select(s => new SelectListItem
                {
                    Value = s.idSede.ToString(),
                    Text = s.nombre + " - " + s.direccion
                }).ToList();

            return View(cita);
        }

        // Acción para mostrar la vista de inicio o menú
        public IActionResult Index()
        {
            return View(); // Vista de inicio o menú
        }

        // Acción para listar todas las citas
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
