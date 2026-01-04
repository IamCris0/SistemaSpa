using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class Membresias
    {
        private int membresiaID;
        private string nombreMenbresia;
        private string descripcion;
        private int duracionMeses;
        private double precio;
        private double descuento;
        private string estado;

        public Membresias()
        {
        }

        public Membresias(int membresiaID, string nombreMenbresia, string descripcion, int duracionMeses, double precio, double descuento, string estado)
        {
            this.MembresiaID = membresiaID;
            this.NombreMenbresia = nombreMenbresia;
            this.Descripcion = descripcion;
            this.DuracionMeses = duracionMeses;
            this.Precio = precio;
            this.Descuento = descuento;
            this.Estado = estado;
        }

        public int MembresiaID { get => membresiaID; set => membresiaID = value; }
        public string NombreMenbresia { get => nombreMenbresia; set => nombreMenbresia = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int DuracionMeses { get => duracionMeses; set => duracionMeses = value; }
        public double Precio { get => precio; set => precio = value; }
        public double Descuento { get => descuento; set => descuento = value; }
        public string Estado { get => estado; set => estado = value; }
    }
}
