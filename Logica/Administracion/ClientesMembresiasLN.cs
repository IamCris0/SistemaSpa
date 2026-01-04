using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientesMembresias = Entidades.Administracion.ClientesMembresias;
namespace Logica.Administracion
{
    public class ClienteMembresiasLN
    {
        public List<ClientesMembresias> ShowClienteMembresias()
        {
            List<ClientesMembresias> lista = new List<ClientesMembresias>();
            ClientesMembresias oc;

            try
            {
                List<Datos.LinqtoSql.ClientesMembresias> auxLista =
                    ClientesMembresiasCD.ListarClienteMembresias();

                foreach (Datos.LinqtoSql.ClientesMembresias op in auxLista)
                {
                    oc = new ClientesMembresias(
                        op.ClienteMembresiaID,
                        (int)op.ClienteID,
                        (int)op.MembresiaID,
                        (DateTime)op.FechaInicio,
                        (DateTime)op.FechaFin,
                        op.EstadoMembresia,
                        (DateTime)op.FechaRegistro
                    );
                    lista.Add(oc);
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones(
                    "Error al mostrar ClienteMembresias sin filtro", ex);
            }
            finally
            {
            }

            return lista;
        }
        public List<ClientesMembresias> ShowClientesMembresiasFiltro(string valor)
        {
            List<ClientesMembresias> lista = new List<ClientesMembresias>();
            ClientesMembresias ow;

            try
            {
                List<CP_ListarClientesMembresiasFiltroResult> auxLista = ClientesMembresiasCD.ListarClientesMembresiasFiltro(valor);

                foreach (CP_ListarClientesMembresiasFiltroResult op in auxLista)
                {
                    ow = new ClientesMembresias(
                        op.ClienteMembresiaID,
                        (int)op.ClienteID,
                        (int)op.MembresiaID,
                        (DateTime)op.FechaInicio,
                        (DateTime)op.FechaFin,
                        op.EstadoMembresia,
                        (DateTime)op.FechaRegistro
                    );
                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar ClientesMembresias con filtro en el procedimiento almacenado", ex);
            }
            finally
            {
            }

            return lista;
        }

        public bool InsertClienteMembresias(ClientesMembresias oc)
        {
            try
            {
                ClientesMembresiasCD.InsertarClienteMembresias(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar ClienteMembresias en la BD", ex);
            }
        }

        public bool UpdateClienteMembresias(ClientesMembresias oc)
        {
            try
            {
                ClientesMembresiasCD.ModificarClienteMembresias(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al actualizar ClienteMembresias en la BD", ex);
            }
        }

        public bool DeleteClienteMembresias(ClientesMembresias oc)
        {
            try
            {
                ClientesMembresiasCD.EliminarClienteMembresias(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al eliminar ClienteMembresias en la BD", ex);
            }
        }
    }
}
