using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class EmpleadosCD
    {
        public static List<Empleados> ListarEmpleados()
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.Empleados.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla Empleados", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static List<CP_ListarEmpleadosFiltroResult> ListarEmpleadosFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarEmpleadosFiltro(val).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar el procedimiento listar empleado", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void InsertarEmpleados(Entidades.Administracion.Empleados op)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarEmpleados(
                        op.EmpleadoID,
                        op.Nombre,
                        op.Apellido,
                        op.Email,
                        op.Telefono,
                        op.Cargo,
                        op.FechaContratacion,
                        op.Salario,
                        op.Estado
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al insertar Empleados", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static void ModificarEmpleados(Entidades.Administracion.Empleados op)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarEmpleados(
                        op.EmpleadoID,
                        op.Nombre,
                        op.Apellido,
                        op.Email,
                        op.Telefono,
                        op.Cargo,
                        op.FechaContratacion,
                        op.Salario,
                        op.Estado
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al modificar Empleados", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static void EliminarEmpleados(Entidades.Administracion.Empleados op)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarEmpleados(op.EmpleadoID);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar Empleados", ex);
            }
            finally
            {
                DB = null;
            }
        }
    }
}
