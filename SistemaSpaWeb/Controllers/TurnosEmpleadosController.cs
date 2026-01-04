using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class TurnosEmpleadosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TurnosEmpleadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var turnos = await _context.TurnosEmpleados
                .Include(t => t.Empleado)
                .OrderBy(t => t.DiaSemana)
                .ThenBy(t => t.HoraInicio)
                .ToListAsync();
            return View(turnos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var turno = await _context.TurnosEmpleados
                .Include(t => t.Empleado)
                .FirstOrDefaultAsync(m => m.TurnoID == id);
            if (turno == null) return NotFound();
            return View(turno);
        }

        public IActionResult Create()
        {
            ViewData["EmpleadoID"] = new SelectList(_context.Empleados.Where(e => e.Estado == "Activo"), "EmpleadoID", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TurnoID,EmpleadoID,DiaSemana,HoraInicio,HoraFin,TipoTurno,Estado")] TurnoEmpleado turno)
        {
            if (ModelState.IsValid)
            {
                turno.FechaRegistro = DateTime.Now;
                _context.Add(turno);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Turno asignado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoID"] = new SelectList(_context.Empleados, "EmpleadoID", "Nombre", turno.EmpleadoID);
            return View(turno);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var turno = await _context.TurnosEmpleados.FindAsync(id);
            if (turno == null) return NotFound();
            ViewData["EmpleadoID"] = new SelectList(_context.Empleados, "EmpleadoID", "Nombre", turno.EmpleadoID);
            return View(turno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TurnoID,EmpleadoID,DiaSemana,HoraInicio,HoraFin,TipoTurno,Estado,FechaRegistro")] TurnoEmpleado turno)
        {
            if (id != turno.TurnoID) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turno);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Turno actualizado exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.TurnosEmpleados.Any(e => e.TurnoID == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoID"] = new SelectList(_context.Empleados, "EmpleadoID", "Nombre", turno.EmpleadoID);
            return View(turno);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var turno = await _context.TurnosEmpleados
                .Include(t => t.Empleado)
                .FirstOrDefaultAsync(m => m.TurnoID == id);
            if (turno == null) return NotFound();
            return View(turno);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turno = await _context.TurnosEmpleados.FindAsync(id);
            if (turno != null)
            {
                _context.TurnosEmpleados.Remove(turno);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Turno eliminado exitosamente";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
