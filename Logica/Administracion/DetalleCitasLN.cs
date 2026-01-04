using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DetalleCitas = Entidades.Administracion.DetalleCitas;

namespace Logica.Administracion
{
    public class DetalleCitasLN
    {
        public List<DetalleCitas> ShowDetalleCitas()
        {
            List<DetalleCitas> lista = new List<DetalleCitas>();
            DetalleCitas od;

            try
            {
                List<Datos.LinqtoSql.DetalleCitas> auxLista =
                    DetalleCitasCD.ListarDetalleCitas();

                foreach (Datos.LinqtoSql.DetalleCitas op in auxLista)
                {
                    od = new DetalleCitas(
                        op.DetalleCitaID,
                        (int)op.CitaID,
                        (int)op.ServicioID,
                        (decimal)op.PrecioServicio,
                        (decimal)op.Descuento
                    );
                    lista.Add(od);
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones(
                    "Error al mostrar DetalleCitas sin filtro", ex);
            }
            finally
            {
            }

            return lista;
        }
        public List<DetalleCitas> ShowDetalleCitasFiltro(string valor)
        {
            List<DetalleCitas> lista = new List<DetalleCitas>();
            DetalleCitas ow;

            try
            {
                List<CP_ListarDetalleCitasFiltroResult> auxLista = DetalleCitasCD.ListarDetalleCitasFiltro(valor);

                foreach (CP_ListarDetalleCitasFiltroResult op in auxLista)
                {
                    ow = new DetalleCitas(
                        op.DetalleCitaID,
                        (int)op.CitaID,
                        (int)op.ServicioID,
                        (decimal)op.PrecioServicio,
                        (decimal)op.Descuento
                    );
                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar DetalleCitas con filtro en el procedimiento almacenado", ex);
            }
            finally
            {
            }

            return lista;
        }
        public bool InsertDetalleCitas(DetalleCitas od)
        {
            try
            {
                DetalleCitasCD.InsertarDetalleCitas(od);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar DetalleCitas en la BD", ex);
            }
        }

        public bool UpdateDetalleCitas(DetalleCitas od)
        {
            try
            {
                DetalleCitasCD.ModificarDetalleCitas(od);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al actualizar DetalleCitas en la BD", ex);
            }
        }

        public bool DeleteDetalleCitas(DetalleCitas od)
        {
            try
            {
                DetalleCitasCD.EliminarDetalleCitas(od);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al eliminar DetalleCitas en la BD", ex);
            }
        }
    }
}
