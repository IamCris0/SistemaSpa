using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
    [Table("Empleados")]
    public class Empleado
    {
        [Key]
        [Display(Name = "ID Empleado")]
        public int EmpleadoID { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(100)]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es requerido")]
        [StringLength(150)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        [Display(Name = "Teléfono")]
        public string? Telefono { get; set; }

        [StringLength(50)]
        [Display(Name = "Cargo")]
        public string? Cargo { get; set; }

        [Required]
        [Display(Name = "Fecha de Contratación")]
        [DataType(DataType.Date)]
        public DateTime FechaContratacion { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Salario")]
        public decimal? Salario { get; set; }

        [StringLength(50)]
        [Display(Name = "Estado")]
        public string? Estado { get; set; }

        public virtual ICollection<Cita>? Citas { get; set; }
        public virtual ICollection<Venta>? Ventas { get; set; }
        public virtual ICollection<TurnoEmpleado>? TurnosEmpleados { get; set; }
    }

}
