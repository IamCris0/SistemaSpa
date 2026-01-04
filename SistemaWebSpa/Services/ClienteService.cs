using Dapper;
using SpaWebMVC.Models;
using System.Data;

namespace SpaWebMVC.Services
{
    public class ClienteService
    {
        private readonly DatabaseService _database;

        public ClienteService(DatabaseService database)
        {
            _database = database;
        }

        // Listar todos los clientes
        public async Task<List<Cliente>> ListarClientes()
        {
            using var connection = _database.CreateConnection();
            var clientes = await connection.QueryAsync<Cliente>(
                "CP_ListarClientes",
                commandType: CommandType.StoredProcedure
            );
            return clientes.ToList();
        }

        // Listar clientes con filtro
        public async Task<List<Cliente>> ListarClientesFiltro(string filtro)
        {
            using var connection = _database.CreateConnection();
            var clientes = await connection.QueryAsync<Cliente>(
                "CP_ListarClientesFiltro",
                new { Valor = filtro },
                commandType: CommandType.StoredProcedure
            );
            return clientes.ToList();
        }

        // Obtener cliente por ID
        public async Task<Cliente?> ObtenerClientePorId(int clienteId)
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Cliente>(
                "SELECT * FROM Clientes WHERE ClienteID = @ClienteID",
                new { ClienteID = clienteId }
            );
        }

        // Insertar cliente
        public async Task<bool> InsertarCliente(Cliente cliente)
        {
            try
            {
                using var connection = _database.CreateConnection();
                
                // Obtener el siguiente ID
                var maxId = await connection.QueryFirstOrDefaultAsync<int>(
                    "SELECT ISNULL(MAX(ClienteID), 0) + 1 FROM Clientes"
                );

                await connection.ExecuteAsync(
                    "CP_InsertarClientes",
                    new
                    {
                        ClienteID = maxId,
                        cliente.Nombre,
                        cliente.Apellido,
                        cliente.Email,
                        cliente.Telefono,
                        cliente.Direccion,
                        cliente.FechaNacimiento,
                        FechaRegistro = DateTime.Now,
                        cliente.Estado
                    },
                    commandType: CommandType.StoredProcedure
                );
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Modificar cliente
        public async Task<bool> ModificarCliente(Cliente cliente)
        {
            try
            {
                using var connection = _database.CreateConnection();
                await connection.ExecuteAsync(
                    "CP_ModificarClientes",
                    new
                    {
                        cliente.ClienteID,
                        cliente.Nombre,
                        cliente.Apellido,
                        cliente.Email,
                        cliente.Telefono,
                        cliente.Direccion,
                        cliente.FechaNacimiento,
                        cliente.FechaRegistro,
                        cliente.Estado
                    },
                    commandType: CommandType.StoredProcedure
                );
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Eliminar cliente
        public async Task<bool> EliminarCliente(int clienteId)
        {
            try
            {
                using var connection = _database.CreateConnection();
                await connection.ExecuteAsync(
                    "CP_EliminarClientes",
                    new { ClienteID = clienteId },
                    commandType: CommandType.StoredProcedure
                );
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
