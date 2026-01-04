using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class Compras
    {
        private int compraID;
        private int proveedorID;
        private DateTime fechaCompra;
        private decimal total;
        private string estadoCompra;
        private string observaciones;

        public Compras()
        {
        }

        public Compras(int compraID, int proveedorID, DateTime fechaCompra,
                      decimal total, string estadoCompra, string observaciones)
        {
            this.CompraID = compraID;
            this.ProveedorID = proveedorID;
            this.FechaCompra = fechaCompra;
            this.Total = total;
            this.EstadoCompra = estadoCompra;
            this.Observaciones = observaciones;
        }

        public int CompraID { get => compraID; set => compraID = value; }
        public int ProveedorID { get => proveedorID; set => proveedorID = value; }
        public DateTime FechaCompra { get => fechaCompra; set => fechaCompra = value; }
        public decimal Total { get => total; set => total = value; }
        public string EstadoCompra { get => estadoCompra; set => estadoCompra = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
    }
}
