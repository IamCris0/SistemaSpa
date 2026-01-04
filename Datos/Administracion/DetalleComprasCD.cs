using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class DetalleComprasCD
    {
        public static List<DetalleCompras> ListarDetalleCompras()
        {

            try
            {
                using (DataClasses1DataContext DB = new DataClasses1DataContext())
                {
                    return DB.DetalleCompras.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla DetalleCompras", ex);
            }
        }


        public static List<CP_ListarDetalleComprasFiltroResult> ListarDetalleComprasFiltro(string val)
        {
            try
            {
                using (DataClasses1DataContext DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarDetalleComprasFiltro(val).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar el detalle de ventas", ex);
            }
        }


        // INSERTAR
        public static void InsertarDetalleCompras(Entidades.Administracion.DetalleCompras op)

        {
            try
            {
                using (DataClasses1DataContext DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarDetalleCompras(
                        op.DetalleCompraID,
                        op.CompraID,
                        op.ProductoID,
                        op.Cantidad,
                        op.PrecioUnitario
                    );
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al insertar DetalleCompras", ex);
            }
        }

        // MODIFICAR
        public static void ModificarDetalleCompras(Entidades.Administracion.DetalleCompras op)
        {
            try
            {
                using (DataClasses1DataContext DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarDetalleCompras(
                        op.DetalleCompraID,
                        op.CompraID,
                        op.ProductoID,
                        op.Cantidad,
                        op.PrecioUnitario
                    );
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al modificar DetalleCompras", ex);
            }
        }

        public static void EliminarDetalleCompras(Entidades.Administracion.DetalleCompras op)
        {
            try
            {
                using (DataClasses1DataContext DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarDetalleCompras(op.DetalleCompraID);
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar DetalleCompras", ex);
            }
        }
    }
}