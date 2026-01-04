using Microsoft.EntityFrameworkCore;

namespace SistemaSpaWeb.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets - Tablas de la base de datos
        public DbSet<CategoriasServicios> CategoriasServicios { get; set; }
        public DbSet<Servicios> Servicios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<DetalleCita> DetalleCitas { get; set; }
        public DbSet<PagoCita> PagosCitas { get; set; }
        public DbSet<HistorialCliente> HistorialClientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalleCompra> DetalleCompras { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }
        public DbSet<Membresia> Membresias { get; set; }
        public DbSet<ClienteMembresia> ClientesMembresias { get; set; }
        public DbSet<GastoOperativo> GastosOperativos { get; set; }
        public DbSet<TurnoEmpleado> TurnosEmpleados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de relaciones
            modelBuilder.Entity<CategoriasServicios>()
                .HasMany(c => c.Servicios)
                .WithOne(s => s.Categoria)
                .HasForeignKey(s => s.CategoriaID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Citas)
                .WithOne(ci => ci.Cliente)
                .HasForeignKey(ci => ci.ClienteID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Ventas)
                .WithOne(v => v.Cliente)
                .HasForeignKey(v => v.ClienteID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.ClientesMembresias)
                .WithOne(cm => cm.Cliente)
                .HasForeignKey(cm => cm.ClienteID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Empleado>()
                .HasMany(e => e.Citas)
                .WithOne(c => c.Empleado)
                .HasForeignKey(c => c.EmpleadoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Empleado>()
                .HasMany(e => e.Ventas)
                .WithOne(v => v.Empleado)
                .HasForeignKey(v => v.EmpleadoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sala>()
                .HasMany(s => s.Citas)
                .WithOne(c => c.Sala)
                .HasForeignKey(c => c.SalaID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cita>()
                .HasMany(c => c.DetalleCitas)
                .WithOne(d => d.Cita)
                .HasForeignKey(d => d.CitaID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cita>()
                .HasMany(c => c.PagosCitas)
                .WithOne(p => p.Cita)
                .HasForeignKey(p => p.CitaID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Servicios>()
                .HasMany(s => s.DetalleCitas)
                .WithOne(d => d.Servicio)
                .HasForeignKey(d => d.ServicioID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Proveedor>()
                .HasMany(p => p.Compras)
                .WithOne(c => c.Proveedor)
                .HasForeignKey(c => c.ProveedorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Proveedor>()
                .HasMany(p => p.GastosOperativos)
                .WithOne(g => g.Proveedor)
                .HasForeignKey(g => g.ProveedorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Compra>()
                .HasMany(c => c.DetalleCompras)
                .WithOne(d => d.Compra)
                .HasForeignKey(d => d.CompraID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Venta>()
                .HasMany(v => v.DetalleVentas)
                .WithOne(d => d.Venta)
                .HasForeignKey(d => d.VentaID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Producto>()
                .HasMany(p => p.DetalleCompras)
                .WithOne(d => d.Producto)
                .HasForeignKey(d => d.ProductoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Producto>()
                .HasMany(p => p.DetalleVentas)
                .WithOne(d => d.Producto)
                .HasForeignKey(d => d.ProductoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Membresia>()
                .HasMany(m => m.ClientesMembresias)
                .WithOne(cm => cm.Membresia)
                .HasForeignKey(cm => cm.MembresiaID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Empleado>()
                .HasMany(e => e.TurnosEmpleados)
                .WithOne(t => t.Empleado)
                .HasForeignKey(t => t.EmpleadoID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de columnas calculadas
            modelBuilder.Entity<DetalleCompra>()
                .Property(d => d.Subtotal)
                .HasComputedColumnSql("[Cantidad] * [PrecioUnitario]");

            modelBuilder.Entity<DetalleVenta>()
                .Property(d => d.Subtotal)
                .HasComputedColumnSql("[Cantidad] * [PrecioUnitario]");
        }
    }
}