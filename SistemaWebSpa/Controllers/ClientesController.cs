using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SpaWebMVC.Models;
using System.Data;

namespace SpaWebMVC.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly string _connectionString;

        public ClientesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SpaConnection")!;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var clientes = new List<Cliente>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("CP_ListarClientes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            clientes.Add(new Cliente
                            {
                                ClienteID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                Email = reader.GetString(3),
                                Telefono = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Direccion = reader.IsDBNull(5) ? null : reader.GetString(5),
                                FechaNacimiento = reader.IsDBNull(6) ? null : reader.GetDateTime(6),
                                FechaRegistro = reader.IsDBNull(7) ? DateTime.Now : reader.GetDateTime(7),
                                Estado = reader.IsDBNull(8) ? "Activo" : reader.GetString(8)
                            });
                        }
                    }
                }
            }

            return View(clientes);
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cliente? cliente = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SELECT * FROM Clientes WHERE ClienteID = @ClienteID", connection))
                {
                    command.Parameters.AddWithValue("@ClienteID", id);
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            cliente = new Cliente
                            {
                                ClienteID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                Email = reader.GetString(3),
                                Telefono = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Direccion = reader.IsDBNull(5) ? null : reader.GetString(5),
                                FechaNacimiento = reader.IsDBNull(6) ? null : reader.GetDateTime(6),
                                FechaRegistro = reader.IsDBNull(7) ? DateTime.Now : reader.GetDateTime(7),
                                Estado = reader.IsDBNull(8) ? "Activo" : reader.GetString(8)
                            };
                        }
                    }
                }
            }

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
        public async Task<IActionResult> Create([Bind("Nombre,Apellido,Email,Telefono,Direccion,FechaNacimiento,Estado")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        int nuevoId;
                        using (var cmdId = new SqlCommand("SELECT ISNULL(MAX(ClienteID), 0) + 1 FROM Clientes", connection))
                        {
                            await connection.OpenAsync();
                            nuevoId = (int)(await cmdId.ExecuteScalarAsync())!;
                        }

                        using (var command = new SqlCommand("CP_InsertarClientes", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@ClienteID", nuevoId);
                            command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                            command.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                            command.Parameters.AddWithValue("@Email", cliente.Email);
                            command.Parameters.AddWithValue("@Telefono", (object?)cliente.Telefono ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Direccion", (object?)cliente.Direccion ?? DBNull.Value);
                            command.Parameters.AddWithValue("@FechaNacimiento", (object?)cliente.FechaNacimiento ?? DBNull.Value);
                            command.Parameters.AddWithValue("@FechaRegistro", DateTime.Now);
                            command.Parameters.AddWithValue("@Estado", cliente.Estado);

                            if (connection.State != ConnectionState.Open)
                                await connection.OpenAsync();

                            await command.ExecuteNonQueryAsync();
                        }
                    }

                    TempData["Success"] = "Cliente creado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al crear el cliente: " + ex.Message;
                }
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

            Cliente? cliente = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SELECT * FROM Clientes WHERE ClienteID = @ClienteID", connection))
                {
                    command.Parameters.AddWithValue("@ClienteID", id);
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            cliente = new Cliente
                            {
                                ClienteID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                Email = reader.GetString(3),
                                Telefono = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Direccion = reader.IsDBNull(5) ? null : reader.GetString(5),
                                FechaNacimiento = reader.IsDBNull(6) ? null : reader.GetDateTime(6),
                                FechaRegistro = reader.IsDBNull(7) ? DateTime.Now : reader.GetDateTime(7),
                                Estado = reader.IsDBNull(8) ? "Activo" : reader.GetString(8)
                            };
                        }
                    }
                }
            }

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
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        using (var command = new SqlCommand("CP_ModificarClientes", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@ClienteID", cliente.ClienteID);
                            command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                            command.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                            command.Parameters.AddWithValue("@Email", cliente.Email);
                            command.Parameters.AddWithValue("@Telefono", (object?)cliente.Telefono ?? DBNull.Value);
                            command.Parameters.AddWithValue("@Direccion", (object?)cliente.Direccion ?? DBNull.Value);
                            command.Parameters.AddWithValue("@FechaNacimiento", (object?)cliente.FechaNacimiento ?? DBNull.Value);
                            command.Parameters.AddWithValue("@FechaRegistro", cliente.FechaRegistro);
                            command.Parameters.AddWithValue("@Estado", cliente.Estado);

                            await connection.OpenAsync();
                            await command.ExecuteNonQueryAsync();
                        }
                    }

                    TempData["Success"] = "Cliente actualizado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    if (!await ClienteExists(cliente.ClienteID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
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

            Cliente? cliente = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SELECT * FROM Clientes WHERE ClienteID = @ClienteID", connection))
                {
                    command.Parameters.AddWithValue("@ClienteID", id);
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            cliente = new Cliente
                            {
                                ClienteID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                Email = reader.GetString(3),
                                Telefono = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Direccion = reader.IsDBNull(5) ? null : reader.GetString(5),
                                FechaNacimiento = reader.IsDBNull(6) ? null : reader.GetDateTime(6),
                                FechaRegistro = reader.IsDBNull(7) ? DateTime.Now : reader.GetDateTime(7),
                                Estado = reader.IsDBNull(8) ? "Activo" : reader.GetString(8)
                            };
                        }
                    }
                }
            }

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
            try
            {
                // Verificar si tiene citas asociadas
                bool tieneCitas = false;
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("SELECT COUNT(*) FROM Citas WHERE ClienteID = @ClienteID", connection))
                    {
                        command.Parameters.AddWithValue("@ClienteID", id);
                        await connection.OpenAsync();
                        tieneCitas = (int)(await command.ExecuteScalarAsync())! > 0;
                    }
                }

                if (tieneCitas)
                {
                    TempData["Error"] = "No se puede eliminar el cliente porque tiene citas asociadas";
                    return RedirectToAction(nameof(Index));
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("CP_EliminarClientes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ClienteID", id);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }

                TempData["Success"] = "Cliente eliminado exitosamente";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al eliminar el cliente: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ClienteExists(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SELECT COUNT(*) FROM Clientes WHERE ClienteID = @ClienteID", connection))
                {
                    command.Parameters.AddWithValue("@ClienteID", id);
                    await connection.OpenAsync();
                    return (int)(await command.ExecuteScalarAsync())! > 0;
                }
            }
        }
    }
}
