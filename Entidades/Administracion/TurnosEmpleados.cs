using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class TurnosEmpleados
    {
        private int turnoID;
        private int empleadoID;
        private string diaSemana;
        private TimeSpan horaInicio;
        private TimeSpan horaFin;
        private string tipoTurno;
        private string estado;
        private DateTime fechaRegistro;

        public TurnosEmpleados()
        {
        }

        public TurnosEmpleados(int turnoID, int empleadoID, string diaSemana, TimeSpan horaInicio, TimeSpan horaFin, string tipoTurno, string estado, DateTime fechaRegistro)
        {
            this.TurnoID = turnoID;
            this.EmpleadoID = empleadoID;
            this.DiaSemana = diaSemana;
            this.HoraInicio = horaInicio;
            this.HoraFin = horaFin;
            this.TipoTurno = tipoTurno;
            this.Estado = estado;
            this.FechaRegistro = fechaRegistro;
        }

        public int TurnoID { get => turnoID; set => turnoID = value; }
        public int EmpleadoID { get => empleadoID; set => empleadoID = value; }
        public string DiaSemana { get => diaSemana; set => diaSemana = value; }
        public TimeSpan HoraInicio { get => horaInicio; set => horaInicio = value; }
        public TimeSpan HoraFin { get => horaFin; set => horaFin = value; }
        public string TipoTurno { get => tipoTurno; set => tipoTurno = value; }
        public string Estado { get => estado; set => estado = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
    }
}
