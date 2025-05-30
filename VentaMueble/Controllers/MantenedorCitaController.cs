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
            // Simulación de datos para los combos
            ViewBag.Fechas = new SelectList(new List<string>
            {
                "2024-09-10", "2024-09-11", "2024-09-12"
            });

            ViewBag.Horas = new SelectList(new List<string>
            {
                "08:00 AM", "09:00 AM", "10:00 AM", "11:00 AM"
            });

            ViewBag.Sedes = new SelectList(new List<string>
            {
                "Sede Central", "Sede Norte", "Sede Sur"
            });

            ViewBag.Medicos = new SelectList(new List<string>
            {
                "Dr. Juan Pérez", "Dra. Ana Torres", "Dr. Luis Gómez"
            });

            ViewBag.Especialidades = new SelectList(new List<string>
            {
                "Cardiología", "Pediatría", "Neurología"
            });

            return View();
        }

        // POST: MantenedorCita/InsertarCita
        [HttpPost]
        public IActionResult InsertarCita(entCita cita)
        {
            if (ModelState.IsValid)
            {
                // Aquí iría la lógica para guardar la cita usando la CapaLogica
                // logCita.Instancia.InsertarCita(cita);

                TempData["mensaje"] = "Cita reservada correctamente.";
                return RedirectToAction("ListarCita");
            }

            // Si hay error de validación, volver a cargar combos y devolver la vista
            InsertarCita(); // para cargar los combos
            return View(cita);
        }

        public IActionResult Index()
        {
            return View(); // Vista de inicio o menú
        }

        [HttpGet]
        public IActionResult ListarCita()
        {
            // Simulación de datos (normalmente esto vendría desde la Capa Lógica)
            var listaCitas = new List<entCita>
            {
                new entCita
                {
                    idCita = 1,
                    fCita = "2024-09-10",
                    hCita = "08:00 AM",
                    sedeCita = "Sede Central",
                    consCita = "Consultorio 1",
                    nombrePaciente = "María López",
                    nombreMedico = "Dr. Juan Pérez",
                    especialidadMedico = "Cardiología"
                },
                new entCita
                {
                    idCita = 2,
                    fCita = "2024-09-11",
                    hCita = "10:00 AM",
                    sedeCita = "Sede Norte",
                    consCita = "Consultorio 2",
                    nombrePaciente = "Carlos Ramírez",
                    nombreMedico = "Dra. Ana Torres",
                    especialidadMedico = "Pediatría"
                }
            };

            return View(listaCitas);
        }
    }
}
