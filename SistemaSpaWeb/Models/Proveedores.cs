using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSpaWeb.Models
{
    [Table("Proveedores")]
    public class Proveedor
    {
        [Key]
        [Display(Name = "ID Proveedor")]
        public int ProveedorID { get; set; }

        [StringLength(150)]
        [Display(Name = "Nombre del Proveedor")]
        public string? NombreProveedor { get; set; }

        [StringLength(100)]
        [Display(Name = "Contacto")]
        public string? Contacto { get; set; }

        [StringLength(20)]
        [Display(Name = "Teléfono")]
        public string? Telefono { get; set; }

        [StringLength(150)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [StringLength(200)]
        [Display(Name = "Dirección")]
        public string? Direccion { get; set; }

        [StringLength(50)]
        [Display(Name = "Estado")]
        public string? Estado { get; set; }

        public virtual ICollection<Compra>? Compras { get; set; }
        public virtual ICollection<GastoOperativo>? GastosOperativos { get; set; }
    }
}
