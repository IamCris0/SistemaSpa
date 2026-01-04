using Entidades.Administracion;
using Logica.Administracion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Administracion
{
    public partial class frmAdminProducto : Form
    {
        public frmAdminProducto()
        {
            InitializeComponent();
        }
        ProductosLN oln = new ProductosLN();
        public Productos obj = new Productos();

        public void ListarProductos(string val)
        {
            dataGridView1.DataSource = oln.ShowProductosFiltro(val);

        }

        public void Nuevo()
        {
            try
            {
                frmEditProductos frm = new frmEditProductos();
                frm.Text = "Insertar Producto";
                frm.label1.Text = "Insertar Producto";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    Productos oc = frm.CrearObjeto();
                    oln.InsertProducto(oc);
                    frm.Close();
                    toolStripStatusLabel1.Text = "Producto ingresado correctamente";
                    ListarProductos(textBox1.Text);
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al insertar Producto. " + ex.Message;
            }

        }

        public void Actualizar()
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    frmEditProductos frm = new frmEditProductos();
                    frm.Text = "Modificar Categoria";

                    Productos obj = dataGridView1.CurrentRow.DataBoundItem as Productos;
                    frm.setDatos(obj);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        Productos oe = frm.CrearObjeto();
                        oln.UpdateProducto(oe);
                        ListarProductos(textBox1.Text);
                        toolStripStatusLabel1.Text = "Producto actualizado correctamente";
                        timer1.Start();
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione la fila a modificar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar Producto: " + ex.Message);
            }
        }

        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var resp = MessageBox.Show(
                        "¿Desea eliminar el Producto?",
                        "Eliminar Producto",
                        MessageBoxButtons.YesNo);

                    if (resp == DialogResult.Yes)
                    {
                        Productos obj = dataGridView1.CurrentRow.DataBoundItem as Productos;
                        oln.DeleteProducto(obj);
                        ListarProductos(textBox1.Text);
                        toolStripStatusLabel1.Text = "Producto eliminado correctamente";
                        timer1.Start();
                    }
                    else
                        MessageBox.Show("Eliminación cancelada.");
                }
                else
                {
                    MessageBox.Show("Seleccione la fila a eliminar.");
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al eliminar Producto. " + ex.Message;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "...";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarProductos(textBox1.Text);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void frmAdminProducto_Load(object sender, EventArgs e)
        {
            ListarProductos("");
        }
    }
}
