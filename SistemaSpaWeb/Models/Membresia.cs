using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
    [Table("Membresias")]
    public class Membresia
    {
        [Key]
        [Display(Name = "ID Membresía")]
        public int MembresiaID { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre de la Membresía")]
        public string? NombreMembresia { get; set; }

        [StringLength(500)]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Display(Name = "Duración (meses)")]
        public int? DuracionMeses { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Precio")]
        public decimal? Precio { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        [Display(Name = "Descuento (%)")]
        public decimal? Descuento { get; set; }

        [StringLength(50)]
        [Display(Name = "Estado")]
        public string? Estado { get; set; }

        public virtual ICollection<ClienteMembresia>? ClientesMembresias { get; set; }
        
    }
}
