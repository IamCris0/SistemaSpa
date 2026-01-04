using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
    [Table("Ventas")]
    public class Venta
    {
        [Key]
        [Display(Name = "ID Venta")]
        public int VentaID { get; set; }

        public int? ClienteID { get; set; }
        public int? EmpleadoID { get; set; }

        [Display(Name = "Fecha de Venta")]
        public DateTime? FechaVenta { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        [Display(Name = "Total")]
        public decimal? Total { get; set; }

        [StringLength(50)]
        [Display(Name = "Método de Pago")]
        public string? MetodoPago { get; set; }

        [StringLength(50)]
        [Display(Name = "Estado")]
        public string? Estado { get; set; }

        [ForeignKey("ClienteID")]
        public virtual Cliente? Cliente { get; set; }

        [ForeignKey("EmpleadoID")]
        public virtual Empleado? Empleado { get; set; }

        public virtual ICollection<DetalleVenta>? DetalleVentas { get; set; }
    }
}
