using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class ClientesMembresiasCD
    {
        public static List<ClientesMembresias> ListarClienteMembresias()
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.ClientesMembresias.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla ClienteMembresias", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static List<CP_ListarClientesMembresiasFiltroResult> ListarClientesMembresiasFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarClientesMembresiasFiltro(val).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar el procedimiento listar cliente membresia", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void InsertarClienteMembresias(Entidades.Administracion.ClientesMembresias op)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarClientesMembresias(
                        op.ClienteMembresiaID,
                        op.ClienteID,
                        op.MembresiaID,
                        op.FechaInicio,
                        op.FechaFin,
                        op.EstadoMembresia,
                        op.FechaRegistro
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al insertar ClienteMembresias", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static void ModificarClienteMembresias(Entidades.Administracion.ClientesMembresias op)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarClientesMembresias(
                        op.ClienteMembresiaID,
                        op.ClienteID,
                        op.MembresiaID,
                        op.FechaInicio,
                        op.FechaFin,
                        op.EstadoMembresia,
                        op.FechaRegistro
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al modificar ClienteMembresias", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static void EliminarClienteMembresias(Entidades.Administracion.ClientesMembresias op)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarClientesMembresias(op.ClienteMembresiaID);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar ClienteMembresias", ex);
            }
            finally
            {
                DB = null;
            }
        }
    }
}
