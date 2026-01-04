using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DetalleCompras = Entidades.Administracion.DetalleCompras;

namespace Logica.Administracion
{
    public class DetalleComprasLN
    { // LISTAR TODO
        public List<DetalleCompras> ShowDetalleCompras()
        {
            List<DetalleCompras> lista = new List<DetalleCompras>();

            try
            {
                List<Datos.LinqtoSql.DetalleCompras> auxLista =
                    DetalleComprasCD.ListarDetalleCompras();

                foreach (Datos.LinqtoSql.DetalleCompras op in auxLista)
                {
                    lista.Add(new DetalleCompras(
                        op.DetalleCompraID,
                        op.CompraID ?? 0,
                        op.ProductoID ?? 0,
                        op.Cantidad ?? 0,
                        op.PrecioUnitario ?? 0
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones(
                    "Error al mostrar DetalleCompras", ex);
            }

            return lista;
        }

        // LISTAR CON FILTRO
        public List<DetalleCompras> ShowDetalleComprasFiltro(string valor)
        {
            List<DetalleCompras> lista = new List<DetalleCompras>();

            try
            {
                List<CP_ListarDetalleComprasFiltroResult> auxLista =
                    DetalleComprasCD.ListarDetalleComprasFiltro(valor);

                foreach (CP_ListarDetalleComprasFiltroResult op in auxLista)
                {
                    lista.Add(new DetalleCompras(
                        op.DetalleCompraID,
                        op.CompraID ?? 0,
                        op.ProductoID ?? 0,
                        op.Cantidad ?? 0,
                        op.PrecioUnitario ?? 0
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al mostrar DetalleCompras con filtro", ex);
            }

            return lista;
        }

        // INSERTAR
        public bool InsertDetalleCompras(DetalleCompras detalle)
        {
            try
            {
                DetalleComprasCD.InsertarDetalleCompras(detalle);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar DetalleCompras", ex);
            }
        }

        // MODIFICAR
        public bool UpdateDetalleCompras(DetalleCompras detalle)
        {
            try
            {
                DetalleComprasCD.ModificarDetalleCompras(detalle);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al modificar DetalleCompras", ex);
            }
        }

        // ELIMINAR
        public bool DeleteDetalleCompras(DetalleCompras detalle)
        {
            try
            {
                DetalleComprasCD.EliminarDetalleCompras(detalle);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al eliminar DetalleCompras", ex);
            }
        }
    }
}
