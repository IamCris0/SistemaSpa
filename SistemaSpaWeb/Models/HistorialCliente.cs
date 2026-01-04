using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
    [Table("HistorialClientes")]
    public class HistorialCliente
    {
        [Key]
        [Display(Name = "ID Historial")]
        public int HistorialID { get; set; }

        public int? ClienteID { get; set; }
        public int? CitaID { get; set; }

        [Display(Name = "Fecha de Visita")]
        public DateTime? FechaVisita { get; set; }

        [StringLength(1000)]
        [Display(Name = "Observaciones")]
        public string? Observaciones { get; set; }

        [Display(Name = "Calificación")]
        [Range(1, 5)]
        public int? Calificacion { get; set; }

        [StringLength(500)]
        [Display(Name = "Alergias/Procedimiento")]
        public string? AlergiasProcedimiento { get; set; }

        [StringLength(1000)]
        [Display(Name = "Resultados del Tratamiento")]
        public string? ResultadosTratamiento { get; set; }

        [Display(Name = "Fecha de Registro")]
        public DateTime? FechaRegistro { get; set; }

        [ForeignKey("ClienteID")]
        public virtual Cliente? Cliente { get; set; }

        [ForeignKey("CitaID")]
        public virtual Cita? Cita { get; set; }
    }
}
