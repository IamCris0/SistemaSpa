using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GastosOperativos = Entidades.Administracion.GastosOperativos;

namespace Logica.Administracion
{
    public class GastosOperativosLN
    {
        public List<GastosOperativos> ShowGastosOperativos()
        {
            List<GastosOperativos> lista = new List<GastosOperativos>();
            GastosOperativos og;

            try
            {
                List<Datos.LinqtoSql.GastosOperativos> auxLista =
                    GastosOperativosCD.ListarGastosOperativos();

                foreach (Datos.LinqtoSql.GastosOperativos op in auxLista)
                {
                    og = new GastosOperativos(
                        op.GastoID,
                        (int)op.ProveedorID,
                        op.TipoGasto,
                        op.Descripcion,
                        (decimal)op.Monto,
                        (DateTime)op.FechaGasto,
                        op.MetodoPago,
                        op.Comprobante,
                        op.Estado,
                        (DateTime)op.FechaRegistro

                    );
                    lista.Add(og);
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones(
                    "Error al mostrar GastosOperativos sin filtro", ex);
            }
            finally
            {
            }

            return lista;
        }
        public List<GastosOperativos> ShowGastosOperativosFiltro(string valor)
        {
            List<GastosOperativos> lista = new List<GastosOperativos>();
            GastosOperativos ow;

            try
            {
                List<CP_ListarGastosOperativosFiltroResult> auxLista = GastosOperativosCD.ListarGastosOperativosFiltro(valor);

                foreach (CP_ListarGastosOperativosFiltroResult op in auxLista)
                {
                    ow = new GastosOperativos(
                        op.GastoID,
                        (int)op.ProveedorID,
                        op.TipoGasto,
                        op.Descripcion,
                        (decimal)op.Monto,
                        (DateTime)op.FechaGasto,
                        op.MetodoPago,
                        op.Comprobante,
                        op.Estado,
                        (DateTime)op.FechaRegistro
                    );
                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar GastosOperativos con filtro en el procedimiento almacenado", ex);
            }
            finally
            {
            }

            return lista;
        }
        public bool InsertGastosOperativos(GastosOperativos og)
        {
            try
            {
                GastosOperativosCD.InsertarGastosOperativos(og);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar GastosOperativos en la BD", ex);
            }
        }

        public bool UpdateGastosOperativos(GastosOperativos og)
        {
            try
            {
                GastosOperativosCD.ModificarGastosOperativos(og);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al actualizar GastosOperativos en la BD", ex);
            }
        }

        public bool DeleteGastosOperativos(GastosOperativos og)
        {
            try
            {
                GastosOperativosCD.EliminarGastosOperativos(og);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al eliminar GastosOperativos en la BD", ex);
            }
        }
    }
}
