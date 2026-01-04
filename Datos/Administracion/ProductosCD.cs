using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class ProductosCD
    {
        public static List<LinqtoSql.Productos> ListarProductos()
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.Productos.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla Productos", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void InsertarProducto(Entidades.Administracion.Productos oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarProductos(
                        oc.ProductoID,
                        oc.NombreProducto,
                        oc.Descripcion,
                        oc.Marca,
                       (decimal)oc.PrecioUnitario,
                        oc.Stock,
                        oc.StockMinimo,
                        oc.Estado
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al insertar Producto", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static List<CP_ListarProductosFiltroResult> ListarProductoFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarProductosFiltro(val).ToList();
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
        public static void ModificarProducto(Entidades.Administracion.Productos oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarProductos(
                        oc.ProductoID,
                        oc.NombreProducto,
                        oc.Descripcion,
                        oc.Marca,
                       (decimal)oc.PrecioUnitario,
                        oc.Stock,
                        oc.StockMinimo,
                        oc.Estado
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al modificar Producto", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void EliminarProducto(Entidades.Administracion.Productos oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarProductos(oc.ProductoID);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar Producto", ex);
            }
            finally
            {
                DB = null;
            }
        }
    }
}
