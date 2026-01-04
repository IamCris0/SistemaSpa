using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class CitasCD
    {
        public static List<LinqtoSql.Citas> ListarCitas()
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.Citas.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla Citas", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static List<CP_ListarCitasFiltroResult> ListarCitasFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarCitasFiltro(val).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar el procedimiento listar citas", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void InsertarCita(Entidades.Administracion.Citas oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarCitas(
                        oc.CitaID,
                        oc.ClienteID,
                        oc.EmpleadoID,
                        oc.SalaID,
                        oc.FechaCita,
                        oc.HoraInicio,
                        oc.HoraFin,
                        oc.EstadoCita,
                        oc.Observaciones,
                        oc.FechaCreacion
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al insertar cita", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void ModificarCita(Entidades.Administracion.Citas oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarCitas(
                        oc.CitaID,
                        oc.ClienteID,
                        oc.EmpleadoID,
                        oc.SalaID,
                        oc.FechaCita,
                        oc.HoraInicio,
                        oc.HoraFin,
                        oc.EstadoCita,
                        oc.Observaciones,
                        oc.FechaCreacion
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al modificar cita", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void EliminarCita(Entidades.Administracion.Citas oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarCitas(oc.CitaID);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar cita", ex);
            }
            finally
            {
                DB = null;
            }
        }
    }
}
