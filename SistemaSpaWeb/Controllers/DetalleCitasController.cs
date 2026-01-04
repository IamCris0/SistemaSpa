using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class DetalleCitasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetalleCitasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var detalles = await _context.DetalleCitas
                .Include(d => d.Cita).ThenInclude(c => c.Cliente)
                .Include(d => d.Servicio)
                .ToListAsync();
            return View(detalles);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var detalle = await _context.DetalleCitas
                .Include(d => d.Cita).ThenInclude(c => c.Cliente)
                .Include(d => d.Servicio)
                .FirstOrDefaultAsync(m => m.DetalleCitaID == id);
            if (detalle == null) return NotFound();
            return View(detalle);
        }

        public IActionResult Create(int? citaId)
        {
            ViewData["CitaID"] = citaId.HasValue 
                ? new SelectList(_context.Citas, "CitaID", "CitaID", citaId.Value)
                : new SelectList(_context.Citas, "CitaID", "CitaID");
            ViewData["ServicioID"] = new SelectList(_context.Servicios.Where(s => s.Estado == "Activo"), "ServicioID", "NombreServicio");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetalleCitaID,CitaID,ServicioID,PrecioServicio,Descuento")] DetalleCita detalle)
        {
            if (ModelState.IsValid)
            {
                // Si no se especificó precio, usar el precio del servicio
                if (!detalle.PrecioServicio.HasValue)
                {
                    var servicio = await _context.Servicios.FindAsync(detalle.ServicioID);
                    if (servicio != null)
                    {
                        detalle.PrecioServicio = servicio.Precio;
                    }
                }
                
                _context.Add(detalle);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Servicio agregado a la cita exitosamente";
                return RedirectToAction("Details", "Citas", new { id = detalle.CitaID });
            }
            ViewData["CitaID"] = new SelectList(_context.Citas, "CitaID", "CitaID", detalle.CitaID);
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "NombreServicio", detalle.ServicioID);
            return View(detalle);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var detalle = await _context.DetalleCitas.FindAsync(id);
            if (detalle == null) return NotFound();
            ViewData["CitaID"] = new SelectList(_context.Citas, "CitaID", "CitaID", detalle.CitaID);
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "NombreServicio", detalle.ServicioID);
            return View(detalle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetalleCitaID,CitaID,ServicioID,PrecioServicio,Descuento")] DetalleCita detalle)
        {
            if (id != detalle.DetalleCitaID) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalle);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Servicio actualizado exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.DetalleCitas.Any(e => e.DetalleCitaID == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Details", "Citas", new { id = detalle.CitaID });
            }
            ViewData["CitaID"] = new SelectList(_context.Citas, "CitaID", "CitaID", detalle.CitaID);
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "NombreServicio", detalle.ServicioID);
            return View(detalle);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var detalle = await _context.DetalleCitas
                .Include(d => d.Cita)
                .Include(d => d.Servicio)
                .FirstOrDefaultAsync(m => m.DetalleCitaID == id);
            if (detalle == null) return NotFound();
            return View(detalle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalle = await _context.DetalleCitas.FindAsync(id);
            int? citaId = detalle?.CitaID;
            if (detalle != null)
            {
                _context.DetalleCitas.Remove(detalle);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Servicio eliminado de la cita";
            }
            return RedirectToAction("Details", "Citas", new { id = citaId });
        }
    }
}
