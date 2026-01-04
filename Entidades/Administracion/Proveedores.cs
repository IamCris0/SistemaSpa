using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class Proveedores
    {
        private int proveedorID;
        private string nombreProveedor;
        private string contacto;
        private string telefono;
        private string email;
        private string direccion;
        private string estado;

        public Proveedores()
        {
        }

        public Proveedores(int proveedorID, string nombreProveedor, string contacto, string telefono, string email, string direccion, string estado)
        {
            this.ProveedorID = proveedorID;
            this.NombreProveedor = nombreProveedor;
            this.Contacto = contacto;
            this.Telefono = telefono;
            this.Email = email;
            this.Direccion = direccion;
            this.Estado = estado;
        }

        public int ProveedorID { get => proveedorID; set => proveedorID = value; }
        public string NombreProveedor { get => nombreProveedor; set => nombreProveedor = value; }
        public string Contacto { get => contacto; set => contacto = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Email { get => email; set => email = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Estado { get => estado; set => estado = value; }
    }
}

