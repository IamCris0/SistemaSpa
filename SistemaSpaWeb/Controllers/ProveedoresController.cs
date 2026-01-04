using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProveedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var proveedores = await _context.Proveedores
                .Include(p => p.Compras)
                .Include(p => p.GastosOperativos)
                .ToListAsync();
            return View(proveedores);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var proveedor = await _context.Proveedores
                .Include(p => p.Compras)
                .Include(p => p.GastosOperativos)
                .FirstOrDefaultAsync(m => m.ProveedorID == id);
            if (proveedor == null) return NotFound();
            return View(proveedor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProveedorID,NombreProveedor,Contacto,Telefono,Email,Direccion,Estado")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedor);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Proveedor creado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null) return NotFound();
            return View(proveedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProveedorID,NombreProveedor,Contacto,Telefono,Email,Direccion,Estado")] Proveedor proveedor)
        {
            if (id != proveedor.ProveedorID) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedor);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Proveedor actualizado exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Proveedores.Any(e => e.ProveedorID == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var proveedor = await _context.Proveedores
                .Include(p => p.Compras)
                .Include(p => p.GastosOperativos)
                .FirstOrDefaultAsync(m => m.ProveedorID == id);
            if (proveedor == null) return NotFound();
            return View(proveedor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor != null)
            {
                var tieneCompras = await _context.Compras.AnyAsync(c => c.ProveedorID == id);
                var tieneGastos = await _context.GastosOperativos.AnyAsync(g => g.ProveedorID == id);

                if (tieneCompras || tieneGastos)
                {
                    TempData["Error"] = "No se puede eliminar el proveedor porque tiene compras o gastos asociados";
                    return RedirectToAction(nameof(Index));
                }

                _context.Proveedores.Remove(proveedor);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Proveedor eliminado exitosamente";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
