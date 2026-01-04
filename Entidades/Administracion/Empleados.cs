using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class Empleados
    {
        private int empleadoID;
        private string nombre;
        private string apellido;
        private string email;
        private string telefono;
        private string cargo;
        private DateTime fechaContratacion;
        private decimal salario;
        private string estado;

        public Empleados()
        {
        }

        public Empleados(int empleadoID, string nombre, string apellido, string email,
                        string telefono, string cargo, DateTime fechaContratacion,
                        decimal salario, string estado)
        {
            this.EmpleadoID = empleadoID;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Email = email;
            this.Telefono = telefono;
            this.Cargo = cargo;
            this.FechaContratacion = fechaContratacion;
            this.Salario = salario;
            this.Estado = estado;
        }

        public int EmpleadoID { get => empleadoID; set => empleadoID = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Email { get => email; set => email = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Cargo { get => cargo; set => cargo = value; }
        public DateTime FechaContratacion { get => fechaContratacion; set => fechaContratacion = value; }
        public decimal Salario { get => salario; set => salario = value; }
        public string Estado { get => estado; set => estado = value; }
    }
}
