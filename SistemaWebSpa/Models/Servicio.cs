using System.ComponentModel.DataAnnotations;

namespace SpaWebMVC.Models
{
    public class Servicio
    {
        public int ServicioID { get; set; }

        [Display(Name = "Categoría")]
        public int? CategoriaID { get; set; }

        [Required(ErrorMessage = "El nombre del servicio es requerido")]
        [StringLength(150)]
        [Display(Name = "Nombre del Servicio")]
        public string NombreServicio { get; set; } = string.Empty;

        [StringLength(500)]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La duración es requerida")]
        [Range(1, 480, ErrorMessage = "La duración debe estar entre 1 y 480 minutos")]
        [Display(Name = "Duración (minutos)")]
        public int Duracion { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, 9999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [StringLength(50)]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Activo";

        // Propiedad de navegación
        [Display(Name = "Categoría")]
        public string? NombreCategoria { get; set; }
    }
}
