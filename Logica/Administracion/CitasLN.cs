using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Citas = Entidades.Administracion.Citas;
namespace Logica.Administracion
{
    public class CitasLN
    {
        public List<Citas> ShowCitas()
        {
            List<Citas> lista = new List<Citas>();
            Citas oc;

            try
            {
                List<Datos.LinqtoSql.Citas> auxLista =
                    CitasCD.ListarCitas();

                foreach (Datos.LinqtoSql.Citas op in auxLista)
                {
                    oc = new Citas(
                        op.CitaID,
                        (int)op.ClienteID,
                        (int)op.EmpleadoID,
                        (int)op.SalaID,
                        op.FechaCita,
                        op.HoraInicio,
                        op.HoraFin,
                        op.EstadoCita,
                        op.Observaciones,
                        (DateTime)op.FechaCreacion
                    );
                    lista.Add(oc);
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones(
                    "Error al mostrar Citas sin filtro", ex);
            }
            finally
            {
            }

            return lista;
        }
        public List<Citas> ShowCitasFiltro(string valor)
        {
            List<Citas> lista = new List<Citas>();
            Citas ow;

            try
            {
                List<CP_ListarCitasFiltroResult> auxLista = CitasCD.ListarCitasFiltro(valor);

                foreach (CP_ListarCitasFiltroResult op in auxLista)
                {
                    ow = new Citas(
                        op.CitaID,
                        (int)op.ClienteID,
                        (int)op.EmpleadoID,
                        (int)op.SalaID,
                        op.FechaCita,
                        op.HoraInicio,
                        op.HoraFin,
                        op.EstadoCita,
                        op.Observaciones,
                        (DateTime)op.FechaCreacion
                    );
                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar Citas con filtro en el procedimiento almacenado", ex);
            }
            finally
            {
            }

            return lista;
        }
        public bool InsertCita(Citas oc)
        {
            try
            {
                CitasCD.InsertarCita(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar Cita en la BD", ex);
            }
        }

        public bool UpdateCita(Citas oc)
        {
            try
            {
                CitasCD.ModificarCita(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al actualizar Cita en la BD", ex);
            }
        }

        public bool DeleteCita(Citas oc)
        {
            try
            {
                CitasCD.EliminarCita(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al eliminar Cita en la BD", ex);
            }
        }
    }
}
