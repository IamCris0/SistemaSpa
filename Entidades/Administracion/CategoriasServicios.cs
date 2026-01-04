using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Administracion
{
    public class CategoriasServicios
    {
        private int categoriaID;
        private string nombreCategoria;
        private string descripcion;
        private string estado;

        public CategoriasServicios()
        {
        }

        public CategoriasServicios(int categoriaID, string nombreCategoria,
                                 string descripcion, string estado)
        {
            this.CategoriaID = categoriaID;
            this.NombreCategoria = nombreCategoria;
            this.Descripcion = descripcion;
            this.Estado = estado;
        }

        public int CategoriaID { get => categoriaID; set => categoriaID = value; }
        public string NombreCategoria { get => nombreCategoria; set => nombreCategoria = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Estado { get => estado; set => estado = value; }
    }
}
