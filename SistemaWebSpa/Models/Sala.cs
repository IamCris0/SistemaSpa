using System.ComponentModel.DataAnnotations;

namespace SpaWebMVC.Models
{
    public class Sala
    {
        public int SalaID { get; set; }

        [Required(ErrorMessage = "El nombre de la sala es requerido")]
        [StringLength(100)]
        [Display(Name = "Nombre de la Sala")]
        public string NombreSala { get; set; } = string.Empty;

        [StringLength(50)]
        [Display(Name = "Tipo de Sala")]
        public string? TipoSala { get; set; }

        [Range(1, 50, ErrorMessage = "La capacidad debe estar entre 1 y 50")]
        [Display(Name = "Capacidad")]
        public int? Capacidad { get; set; }

        [StringLength(50)]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Disponible";
    }
}
