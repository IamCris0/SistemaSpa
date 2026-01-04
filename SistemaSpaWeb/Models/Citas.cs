using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
    [Table("Citas")]
    public class Cita
    {
        [Key]
        [Display(Name = "ID Cita")]
        public int CitaID { get; set; }

        [Display(Name = "Cliente")]
        public int? ClienteID { get; set; }

        [Display(Name = "Empleado")]
        public int? EmpleadoID { get; set; }

        [Display(Name = "Sala")]
        public int? SalaID { get; set; }

        [Required]
        [Display(Name = "Fecha de Cita")]
        [DataType(DataType.Date)]
        public DateTime FechaCita { get; set; }

        [Required]
        [Display(Name = "Hora de Inicio")]
        [DataType(DataType.Time)]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        [Display(Name = "Hora de Fin")]
        [DataType(DataType.Time)]
        public TimeSpan HoraFin { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado de la Cita")]
        public string? EstadoCita { get; set; }

        [StringLength(500)]
        [Display(Name = "Observaciones")]
        public string? Observaciones { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime? FechaCreacion { get; set; }

        [ForeignKey("ClienteID")]
        public virtual Cliente? Cliente { get; set; }

        [ForeignKey("EmpleadoID")]
        public virtual Empleado? Empleado { get; set; }

        [ForeignKey("SalaID")]
        public virtual Sala? Sala { get; set; }

        public virtual ICollection<DetalleCita>? DetalleCitas { get; set; }
        public virtual ICollection<PagoCita>? PagosCitas { get; set; }
        public virtual ICollection<HistorialCliente>? HistorialClientes { get; set; }
    }
}
