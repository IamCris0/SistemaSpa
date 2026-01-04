using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas = Entidades.Administracion.Ventas;

namespace Logica.Administracion
{
    public class VentasLN
    {
        public List<Ventas> ShowVentas()
        {
            List<Ventas> lista = new List<Ventas>();
            Ventas oc;

            try
            {
                List<Datos.LinqtoSql.Ventas> auxLista =
                    VentasCD.ListarVentas();

                foreach (Datos.LinqtoSql.Ventas op in auxLista)
                {
                    oc = new Ventas(
                            op.VentaID,
                             (int)op.ClienteID,   
                             (int)op.EmpleadoID,
                             (DateTime)op.FechaVenta,
                            (double)op.Total,
                            op.MetodoPago,
                            op.Estado
                    );
                    lista.Add(oc);
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones(
                    "Error al mostrar Venta sin filtro", ex);
            }
            finally
            {
            }

            return lista;
        }
        public List<Ventas> ShowVentasFiltro(string valor)
        {
            List<Ventas> lista = new List<Ventas>();
            Ventas ow;

            try
            {
                List<CP_ListarVentasFiltroResult> auxLista = VentasCD.ListarVentaFiltro(valor);

                foreach (CP_ListarVentasFiltroResult op in auxLista)
                {
                    ow = new Ventas(
                         op.VentaID,
                             (int)op.ClienteID,
                             (int)op.EmpleadoID,
                             (DateTime)op.FechaVenta,
                            (double)op.Total,
                            op.MetodoPago,
                            op.Estado
                    );
                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar Ventas con filtro en el procedimiento almacenado", ex);
            }
            finally
            {
            }

            return lista;
        }
        public bool InsertVenta(Ventas oc)
        {
            try
            {
                VentasCD.InsertarVenta(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar Venta en la BD", ex);
            }
        }

        public bool UpdateVenta(Ventas oc)
        {
            try
            {
                VentasCD.ModificarVenta(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al actualizar Venta en la BD", ex);
            }
        }

        public bool DeleteVenta(Ventas oc)
        {
            try
            {
                VentasCD.EliminarVenta(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al eliminar Venta en la BD", ex);
            }
        }
    }
}
