using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using DetalleVentas = Entidades.Administracion.DetalleVentas;

namespace Logica.Administracion
{
    public class DetalleVentasLN
    {
        // LISTAR TODO
        public List<DetalleVentas> ShowDetalleVentas()
        {
            List<DetalleVentas> lista = new List<DetalleVentas>();

            try
            {
                List<Datos.LinqtoSql.DetalleVentas> auxLista =
                    DetalleVentasCD.ListarDetalleVentas();

                foreach (Datos.LinqtoSql.DetalleVentas op in auxLista)
                {
                    lista.Add(new DetalleVentas(
                        op.DetalleVentaID,
                        op.VentaID ?? 0,
                        op.ProductoID ?? 0,
                        op.Cantidad ?? 0,
                        op.PrecioUnitario ?? 0
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones(
                    "Error al mostrar DetalleVentas", ex);
            }

            return lista;
        }

        // LISTAR CON FILTRO
        public List<DetalleVentas> ShowDetalleVentasFiltro(string valor)
        {
            List<DetalleVentas> lista = new List<DetalleVentas>();

            try
            {
                List<CP_ListarDetalleVentasFiltroResult> auxLista =
                    DetalleVentasCD.ListarDetalleVentasFiltro(valor);

                foreach (CP_ListarDetalleVentasFiltroResult op in auxLista)
                {
                    lista.Add(new DetalleVentas(
                        op.DetalleVentaID,
                        op.VentaID ?? 0,
                        op.ProductoID ?? 0,
                        op.Cantidad ?? 0,
                        op.PrecioUnitario ?? 0
                    ));
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al mostrar DetalleVentas con filtro", ex);
            }

            return lista;
        }

        // INSERTAR
        public bool InsertDetalleVentas(DetalleVentas detalle)
        {
            try
            {
                DetalleVentasCD.InsertarDetalleVentas(detalle);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar DetalleVentas", ex);
            }
        }

        // MODIFICAR
        public bool UpdateDetalleVentas(DetalleVentas detalle)
        {
            try
            {
                DetalleVentasCD.ModificarDetalleVentas(detalle);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al modificar DetalleVentas", ex);
            }
        }

        // ELIMINAR
        public bool DeleteDetalleVentas(DetalleVentas detalle)
        {
            try
            {
                DetalleVentasCD.EliminarDetalleVentas(detalle);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al eliminar DetalleVentas", ex);
            }
        }
    }
}
