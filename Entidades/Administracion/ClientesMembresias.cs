using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class ClientesMembresias
    {
        private int clienteMembresiaID;
        private int clienteID;
        private int membresiaID;
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private string estadoMembresia;
        private DateTime fechaRegistro;

        public ClientesMembresias()
        {
        }

        public ClientesMembresias(int clienteMembresiaID, int clienteID, int membresiaID,
                                DateTime fechaInicio, DateTime fechaFin,
                                string estadoMembresia, DateTime fechaRegistro)
        {
            this.ClienteMembresiaID = clienteMembresiaID;
            this.ClienteID = clienteID;
            this.MembresiaID = membresiaID;
            this.FechaInicio = fechaInicio;
            this.FechaFin = fechaFin;
            this.EstadoMembresia = estadoMembresia;
            this.FechaRegistro = fechaRegistro;
        }

        public int ClienteMembresiaID { get => clienteMembresiaID; set => clienteMembresiaID = value; }
        public int ClienteID { get => clienteID; set => clienteID = value; }
        public int MembresiaID { get => membresiaID; set => membresiaID = value; }
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public DateTime FechaFin { get => fechaFin; set => fechaFin = value; }
        public string EstadoMembresia { get => estadoMembresia; set => estadoMembresia = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
    }
}
