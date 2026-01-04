using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compras = Entidades.Administracion.Compras;

namespace Logica.Administracion
{
    public class CompraLN
    {
        public List<Compras> ShowCompra()
        {
            List<Compras> lista = new List<Compras>();
            Compras oc;

            try
            {
                List<Datos.LinqtoSql.Compras> auxLista =
                    ComprasCD.ListarCompras();

                foreach (Datos.LinqtoSql.Compras op in auxLista)
                {
                    oc = new Compras(
                        op.CompraID,
                        (int)op.ProveedorID,
                        (DateTime)op.FechaCompra,
                        (decimal)op.Total,
                        op.EstadoCompra,
                        op.Observaciones
                    );
                    lista.Add(oc);
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones(
                    "Error al mostrar Compra sin filtro", ex);
            }
            finally
            {
            }

            return lista;
        }
        public List<Compras> ShowComprasFiltro(string valor)
        {
            List<Compras> lista = new List<Compras>();
            Compras ow;

            try
            {
                List<CP_ListarComprasFiltroResult> auxLista = ComprasCD.ListarComprasFiltro(valor);

                foreach (CP_ListarComprasFiltroResult op in auxLista)
                {
                    ow = new Compras(
                        op.CompraID,
                        (int)op.ProveedorID,
                        (DateTime)op.FechaCompra,
                        (decimal)op.Total,
                        op.EstadoCompra,
                        op.Observaciones
                    );
                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar Compras con filtro en el procedimiento almacenado", ex);
            }
            finally
            {
            }

            return lista;
        }
        public bool InsertCompra(Compras oc)
        {
            try
            {
                ComprasCD.InsertarCompra(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar Compra en la BD", ex);
            }
        }

        public bool UpdateCompra(Compras oc)
        {
            try
            {
                ComprasCD.ModificarCompra(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al actualizar Compra en la BD", ex);
            }
        }

        public bool DeleteCompra(Compras oc)
        {
            try
            {
                ComprasCD.EliminarCompra(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al eliminar Compra en la BD", ex);
            }
        }
    }
}
