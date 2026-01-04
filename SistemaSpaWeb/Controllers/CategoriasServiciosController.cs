using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class CategoriasServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriasServiciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CategoriasServicios
        public async Task<IActionResult> Index()
        {
            var categorias = await _context.CategoriasServicios
                .Include(c => c.Servicios)
                .ToListAsync();
            return View(categorias);
        }

        // GET: CategoriasServicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.CategoriasServicios
                .Include(c => c.Servicios)
                .FirstOrDefaultAsync(m => m.CategoriaID == id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: CategoriasServicios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriasServicios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaID,NombreCategoria,Descripcion,Estado")] CategoriasServicios categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Categoría creada exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: CategoriasServicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.CategoriasServicios.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: CategoriasServicios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaID,NombreCategoria,Descripcion,Estado")] CategoriasServicios categoria)
        {
            if (id != categoria.CategoriaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Categoría actualizada exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.CategoriaID))
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
            return View(categoria);
        }

        // GET: CategoriasServicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.CategoriasServicios
                .Include(c => c.Servicios)
                .FirstOrDefaultAsync(m => m.CategoriaID == id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: CategoriasServicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.CategoriasServicios.FindAsync(id);
            if (categoria != null)
            {
                var tieneServicios = await _context.Servicios.AnyAsync(s => s.CategoriaID == id);

                if (tieneServicios)
                {
                    TempData["Error"] = "No se puede eliminar la categoría porque tiene servicios asociados";
                    return RedirectToAction(nameof(Index));
                }

                _context.CategoriasServicios.Remove(categoria);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Categoría eliminada exitosamente";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.CategoriasServicios.Any(e => e.CategoriaID == id);
        }
    }
}