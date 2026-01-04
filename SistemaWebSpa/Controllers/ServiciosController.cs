using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using SpaWebMVC.Models;
using System.Data;

namespace SpaWebMVC.Controllers
{
    [Authorize]
    public class ServiciosController : Controller
    {
        private readonly string _connectionString;

        public ServiciosController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SpaConnection")!;
        }

        public async Task<IActionResult> Index()
        {
            var servicios = new List<Servicio>();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(@"
                    SELECT s.ServicioID, s.CategoriaID, s.NombreServicio, s.Descripcion, 
                           s.Duracion, s.Precio, s.Estado, c.NombreCategoria
                    FROM Servicios s
                    LEFT JOIN CategoriasServicios c ON s.CategoriaID = c.CategoriaID", connection))
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            servicios.Add(new Servicio
                            {
                                ServicioID = reader.GetInt32(0),
                                CategoriaID = reader.IsDBNull(1) ? null : reader.GetInt32(1),
                                NombreServicio = reader.GetString(2),
                                Descripcion = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Duracion = reader.GetInt32(4),
                                Precio = reader.GetDecimal(5),
                                Estado = reader.IsDBNull(6) ? "Activo" : reader.GetString(6),
                                NombreCategoria = reader.IsDBNull(7) ? null : reader.GetString(7)
                            });
                        }
                    }
                }
            }
            return View(servicios);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            Servicio? servicio = await ObtenerServicioPorId(id.Value);
            if (servicio == null) return NotFound();
            return View(servicio);
        }

        public async Task<IActionResult> Create()
        {
            await CargarCategorias();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaID,NombreServicio,Descripcion,Duracion,Precio,Estado")] Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        int nuevoId;
                        using (var cmdId = new SqlCommand("SELECT ISNULL(MAX(ServicioID), 0) + 1 FROM Servicios", connection))
                        {
                            await connection.OpenAsync();
                            nuevoId = (int)(await cmdId.ExecuteScalarAsync())!;
                        }

                        using (var command = new SqlCommand("CP_InsertarServicios", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@ServicioID", nuevoId);
                            command.Parameters.AddWithValue("@CategoriaID", (object?)servicio.CategoriaID ?? DBNull.Value);
                            command.Parameters.AddWithValue("@NombreServicio", servicio.NombreServicio);
                            command.Parameters.AddWithValue("@Descripcion", (object?)servicio.Descripcion ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Duracion", servicio.Duracion);
                            command.Parameters.AddWithValue("@Precio", servicio.Precio);
                            command.Parameters.AddWithValue("@Estado", servicio.Estado);

                            if (connection.State != ConnectionState.Open)
                                await connection.OpenAsync();
                            await command.ExecuteNonQueryAsync();
                        }
                    }
                    TempData["Success"] = "Servicio creado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error: " + ex.Message;
                }
            }
            await CargarCategorias();
            return View(servicio);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var servicio = await ObtenerServicioPorId(id.Value);
            if (servicio == null) return NotFound();
            await CargarCategorias();
            return View(servicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServicioID,CategoriaID,NombreServicio,Descripcion,Duracion,Precio,Estado")] Servicio servicio)
        {
            if (id != servicio.ServicioID) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        using (var command = new SqlCommand("CP_ModificarServicios", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@ServicioID", servicio.ServicioID);
                            command.Parameters.AddWithValue("@CategoriaID", (object?)servicio.CategoriaID ?? DBNull.Value);
                            command.Parameters.AddWithValue("@NombreServicio", servicio.NombreServicio);
                            command.Parameters.AddWithValue("@Descripcion", (object?)servicio.Descripcion ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Duracion", servicio.Duracion);
                            command.Parameters.AddWithValue("@Precio", servicio.Precio);
                            command.Parameters.AddWithValue("@Estado", servicio.Estado);

                            await connection.OpenAsync();
                            await command.ExecuteNonQueryAsync();
                        }
                    }
                    TempData["Success"] = "Servicio actualizado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    if (!await ServicioExists(servicio.ServicioID))
                        return NotFound();
                    throw;
                }
            }
            await CargarCategorias();
            return View(servicio);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var servicio = await ObtenerServicioPorId(id.Value);
            if (servicio == null) return NotFound();
            return View(servicio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool tieneDetalles = false;
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SELECT COUNT(*) FROM DetalleCitas WHERE ServicioID = @ServicioID", connection))
                    {
                        command.Parameters.AddWithValue("@ServicioID", id);
                        await connection.OpenAsync();
                        tieneDetalles = (int)(await command.ExecuteScalarAsync())! > 0;
                    }
                }

                if (tieneDetalles)
                {
                    TempData["Error"] = "No se puede eliminar el servicio porque está asociado a citas";
                    return RedirectToAction(nameof(Index));
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("CP_EliminarServicios", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ServicioID", id);
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
                TempData["Success"] = "Servicio eliminado exitosamente";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<Servicio?> ObtenerServicioPorId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(@"
                    SELECT s.ServicioID, s.CategoriaID, s.NombreServicio, s.Descripcion, 
                           s.Duracion, s.Precio, s.Estado, c.NombreCategoria
                    FROM Servicios s
                    LEFT JOIN CategoriasServicios c ON s.CategoriaID = c.CategoriaID
                    WHERE s.ServicioID = @ServicioID", connection))
                {
                    command.Parameters.AddWithValue("@ServicioID", id);
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Servicio
                            {
                                ServicioID = reader.GetInt32(0),
                                CategoriaID = reader.IsDBNull(1) ? null : reader.GetInt32(1),
                                NombreServicio = reader.GetString(2),
                                Descripcion = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Duracion = reader.GetInt32(4),
                                Precio = reader.GetDecimal(5),
                                Estado = reader.IsDBNull(6) ? "Activo" : reader.GetString(6),
                                NombreCategoria = reader.IsDBNull(7) ? null : reader.GetString(7)
                            };
                        }
                    }
                }
            }
            return null;
        }

        private async Task CargarCategorias()
        {
            var categorias = new List<SelectListItem>();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SELECT CategoriaID, NombreCategoria FROM CategoriasServicios WHERE Estado = 'Activo'", connection))
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            categorias.Add(new SelectListItem
                            {
                                Value = reader.GetInt32(0).ToString(),
                                Text = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            ViewBag.Categorias = categorias;
        }

        private async Task<bool> ServicioExists(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SELECT COUNT(*) FROM Servicios WHERE ServicioID = @ServicioID", connection))
                {
                    command.Parameters.AddWithValue("@ServicioID", id);
                    await connection.OpenAsync();
                    return (int)(await command.ExecuteScalarAsync())! > 0;
                }
            }
        }
    }
}
