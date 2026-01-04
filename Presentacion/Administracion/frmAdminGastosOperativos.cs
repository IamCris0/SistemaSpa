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
    public partial class frmAdminGastosOperativos : Form
    {
        public frmAdminGastosOperativos()
        {
            InitializeComponent();
        }
        GastosOperativosLN oln = new GastosOperativosLN();
        public GastosOperativos obj = new GastosOperativos();

        public void ListarGastosOperativos(string val)
        {
            dataGridView1.DataSource = oln.ShowGastosOperativosFiltro(val);

        }

        public void Nuevo()
        {
            try
            {
                frmEditGastosOperativos frm = new frmEditGastosOperativos();
                frm.Text = "Insertar GastosOperativos";
                frm.label1.Text = "Insertar GastosOperativos";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    GastosOperativos oc = frm.CrearObjeto();
                    oln.InsertGastosOperativos(oc);
                    frm.Close();
                    toolStripStatusLabel1.Text = "GastosOperativos ingresado correctamente";
                    ListarGastosOperativos(textBox1.Text);
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al insertar GastosOperativos. " + ex.Message;
            }

        }

        public void Actualizar()
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    frmEditGastosOperativos frm = new frmEditGastosOperativos();
                    frm.Text = "Modificar Categoria";

                    GastosOperativos obj = dataGridView1.CurrentRow.DataBoundItem as GastosOperativos;
                    frm.setDatos(obj);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        GastosOperativos oe = frm.CrearObjeto();
                        oln.UpdateGastosOperativos(oe);
                        ListarGastosOperativos(textBox1.Text);
                        toolStripStatusLabel1.Text = "GastosOperativos actualizado correctamente";
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
                MessageBox.Show("Error al modificar GastosOperativos: " + ex.Message);
            }
        }

        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var resp = MessageBox.Show(
                        "¿Desea eliminar el GastosOperativos?",
                        "Eliminar GastosOperativos",
                        MessageBoxButtons.YesNo);

                    if (resp == DialogResult.Yes)
                    {
                        GastosOperativos obj = dataGridView1.CurrentRow.DataBoundItem as GastosOperativos;
                        oln.DeleteGastosOperativos(obj);
                        ListarGastosOperativos(textBox1.Text);
                        toolStripStatusLabel1.Text = "GastosOperativos eliminado correctamente";
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
                toolStripStatusLabel1.Text = "Error al eliminar GastosOperativos. " + ex.Message;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "...";
        }

        private void frmAdminGastosOperativos_Load(object sender, EventArgs e)
        {
            ListarGastosOperativos("");
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
