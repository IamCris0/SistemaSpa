using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagosCitas = Entidades.Administracion.PagosCitas;

namespace Logica.Administracion
{
    public class PagosCitasLN
    {
  

        public List<PagosCitas> ShowPagosCitasFiltro(string valor)
        {
            List<PagosCitas> lista = new List<PagosCitas>();
            PagosCitas ow;

            try
            {
                List<CP_ListarPagosCitasFiltroResult> auxLista = PagosCitasCD.ListarPagosCitasFiltro(valor);

                foreach (CP_ListarPagosCitasFiltroResult op in auxLista)
                {
                    ow = new PagosCitas(
                  op.PagoID,
                  (int)op.CitaID,
                  (DateTime)op.FechaPago,
                  (double)op.Monto,
                  op.MetodoPago,
                  op.Referencia,
                  op.EstadoPago
                    );
                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar PagosCitas con filtro en el procedimiento almacenado", ex);
            }
            finally
            {
            }

            return lista;
        }
        public bool InsertPagosCitas(PagosCitas oc)
        {
            try
            {
                PagosCitasCD.InsertarPagosCitas(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar PagosCitas en la BD", ex);
            }
        }

        public bool UpdatePagosCitas(PagosCitas oc)
        {
            try
            {
                PagosCitasCD.ModificarPagosCitas(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al actualizar PagosCitas en la BD", ex);
            }
        }

        public bool DeletePagosCitas(PagosCitas oc)
        {
            try
            {
                PagosCitasCD.EliminarpPagosCitas(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al eliminar PagosCitas en la BD", ex);
            }
        }
    }
}
