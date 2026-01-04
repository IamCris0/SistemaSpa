using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class ComprasCD
    {
        public static List<LinqtoSql.Compras> ListarCompras()
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.Compras.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla compras", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static List<CP_ListarComprasFiltroResult> ListarComprasFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarComprasFiltro(val).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar el procedimiento listar compras", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void InsertarCompra(Entidades.Administracion.Compras oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarCompras(
                        oc.CompraID,
                        oc.ProveedorID,
                        oc.FechaCompra,
                        oc.Total,
                        oc.EstadoCompra,
                        oc.Observaciones
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al insertar compra", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static void ModificarCompra(Entidades.Administracion.Compras oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarCompras(
                        oc.CompraID,
                        oc.ProveedorID,
                        oc.FechaCompra,
                        oc.Total,
                        oc.EstadoCompra,
                        oc.Observaciones
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al modificar compra", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static void EliminarCompra(Entidades.Administracion.Compras oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarCompras(oc.CompraID);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar compra", ex);
            }
            finally
            {
                DB = null;
            }
        }
    }

}
