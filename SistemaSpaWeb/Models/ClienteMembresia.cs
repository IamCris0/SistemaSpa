using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
    [Table("ClientesMembresias")]
    public class ClienteMembresia
    {
        [Key]
        public int ClienteMembresiaID { get; set; }
        public int? ClienteID { get; set; }
        public int? MembresiaID { get; set; }

        [Display(Name = "Fecha de Inicio")]
        public DateTime? FechaInicio { get; set; }

        [Display(Name = "Fecha de Fin")]
        public DateTime? FechaFin { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado de la Membresía")]
        public string? EstadoMembresia { get; set; }

        [Display(Name = "Fecha de Registro")]
        public DateTime? FechaRegistro { get; set; }

        [ForeignKey("ClienteID")]
        public virtual Cliente? Cliente { get; set; }

        [ForeignKey("MembresiaID")]
        public virtual Membresia? Membresia { get; set; }
    }
}

