using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class Citas
    {
        private int citaID;
        private int clienteID;
        private int empleadoID;
        private int salaID;
        private DateTime fechaCita;
        private TimeSpan horaInicio;
        private TimeSpan horaFin;
        private string estadoCita;
        private string observaciones;
        private DateTime fechaCreacion;

        public Citas()
        {
        }

        public Citas(int citaID, int clienteID, int empleadoID, int salaID,
                    DateTime fechaCita, TimeSpan horaInicio, TimeSpan horaFin,
                    string estadoCita, string observaciones, DateTime fechaCreacion)
        {
            this.CitaID = citaID;
            this.ClienteID = clienteID;
            this.EmpleadoID = empleadoID;
            this.SalaID = salaID;
            this.FechaCita = fechaCita;
            this.HoraInicio = horaInicio;
            this.HoraFin = horaFin;
            this.EstadoCita = estadoCita;
            this.Observaciones = observaciones;
            this.FechaCreacion = fechaCreacion;
        }

        public int CitaID { get => citaID; set => citaID = value; }
        public int ClienteID { get => clienteID; set => clienteID = value; }
        public int EmpleadoID { get => empleadoID; set => empleadoID = value; }
        public int SalaID { get => salaID; set => salaID = value; }
        public DateTime FechaCita { get => fechaCita; set => fechaCita = value; }
        public TimeSpan HoraInicio { get => horaInicio; set => horaInicio = value; }
        public TimeSpan HoraFin { get => horaFin; set => horaFin = value; }
        public string EstadoCita { get => estadoCita; set => estadoCita = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
        public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    }
}
