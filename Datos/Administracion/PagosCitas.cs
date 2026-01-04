using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class PagosCitas
    {
        public static List<LinqtoSql.PagosCitas> ListarPagosCitas()
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.PagosCitas.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla PagosCitas", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void InsertarCliente(Entidades.Administracion.PagosCitas oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarPagosCitas(
                        oc.PagoID,
                        oc.CitaID,
                        oc.FechaPago,
                       (decimal)  oc.Monto,
                        oc.MetodoPago,
                        oc.Referencia,
                        oc.EstadoPago
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

        public static List<CP_ListarPagosCitasFiltroResult> ListarClienteFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarPagosCitasFiltro(val).ToList();
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
        public static void ModificarCliente(Entidades.Administracion.PagosCitas oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarPagosCitas(
                          oc.PagoID,
                        oc.CitaID,
                        oc.FechaPago,
                       (decimal)oc.Monto,
                        oc.MetodoPago,
                        oc.Referencia,
                        oc.EstadoPago
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
        public static void EliminarCliente(Entidades.Administracion.PagosCitas oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarPagosCitas(oc.PagoID);
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
