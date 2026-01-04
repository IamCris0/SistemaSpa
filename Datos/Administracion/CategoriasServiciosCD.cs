using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class CategoriasServiciosCD
    {
        public static List<LinqtoSql.CategoriasServicios> ListarCategoriaServicio()
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CategoriasServicios.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla CategoriaServicio", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static List<CP_ListarCategoriasServiciosFiltroResult> ListarCategoriasServiciosFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarCategoriasServiciosFiltro(val).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar el procedimiento listar categoria de servicios", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void InsertarCategoriaServicio(Entidades.Administracion.CategoriasServicios op)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarCategoriasServicios(
                        op.CategoriaID,
                        op.NombreCategoria,
                        op.Descripcion,
                        op.Estado
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al insertar CategoriaServicio", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static void ModificarCategoriaServicio(Entidades.Administracion.CategoriasServicios op)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarCategoriasServicios(
                        op.CategoriaID,
                        op.NombreCategoria,
                        op.Descripcion,
                        op.Estado
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al modificar CategoriaServicio", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static void EliminarCategoriaServicio(Entidades.Administracion.CategoriasServicios op)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarCategoriasServicios(op.CategoriaID);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar CategoriaServicio", ex);
            }
            finally
            {
                DB = null;
            }
        }
    }
}
