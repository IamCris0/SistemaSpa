using System;

namespace Entidades.Administracion
{
    public class DetalleCompras
    {
        public int DetalleCompraID { get; set; }
        public int CompraID { get; set; }
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        // Propiedad calculada: subtotal
        public decimal Subtotal
        {
            get { return Cantidad * PrecioUnitario; }
        }

        // Constructor vacío
        public DetalleCompras()
        {
        }

        // Constructor con parámetros
        public DetalleCompras(int detalleCompraID, int compraID, int productoID,
                              int cantidad, decimal precioUnitario)
        {
            DetalleCompraID = detalleCompraID;
            CompraID = compraID;
            ProductoID = productoID;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }
    }
}

