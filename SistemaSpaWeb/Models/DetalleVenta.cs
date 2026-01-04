using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
    [Table("DetalleVentas")]
    public class DetalleVenta
    {
        [Key]
        public int DetalleVentaID { get; set; }
        public int? VentaID { get; set; }
        public int? ProductoID { get; set; }
        public int? Cantidad { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? PrecioUnitario { get; set; }

        [Column(TypeName = "decimal(21,2)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? Subtotal { get; set; }

        [ForeignKey("VentaID")]
        public virtual Venta? Venta { get; set; }

        [ForeignKey("ProductoID")]
        public virtual Producto? Producto { get; set; }
    }
}
