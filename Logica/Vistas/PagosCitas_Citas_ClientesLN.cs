using Datos.Administracion;
using Datos.LinqtoSql;
using Datos.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagosCitasVista = Entidades.Vistas.PagoCitas_Citas_Cliente;
namespace Logica.Vistas
{
    public class PagosCitas_Citas_ClientesLN
    {

        public List<PagosCitasVista> ShowPagosCitasVistaFiltro(string valor)
        {
            List<PagosCitasVista> lista = new List<PagosCitasVista>();
            PagosCitasVista ow;

            try
            {
                var auxLista = PagoCitas_Citas_ClientesCD.ListarPagosCitasVistaFiltro(valor);

                foreach (var op in auxLista)
                {
                    ow = new PagosCitasVista(
                        op.PagoID,
                        (int)op.CitaID,
                        op.Cliente,
                        (DateTime)op.FechaPago,
                        (decimal)op.Monto,
                        op.MetodoPago,
                        op.Referencia,
                        op.EstadoPago
                    );

                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al mostrar PagosCitas Vista", ex);
            }

            finally
            {
            }

            return lista;
        }
    }
}
