using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Administracion
{
    public class GastosOperativosCD
    {
        public static List<GastosOperativos> ListarGastosOperativos()
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.GastosOperativos.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar la tabla GastosOperativos", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static List<CP_ListarGastosOperativosFiltroResult> ListarGastosOperativosFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarGastosOperativosFiltro(val).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al listar el procedimiento listar gastos operativos", ex);
            }
            finally
            {
                DB = null;
            }
        }
        public static void InsertarGastosOperativos(Entidades.Administracion.GastosOperativos op)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_InsertarGastosOperativos(
                        op.GastoID,
                        op.ProveedorID,
                        op.TipoGasto,
                        op.Descripcion,
                        op.Monto,
                        op.FechaGasto,
                        op.MetodoPago,
                        op.Comprobante,
                        op.Estado,
                        op.FechaRegistro
                        
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al insertar GastosOperativos", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static void ModificarGastosOperativos(Entidades.Administracion.GastosOperativos op)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_ModificarGastosOperativos(
                        op.GastoID,
                        op.ProveedorID,
                        op.TipoGasto,
                        op.Descripcion,
                        op.Monto,
                        op.FechaGasto,
                        op.MetodoPago,

                        op.Comprobante,
                        op.Estado,
                        op.FechaRegistro
                        
                    );
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al modificar GastosOperativos", ex);
            }
            finally
            {
                DB = null;
            }
        }

        public static void EliminarGastosOperativos(Entidades.Administracion.GastosOperativos op)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    DB.CP_EliminarGastosOperativos(op.GastoID);
                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones("Error al eliminar GastosOperativos", ex);
            }
            finally
            {
                DB = null;
            }
        }
    }
}
