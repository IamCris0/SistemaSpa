using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class SalasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var salas = await _context.Salas.Include(s => s.Citas).ToListAsync();
            return View(salas);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var sala = await _context.Salas.Include(s => s.Citas).FirstOrDefaultAsync(m => m.SalaID == id);
            if (sala == null) return NotFound();
            return View(sala);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalaID,NombreSala,TipoSala,Capacidad,Estado")] Sala sala)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sala);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Sala creada exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var sala = await _context.Salas.FindAsync(id);
            if (sala == null) return NotFound();
            return View(sala);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalaID,NombreSala,TipoSala,Capacidad,Estado")] Sala sala)
        {
            if (id != sala.SalaID) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sala);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Sala actualizada exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Salas.Any(e => e.SalaID == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var sala = await _context.Salas.Include(s => s.Citas).FirstOrDefaultAsync(m => m.SalaID == id);
            if (sala == null) return NotFound();
            return View(sala);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            if (sala != null)
            {
                if (await _context.Citas.AnyAsync(c => c.SalaID == id))
                {
                    TempData["Error"] = "No se puede eliminar la sala porque tiene citas asociadas";
                    return RedirectToAction(nameof(Index));
                }
                _context.Salas.Remove(sala);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Sala eliminada exitosamente";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
