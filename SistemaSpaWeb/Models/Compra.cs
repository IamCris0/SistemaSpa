using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
    [Table("Compras")]
    public class Compra
    {
        [Key]
        [Display(Name = "ID Compra")]
        public int CompraID { get; set; }

        [Display(Name = "Proveedor")]
        public int? ProveedorID { get; set; }

        [Display(Name = "Fecha de Compra")]
        public DateTime? FechaCompra { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        [Display(Name = "Total")]
        public decimal? Total { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado de la Compra")]
        public string? EstadoCompra { get; set; }

        [StringLength(500)]
        [Display(Name = "Observaciones")]
        public string? Observaciones { get; set; }

        [ForeignKey("ProveedorID")]
        public virtual Proveedor? Proveedor { get; set; }
        public virtual ICollection<DetalleCompra>? DetalleCompras { get; set; }
    }
}
