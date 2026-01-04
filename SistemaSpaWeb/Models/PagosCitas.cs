using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
    [Table("PagosCitas")]
    public class PagoCita
    {
        [Key]
        [Display(Name = "ID Pago")]
        public int PagoID { get; set; }

        [Display(Name = "Cita")]
        public int? CitaID { get; set; }

        [Display(Name = "Fecha de Pago")]
        public DateTime? FechaPago { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Monto")]
        public decimal? Monto { get; set; }

        [StringLength(50)]
        [Display(Name = "Método de Pago")]
        public string? MetodoPago { get; set; }

        [StringLength(100)]
        [Display(Name = "Referencia")]
        public string? Referencia { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado del Pago")]
        public string? EstadoPago { get; set; }

        [ForeignKey("CitaID")]
        public virtual Cita? Cita { get; set; }
    }
}
