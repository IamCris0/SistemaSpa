using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class DetalleComprasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetalleComprasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DetalleCompras
        public async Task<IActionResult> Index()
        {
            var detalles = await _context.DetalleCompras
                .Include(d => d.Compra).ThenInclude(c => c.Proveedor)
                .Include(d => d.Producto)
                .OrderByDescending(d => d.DetalleCompraID)
                .ToListAsync();
            return View(detalles);
        }

        // GET: DetalleCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            
            var detalle = await _context.DetalleCompras
                .Include(d => d.Compra).ThenInclude(c => c.Proveedor)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.DetalleCompraID == id);
            
            if (detalle == null) return NotFound();
            return View(detalle);
        }

        // GET: DetalleCompras/Create
        public IActionResult Create(int? compraId)
        {
            ViewData["CompraID"] = compraId.HasValue 
                ? new SelectList(_context.Compras.Include(c => c.Proveedor), "CompraID", "CompraID", compraId.Value)
                : new SelectList(_context.Compras.Include(c => c.Proveedor), "CompraID", "CompraID");
            ViewData["ProductoID"] = new SelectList(_context.Productos.Where(p => p.Estado == "Activo"), "ProductoID", "NombreProducto");
            return View();
        }

        // POST: DetalleCompras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetalleCompraID,CompraID,ProductoID,Cantidad,PrecioUnitario")] DetalleCompra detalle)
        {
            if (ModelState.IsValid)
            {
                // Si no se especificó precio, usar el precio del producto
                if (!detalle.PrecioUnitario.HasValue || detalle.PrecioUnitario == 0)
                {
                    var producto1 = await _context.Productos.FindAsync(detalle.ProductoID);
                    if (producto1 != null)
                    {
                        detalle.PrecioUnitario = producto1.PrecioUnitario;
                    }
                }

                _context.Add(detalle);
                await _context.SaveChangesAsync();

                // Actualizar stock del producto
                var producto = await _context.Productos.FindAsync(detalle.ProductoID);
                if (producto != null)
                {
                    producto.Stock = (producto.Stock ?? 0) + (detalle.Cantidad ?? 0);
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }

                // Actualizar total de la compra
                await ActualizarTotalCompra(detalle.CompraID);

                TempData["Success"] = "Producto agregado a la compra exitosamente";
                return RedirectToAction("Details", "Compras", new { id = detalle.CompraID });
            }
            
            ViewData["CompraID"] = new SelectList(_context.Compras, "CompraID", "CompraID", detalle.CompraID);
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ProductoID", "NombreProducto", detalle.ProductoID);
            return View(detalle);
        }

        // GET: DetalleCompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            
            var detalle = await _context.DetalleCompras.FindAsync(id);
            if (detalle == null) return NotFound();
            
            ViewData["CompraID"] = new SelectList(_context.Compras, "CompraID", "CompraID", detalle.CompraID);
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ProductoID", "NombreProducto", detalle.ProductoID);
            return View(detalle);
        }

        // POST: DetalleCompras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetalleCompraID,CompraID,ProductoID,Cantidad,PrecioUnitario")] DetalleCompra detalle)
        {
            if (id != detalle.DetalleCompraID) return NotFound();
            
            if (ModelState.IsValid)
            {
                try
                {
                    var detalleOriginal = await _context.DetalleCompras.AsNoTracking().FirstOrDefaultAsync(d => d.DetalleCompraID == id);
                    
                    _context.Update(detalle);
                    await _context.SaveChangesAsync();

                    // Actualizar stock si cambió la cantidad
                    if (detalleOriginal != null && detalleOriginal.Cantidad != detalle.Cantidad)
                    {
                        var producto = await _context.Productos.FindAsync(detalle.ProductoID);
                        if (producto != null)
                        {
                            var diferencia = (detalle.Cantidad ?? 0) - (detalleOriginal.Cantidad ?? 0);
                            producto.Stock = (producto.Stock ?? 0) + diferencia;
                            _context.Update(producto);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Actualizar total de la compra
                    await ActualizarTotalCompra(detalle.CompraID);

                    TempData["Success"] = "Detalle actualizado exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.DetalleCompras.Any(e => e.DetalleCompraID == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Details", "Compras", new { id = detalle.CompraID });
            }
            
            ViewData["CompraID"] = new SelectList(_context.Compras, "CompraID", "CompraID", detalle.CompraID);
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ProductoID", "NombreProducto", detalle.ProductoID);
            return View(detalle);
        }

        // GET: DetalleCompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            
            var detalle = await _context.DetalleCompras
                .Include(d => d.Compra)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.DetalleCompraID == id);
            
            if (detalle == null) return NotFound();
            return View(detalle);
        }

        // POST: DetalleCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalle = await _context.DetalleCompras.FindAsync(id);
            int? compraId = detalle?.CompraID;
            
            if (detalle != null)
            {
                // Actualizar stock del producto
                var producto = await _context.Productos.FindAsync(detalle.ProductoID);
                if (producto != null)
                {
                    producto.Stock = (producto.Stock ?? 0) - (detalle.Cantidad ?? 0);
                    _context.Update(producto);
                }

                _context.DetalleCompras.Remove(detalle);
                await _context.SaveChangesAsync();

                // Actualizar total de la compra
                if (compraId.HasValue)
                {
                    await ActualizarTotalCompra(compraId);
                }

                TempData["Success"] = "Producto eliminado de la compra";
            }
            
            return RedirectToAction("Details", "Compras", new { id = compraId });
        }

        private async Task ActualizarTotalCompra(int? compraId)
        {
            if (compraId.HasValue)
            {
                var compra = await _context.Compras.FindAsync(compraId.Value);
                if (compra != null)
                {
                    var total = await _context.DetalleCompras
                        .Where(d => d.CompraID == compraId.Value)
                        .SumAsync(d => (d.Cantidad ?? 0) * (d.PrecioUnitario ?? 0));
                    
                    compra.Total = total;
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}