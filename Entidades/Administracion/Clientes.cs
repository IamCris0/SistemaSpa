using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class Clientes
    {
        private int clienteID;
        private string nombre;
        private string apellido;
        private string email;
        private string telefono;
        private string direccion;
        private DateTime fechaNacimiento;
        private DateTime fechaRegistro;
        private string estado;

        public Clientes()
        {
        }

        public Clientes(int clienteID, string nombre, string apellido, string email,
                       string telefono, string direccion, DateTime fechaNacimiento,
                       DateTime fechaRegistro, string estado)
        {
            this.ClienteID = clienteID;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Email = email;
            this.Telefono = telefono;
            this.Direccion = direccion;
            this.FechaNacimiento = fechaNacimiento;
            this.FechaRegistro = fechaRegistro;
            this.Estado = estado;
        }

        public int ClienteID { get => clienteID; set => clienteID = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Email { get => email; set => email = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
        public string Estado { get => estado; set => estado = value; }
    }
}
