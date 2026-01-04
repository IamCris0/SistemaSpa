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
    public partial class frmAdminDetalleCompras : Form
    {
        public frmAdminDetalleCompras()
        {
            InitializeComponent();
        }
        DetalleComprasLN oln = new DetalleComprasLN();

        // ================= LISTAR =================
        public void ListarDetalleCompras(string val)
        {
            dataGridView1.DataSource = oln.ShowDetalleComprasFiltro(val);
        }

        // ================= NUEVO =================
        public void Nuevo()
        {
            try
            {
                frmEditDetalleCompras frm = new frmEditDetalleCompras();
                frm.Text = "Insertar Detalle de Venta";
                frm.label1.Text = "Insertar Detalle de Venta";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    DetalleCompras obj = frm.CrearObjeto();
                    oln.InsertDetalleCompras(obj);

                    frm.Close();
                    ListarDetalleCompras(textBox1.Text);
                    toolStripStatusLabel1.Text = "Detalle de venta ingresado correctamente";
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al insertar DetalleCompras. " + ex.Message;
            }
        }

        // ================= MODIFICAR =================
        public void Actualizar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    frmEditDetalleCompras frm = new frmEditDetalleCompras();
                    frm.Text = "Modificar Detalle de Venta";

                    DetalleCompras obj = dataGridView1.CurrentRow.DataBoundItem as DetalleCompras;
                    frm.setDatos(obj);

                    frm.ShowDialog();

                    if (frm.DialogResult == DialogResult.OK)
                    {
                        DetalleCompras objEditado = frm.CrearObjeto();
                        oln.UpdateDetalleCompras(objEditado);

                        ListarDetalleCompras(textBox1.Text);
                        toolStripStatusLabel1.Text = "Detalle de venta actualizado correctamente";
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
                MessageBox.Show("Error al modificar DetalleCompras: " + ex.Message);
            }
        }

        // ================= ELIMINAR =================
        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var resp = MessageBox.Show(
                        "¿Desea eliminar el detalle de venta?",
                        "Eliminar Detalle de Venta",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (resp == DialogResult.Yes)
                    {
                        DetalleCompras obj = dataGridView1.CurrentRow.DataBoundItem as DetalleCompras;
                        oln.DeleteDetalleCompras(obj);

                        ListarDetalleCompras(textBox1.Text);
                        toolStripStatusLabel1.Text = "Detalle de venta eliminado correctamente";
                        timer1.Start();
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione la fila a eliminar.");
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al eliminar DetalleCompras. " + ex.Message;
            }
        }

        // ================= EVENTOS =================
        private void frmAdminDetalleCompras_Load(object sender, EventArgs e)
        {
            ListarDetalleCompras("");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarDetalleCompras(textBox1.Text);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "...";
        }
    }
}
