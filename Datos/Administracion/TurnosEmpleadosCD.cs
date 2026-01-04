using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class TurnosEmpleadosCD
    {
        public static List<LinqtoSql.TurnosEmpleados> ListarTurnoEmpleados()
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.TurnosEmpleados.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla TurnoEmpleados", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void InsertarTurnoEmpleado(Entidades.Administracion.TurnosEmpleados oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarTurnosEmpleados(
                        oc.TurnoID,
                        oc.EmpleadoID,
                        oc.DiaSemana,
                        oc.HoraInicio,
                        oc.HoraFin,
                        oc.TipoTurno,
                        oc.Estado,
                        oc.FechaRegistro
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al insertar TurnoEmpleado", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static List<CP_ListarTurnosEmpleadosFiltroResult> ListarTurnoEmpleadoFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarTurnosEmpleadosFiltro(val).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar el procedimiento listar empleado", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void ModificarTurnoEmpleado(Entidades.Administracion.TurnosEmpleados oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarTurnosEmpleados(
                        oc.TurnoID,
                        oc.EmpleadoID,
                        oc.DiaSemana,
                        oc.HoraInicio,
                        oc.HoraFin,
                        oc.TipoTurno,
                        oc.Estado,
                        oc.FechaRegistro
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al modificar TurnoEmpleado", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void EliminarTurnoEmpleado(Entidades.Administracion.TurnosEmpleados oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarTurnosEmpleados(oc.TurnoID);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar TurnoEmpleado", ex);
            }
            finally
            {
                DB = null;
            }
        }
    }
}
