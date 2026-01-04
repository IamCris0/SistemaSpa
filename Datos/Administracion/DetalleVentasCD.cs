using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class DetalleVentasCD
    {
        public static List<DetalleVentas> ListarDetalleVentas()
        {
           
            try
            {
                using (DataClasses1DataContext DB = new DataClasses1DataContext())
                {
                    return DB.DetalleVentas.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla DetalleVentas", ex);
            }
        }


        public static List<CP_ListarDetalleVentasFiltroResult> ListarDetalleVentasFiltro(string val)
        {
            try
            {
                using (DataClasses1DataContext DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarDetalleVentasFiltro(val).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar el detalle de ventas", ex);
            }
        }


        // INSERTAR
        public static void InsertarDetalleVentas(Entidades.Administracion.DetalleVentas op)
        
            {
                try
                {
                    using (DataClasses1DataContext DB = new DataClasses1DataContext())
                    {
                        DB.CP_InsertarDetalleVentas(
                            op.DetalleVentaID,
                            op.VentaID,
                            op.ProductoID,
                            op.Cantidad,
                            op.PrecioUnitario
                        );
                    }
                }
                catch (Exception ex)
                {
                    throw new DatosExcepciones("Error al insertar DetalleVentas", ex);
                }
            }

        // MODIFICAR
        public static void ModificarDetalleVentas(Entidades.Administracion.DetalleVentas op)
        {
            try
            {
                using (DataClasses1DataContext DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarDetalleVentas(
                        op.DetalleVentaID,
                        op.VentaID,
                        op.ProductoID,
                        op.Cantidad,
                        op.PrecioUnitario
                    );
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al modificar DetalleVentas", ex);
            }
        }

        public static void EliminarDetalleVentas(Entidades.Administracion.DetalleVentas op)
        {
            try
            {
                using (DataClasses1DataContext DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarDetalleVentas(op.DetalleVentaID);
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar DetalleVentas", ex);
            }
        }
    }
}