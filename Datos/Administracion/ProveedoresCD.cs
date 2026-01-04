using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class ProveedoresCD
    {

        public static List<LinqtoSql.Proveedores> ListarProveedores()
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.Proveedores.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla Proveedores", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void InsertarProveedore(Entidades.Administracion.Proveedores oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarProveedores(
                        oc.ProveedorID,
                        oc.NombreProveedor,
                        oc.Contacto,
                        oc.Telefono,
                        oc.Email,
                        oc.Direccion,
                        oc.Estado
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al insertar Proveedore", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static List<CP_ListarProveedoresFiltroResult> ListarProveedoreFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarProveedoresFiltro(val).ToList();
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
        public static void ModificarProveedore(Entidades.Administracion.Proveedores oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarProveedores(
                        oc.ProveedorID,
                        oc.NombreProveedor,
                        oc.Contacto,
                        oc.Telefono,
                        oc.Email,
                        oc.Direccion,
                        oc.Estado
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al modificar Proveedore", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void EliminarProveedore(Entidades.Administracion.Proveedores oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarProveedores(oc.ProveedorID);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar Proveedore", ex);
            }
            finally
            {
                DB = null;
            }
        }
    }
}
