using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salas = Entidades.Administracion.Salas;
namespace Logica.Administracion
{
    public class SalasLN
    {
        public List<Salas> ShowSala()
        {
            List<Salas> lista = new List<Salas>();
            Salas oc;

            try
            {
                List<Datos.LinqtoSql.Salas> auxLista = SalasCD.ListarSalas();

                foreach (Datos.LinqtoSql.Salas op in auxLista)
                {
                    oc = new Salas(
                       op.SalaID,
                        op.NombreSala,
                        op.TipoSala,
                       (int)op.Capacidad,
                        op.Estado
                    );
                    lista.Add(oc);
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones(
                    "Error al mostrar Sala sin filtro", ex);
            }
            finally
            {
            }

            return lista;
        }
        public List<Salas> ShowSalasFiltro(string valor)
        {
            List<Salas> lista = new List<Salas>();
            Salas ow;

            try
            {
                List<CP_ListarSalasFiltroResult> auxLista = SalasCD.ListarSalaFiltro(valor);

                foreach (CP_ListarSalasFiltroResult op in auxLista)
                {
                    ow = new Salas(
                         op.SalaID,
                        op.NombreSala,
                        op.TipoSala,
                        (int)op.Capacidad,
                        op.Estado
                    );
                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar Salas con filtro en el procedimiento almacenado", ex);
            }
            finally
            {
            }

            return lista;
        }
        public bool InsertSala(Salas oc)
        {
            try
            {
                SalasCD.InsertarSala(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar Sala en la BD", ex);
            }
        }

        public bool UpdateSala(Salas oc)
        {
            try
            {
                SalasCD.ModificarSala(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al actualizar Sala en la BD", ex);
            }
        }

        public bool DeleteSala(Salas oc)
        {
            try
            {
                SalasCD.EliminarSala(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al eliminar Sala en la BD", ex);
            }

        }
    }
}
