using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
    [Table("GastosOperativos")]
    public class GastoOperativo
    {
        [Key]
        [Display(Name = "ID Gasto")]
        public int GastoID { get; set; }

        public int? ProveedorID { get; set; }

        [StringLength(100)]
        [Display(Name = "Tipo de Gasto")]
        public string? TipoGasto { get; set; }

        [StringLength(500)]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Monto")]
        public decimal? Monto { get; set; }

        [Display(Name = "Fecha del Gasto")]
        public DateTime? FechaGasto { get; set; }

        [StringLength(50)]
        [Display(Name = "Método de Pago")]
        public string? MetodoPago { get; set; }

        [StringLength(100)]
        [Display(Name = "Comprobante")]
        public string? Comprobante { get; set; }

        [StringLength(50)]
        [Display(Name = "Estado")]
        public string? Estado { get; set; }

        [Display(Name = "Fecha de Registro")]
        public DateTime? FechaRegistro { get; set; }

        [ForeignKey("ProveedorID")]
        public virtual Proveedor? Proveedor { get; set; }
    }
}
