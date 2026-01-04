using System.ComponentModel.DataAnnotations;

namespace SpaWebMVC.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder 50 caracteres")]
        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(150, ErrorMessage = "El email no puede exceder 150 caracteres")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre completo es requerido")]
        [StringLength(200, ErrorMessage = "El nombre completo no puede exceder 200 caracteres")]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Display(Name = "Rol")]
        public string Rol { get; set; } = "Usuario";

        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Activo";

        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Último Acceso")]
        public DateTime? UltimoAcceso { get; set; }
    }
}
