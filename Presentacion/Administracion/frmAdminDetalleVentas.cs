using Entidades.Administracion;
using Logica.Administracion;
using System;
using System.Windows.Forms;

namespace Presentacion.Administracion
{
    public partial class frmAdminDetalleVentas : Form
    {
        public frmAdminDetalleVentas()
        {
            InitializeComponent();
        }

        DetalleVentasLN oln = new DetalleVentasLN();

        // ================= LISTAR =================
        public void ListarDetalleVentas(string val)
        {
            dataGridView1.DataSource = oln.ShowDetalleVentasFiltro(val);
        }

        // ================= NUEVO =================
        public void Nuevo()
        {
            try
            {
                frmEditDetalleVentas frm = new frmEditDetalleVentas();
                frm.Text = "Insertar Detalle de Venta";
                frm.label1.Text = "Insertar Detalle de Venta";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    DetalleVentas obj = frm.CrearObjeto();
                    oln.InsertDetalleVentas(obj);

                    frm.Close();
                    ListarDetalleVentas(textBox1.Text);
                    toolStripStatusLabel1.Text = "Detalle de venta ingresado correctamente";
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al insertar DetalleVentas. " + ex.Message;
            }
        }

        // ================= MODIFICAR =================
        public void Actualizar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    frmEditDetalleVentas frm = new frmEditDetalleVentas();
                    frm.Text = "Modificar Detalle de Venta";

                    DetalleVentas obj = dataGridView1.CurrentRow.DataBoundItem as DetalleVentas;
                    frm.setDatos(obj);

                    frm.ShowDialog();

                    if (frm.DialogResult == DialogResult.OK)
                    {
                        DetalleVentas objEditado = frm.CrearObjeto();
                        oln.UpdateDetalleVentas(objEditado);

                        ListarDetalleVentas(textBox1.Text);
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
                MessageBox.Show("Error al modificar DetalleVentas: " + ex.Message);
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
                        DetalleVentas obj = dataGridView1.CurrentRow.DataBoundItem as DetalleVentas;
                        oln.DeleteDetalleVentas(obj);

                        ListarDetalleVentas(textBox1.Text);
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
                toolStripStatusLabel1.Text = "Error al eliminar DetalleVentas. " + ex.Message;
            }
        }

        // ================= EVENTOS =================
        private void frmAdminDetalleVentas_Load(object sender, EventArgs e)
        {
            ListarDetalleVentas("");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarDetalleVentas(textBox1.Text);
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
