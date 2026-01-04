using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class Servicios
    {
        private int servicioID;
        private int categoriaID;
        private string nombreServicio;
        private string descripcion;
        private int duracion;
        private double precio;
        private string estado;

        public Servicios()
        {
        }

        public Servicios(int servicioID, int categoriaID, string nombreServicio, string descripcion, int duracion, double precio, string estado)
        {
            this.ServicioID = servicioID;
            this.CategoriaID = categoriaID;
            this.NombreServicio = nombreServicio;
            this.Descripcion = descripcion;
            this.Duracion = duracion;
            this.Precio = precio;
            this.Estado = estado;
        }

        public int ServicioID { get => servicioID; set => servicioID = value; }
        public int CategoriaID { get => categoriaID; set => categoriaID = value; }
        public string NombreServicio { get => nombreServicio; set => nombreServicio = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Duracion { get => duracion; set => duracion = value; }
        public double Precio { get => precio; set => precio = value; }
        public string Estado { get => estado; set => estado = value; }
    }
}
