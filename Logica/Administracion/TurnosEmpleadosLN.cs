using Datos;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnosEmpleados = Entidades.Administracion.TurnosEmpleados;
using Datos.Administracion;

namespace Logica.Administracion
{
    public class TurnosEmpleadosLN
    {

        public List<TurnosEmpleados> ShowTurnosEmpleados()
        {
            List<TurnosEmpleados> lista = new List<TurnosEmpleados>();
            TurnosEmpleados oc;

            try
            {
                List<Datos.LinqtoSql.TurnosEmpleados> auxLista =
                    TurnosEmpleadosCD.ListarTurnoEmpleados();

                foreach (Datos.LinqtoSql.TurnosEmpleados op in auxLista)
                {
                    oc = new TurnosEmpleados(
                       op.TurnoID,
                        (int)op.EmpleadoID,
                        op.DiaSemana,
                        (TimeSpan)op.HoraInicio,
                       (TimeSpan)op.HoraFin,
                        op.TipoTurno,
                        op.Estado,
                        (DateTime)op.FechaRegistro
                    );
                    lista.Add(oc);
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones(
                    "Error al mostrar TurnosEmpleados sin filtro", ex);
            }
            finally
            {
            }

            return lista;
        }
        public List<TurnosEmpleados> ShowTurnosEmpleadosFiltro(string valor)
        {
            List<TurnosEmpleados> lista = new List<TurnosEmpleados>();
            TurnosEmpleados ow;

            try
            {
                List<CP_ListarTurnosEmpleadosFiltroResult> auxLista = TurnosEmpleadosCD.ListarTurnoEmpleadoFiltro(valor);

                foreach (CP_ListarTurnosEmpleadosFiltroResult op in auxLista)
                {
                    ow = new TurnosEmpleados(
                       op.TurnoID,
                        (int)op.EmpleadoID,
                        op.DiaSemana,
                        (TimeSpan)op.HoraInicio,
                       (TimeSpan)op.HoraFin,
                        op.TipoTurno,
                        op.Estado,
                        (DateTime)op.FechaRegistro
                    );
                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar TurnosEmpleados con filtro en el procedimiento almacenado", ex);
            }
            finally
            {
            }

            return lista;
        }
        public bool InsertTurnosEmpleados(TurnosEmpleados oc)
        {
            try
            {
                TurnosEmpleadosCD.InsertarTurnoEmpleado(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar TurnosEmpleados en la BD", ex);
            }
        }

        public bool UpdateTurnosEmpleados(TurnosEmpleados oc)
        {
            try
            {
                TurnosEmpleadosCD.ModificarTurnoEmpleado(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al actualizar TurnosEmpleados en la BD", ex);
            }
        }

        public bool DeleteTurnosEmpleados(TurnosEmpleados oc)
        {
            try
            {
                TurnosEmpleadosCD.EliminarTurnoEmpleado(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al eliminar TurnosEmpleados en la BD", ex);
            }
        }
    }
}
