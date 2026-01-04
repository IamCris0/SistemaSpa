using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class ComprasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComprasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var compras = await _context.Compras
                .Include(c => c.Proveedor)
                .Include(c => c.DetalleCompras).ThenInclude(dc => dc.Producto)
                .OrderByDescending(c => c.FechaCompra)
                .ToListAsync();
            return View(compras);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var compra = await _context.Compras
                .Include(c => c.Proveedor)
                .Include(c => c.DetalleCompras).ThenInclude(dc => dc.Producto)
                .FirstOrDefaultAsync(m => m.CompraID == id);
            if (compra == null) return NotFound();
            return View(compra);
        }

        public IActionResult Create()
        {
            ViewData["ProveedorID"] = new SelectList(_context.Proveedores.Where(p => p.Estado == "Activo"), "ProveedorID", "NombreProveedor");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompraID,ProveedorID,FechaCompra,Total,EstadoCompra,Observaciones")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                if (compra.FechaCompra == null) compra.FechaCompra = DateTime.Now;
                _context.Add(compra);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Compra creada exitosamente";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProveedorID"] = new SelectList(_context.Proveedores, "ProveedorID", "NombreProveedor", compra.ProveedorID);
            return View(compra);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var compra = await _context.Compras.FindAsync(id);
            if (compra == null) return NotFound();
            ViewData["ProveedorID"] = new SelectList(_context.Proveedores, "ProveedorID", "NombreProveedor", compra.ProveedorID);
            return View(compra);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompraID,ProveedorID,FechaCompra,Total,EstadoCompra,Observaciones")] Compra compra)
        {
            if (id != compra.CompraID) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Compra actualizada exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Compras.Any(e => e.CompraID == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProveedorID"] = new SelectList(_context.Proveedores, "ProveedorID", "NombreProveedor", compra.ProveedorID);
            return View(compra);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var compra = await _context.Compras
                .Include(c => c.Proveedor)
                .Include(c => c.DetalleCompras)
                .FirstOrDefaultAsync(m => m.CompraID == id);
            if (compra == null) return NotFound();
            return View(compra);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                if (await _context.DetalleCompras.AnyAsync(dc => dc.CompraID == id))
                {
                    TempData["Error"] = "No se puede eliminar la compra porque tiene productos asociados";
                    return RedirectToAction(nameof(Index));
                }
                _context.Compras.Remove(compra);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Compra eliminada exitosamente";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}