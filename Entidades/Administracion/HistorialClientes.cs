using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class HistorialClientes
    {
        private int historialID;
        private int clienteID;
        private int citaID;
        private DateTime fechaVisita;
        private string observaciones;
        private int calificacion;
        private string alergiasProcedimiento;
        private string resultadosTratamiento;
        private DateTime fechaRegistro;

        public HistorialClientes()
        {
        }

        public HistorialClientes(int historialID, int clienteID, int citaID, DateTime fechaVisita, string observaciones, int calificacion, string alergiasProcedimiento, string resultadosTratamiento, DateTime fechaRegistro)
        {
            this.HistorialID = historialID;
            this.ClienteID = clienteID;
            this.CitaID = citaID;
            this.FechaVisita = fechaVisita;
            this.Observaciones = observaciones;
            this.Calificacion = calificacion;
            this.AlergiasProcedimiento = alergiasProcedimiento;
            this.ResultadosTratamiento = resultadosTratamiento;
            this.FechaRegistro = fechaRegistro;
        }

        public int HistorialID { get => historialID; set => historialID = value; }
        public int ClienteID { get => clienteID; set => clienteID = value; }
        public int CitaID { get => citaID; set => citaID = value; }
        public DateTime FechaVisita { get => fechaVisita; set => fechaVisita = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
        public int Calificacion { get => calificacion; set => calificacion = value; }
        public string AlergiasProcedimiento { get => alergiasProcedimiento; set => alergiasProcedimiento = value; }
        public string ResultadosTratamiento { get => resultadosTratamiento; set => resultadosTratamiento = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
    }

}
