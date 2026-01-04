
using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clientes = Entidades.Administracion.Clientes;

namespace Logica.Administracion
{
    public class ClientesLN
    {
        public List<Clientes> ShowCliente()
        {
            List<Clientes> lista = new List<Clientes>();
            Clientes oc;

            try
            {
                List<Datos.LinqtoSql.Clientes> auxLista =
                    ClientesCD.ListarClientes();

                foreach (Datos.LinqtoSql.Clientes op in auxLista)
                {
                    oc = new Clientes(
                        op.ClienteID,
                        op.Nombre,
                        op.Apellido,
                        op.Email,
                        op.Telefono,
                        op.Direccion,
                        (DateTime)op.FechaNacimiento,
                        (DateTime)op.FechaRegistro,
                        op.Estado
                    );
                    lista.Add(oc);
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones(
                    "Error al mostrar Cliente sin filtro", ex);
            }
            finally
            {
            }

            return lista;
        }
        public List<Clientes> ShowClientesFiltro(string valor)
        {
            List<Clientes> lista = new List<Clientes>();
            Clientes ow;

            try
            {
                List<CP_ListarClientesFiltroResult> auxLista = ClientesCD.ListarClienteFiltro(valor);

                foreach (CP_ListarClientesFiltroResult op in auxLista)
                {
                    ow = new Clientes(
                        op.ClienteID,
                        op.Nombre,
                        op.Apellido,
                        op.Email,
                        op.Telefono,
                        op.Direccion,
                        (DateTime)op.FechaNacimiento,
                        (DateTime)op.FechaRegistro,
                        op.Estado
                    );
                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar Clientes con filtro en el procedimiento almacenado", ex);
            }
            finally
            {
            }

            return lista;
        }
        public bool InsertCliente(Clientes oc)
        {
            try
            {
                ClientesCD.InsertarCliente(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar Cliente en la BD", ex);
            }
        }

        public bool UpdateCliente(Clientes oc)
        {
            try
            {
                ClientesCD.ModificarCliente(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al actualizar Cliente en la BD", ex);
            }
        }

        public bool DeleteCliente(Clientes oc)
        {
            try
            {
                ClientesCD.EliminarCliente(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al eliminar Cliente en la BD", ex);
            }
        }
    }
}

