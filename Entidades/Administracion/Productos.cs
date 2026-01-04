using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class Productos
    {
        private int productoID;
        private string nombreProducto;
        private string descripcion;
        private string marca;
        private double precioUnitario;
        private int stock;
        private int stockMinimo;
        private string estado;

        public Productos()
        {
        }

        public Productos(int productoID, string nombreProducto, string descripcion, string marca, double precioUnitario, int stock, int stockMinimo, string estado)
        {
            this.ProductoID = productoID;
            this.NombreProducto = nombreProducto;
            this.Descripcion = descripcion;
            this.Marca = marca;
            this.PrecioUnitario = precioUnitario;
            this.Stock = stock;
            this.StockMinimo = stockMinimo;
            this.Estado = estado;
        }

        public int ProductoID { get => productoID; set => productoID = value; }
        public string NombreProducto { get => nombreProducto; set => nombreProducto = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Marca { get => marca; set => marca = value; }
        public double PrecioUnitario { get => precioUnitario; set => precioUnitario = value; }
        public int Stock { get => stock; set => stock = value; }
        public int StockMinimo { get => stockMinimo; set => stockMinimo = value; }
        public string Estado { get => estado; set => estado = value; }
    }
}
