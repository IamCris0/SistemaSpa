using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class MembresiasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembresiasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var membresias = await _context.Membresias.Include(m => m.ClientesMembresias).ToListAsync();
            return View(membresias);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var membresia = await _context.Membresias.Include(m => m.ClientesMembresias).FirstOrDefaultAsync(m => m.MembresiaID == id);
            if (membresia == null) return NotFound();
            return View(membresia);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MembresiaID,NombreMembresia,Descripcion,DuracionMeses,Precio,Descuento,Estado")] Membresia membresia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membresia);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Membresía creada exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View(membresia);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var membresia = await _context.Membresias.FindAsync(id);
            if (membresia == null) return NotFound();
            return View(membresia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MembresiaID,NombreMembresia,Descripcion,DuracionMeses,Precio,Descuento,Estado")] Membresia membresia)
        {
            if (id != membresia.MembresiaID) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membresia);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Membresía actualizada exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Membresias.Any(e => e.MembresiaID == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(membresia);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var membresia = await _context.Membresias.Include(m => m.ClientesMembresias).FirstOrDefaultAsync(m => m.MembresiaID == id);
            if (membresia == null) return NotFound();
            return View(membresia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membresia = await _context.Membresias.FindAsync(id);
            if (membresia != null)
            {
                if (await _context.ClientesMembresias.AnyAsync(cm => cm.MembresiaID == id))
                {
                    TempData["Error"] = "No se puede eliminar la membresía porque tiene clientes asociados";
                    return RedirectToAction(nameof(Index));
                }
                _context.Membresias.Remove(membresia);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Membresía eliminada exitosamente";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
