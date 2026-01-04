using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
    [Table("DetalleCompras")]
    public class DetalleCompra
    {
        [Key]
        public int DetalleCompraID { get; set; }
        public int? CompraID { get; set; }
        public int? ProductoID { get; set; }
        public int? Cantidad { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? PrecioUnitario { get; set; }

        [Column(TypeName = "decimal(21,2)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? Subtotal { get; set; }

        [ForeignKey("CompraID")]
        public virtual Compra? Compra { get; set; }

        [ForeignKey("ProductoID")]
        public virtual Producto? Producto { get; set; }
    }
}
