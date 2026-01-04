using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class SalasCD
    {
        public static List<LinqtoSql.Salas> ListarSalas()
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.Salas.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla Salas", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void InsertarSala(Entidades.Administracion.Salas oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarSalas(
                        oc.SalaID,
                        oc.NombreSala,
                        oc.TipoSala,
                        oc.Capacidad,
                        oc.Estado
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al insertar Sala", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static List<CP_ListarSalasFiltroResult> ListarSalaFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarSalasFiltro(val).ToList();
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
        public static void ModificarSala(Entidades.Administracion.Salas oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarSalas(
                         oc.SalaID,
                        oc.NombreSala,
                        oc.TipoSala,
                        oc.Capacidad,
                        oc.Estado
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al modificar Sala", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void EliminarSala(Entidades.Administracion.Salas oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarSalas(oc.SalaID);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar Sala", ex);
            }
            finally
            {
                DB = null;
            }
        }
    }
}

