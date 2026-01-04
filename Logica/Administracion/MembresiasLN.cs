using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Membresias = Entidades.Administracion.Membresias;

namespace Logica.Administracion
{
    public class MembresiasLN
    {
        public List<Membresias> ShowMembresias()
        {
            List<Membresias> lista = new List<Membresias>();
            Membresias oc;

            try
            {
                List<Datos.LinqtoSql.Membresias> auxLista =
                    MembresiasCD.ListarMembresias();

                foreach (Datos.LinqtoSql.Membresias op in auxLista)
                {
                    oc = new Membresias(
                         op.MembresiaID,
                        op.NombreMembresia,
                        op.Descripcion,
                        (int)op.DuracionMeses,
                       (double)op.Precio,
                       (double)op.Descuento,
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
        public List<Membresias> ShowMembresiasFiltro(string valor)
        {
            List<Membresias> lista = new List<Membresias>();
            Membresias ow;

            try
            {
                List<CP_ListarMembresiasFiltroResult> auxLista = MembresiasCD.ListarClienteFiltro(valor);

                foreach (CP_ListarMembresiasFiltroResult op in auxLista)
                {
                    ow = new Membresias(
                        op.MembresiaID,
                        op.NombreMembresia,
                        op.Descripcion,
                        (int)op.DuracionMeses,
                       (double)op.Precio,
                       (double)op.Descuento,
                        op.Estado
                    );
                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar Membresias con filtro en el procedimiento almacenado", ex);
            }
            finally
            {
            }

            return lista;
        }
        public bool InsertCliente(Membresias oc)
        {
            try
            {
                MembresiasCD.InsertarCliente(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar Cliente en la BD", ex);
            }
        }

        public bool UpdateCliente(Membresias oc)
        {
            try
            {
                MembresiasCD.ModificarCliente(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al actualizar Cliente en la BD", ex);
            }
        }

        public bool DeleteCliente(Membresias oc)
        {
            try
            {
                MembresiasCD.EliminarCliente(oc);
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
