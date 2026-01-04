using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Productos = Entidades.Administracion.Productos;

namespace Logica.Administracion
{
    public class ProductosLN
    {
        public List<Productos> ShowProductos()
        {
            List<Productos> lista = new List<Productos>();
            Productos oc;

            try
            {
                List<Datos.LinqtoSql.Productos> auxLista = ProductosCD.ListarProductos();

                foreach (Datos.LinqtoSql.Productos op in auxLista)
                {
                    oc = new Productos(
                    op.ProductoID,
                        op.NombreProducto,
                        op.Descripcion,
                        op.Marca,
                       (double)op.PrecioUnitario,
                        (int)op.Stock,
                        (int)op.StockMinimo,
                        op.Estado

                    );
                    lista.Add(oc);
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones(
                    "Error al mostrar Producto sin filtro", ex);
            }
            finally
            {
            }

            return lista;
        }
        public List<Productos> ShowProductosFiltro(string valor)
        {
            List<Productos> lista = new List<Productos>();
            Productos ow;

            try
            {
                List<CP_ListarProductosFiltroResult> auxLista = ProductosCD.ListarProductoFiltro(valor);

                foreach (CP_ListarProductosFiltroResult op in auxLista)
                {
                    ow = new Productos(
                    op.ProductoID,
                        op.NombreProducto,
                        op.Descripcion,
                        op.Marca,
                       (double)op.PrecioUnitario,
                        (int)op.Stock,
                        (int)op.StockMinimo,
                        op.Estado

                    );
                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar Productos con filtro en el procedimiento almacenado", ex);
            }
            finally
            {
            }

            return lista;
        }
        public bool InsertProducto(Productos oc)
        {
            try
            {
                ProductosCD.InsertarProducto(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar Producto en la BD", ex);
            }
        }

        public bool UpdateProducto(Productos oc)
        {
            try
            {
                ProductosCD.ModificarProducto(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al actualizar Producto en la BD", ex);
            }
        }

        public bool DeleteProducto(Productos oc)
        {
            try
            {
                ProductosCD.EliminarProducto(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al eliminar Producto en la BD", ex);
            }
        }
    }
}
