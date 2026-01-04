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
    public partial class frmAdminCategoriasServicios : Form
    {
        public frmAdminCategoriasServicios()
        {
            InitializeComponent();
        }
        CategoriasServiciosLN oln = new CategoriasServiciosLN();
        public CategoriasServicios obj = new CategoriasServicios();

        public void ListarCategoriasServicios(string val)
        {
            dataGridView1.DataSource = oln.ShowCategoriasServiciosFiltro(val);

        }

        public void Nuevo()
        {
            try
            {
                frmEditCategoriasServicios frm = new frmEditCategoriasServicios();
                frm.Text = "Insertar CategoriasServicios";
                frm.label1.Text = "Insertar CategoriasServicios";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    CategoriasServicios oc = frm.CrearObjeto();
                    oln.InsertCategoriaServicio(oc);
                    frm.Close();
                    toolStripStatusLabel1.Text = "CategoriasServicios ingresado correctamente";
                    ListarCategoriasServicios(textBox1.Text);
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al insertar CategoriasServicios. " + ex.Message;
            }

        }

        public void Actualizar()
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    frmEditCategoriasServicios frm = new frmEditCategoriasServicios();
                    frm.Text = "Modificar Categoria";

                    CategoriasServicios obj = dataGridView1.CurrentRow.DataBoundItem as CategoriasServicios;
                    frm.setDatos(obj);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        CategoriasServicios oe = frm.CrearObjeto();
                        oln.UpdateCategoriaServicio(oe);
                        ListarCategoriasServicios(textBox1.Text);
                        toolStripStatusLabel1.Text = "CategoriasServicios actualizado correctamente";
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
                MessageBox.Show("Error al modificar CategoriasServicios: " + ex.Message);
            }
        }

        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var resp = MessageBox.Show(
                        "¿Desea eliminar el CategoriasServicios?",
                        "Eliminar CategoriasServicios",
                        MessageBoxButtons.YesNo);

                    if (resp == DialogResult.Yes)
                    {
                        CategoriasServicios obj = dataGridView1.CurrentRow.DataBoundItem as CategoriasServicios;
                        oln.DeleteCategoriaServicio(obj);
                        ListarCategoriasServicios(textBox1.Text);
                        toolStripStatusLabel1.Text = "CategoriasServicios eliminado correctamente";
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
                toolStripStatusLabel1.Text = "Error al eliminar CategoriasServicios. " + ex.Message;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarCategoriasServicios(textBox1.Text);
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

        private void frmAdminCategoriasServicios_Load(object sender, EventArgs e)
        {
            ListarCategoriasServicios("");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "...";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
