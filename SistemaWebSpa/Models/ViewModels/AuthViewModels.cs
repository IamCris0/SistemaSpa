using System.ComponentModel.DataAnnotations;

namespace SpaWebMVC.Models.ViewModels
{
    // ViewModel para Login
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El usuario o email es requerido")]
        [Display(Name = "Usuario o Email")]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Recordarme")]
        public bool Recordarme { get; set; }
    }

    // ViewModel para Registro
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 50 caracteres")]
        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre completo es requerido")]
        [StringLength(200, ErrorMessage = "El nombre completo no puede exceder 200 caracteres")]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe confirmar la contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmarPassword { get; set; } = string.Empty;
    }

    // ViewModel para Cambiar Contraseña
    public class CambiarPasswordViewModel
    {
        [Required(ErrorMessage = "La contraseña actual es requerida")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña Actual")]
        public string PasswordActual { get; set; } = string.Empty;

        [Required(ErrorMessage = "La nueva contraseña es requerida")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string NuevaPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe confirmar la nueva contraseña")]
        [DataType(DataType.Password)]
        [Compare("NuevaPassword", ErrorMessage = "Las contraseñas no coinciden")]
        [Display(Name = "Confirmar Nueva Contraseña")]
        public string ConfirmarPassword { get; set; } = string.Empty;
    }
}
