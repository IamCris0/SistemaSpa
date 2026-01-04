using System.ComponentModel.DataAnnotations;

namespace SpaWebMVC.Models
{
    public class Producto
    {
        public int ProductoID { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido")]
        [StringLength(150)]
        [Display(Name = "Nombre del Producto")]
        public string NombreProducto { get; set; } = string.Empty;

        [StringLength(500)]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [StringLength(100)]
        [Display(Name = "Marca")]
        public string? Marca { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, 99999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio Unitario")]
        public decimal PrecioUnitario { get; set; }

        [Range(0, 99999, ErrorMessage = "El stock no puede ser negativo")]
        [Display(Name = "Stock Actual")]
        public int Stock { get; set; }

        [Range(0, 9999, ErrorMessage = "El stock mínimo no puede ser negativo")]
        [Display(Name = "Stock Mínimo")]
        public int StockMinimo { get; set; }

        [StringLength(50)]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Disponible";

        // Propiedad calculada
        [Display(Name = "Stock Bajo")]
        public bool StockBajo => Stock <= StockMinimo;
    }
}
