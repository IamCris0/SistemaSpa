using Microsoft.Data.SqlClient;
using System.Data;

namespace SpaWebMVC.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SpaConnection") 
                ?? throw new InvalidOperationException("Connection string 'SpaConnection' no encontrada");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
