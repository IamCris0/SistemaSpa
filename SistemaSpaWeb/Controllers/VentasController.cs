using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class VentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Empleado)
                .Include(v => v.DetalleVentas)
                    .ThenInclude(dv => dv.Producto)
                .OrderByDescending(v => v.FechaVenta)
                .ToListAsync();
            return View(ventas);
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Empleado)
                .Include(v => v.DetalleVentas)
                    .ThenInclude(dv => dv.Producto)
                .FirstOrDefaultAsync(m => m.VentaID == id);

            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.Clientes.Where(c => c.Estado == "Activo"), "ClienteID", "Nombre");
            ViewData["EmpleadoID"] = new SelectList(_context.Empleados.Where(e => e.Estado == "Activo"), "EmpleadoID", "Nombre");
            return View();
        }

        // POST: Ventas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VentaID,ClienteID,EmpleadoID,FechaVenta,Total,MetodoPago,Estado")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                if (venta.FechaVenta == null)
                {
                    venta.FechaVenta = DateTime.Now;
                }

                _context.Add(venta);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Venta creada exitosamente";
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClienteID"] = new SelectList(_context.Clientes.Where(c => c.Estado == "Activo"), "ClienteID", "Nombre", venta.ClienteID);
            ViewData["EmpleadoID"] = new SelectList(_context.Empleados.Where(e => e.Estado == "Activo"), "EmpleadoID", "Nombre", venta.EmpleadoID);
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "Nombre", venta.ClienteID);
            ViewData["EmpleadoID"] = new SelectList(_context.Empleados, "EmpleadoID", "Nombre", venta.EmpleadoID);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VentaID,ClienteID,EmpleadoID,FechaVenta,Total,MetodoPago,Estado")] Venta venta)
        {
            if (id != venta.VentaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Venta actualizada exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.VentaID))
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

            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "Nombre", venta.ClienteID);
            ViewData["EmpleadoID"] = new SelectList(_context.Empleados, "EmpleadoID", "Nombre", venta.EmpleadoID);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Empleado)
                .Include(v => v.DetalleVentas)
                .FirstOrDefaultAsync(m => m.VentaID == id);

            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                var tieneDetalles = await _context.DetalleVentas.AnyAsync(dv => dv.VentaID == id);

                if (tieneDetalles)
                {
                    TempData["Error"] = "No se puede eliminar la venta porque tiene productos asociados";
                    return RedirectToAction(nameof(Index));
                }

                _context.Ventas.Remove(venta);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Venta eliminada exitosamente";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.VentaID == id);
        }
    }
}
