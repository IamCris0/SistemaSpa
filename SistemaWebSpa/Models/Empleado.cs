using System.ComponentModel.DataAnnotations;

namespace SpaWebMVC.Models
{
    public class Empleado
    {
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
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(150)]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        [Display(Name = "Teléfono")]
        public string? Telefono { get; set; }

        [StringLength(50)]
        [Display(Name = "Cargo")]
        public string? Cargo { get; set; }

        [Required(ErrorMessage = "La fecha de contratación es requerida")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Contratación")]
        public DateTime FechaContratacion { get; set; }

        [Range(0, 999999.99)]
        [DataType(DataType.Currency)]
        [Display(Name = "Salario")]
        public decimal? Salario { get; set; }

        [StringLength(50)]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Activo";

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}
