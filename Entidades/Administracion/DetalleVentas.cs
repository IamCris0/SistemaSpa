using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class DetalleVentas
    {
        public int DetalleVentaID { get; set; }
        public int VentaID { get; set; }
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }


        public decimal Subtotal
        {
            get { return Cantidad * PrecioUnitario; }
        }

        public DetalleVentas()
        {
        }

        public DetalleVentas(int detalleVentaID, int ventaID, int productoID,
                             int cantidad, decimal precioUnitario)
        {
            DetalleVentaID = detalleVentaID;
            VentaID = ventaID;
            ProductoID = productoID;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }
    }
}
