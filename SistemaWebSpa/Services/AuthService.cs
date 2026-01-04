using Dapper;
using SpaWebMVC.Models;
using BCrypt.Net;

namespace SpaWebMVC.Services
{
    public class AuthService
    {
        private readonly DatabaseService _database;

        public AuthService(DatabaseService database)
        {
            _database = database;
        }

        // Autenticar usuario
        public async Task<Usuario?> AutenticarUsuario(string nombreUsuario, string password)
        {
            try
            {
                using var connection = _database.CreateConnection();
                
                var usuario = await connection.QueryFirstOrDefaultAsync<Usuario>(
                    "CP_AutenticarUsuario",
                    new { NombreUsuario = nombreUsuario },
                    commandType: System.Data.CommandType.StoredProcedure
                );

                if (usuario == null)
                    return null;

                // Verificar contraseña
                if (!BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash))
                    return null;

                // Verificar que el usuario esté activo
                if (usuario.Estado != "Activo")
                    return null;

                // Actualizar último acceso
                await ActualizarUltimoAcceso(usuario.UsuarioID);

                return usuario;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Registrar nuevo usuario
        public async Task<(bool exito, string mensaje)> RegistrarUsuario(string nombreUsuario, string email, 
            string password, string nombreCompleto, string rol = "Usuario")
        {
            try
            {
                using var connection = _database.CreateConnection();

                // Verificar si ya existe el usuario
                var existeUsuario = await connection.QueryFirstOrDefaultAsync<int>(
                    "SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario = @NombreUsuario OR Email = @Email",
                    new { NombreUsuario = nombreUsuario, Email = email }
                );

                if (existeUsuario > 0)
                    return (false, "El nombre de usuario o email ya está registrado");

                // Hashear la contraseña
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

                // Insertar usuario
                await connection.ExecuteAsync(
                    "CP_InsertarUsuario",
                    new
                    {
                        NombreUsuario = nombreUsuario,
                        Email = email,
                        PasswordHash = passwordHash,
                        NombreCompleto = nombreCompleto,
                        Rol = rol
                    },
                    commandType: System.Data.CommandType.StoredProcedure
                );

                return (true, "Usuario registrado exitosamente");
            }
            catch (Exception ex)
            {
                return (false, $"Error al registrar usuario: {ex.Message}");
            }
        }

        // Actualizar último acceso
        private async Task ActualizarUltimoAcceso(int usuarioID)
        {
            try
            {
                using var connection = _database.CreateConnection();
                await connection.ExecuteAsync(
                    "CP_ActualizarUltimoAcceso",
                    new { UsuarioID = usuarioID },
                    commandType: System.Data.CommandType.StoredProcedure
                );
            }
            catch { }
        }

        // Cambiar contraseña
        public async Task<(bool exito, string mensaje)> CambiarPassword(int usuarioID, 
            string passwordActual, string nuevaPassword)
        {
            try
            {
                using var connection = _database.CreateConnection();

                // Obtener usuario actual
                var usuario = await connection.QueryFirstOrDefaultAsync<Usuario>(
                    "SELECT * FROM Usuarios WHERE UsuarioID = @UsuarioID",
                    new { UsuarioID = usuarioID }
                );

                if (usuario == null)
                    return (false, "Usuario no encontrado");

                // Verificar contraseña actual
                if (!BCrypt.Net.BCrypt.Verify(passwordActual, usuario.PasswordHash))
                    return (false, "La contraseña actual es incorrecta");

                // Hashear nueva contraseña
                string nuevoHash = BCrypt.Net.BCrypt.HashPassword(nuevaPassword);

                // Actualizar contraseña
                await connection.ExecuteAsync(
                    "CP_CambiarPassword",
                    new { UsuarioID = usuarioID, NuevoPasswordHash = nuevoHash },
                    commandType: System.Data.CommandType.StoredProcedure
                );

                return (true, "Contraseña cambiada exitosamente");
            }
            catch (Exception ex)
            {
                return (false, $"Error al cambiar contraseña: {ex.Message}");
            }
        }

        // Obtener usuario por ID
        public async Task<Usuario?> ObtenerUsuarioPorId(int usuarioID)
        {
            try
            {
                using var connection = _database.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<Usuario>(
                    "SELECT * FROM Usuarios WHERE UsuarioID = @UsuarioID",
                    new { UsuarioID = usuarioID }
                );
            }
            catch
            {
                return null;
            }
        }
    }
}
