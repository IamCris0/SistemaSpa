using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Vistas;

namespace Datos.Vistas
{
    public class PagoCitas_Citas_ClientesCD
    {


        public static List<CP_ListarPagosCitasVistaFiltroResult> ListarPagosCitasVistaFiltro(string val)
        {
            DataClasses1DataContext DB = null;
            try
            {
                using (DB = new DataClasses1DataContext())
                {
                    return DB.CP_ListarPagosCitasVistaFiltro(val).ToList();
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
    }
}
