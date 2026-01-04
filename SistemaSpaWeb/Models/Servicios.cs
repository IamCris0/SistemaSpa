using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
    [Table("Servicios")]
    public class Servicios
    {
        [Key]
        [Display(Name = "ID Servicio")]
        public int ServicioID { get; set; }

        [Display(Name = "Categoría")]
        public int? CategoriaID { get; set; }

        [Required(ErrorMessage = "El nombre del servicio es requerido")]
        [StringLength(150)]
        [Display(Name = "Nombre Servicio")]
        public string NombreServicio { get; set; } = string.Empty;

        [StringLength(500)]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Required]
        [Display(Name = "Duración (minutos)")]
        public int Duracion { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Precio")]
        [Range(0.01, 10000, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [StringLength(50)]
        [Display(Name = "Estado")]
        public string? Estado { get; set; }

        [ForeignKey("CategoriaID")]
        public virtual CategoriasServicios? Categoria { get; set; }
        public virtual ICollection<DetalleCita>? DetalleCitas { get; set; }
    }
}
