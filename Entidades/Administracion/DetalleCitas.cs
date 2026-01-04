using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class DetalleCitas
    {
        private int detalleCitaID;
        private int citaID;
        private int servicioID;
        private decimal precioServicio;
        private decimal descuento;

        public DetalleCitas()
        {
        }

        public DetalleCitas(int detalleCitaID, int citaID, int servicioID,
                           decimal precioServicio, decimal descuento)
        {
            this.DetalleCitaID = detalleCitaID;
            this.CitaID = citaID;
            this.ServicioID = servicioID;
            this.PrecioServicio = precioServicio;
            this.Descuento = descuento;
        }

        public int DetalleCitaID { get => detalleCitaID; set => detalleCitaID = value; }
        public int CitaID { get => citaID; set => citaID = value; }
        public int ServicioID { get => servicioID; set => servicioID = value; }
        public decimal PrecioServicio { get => precioServicio; set => precioServicio = value; }
        public decimal Descuento { get => descuento; set => descuento = value; }
    }
}
