using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var clientes = await _context.Clientes
                .Include(c => c.Citas)
                .Include(c => c.Ventas)
                .Include(c => c.ClientesMembresias)
                .ToListAsync();
            return View(clientes);
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Citas)
                .Include(c => c.Ventas)
                .Include(c => c.ClientesMembresias)
                    .ThenInclude(cm => cm.Membresia)
                .FirstOrDefaultAsync(m => m.ClienteID == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteID,Nombre,Apellido,Email,Telefono,Direccion,FechaNacimiento,FechaRegistro,Estado")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (cliente.FechaRegistro == null)
                {
                    cliente.FechaRegistro = DateTime.Now;
                }

                _context.Add(cliente);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cliente creado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteID,Nombre,Apellido,Email,Telefono,Direccion,FechaNacimiento,FechaRegistro,Estado")] Cliente cliente)
        {
            if (id != cliente.ClienteID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cliente actualizado exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.ClienteID))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Citas)
                .Include(c => c.Ventas)
                .Include(c => c.ClientesMembresias)
                .FirstOrDefaultAsync(m => m.ClienteID == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                var tieneCitas = await _context.Citas.AnyAsync(c => c.ClienteID == id);
                var tieneVentas = await _context.Ventas.AnyAsync(v => v.ClienteID == id);
                var tieneMembresias = await _context.ClientesMembresias.AnyAsync(cm => cm.ClienteID == id);

                if (tieneCitas || tieneVentas || tieneMembresias)
                {
                    TempData["Error"] = "No se puede eliminar el cliente porque tiene citas, ventas o membresías asociadas";
                    return RedirectToAction(nameof(Index));
                }

                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cliente eliminado exitosamente";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.ClienteID == id);
        }
    }
}