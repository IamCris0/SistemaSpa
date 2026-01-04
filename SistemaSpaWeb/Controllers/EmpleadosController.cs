using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpleadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            var empleados = await _context.Empleados
                .Include(e => e.Citas)
                .Include(e => e.Ventas)
                .Include(e => e.TurnosEmpleados)
                .ToListAsync();
            return View(empleados);
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.Citas)
                .Include(e => e.Ventas)
                .Include(e => e.TurnosEmpleados)
                .FirstOrDefaultAsync(m => m.EmpleadoID == id);

            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpleadoID,Nombre,Apellido,Email,Telefono,Cargo,FechaContratacion,Salario,Estado")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Empleado creado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpleadoID,Nombre,Apellido,Email,Telefono,Cargo,FechaContratacion,Salario,Estado")] Empleado empleado)
        {
            if (id != empleado.EmpleadoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Empleado actualizado exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.EmpleadoID))
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
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.Citas)
                .Include(e => e.Ventas)
                .Include(e => e.TurnosEmpleados)
                .FirstOrDefaultAsync(m => m.EmpleadoID == id);

            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                var tieneCitas = await _context.Citas.AnyAsync(c => c.EmpleadoID == id);
                var tieneVentas = await _context.Ventas.AnyAsync(v => v.EmpleadoID == id);
                var tieneTurnos = await _context.TurnosEmpleados.AnyAsync(t => t.EmpleadoID == id);

                if (tieneCitas || tieneVentas || tieneTurnos)
                {
                    TempData["Error"] = "No se puede eliminar el empleado porque tiene citas, ventas o turnos asociados";
                    return RedirectToAction(nameof(Index));
                }

                _context.Empleados.Remove(empleado);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Empleado eliminado exitosamente";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.EmpleadoID == id);
        }
    }
}
