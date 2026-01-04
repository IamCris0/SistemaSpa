using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
    [Table("Productos")]
    public class Producto
    {
        [Key]
        [Display(Name = "ID Producto")]
        public int ProductoID { get; set; }

        [StringLength(150)]
        [Display(Name = "Nombre del Producto")]
        public string? NombreProducto { get; set; }

        [StringLength(500)]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [StringLength(100)]
        [Display(Name = "Marca")]
        public string? Marca { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Precio Unitario")]
        public decimal? PrecioUnitario { get; set; }

        [Display(Name = "Stock")]
        public int? Stock { get; set; }

        [Display(Name = "Stock Mínimo")]
        public int? StockMinimo { get; set; }

        [StringLength(50)]
        [Display(Name = "Estado")]
        public string? Estado { get; set; }

        public virtual ICollection<DetalleCompra>? DetalleCompras { get; set; }
        public virtual ICollection<DetalleVenta>? DetalleVentas { get; set; }
    }
}
