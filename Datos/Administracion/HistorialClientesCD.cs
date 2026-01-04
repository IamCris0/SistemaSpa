using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class HistorialClientesCD
    {
        public static List<LinqtoSql.HistorialClientes> ListarHistorialClientes()
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.HistorialClientes.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla HistorialClientes", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void InsertarHistorialClientes(Entidades.Administracion.HistorialClientes oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarHistorialClientes(
                        oc.HistorialID,
                        oc.ClienteID,
                        oc.CitaID,
                        oc.FechaVisita,
                        oc.Observaciones,
                        oc.Calificacion,
                        oc.AlergiasProcedimiento,
                        oc.ResultadosTratamiento,
                        oc.FechaRegistro

                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al insertar cliente", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static List<CP_ListarHistorialClientesFiltroResult> ListarHistorialClienteFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarHistorialClientesFiltro(val).ToList();
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
        public static void ModificarHistorialCliente(Entidades.Administracion.HistorialClientes oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarHistorialClientes(
                        oc.HistorialID,
                        oc.ClienteID,
                        oc.CitaID,
                        oc.FechaVisita,
                        oc.Observaciones,
                        oc.Calificacion,
                        oc.AlergiasProcedimiento,
                        oc.ResultadosTratamiento,
                        oc.FechaRegistro
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al modificar cliente", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void EliminarHistorialCliente(Entidades.Administracion.HistorialClientes oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarHistorialClientes(oc.HistorialID);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar cliente", ex);
            }
            finally
            {
                DB = null;
            }
        }
    }
}
