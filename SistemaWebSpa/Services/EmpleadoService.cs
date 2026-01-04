using Dapper;
using SpaWebMVC.Models;
using System.Data;

namespace SpaWebMVC.Services
{
    public class EmpleadoService
    {
        private readonly DatabaseService _database;

        public EmpleadoService(DatabaseService database)
        {
            _database = database;
        }

        public async Task<List<Empleado>> ListarEmpleados()
        {
            using var connection = _database.CreateConnection();
            var empleados = await connection.QueryAsync<Empleado>(
                "CP_ListarEmpleados",
                commandType: CommandType.StoredProcedure
            );
            return empleados.ToList();
        }

        public async Task<List<Empleado>> ListarEmpleadosFiltro(string filtro)
        {
            using var connection = _database.CreateConnection();
            var empleados = await connection.QueryAsync<Empleado>(
                "CP_ListarEmpleadosFiltro",
                new { Valor = filtro },
                commandType: CommandType.StoredProcedure
            );
            return empleados.ToList();
        }

        public async Task<Empleado?> ObtenerEmpleadoPorId(int empleadoId)
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Empleado>(
                "SELECT * FROM Empleados WHERE EmpleadoID = @EmpleadoID",
                new { EmpleadoID = empleadoId }
            );
        }

        public async Task<bool> InsertarEmpleado(Empleado empleado)
        {
            try
            {
                using var connection = _database.CreateConnection();
                var maxId = await connection.QueryFirstOrDefaultAsync<int>(
                    "SELECT ISNULL(MAX(EmpleadoID), 0) + 1 FROM Empleados"
                );

                await connection.ExecuteAsync(
                    "CP_InsertarEmpleados",
                    new
                    {
                        EmpleadoID = maxId,
                        empleado.Nombre,
                        empleado.Apellido,
                        empleado.Email,
                        empleado.Telefono,
                        empleado.Cargo,
                        empleado.FechaContratacion,
                        empleado.Salario,
                        empleado.Estado
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

        public async Task<bool> ModificarEmpleado(Empleado empleado)
        {
            try
            {
                using var connection = _database.CreateConnection();
                await connection.ExecuteAsync(
                    "CP_ModificarEmpleados",
                    empleado,
                    commandType: CommandType.StoredProcedure
                );
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EliminarEmpleado(int empleadoId)
        {
            try
            {
                using var connection = _database.CreateConnection();
                await connection.ExecuteAsync(
                    "CP_EliminarEmpleados",
                    new { EmpleadoID = empleadoId },
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
