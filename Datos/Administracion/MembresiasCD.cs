using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class MembresiasCD
    {
        public static List<LinqtoSql.Membresias> ListarMembresias()
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.Membresias.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla Membresias", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void InsertarCliente(Entidades.Administracion.Membresias oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarMembresias(
                        oc.MembresiaID,
                        oc.NombreMenbresia,
                        oc.Descripcion,
                        oc.DuracionMeses,
                       (decimal)oc.Precio,
                       (decimal)oc.Descuento,
                        oc.Estado

                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al insertar cliente", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static List<CP_ListarMembresiasFiltroResult> ListarClienteFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarMembresiasFiltro(val).ToList();
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
        public static void ModificarCliente(Entidades.Administracion.Membresias oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarMembresias(
                       oc.MembresiaID,
                        oc.NombreMenbresia,
                        oc.Descripcion,
                        oc.DuracionMeses,
                       (decimal)oc.Precio,
                       (decimal)oc.Descuento,
                        oc.Estado
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al modificar cliente", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void EliminarCliente(Entidades.Administracion.Membresias oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarMembresias(oc.MembresiaID);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar cliente", ex);
            }
            finally
            {
                DB = null;
            }
        }
    }
}
