using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class VentasCD
    {
        public static List<LinqtoSql.Ventas> ListarVentas()
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.Ventas.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla Ventas", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void InsertarVenta(Entidades.Administracion.Ventas oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarVentas(
                        oc.VentaID,
                        oc.ClienteID,
                        oc.EmpleadoID,
                        oc.FechaVenta,
                        (decimal)oc.Total,
                        oc.MetodoPago,
                        oc.Estado
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al insertar Venta", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static List<CP_ListarVentasFiltroResult> ListarVentaFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarVentasFiltro(val).ToList();
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
        public static void ModificarVenta(Entidades.Administracion.Ventas oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarVentas(

                        oc.VentaID,
                        oc.ClienteID,
                        oc.EmpleadoID,
                        oc.FechaVenta,
                        (decimal)oc.Total,
                        oc.MetodoPago,
                        oc.Estado
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al modificar Venta", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void EliminarVenta(Entidades.Administracion.Ventas oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarVentas(oc.VentaID);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar Venta", ex);
            }
            finally
            {
                DB = null;
            }
        }
    }
}
