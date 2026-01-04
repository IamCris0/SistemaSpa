using System.ComponentModel.DataAnnotations;

namespace SpaWebMVC.Models
{
    public class CategoriaServicio
    {
        public int CategoriaID { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        [Display(Name = "Nombre de Categoría")]
        public string NombreCategoria { get; set; } = string.Empty;

        [StringLength(300)]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [StringLength(50)]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Activo";
    }
}
