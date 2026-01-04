using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
    [Table("DetalleCitas")]
    public class DetalleCita
    {
        [Key]
        public int DetalleCitaID { get; set; }

        public int? CitaID { get; set; }
        public int? ServicioID { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Precio del Servicio")]
        public decimal? PrecioServicio { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        [Display(Name = "Descuento (%)")]
        public decimal? Descuento { get; set; }

        [ForeignKey("CitaID")]
        public virtual Cita? Cita { get; set; }

        [ForeignKey("ServicioID")]
        public virtual Servicios? Servicio { get; set; }
    }
}
