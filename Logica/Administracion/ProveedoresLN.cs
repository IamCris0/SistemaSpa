using Datos;
using Datos.Administracion;
using Datos.LinqtoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proveedores = Entidades.Administracion.Proveedores;

namespace Logica.Administracion
{
    public class ProveedoresLN
    {
        public List<Proveedores> ShowProveedores()
        {
            List<Proveedores> lista = new List<Proveedores>();
            Proveedores oc;

            try
            {
                List<Datos.LinqtoSql.Proveedores> auxLista = ProveedoresCD.ListarProveedores();

                foreach (Datos.LinqtoSql.Proveedores op in auxLista)
                {
                    oc = new Proveedores(
                    op.ProveedorID,
                        op.NombreProveedor,
                        op.Contacto,
                        op.Telefono,
                        op.Email,
                        op.Direccion,
                        op.Estado

                    );
                    lista.Add(oc);
                }
            }
            catch (Exception ex)
            {
                throw new DatosExcepciones(
                    "Error al mostrar Proveedores sin filtro", ex);
            }
            finally
            {
            }

            return lista;
        }
        public List<Proveedores> ShowProveedoresFiltro(string valor)
        {
            List<Proveedores> lista = new List<Proveedores>();
            Proveedores ow;

            try
            {
                List<CP_ListarProveedoresFiltroResult> auxLista = ProveedoresCD.ListarProveedoreFiltro(valor);

                foreach (CP_ListarProveedoresFiltroResult op in auxLista)
                {
                    ow = new Proveedores(
                    op.ProveedorID,
                        op.NombreProveedor,
                        op.Contacto,
                        op.Telefono,
                        op.Email,
                        op.Direccion,
                        op.Estado

                    );
                    lista.Add(ow);
                }
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones("Error al mostrar Proveedores con filtro en el procedimiento almacenado", ex);
            }
            finally
            {
            }

            return lista;
        }
        public bool InsertProveedores(Proveedores oc)
        {
            try
            {
                ProveedoresCD.InsertarProveedore(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al insertar Proveedores en la BD", ex);
            }
        }

        public bool UpdateProveedores(Proveedores oc)
        {
            try
            {
                ProveedoresCD.ModificarProveedore(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al actualizar Proveedores en la BD", ex);
            }
        }

        public bool DeleteProveedores(Proveedores oc)
        {
            try
            {
                ProveedoresCD.EliminarProveedore(oc);
                return true;
            }
            catch (Exception ex)
            {
                throw new LogicaExcepciones(
                    "Error al eliminar Proveedores en la BD", ex);
            }
        }
    }
}
