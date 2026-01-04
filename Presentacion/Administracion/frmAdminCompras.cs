using Entidades.Administracion;
using Logica.Administracion;
using System;
using System.Windows.Forms;

namespace Presentacion.Administracion
{
    public partial class frmAdminCompras : Form
    {
        public frmAdminCompras()
        {
            InitializeComponent();
        }
        CompraLN oln = new CompraLN();
        public Compras obj = new Compras();

        public void ListarCompras(string val)
        {
            dataGridView1.DataSource = oln.ShowComprasFiltro(val);

        }

        public void Nuevo()
        {
            try
            {
                frmEditCompras frm = new frmEditCompras();
                frm.Text = "Insertar Compra";
                frm.label1.Text = "Insertar Compra";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    Compras oc = frm.CrearObjeto();
                    oln.InsertCompra(oc);
                    frm.Close();
                    toolStripStatusLabel1.Text = "Compra ingresado correctamente";
                    ListarCompras(textBox1.Text);
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al insertar Compra. " + ex.Message;
            }

        }

        public void Actualizar()
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    frmEditCompras frm = new frmEditCompras();
                    frm.Text = "Modificar Categoria";

                    Compras obj = dataGridView1.CurrentRow.DataBoundItem as Compras;
                    frm.setDatos(obj);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        Compras oe = frm.CrearObjeto();
                        oln.UpdateCompra(oe);
                        ListarCompras(textBox1.Text);
                        toolStripStatusLabel1.Text = "Compra actualizado correctamente";
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
                MessageBox.Show("Error al modificar Compra: " + ex.Message);
            }
        }

        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var resp = MessageBox.Show(
                        "¿Desea eliminar el Compra?",
                        "Eliminar Compra",
                        MessageBoxButtons.YesNo);

                    if (resp == DialogResult.Yes)
                    {
                        Compras obj = dataGridView1.CurrentRow.DataBoundItem as Compras;
                        oln.DeleteCompra(obj);
                        ListarCompras(textBox1.Text);
                        toolStripStatusLabel1.Text = "Compra eliminado correctamente";
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
                toolStripStatusLabel1.Text = "Error al eliminar Compra. " + ex.Message;
            }
        }
        private void frmAdminCompras_Load(object sender, EventArgs e)
        {
            ListarCompras("");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarCompras(textBox1.Text);
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
    }
}
