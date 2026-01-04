using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class CitasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CitasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
            var citas = await _context.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Empleado)
                .Include(c => c.Sala)
                .Include(c => c.DetalleCitas)
                    .ThenInclude(dc => dc.Servicio)
                .Include(c => c.PagosCitas)
                .OrderByDescending(c => c.FechaCita)
                .ToListAsync();
            return View(citas);
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Empleado)
                .Include(c => c.Sala)
                .Include(c => c.DetalleCitas)
                    .ThenInclude(dc => dc.Servicio)
                .Include(c => c.PagosCitas)
                .FirstOrDefaultAsync(m => m.CitaID == id);

            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "Nombre");
            ViewData["EmpleadoID"] = new SelectList(_context.Empleados, "EmpleadoID", "Nombre");
            ViewData["SalaID"] = new SelectList(_context.Salas, "SalaID", "NombreSala");
            ViewData["Servicios"] = new SelectList(_context.Servicios.Where(s => s.Estado == "Activo"), "ServicioID", "NombreServicio");
            return View();
        }

        // POST: Citas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitaID,ClienteID,EmpleadoID,SalaID,FechaCita,HoraInicio,HoraFin,EstadoCita,Observaciones")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                if (cita.FechaCreacion == null)
                {
                    cita.FechaCreacion = DateTime.Now;
                }

                // Validar que la hora de fin sea mayor a la hora de inicio
                if (cita.HoraFin <= cita.HoraInicio)
                {
                    ModelState.AddModelError("HoraFin", "La hora de fin debe ser mayor a la hora de inicio");
                    ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "Nombre", cita.ClienteID);
                    ViewData["EmpleadoID"] = new SelectList(_context.Empleados, "EmpleadoID", "Nombre", cita.EmpleadoID);
                    ViewData["SalaID"] = new SelectList(_context.Salas, "SalaID", "NombreSala", cita.SalaID);
                    ViewData["Servicios"] = new SelectList(_context.Servicios.Where(s => s.Estado == "Activo"), "ServicioID", "NombreServicio");
                    return View(cita);
                }

                _context.Add(cita);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cita creada exitosamente";
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "Nombre", cita.ClienteID);
            ViewData["EmpleadoID"] = new SelectList(_context.Empleados, "EmpleadoID", "Nombre", cita.EmpleadoID);
            ViewData["SalaID"] = new SelectList(_context.Salas, "SalaID", "NombreSala", cita.SalaID);
            ViewData["Servicios"] = new SelectList(_context.Servicios.Where(s => s.Estado == "Activo"), "ServicioID", "NombreServicio");
            return View(cita);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }

            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "Nombre", cita.ClienteID);
            ViewData["EmpleadoID"] = new SelectList(_context.Empleados, "EmpleadoID", "Nombre", cita.EmpleadoID);
            ViewData["SalaID"] = new SelectList(_context.Salas, "SalaID", "NombreSala", cita.SalaID);
            return View(cita);
        }

        // POST: Citas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CitaID,ClienteID,EmpleadoID,SalaID,FechaCita,HoraInicio,HoraFin,EstadoCita,Observaciones,FechaCreacion")] Cita cita)
        {
            if (id != cita.CitaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Validar que la hora de fin sea mayor a la hora de inicio
                if (cita.HoraFin <= cita.HoraInicio)
                {
                    ModelState.AddModelError("HoraFin", "La hora de fin debe ser mayor a la hora de inicio");
                    ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "Nombre", cita.ClienteID);
                    ViewData["EmpleadoID"] = new SelectList(_context.Empleados, "EmpleadoID", "Nombre", cita.EmpleadoID);
                    ViewData["SalaID"] = new SelectList(_context.Salas, "SalaID", "NombreSala", cita.SalaID);
                    return View(cita);
                }

                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cita actualizada exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.CitaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "Nombre", cita.ClienteID);
            ViewData["EmpleadoID"] = new SelectList(_context.Empleados, "EmpleadoID", "Nombre", cita.EmpleadoID);
            ViewData["SalaID"] = new SelectList(_context.Salas, "SalaID", "NombreSala", cita.SalaID);
            return View(cita);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Empleado)
                .Include(c => c.Sala)
                .Include(c => c.DetalleCitas)
                .Include(c => c.PagosCitas)
                .FirstOrDefaultAsync(m => m.CitaID == id);

            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null)
            {
                var tieneDetalles = await _context.DetalleCitas.AnyAsync(dc => dc.CitaID == id);
                var tienePagos = await _context.PagosCitas.AnyAsync(pc => pc.CitaID == id);

                if (tieneDetalles || tienePagos)
                {
                    TempData["Error"] = "No se puede eliminar la cita porque tiene servicios o pagos asociados";
                    return RedirectToAction(nameof(Index));
                }

                _context.Citas.Remove(cita);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cita eliminada exitosamente";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.CitaID == id);
        }
    }
}
