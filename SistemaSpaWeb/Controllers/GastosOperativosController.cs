using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class GastosOperativosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GastosOperativosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var gastos = await _context.GastosOperativos
                .Include(g => g.Proveedor)
                .OrderByDescending(g => g.FechaGasto)
                .ToListAsync();
            return View(gastos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var gasto = await _context.GastosOperativos
                .Include(g => g.Proveedor)
                .FirstOrDefaultAsync(m => m.GastoID == id);
            if (gasto == null) return NotFound();
            return View(gasto);
        }

        public IActionResult Create()
        {
            ViewData["ProveedorID"] = new SelectList(_context.Proveedores.Where(p => p.Estado == "Activo"), "ProveedorID", "NombreProveedor");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GastoID,ProveedorID,TipoGasto,Descripcion,Monto,FechaGasto,MetodoPago,Comprobante,Estado")] GastoOperativo gasto)
        {
            if (ModelState.IsValid)
            {
                gasto.FechaRegistro = DateTime.Now;
                if (gasto.FechaGasto == null) gasto.FechaGasto = DateTime.Now;
                _context.Add(gasto);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Gasto registrado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProveedorID"] = new SelectList(_context.Proveedores, "ProveedorID", "NombreProveedor", gasto.ProveedorID);
            return View(gasto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var gasto = await _context.GastosOperativos.FindAsync(id);
            if (gasto == null) return NotFound();
            ViewData["ProveedorID"] = new SelectList(_context.Proveedores, "ProveedorID", "NombreProveedor", gasto.ProveedorID);
            return View(gasto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GastoID,ProveedorID,TipoGasto,Descripcion,Monto,FechaGasto,MetodoPago,Comprobante,Estado,FechaRegistro")] GastoOperativo gasto)
        {
            if (id != gasto.GastoID) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gasto);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Gasto actualizado exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.GastosOperativos.Any(e => e.GastoID == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProveedorID"] = new SelectList(_context.Proveedores, "ProveedorID", "NombreProveedor", gasto.ProveedorID);
            return View(gasto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var gasto = await _context.GastosOperativos
                .Include(g => g.Proveedor)
                .FirstOrDefaultAsync(m => m.GastoID == id);
            if (gasto == null) return NotFound();
            return View(gasto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gasto = await _context.GastosOperativos.FindAsync(id);
            if (gasto != null)
            {
                _context.GastosOperativos.Remove(gasto);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Gasto eliminado exitosamente";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
