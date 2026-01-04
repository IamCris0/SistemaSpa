using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class HistorialClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistorialClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var historial = await _context.HistorialClientes
                .Include(h => h.Cliente)
                .Include(h => h.Cita)
                .OrderByDescending(h => h.FechaVisita)
                .ToListAsync();
            return View(historial);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var historial = await _context.HistorialClientes
                .Include(h => h.Cliente)
                .Include(h => h.Cita)
                .FirstOrDefaultAsync(m => m.HistorialID == id);
            if (historial == null) return NotFound();
            return View(historial);
        }

        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.Clientes.Where(c => c.Estado == "Activo"), "ClienteID", "Nombre");
            ViewData["CitaID"] = new SelectList(_context.Citas, "CitaID", "CitaID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistorialID,ClienteID,CitaID,FechaVisita,Observaciones,Calificacion,AlergiasProcedimiento,ResultadosTratamiento")] HistorialCliente historial)
        {
            if (ModelState.IsValid)
            {
                historial.FechaRegistro = DateTime.Now;
                _context.Add(historial);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Historial registrado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "Nombre", historial.ClienteID);
            ViewData["CitaID"] = new SelectList(_context.Citas, "CitaID", "CitaID", historial.CitaID);
            return View(historial);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var historial = await _context.HistorialClientes.FindAsync(id);
            if (historial == null) return NotFound();
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "Nombre", historial.ClienteID);
            ViewData["CitaID"] = new SelectList(_context.Citas, "CitaID", "CitaID", historial.CitaID);
            return View(historial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistorialID,ClienteID,CitaID,FechaVisita,Observaciones,Calificacion,AlergiasProcedimiento,ResultadosTratamiento,FechaRegistro")] HistorialCliente historial)
        {
            if (id != historial.HistorialID) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historial);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Historial actualizado exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.HistorialClientes.Any(e => e.HistorialID == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "Nombre", historial.ClienteID);
            ViewData["CitaID"] = new SelectList(_context.Citas, "CitaID", "CitaID", historial.CitaID);
            return View(historial);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var historial = await _context.HistorialClientes
                .Include(h => h.Cliente)
                .Include(h => h.Cita)
                .FirstOrDefaultAsync(m => m.HistorialID == id);
            if (historial == null) return NotFound();
            return View(historial);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historial = await _context.HistorialClientes.FindAsync(id);
            if (historial != null)
            {
                _context.HistorialClientes.Remove(historial);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Historial eliminado exitosamente";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}