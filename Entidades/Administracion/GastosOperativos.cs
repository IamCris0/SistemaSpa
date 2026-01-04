using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class GastosOperativos
    {
        private int gastoID;
        private int proveedorID;
        private string tipoGasto;
        private string descripcion;
        private decimal monto;
        private DateTime fechaGasto;
        private string metodoPago;
        private string comprobante;
        private string estado;
        private DateTime fechaRegistro;
        

        public GastosOperativos()
        {
        }

        public GastosOperativos(int gastoID, int proveedorID,string tipoGasto, string descripcion,
                              decimal monto, DateTime fechaGasto, string metodoPago,
                              string comprobante,
                              string estado, DateTime fechaRegistro)
        {
            this.GastoID = gastoID;
            this.ProveedorID = proveedorID;
            this.TipoGasto = tipoGasto;
            this.Descripcion = descripcion;
            this.Monto = monto;
            this.FechaGasto = fechaGasto;
            this.MetodoPago = metodoPago;

            this.Comprobante = comprobante;
            this.Estado = estado;
            this.FechaRegistro = fechaRegistro;
            
        }

        public int GastoID { get => gastoID; set => gastoID = value; }
        public int ProveedorID { get => proveedorID; set => proveedorID = value; }
        public string TipoGasto { get => tipoGasto; set => tipoGasto = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public decimal Monto { get => monto; set => monto = value; }
        public DateTime FechaGasto { get => fechaGasto; set => fechaGasto = value; }
        public string MetodoPago { get => metodoPago; set => metodoPago = value; }
        
        public string Comprobante { get => comprobante; set => comprobante = value; }
        public string Estado { get => estado; set => estado = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
    }
}
