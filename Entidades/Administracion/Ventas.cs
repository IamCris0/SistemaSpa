using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class Ventas
    {
        private int ventaID;
        private int clienteID;
        private int empleadoID;
        private DateTime fechaVenta;
        private double total;
        private string metodoPago;
        private string estado;

        public Ventas()
        {
        }

        public Ventas(int ventaID, int clienteID, int empleadoID, DateTime fechaVenta, double total, string metodoPago, string estado)
        {
            this.VentaID = ventaID;
            this.ClienteID = clienteID;
            this.EmpleadoID = empleadoID;
            this.FechaVenta = fechaVenta;
            this.Total = total;
            this.MetodoPago = metodoPago;
            this.Estado = estado;
        }

        public int VentaID { get => ventaID; set => ventaID = value; }
        public int ClienteID { get => clienteID; set => clienteID = value; }
        public int EmpleadoID { get => empleadoID; set => empleadoID = value; }
        public DateTime FechaVenta { get => fechaVenta; set => fechaVenta = value; }
        public double Total { get => total; set => total = value; }
        public string MetodoPago { get => metodoPago; set => metodoPago = value; }
        public string Estado { get => estado; set => estado = value; }
    }
}
