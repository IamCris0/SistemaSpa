using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class Salas
    {
        private int salaID;
        private string nombreSala;
        private string tipoSala;
        private int capacidad;
        private string estado;

        public Salas()
        {
        }

        public Salas(int salaID, string nombreSala, string tipoSala, int capacidad, string estado)
        {
            this.SalaID = salaID;
            this.NombreSala = nombreSala;
            this.TipoSala = tipoSala;
            this.Capacidad = capacidad;
            this.Estado = estado;
        }

        public int SalaID { get => salaID; set => salaID = value; }
        public string NombreSala { get => nombreSala; set => nombreSala = value; }
        public string TipoSala { get => tipoSala; set => tipoSala = value; }
        public int Capacidad { get => capacidad; set => capacidad = value; }
        public string Estado { get => estado; set => estado = value; }
    }
}
