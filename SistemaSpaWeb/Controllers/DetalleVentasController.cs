using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class DetalleVentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetalleVentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DetalleVentas
        public async Task<IActionResult> Index()
        {
            var detalles = await _context.DetalleVentas
                .Include(d => d.Venta).ThenInclude(v => v.Cliente)
                .Include(d => d.Producto)
                .OrderByDescending(d => d.DetalleVentaID)
                .ToListAsync();
            return View(detalles);
        }

        // GET: DetalleVentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            
            var detalle = await _context.DetalleVentas
                .Include(d => d.Venta).ThenInclude(v => v.Cliente)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.DetalleVentaID == id);
            
            if (detalle == null) return NotFound();
            return View(detalle);
        }

        // GET: DetalleVentas/Create
        public IActionResult Create(int? ventaId)
        {
            ViewData["VentaID"] = ventaId.HasValue 
                ? new SelectList(_context.Ventas.Include(v => v.Cliente), "VentaID", "VentaID", ventaId.Value)
                : new SelectList(_context.Ventas.Include(v => v.Cliente), "VentaID", "VentaID");
            ViewData["ProductoID"] = new SelectList(_context.Productos.Where(p => p.Estado == "Activo" && p.Stock > 0), "ProductoID", "NombreProducto");
            return View();
        }

        // POST: DetalleVentas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetalleVentaID,VentaID,ProductoID,Cantidad,PrecioUnitario")] DetalleVenta detalle)
        {
            if (ModelState.IsValid)
            {
                // Verificar stock disponible
                var producto = await _context.Productos.FindAsync(detalle.ProductoID);
                if (producto != null)
                {
                    if ((producto.Stock ?? 0) < (detalle.Cantidad ?? 0))
                    {
                        ModelState.AddModelError("Cantidad", $"Stock insuficiente. Disponible: {producto.Stock}");
                        ViewData["VentaID"] = new SelectList(_context.Ventas, "VentaID", "VentaID", detalle.VentaID);
                        ViewData["ProductoID"] = new SelectList(_context.Productos, "ProductoID", "NombreProducto", detalle.ProductoID);
                        return View(detalle);
                    }

                    // Si no se especificó precio, usar el precio del producto
                    if (!detalle.PrecioUnitario.HasValue || detalle.PrecioUnitario == 0)
                    {
                        detalle.PrecioUnitario = producto.PrecioUnitario;
                    }
                }

                _context.Add(detalle);
                await _context.SaveChangesAsync();

                // Actualizar stock del producto
                if (producto != null)
                {
                    producto.Stock = (producto.Stock ?? 0) - (detalle.Cantidad ?? 0);
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }

                // Actualizar total de la venta
                await ActualizarTotalVenta(detalle.VentaID);

                TempData["Success"] = "Producto agregado a la venta exitosamente";
                return RedirectToAction("Details", "Ventas", new { id = detalle.VentaID });
            }
            
            ViewData["VentaID"] = new SelectList(_context.Ventas, "VentaID", "VentaID", detalle.VentaID);
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ProductoID", "NombreProducto", detalle.ProductoID);
            return View(detalle);
        }

        // GET: DetalleVentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            
            var detalle = await _context.DetalleVentas.FindAsync(id);
            if (detalle == null) return NotFound();
            
            ViewData["VentaID"] = new SelectList(_context.Ventas, "VentaID", "VentaID", detalle.VentaID);
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ProductoID", "NombreProducto", detalle.ProductoID);
            return View(detalle);
        }

        // POST: DetalleVentas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetalleVentaID,VentaID,ProductoID,Cantidad,PrecioUnitario")] DetalleVenta detalle)
        {
            if (id != detalle.DetalleVentaID) return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {
                    var detalleOriginal = await _context.DetalleVentas.AsNoTracking().FirstOrDefaultAsync(d => d.DetalleVentaID == id);
                    
                    // Verificar stock si aumentó la cantidad
                    if (detalleOriginal != null && detalle.Cantidad > detalleOriginal.Cantidad)
                    {
                        var producto = await _context.Productos.FindAsync(detalle.ProductoID);
                        if (producto != null)
                        {
                            var diferenciaAumento = (detalle.Cantidad ?? 0) - (detalleOriginal.Cantidad ?? 0);
                            if ((producto.Stock ?? 0) < diferenciaAumento)
                            {
                                ModelState.AddModelError("Cantidad", $"Stock insuficiente. Disponible: {producto.Stock}");
                                ViewData["VentaID"] = new SelectList(_context.Ventas, "VentaID", "VentaID", detalle.VentaID);
                                ViewData["ProductoID"] = new SelectList(_context.Productos, "ProductoID", "NombreProducto", detalle.ProductoID);
                                return View(detalle);
                            }
                        }
                    }

                    _context.Update(detalle);
                    await _context.SaveChangesAsync();

                    // Actualizar stock si cambió la cantidad
                    if (detalleOriginal != null && detalleOriginal.Cantidad != detalle.Cantidad)
                    {
                        var producto = await _context.Productos.FindAsync(detalle.ProductoID);
                        if (producto != null)
                        {
                            var diferencia = (detalleOriginal.Cantidad ?? 0) - (detalle.Cantidad ?? 0);
                            producto.Stock = (producto.Stock ?? 0) + diferencia;
                            _context.Update(producto);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Actualizar total de la venta
                    await ActualizarTotalVenta(detalle.VentaID);

                    TempData["Success"] = "Detalle actualizado exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.DetalleVentas.Any(e => e.DetalleVentaID == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Details", "Ventas", new { id = detalle.VentaID });
            }
            
            ViewData["VentaID"] = new SelectList(_context.Ventas, "VentaID", "VentaID", detalle.VentaID);
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ProductoID", "NombreProducto", detalle.ProductoID);
            return View(detalle);
        }

        // GET: DetalleVentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            
            var detalle = await _context.DetalleVentas
                .Include(d => d.Venta)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.DetalleVentaID == id);
            
            if (detalle == null) return NotFound();
            return View(detalle);
        }

        // POST: DetalleVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalle = await _context.DetalleVentas.FindAsync(id);
            int? ventaId = detalle?.VentaID;
            
            if (detalle != null)
            {
                // Devolver stock al producto
                var producto = await _context.Productos.FindAsync(detalle.ProductoID);
                if (producto != null)
                {
                    producto.Stock = (producto.Stock ?? 0) + (detalle.Cantidad ?? 0);
                    _context.Update(producto);
                }

                _context.DetalleVentas.Remove(detalle);
                await _context.SaveChangesAsync();

                // Actualizar total de la venta
                if (ventaId.HasValue)
                {
                    await ActualizarTotalVenta(ventaId);
                }

                TempData["Success"] = "Producto eliminado de la venta";
            }
            
            return RedirectToAction("Details", "Ventas", new { id = ventaId });
        }

        private async Task ActualizarTotalVenta(int? ventaId)
        {
            if (ventaId.HasValue)
            {
                var venta = await _context.Ventas.FindAsync(ventaId.Value);
                if (venta != null)
                {
                    var total = await _context.DetalleVentas
                        .Where(d => d.VentaID == ventaId.Value)
                        .SumAsync(d => (d.Cantidad ?? 0) * (d.PrecioUnitario ?? 0));
                    
                    venta.Total = total;
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}