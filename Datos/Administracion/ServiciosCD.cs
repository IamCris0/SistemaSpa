using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class ServiciosCD
    {
        public static List<LinqtoSql.Servicios> ListarServicios()
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.Servicios.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla Servicios", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void InsertarServicio(Entidades.Administracion.Servicios oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarServicios(
                        oc.ServicioID,
                        oc.CategoriaID,
                        oc.NombreServicio,
                        oc.Descripcion,
                        oc.Duracion,
                        (decimal)oc.Precio,
                        oc.Estado
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al insertar Servicio", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static List<CP_ListarServiciosFiltroResult> ListarServicioFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarServiciosFiltro(val).ToList();
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
        public static void ModificarServicio(Entidades.Administracion.Servicios oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarServicios(
                          oc.ServicioID,
                        oc.CategoriaID,
                        oc.NombreServicio,
                        oc.Descripcion,
                        oc.Duracion,
                        (decimal)oc.Precio,
                        oc.Estado
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al modificar Servicio", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void EliminarServicio(Entidades.Administracion.Servicios oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarServicios(oc.ServicioID);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar Servicio", ex);
            }
            finally
            {
                DB = null;
            }
        }
    }
}
