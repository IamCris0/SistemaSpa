using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaSpaWeb.Models;

namespace SistemaSpaWeb.Controllers
{
    public class ClientesMembresiasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesMembresiasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var clientesMembresias = await _context.ClientesMembresias
                .Include(cm => cm.Cliente)
                .Include(cm => cm.Membresia)
                .OrderByDescending(cm => cm.FechaInicio)
                .ToListAsync();
            return View(clientesMembresias);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var clienteMembresia = await _context.ClientesMembresias
                .Include(cm => cm.Cliente)
                .Include(cm => cm.Membresia)
                .FirstOrDefaultAsync(m => m.ClienteMembresiaID == id);
            if (clienteMembresia == null) return NotFound();
            return View(clienteMembresia);
        }

        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.Clientes.Where(c => c.Estado == "Activo"), "ClienteID", "Nombre");
            ViewData["MembresiaID"] = new SelectList(_context.Membresias.Where(m => m.Estado == "Activo"), "MembresiaID", "NombreMembresia");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteMembresiaID,ClienteID,MembresiaID,FechaInicio,FechaFin,EstadoMembresia")] ClienteMembresia clienteMembresia)
        {
            if (ModelState.IsValid)
            {
                clienteMembresia.FechaRegistro = DateTime.Now;
                
                // Calcular fecha fin si hay duración
                if (clienteMembresia.FechaInicio.HasValue)
                {
                    var membresia = await _context.Membresias.FindAsync(clienteMembresia.MembresiaID);
                    if (membresia != null && membresia.DuracionMeses.HasValue)
                    {
                        clienteMembresia.FechaFin = clienteMembresia.FechaInicio.Value.AddMonths(membresia.DuracionMeses.Value);
                    }
                }
                
                _context.Add(clienteMembresia);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Membresía asignada exitosamente";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "Nombre", clienteMembresia.ClienteID);
            ViewData["MembresiaID"] = new SelectList(_context.Membresias, "MembresiaID", "NombreMembresia", clienteMembresia.MembresiaID);
            return View(clienteMembresia);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var clienteMembresia = await _context.ClientesMembresias.FindAsync(id);
            if (clienteMembresia == null) return NotFound();
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "Nombre", clienteMembresia.ClienteID);
            ViewData["MembresiaID"] = new SelectList(_context.Membresias, "MembresiaID", "NombreMembresia", clienteMembresia.MembresiaID);
            return View(clienteMembresia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteMembresiaID,ClienteID,MembresiaID,FechaInicio,FechaFin,EstadoMembresia,FechaRegistro")] ClienteMembresia clienteMembresia)
        {
            if (id != clienteMembresia.ClienteMembresiaID) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteMembresia);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Membresía actualizada exitosamente";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ClientesMembresias.Any(e => e.ClienteMembresiaID == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ClienteID", "Nombre", clienteMembresia.ClienteID);
            ViewData["MembresiaID"] = new SelectList(_context.Membresias, "MembresiaID", "NombreMembresia", clienteMembresia.MembresiaID);
            return View(clienteMembresia);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var clienteMembresia = await _context.ClientesMembresias
                .Include(cm => cm.Cliente)
                .Include(cm => cm.Membresia)
                .FirstOrDefaultAsync(m => m.ClienteMembresiaID == id);
            if (clienteMembresia == null) return NotFound();
            return View(clienteMembresia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clienteMembresia = await _context.ClientesMembresias.FindAsync(id);
            if (clienteMembresia != null)
            {
                _context.ClientesMembresias.Remove(clienteMembresia);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Membresía eliminada exitosamente";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
