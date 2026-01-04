using Microsoft.EntityFrameworkCore;
using SpaWebMVC.Models;

namespace SpaWebMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<CategoriaServicio> CategoriasServicios { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}
