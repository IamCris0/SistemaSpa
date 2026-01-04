using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios = Entidades.Administracion.Servicios;

namespace Logica.Administracion
{
    public class ServiciosLN
    {
        public List<Servicios> ShowServicios()
        {
            List<Servicios> lista = new List<Servicios>();
            Servicios oc;

            try
            {
                List<Datos.LinqtoSql.Servicios> auxLista = ServiciosCD.ListarServicios();

                foreach (Datos.LinqtoSql.Servicios op in auxLista)
                {
                    oc = new Servicios(
                       op.ServicioID,
                        (int)op.CategoriaID,
                        op.NombreServicio,
                        op.Descripcion,
                        op.Duracion,
                        (double)op.Precio,
                        op.Estado
                    );
                    lista.Add(oc);
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones(
                    "Error al mostrar Servicios sin filtro", ex);
            }
            finally
            {
            }

            return lista;
        }
        public List<Servicios> ShowServiciosFiltro(string valor)
        {
            List<Servicios> lista = new List<Servicios>();
            Servicios ow;

            try
            {
                List<CP_ListarServiciosFiltroResult> auxLista = ServiciosCD.ListarServicioFiltro(valor);

                foreach (CP_ListarServiciosFiltroResult op in auxLista)
                {
                    ow = new Servicios(
                   op.ServicioID,
                        (int)op.CategoriaID,
                        op.NombreServicio,
                        op.Descripcion,
                        op.Duracion,
                        (double)op.Precio,
                        op.Estado

                    );
                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar Servicios con filtro en el procedimiento almacenado", ex);
            }
            finally
            {
            }

            return lista;
        }
        public bool InsertServicios(Servicios oc)
        {
            try
            {
                ServiciosCD.InsertarServicio(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar Servicios en la BD", ex);
            }
        }

        public bool UpdateServicios(Servicios oc)
        {
            try
            {
                ServiciosCD.ModificarServicio(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al actualizar Servicios en la BD", ex);
            }
        }

        public bool DeleteServicios(Servicios oc)
        {
            try
            {
                ServiciosCD.EliminarServicio(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al eliminar Servicios en la BD", ex);
            }
        }
    }
}
