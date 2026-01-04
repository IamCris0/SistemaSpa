using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class ClientesCD
    {
        public static List<LinqtoSql.Clientes> ListarClientes()
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.Clientes.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla clientes", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void InsertarCliente(Entidades.Administracion.Clientes oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarClientes(
                        oc.ClienteID,
                        oc.Nombre,
                        oc.Apellido,
                        oc.Email,
                        oc.Telefono,
                        oc.Direccion,
                        oc.FechaNacimiento,
                        oc.FechaRegistro,
                        oc.Estado
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

        public static List<CP_ListarClientesFiltroResult> ListarClienteFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarClientesFiltro(val).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar el procedimiento listar clientes", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void ModificarCliente(Entidades.Administracion.Clientes oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarClientes(
                        oc.ClienteID,
                        oc.Nombre,
                        oc.Apellido,
                        oc.Email,
                        oc.Telefono,
                        oc.Direccion,
                        oc.FechaNacimiento,
                        oc.FechaRegistro,
                        oc.Estado
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
        public static void EliminarCliente(Entidades.Administracion.Clientes oc)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarClientes(oc.ClienteID);
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
