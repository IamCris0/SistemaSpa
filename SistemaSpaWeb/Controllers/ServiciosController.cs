using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Servicios
        public async Task<IActionResult> Index()
        {
            var servicios = await _context.Servicios
                .Include(s => s.Categoria)
                .Include(s => s.DetalleCitas)
                .ToListAsync();
            return View(servicios);
        }

        // GET: Servicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios
                .Include(s => s.Categoria)
                .Include(s => s.DetalleCitas)
                .FirstOrDefaultAsync(m => m.ServicioID == id);

            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // GET: Servicios/Create
        public IActionResult Create()
        {
            ViewData["CategoriaID"] = new SelectList(_context.CategoriasServicios, "CategoriaID", "NombreCategoria");
            return View();
        }

        // POST: Servicios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicioID,CategoriaID,NombreServicio,Descripcion,Duracion,Precio,Estado")] Servicios servicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servicio);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Servicio creado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaID"] = new SelectList(_context.CategoriasServicios, "CategoriaID", "NombreCategoria", servicio.CategoriaID);
            return View(servicio);
        }

        // GET: Servicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }
            ViewData["CategoriaID"] = new SelectList(_context.CategoriasServicios, "CategoriaID", "NombreCategoria", servicio.CategoriaID);
            return View(servicio);
        }

        // POST: Servicios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServicioID,CategoriaID,NombreServicio,Descripcion,Duracion,Precio,Estado")] Servicios servicio)
        {
            if (id != servicio.ServicioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicio);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Servicio actualizado exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioExists(servicio.ServicioID))
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
            ViewData["CategoriaID"] = new SelectList(_context.CategoriasServicios, "CategoriaID", "NombreCategoria", servicio.CategoriaID);
            return View(servicio);
        }

        // GET: Servicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicios
                .Include(s => s.Categoria)
                .Include(s => s.DetalleCitas)
                .FirstOrDefaultAsync(m => m.ServicioID == id);

            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio != null)
            {
                var tieneDetalles = await _context.DetalleCitas.AnyAsync(dc => dc.ServicioID == id);

                if (tieneDetalles)
                {
                    TempData["Error"] = "No se puede eliminar el servicio porque tiene citas asociadas";
                    return RedirectToAction(nameof(Index));
                }

                _context.Servicios.Remove(servicio);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Servicio eliminado exitosamente";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicios.Any(e => e.ServicioID == id);
        }
    }
}