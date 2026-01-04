using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class PagosCitas
    {
        private int pagoID;
        private int citaID;
        private DateTime fechaPago;
        private double monto;
        private string metodoPago;
        private string referencia;
        private string estadoPago;

        public PagosCitas()
        {
        }

        public PagosCitas(int pagoID, int citaID, DateTime fechaPago, double monto, string metodoPago, string referencia, string estadoPago)
        {
            this.PagoID = pagoID;
            this.CitaID = citaID;
            this.FechaPago = fechaPago;
            this.Monto = monto;
            this.MetodoPago = metodoPago;
            this.Referencia = referencia;
            this.EstadoPago = estadoPago;
        }

        public int PagoID { get => pagoID; set => pagoID = value; }
        public int CitaID { get => citaID; set => citaID = value; }
        public DateTime FechaPago { get => fechaPago; set => fechaPago = value; }
        public double Monto { get => monto; set => monto = value; }
        public string MetodoPago { get => metodoPago; set => metodoPago = value; }
        public string Referencia { get => referencia; set => referencia = value; }
        public string EstadoPago { get => estadoPago; set => estadoPago = value; }
    }
}
