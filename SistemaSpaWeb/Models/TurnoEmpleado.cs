using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
    [Table("TurnosEmpleados")]
    public class TurnoEmpleado
    {
        [Key]
        [Display(Name = "ID Turno")]
        public int TurnoID { get; set; }

        public int? EmpleadoID { get; set; }

        [StringLength(20)]
        [Display(Name = "Día de la Semana")]
        public string? DiaSemana { get; set; }

        [Display(Name = "Hora de Inicio")]
        [DataType(DataType.Time)]
        public TimeSpan? HoraInicio { get; set; }

        [Display(Name = "Hora de Fin")]
        [DataType(DataType.Time)]
        public TimeSpan? HoraFin { get; set; }

        [StringLength(50)]
        [Display(Name = "Tipo de Turno")]
        public string? TipoTurno { get; set; }

        [StringLength(50)]
        [Display(Name = "Estado")]
        public string? Estado { get; set; }

        [Display(Name = "Fecha de Registro")]
        public DateTime? FechaRegistro { get; set; }

        [ForeignKey("EmpleadoID")]
        public virtual Empleado? Empleado { get; set; }
    }
}
