using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoriasServicios = Entidades.Administracion.CategoriasServicios;
namespace Logica.Administracion
{
    public class CategoriasServiciosLN
    {
        public List<CategoriasServicios> ShowCategoriaServicio()
        {
            List<CategoriasServicios> lista = new List<CategoriasServicios>();
            CategoriasServicios oc;

            try
            {
                List<Datos.LinqtoSql.CategoriasServicios> auxLista =
                    CategoriasServiciosCD.ListarCategoriaServicio();

                foreach (Datos.LinqtoSql.CategoriasServicios op in auxLista)
                {
                    oc = new CategoriasServicios(
                        op.CategoriaID,
                        op.NombreCategoria,
                        op.Descripcion,
                        op.Estado
                    );
                    lista.Add(oc);
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones(
                    "Error al mostrar CategoriaServicio sin filtro", ex);
            }
            finally
            {
            }

            return lista;
        }
        public List<CategoriasServicios> ShowCategoriasServiciosFiltro(string valor)
        {
            List<CategoriasServicios> lista = new List<CategoriasServicios>();
            CategoriasServicios ow;

            try
            {
                List<CP_ListarCategoriasServiciosFiltroResult> auxLista = CategoriasServiciosCD.ListarCategoriasServiciosFiltro(valor);

                foreach (CP_ListarCategoriasServiciosFiltroResult op in auxLista)
                {
                    ow = new CategoriasServicios(
                        op.CategoriaID,
                        op.NombreCategoria,
                        op.Descripcion,
                        op.Estado
                    );
                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar CategoriasServicios con filtro en el procedimiento almacenado", ex);
            }
            finally
            {
            }

            return lista;
        }

        public bool InsertCategoriaServicio(CategoriasServicios oc)
        {
            try
            {
                CategoriasServiciosCD.InsertarCategoriaServicio(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar CategoriaServicio en la BD", ex);
            }
        }

        public bool UpdateCategoriaServicio(CategoriasServicios oc)
        {
            try
            {
                CategoriasServiciosCD.ModificarCategoriaServicio(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al actualizar CategoriaServicio en la BD", ex);
            }
        }

        public bool DeleteCategoriaServicio(CategoriasServicios oc)
        {
            try
            {
                CategoriasServiciosCD.EliminarCategoriaServicio(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al eliminar CategoriaServicio en la BD", ex);
            }
        }
    }
}
