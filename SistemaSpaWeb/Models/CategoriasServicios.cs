using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
   
        [Table("CategoriasServicios")]
        public class CategoriasServicios
        {
            [Key]
            [Display(Name = "ID Categoría")]
            public int CategoriaID { get; set; }

            [Required(ErrorMessage = "El nombre de la categoría es requerido")]
            [StringLength(100)]
            [Display(Name = "Nombre Categoría")]
            public string NombreCategoria { get; set; } = string.Empty;

            [StringLength(300)]
            [Display(Name = "Descripción")]
            public string? Descripcion { get; set; }

            [StringLength(50)]
            [Display(Name = "Estado")]
            public string? Estado { get; set; }

            public virtual ICollection<Servicios>? Servicios { get; set; }
        }
    }