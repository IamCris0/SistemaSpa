using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Vistas
{
    public class PagoCitas_Citas_Cliente
    {
        public PagoCitas_Citas_Cliente(int pagoID,int citaID, string cliente, DateTime fechaPago, decimal monto, string metodoPago, string referencia, string estadoPago)
        {
            PagoID = pagoID;
            CitaID = citaID;
            Cliente = cliente;
            FechaPago = fechaPago;
            Monto = monto;
            MetodoPago = metodoPago;
            Referencia = referencia;
            EstadoPago = estadoPago;
        }

        public int PagoID { get; set; }
        public int CitaID { get; set; }
        public string Cliente { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }
        public string Referencia {  get; set; }
        public string EstadoPago { get; set; }
    }
}
