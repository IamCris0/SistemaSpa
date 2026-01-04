using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class DetalleCitasCD
    {
        public static List<DetalleCitas> ListarDetalleCitas()
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.DetalleCitas.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla DetalleCitas", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static List<CP_ListarDetalleCitasFiltroResult> ListarDetalleCitasFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarDetalleCitasFiltro(val).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar el procedimiento listar detalle citas", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void InsertarDetalleCitas(Entidades.Administracion.DetalleCitas op)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarDetalleCitas(
                        op.DetalleCitaID,
                        op.CitaID,
                        op.ServicioID,
                        op.PrecioServicio,
                        op.Descuento
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al insertar DetalleCitas", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static void ModificarDetalleCitas(Entidades.Administracion.DetalleCitas op)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarDetalleCitas(
                        op.DetalleCitaID,
                        op.CitaID,
                        op.ServicioID,
                        op.PrecioServicio,
                        op.Descuento
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al modificar DetalleCitas", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static void EliminarDetalleCitas(Entidades.Administracion.DetalleCitas op)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarDetalleCitas(op.DetalleCitaID);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar DetalleCitas", ex);
            }
            finally
            {
                DB = null;
            }
        }
    }
}
