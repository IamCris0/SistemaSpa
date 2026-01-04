using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Empleados = Entidades.Administracion.Empleados;

namespace Logica.Administracion
{
    public class EmpleadosLN
    {
        public List<Empleados> ShowEmpleados()
        {
            List<Empleados> lista = new List<Empleados>();
            Empleados oe;

            try
            {
                List<Datos.LinqtoSql.Empleados> auxLista =
                    EmpleadosCD.ListarEmpleados();

                foreach (Datos.LinqtoSql.Empleados op in auxLista)
                {
                    oe = new Empleados(
                        op.EmpleadoID,
                        op.Nombre,
                        op.Apellido,
                        op.Email,
                        op.Telefono,
                        op.Cargo,
                        op.FechaContratacion,
                        (decimal)op.Salario,
                        op.Estado
                    );
                    lista.Add(oe);
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones(
                    "Error al mostrar Empleados sin filtro", ex);
            }
            finally
            {
            }

            return lista;
        }
        public List<Empleados> ShowEmpleadosFiltro(string valor)
        {
            List<Empleados> lista = new List<Empleados>();
            Empleados ow;

            try
            {
                List<CP_ListarEmpleadosFiltroResult> auxLista = EmpleadosCD.ListarEmpleadosFiltro(valor);

                foreach (CP_ListarEmpleadosFiltroResult op in auxLista)
                {
                    ow = new Empleados(
                        op.EmpleadoID,
                        op.Nombre,
                        op.Apellido,
                        op.Email,
                        op.Telefono,
                        op.Cargo,
                        op.FechaContratacion,
                        (decimal)op.Salario,
                        op.Estado
                    );
                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar Empleados con filtro en el procedimiento almacenado", ex);
            }
            finally
            {
            }

            return lista;
        }
        public bool InsertEmpleados(Empleados oe)
        {
            try
            {
                EmpleadosCD.InsertarEmpleados(oe);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar Empleados en la BD", ex);
            }
        }

        public bool UpdateEmpleados(Empleados oe)
        {
            try
            {
                EmpleadosCD.ModificarEmpleados(oe);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al actualizar Empleados en la BD", ex);
            }
        }

        public bool DeleteEmpleados(Empleados oe)
        {
            try
            {
                EmpleadosCD.EliminarEmpleados(oe);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al eliminar Empleados en la BD", ex);
            }
        }
    }
}
