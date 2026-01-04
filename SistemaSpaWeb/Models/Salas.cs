using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
    [Table("Salas")]
    public class Sala
    {
        [Key]
        [Display(Name = "ID Sala")]
        public int SalaID { get; set; }

        [Required(ErrorMessage = "El nombre de la sala es requerido")]
        [StringLength(100)]
        [Display(Name = "Nombre Sala")]
        public string NombreSala { get; set; } = string.Empty;

        [StringLength(50)]
        [Display(Name = "Tipo de Sala")]
        public string? TipoSala { get; set; }

        [Display(Name = "Capacidad")]
        public int? Capacidad { get; set; }

        [StringLength(50)]
        [Display(Name = "Estado")]
        public string? Estado { get; set; }

        public virtual ICollection<Cita>? Citas { get; set; }
    }
}
