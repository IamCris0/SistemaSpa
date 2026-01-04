using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HistorialCLientes = Entidades.Administracion.HistorialClientes;

namespace Logica.Administracion
{
    public class HistorialClientesLN
    {
        public List<HistorialCLientes> ShowHistorialCliente()
        {
            List<HistorialCLientes> lista = new List<HistorialCLientes>();
            HistorialCLientes oc;

            try
            {
                List<Datos.LinqtoSql.HistorialClientes> auxLista = HistorialClientesCD.ListarHistorialClientes();

                foreach (Datos.LinqtoSql.HistorialClientes op in auxLista)
                {
                    oc = new HistorialCLientes(
                        op.HistorialID,
                        (int)op.ClienteID,
                        (int)op.CitaID,
                        (DateTime)op.FechaVisita,
                        op.Observaciones,
                       (int)op.Calificacion,
                        op.AlergiasProcedimiento,
                        op.ResultadosTratamiento,
                       (DateTime)op.FechaRegistro

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
        public List<HistorialCLientes> ShowHistorialClientesFiltro(string valor)
        {
            List<HistorialCLientes> lista = new List<HistorialCLientes>();
            HistorialCLientes ow;

            try
            {
                List<CP_ListarHistorialClientesFiltroResult> auxLista = HistorialClientesCD.ListarHistorialClienteFiltro(valor);

                foreach (CP_ListarHistorialClientesFiltroResult op in auxLista)
                {
                    ow = new HistorialCLientes(
                       op.HistorialID,
                        (int)op.ClienteID,
                        (int)op.CitaID,
                        (DateTime)op.FechaVisita,
                        op.Observaciones,
                       (int)op.Calificacion,
                        op.AlergiasProcedimiento,
                        op.ResultadosTratamiento,
                       (DateTime)op.FechaRegistro

                    );
                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar HistorialClientes con filtro en el procedimiento almacenado", ex);
            }
            finally
            {
            }

            return lista;
        }
        public bool InsertHistorialCliente(HistorialCLientes oc)
        {
            try
            {
                HistorialClientesCD.InsertarHistorialClientes(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar Cliente en la BD", ex);
            }
        }

        public bool UpdateHistorialCliente(HistorialCLientes oc)
        {
            try
            {
                HistorialClientesCD.ModificarHistorialCliente(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al actualizar Cliente en la BD", ex);
            }
        }

        public bool DeleteHistorialCliente(HistorialCLientes oc)
        {
            try
            {
                HistorialClientesCD.EliminarHistorialCliente(oc);
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
