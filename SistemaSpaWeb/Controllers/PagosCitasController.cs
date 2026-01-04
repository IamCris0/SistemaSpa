using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class PagosCitasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PagosCitasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pagos = await _context.PagosCitas
                .Include(p => p.Cita).ThenInclude(c => c.Cliente)
                .OrderByDescending(p => p.FechaPago)
                .ToListAsync();
            return View(pagos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var pago = await _context.PagosCitas
                .Include(p => p.Cita).ThenInclude(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.PagoID == id);
            if (pago == null) return NotFound();
            return View(pago);
        }

        public IActionResult Create(int? citaId)
        {
            ViewData["CitaID"] = citaId.HasValue 
                ? new SelectList(_context.Citas, "CitaID", "CitaID", citaId.Value)
                : new SelectList(_context.Citas, "CitaID", "CitaID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PagoID,CitaID,FechaPago,Monto,MetodoPago,Referencia,EstadoPago")] PagoCita pago)
        {
            if (ModelState.IsValid)
            {
                if (pago.FechaPago == null) pago.FechaPago = DateTime.Now;
                _context.Add(pago);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Pago registrado exitosamente";
                return RedirectToAction("Details", "Citas", new { id = pago.CitaID });
            }
            ViewData["CitaID"] = new SelectList(_context.Citas, "CitaID", "CitaID", pago.CitaID);
            return View(pago);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var pago = await _context.PagosCitas.FindAsync(id);
            if (pago == null) return NotFound();
            ViewData["CitaID"] = new SelectList(_context.Citas, "CitaID", "CitaID", pago.CitaID);
            return View(pago);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PagoID,CitaID,FechaPago,Monto,MetodoPago,Referencia,EstadoPago")] PagoCita pago)
        {
            if (id != pago.PagoID) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pago);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Pago actualizado exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.PagosCitas.Any(e => e.PagoID == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Details", "Citas", new { id = pago.CitaID });
            }
            ViewData["CitaID"] = new SelectList(_context.Citas, "CitaID", "CitaID", pago.CitaID);
            return View(pago);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var pago = await _context.PagosCitas
                .Include(p => p.Cita)
                .FirstOrDefaultAsync(m => m.PagoID == id);
            if (pago == null) return NotFound();
            return View(pago);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pago = await _context.PagosCitas.FindAsync(id);
            int? citaId = pago?.CitaID;
            if (pago != null)
            {
                _context.PagosCitas.Remove(pago);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Pago eliminado exitosamente";
            }
            return RedirectToAction("Details", "Citas", new { id = citaId });
        }
    }
}
