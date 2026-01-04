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
    public partial class frmAdminProveedores : Form
    {
        public frmAdminProveedores()
        {
            InitializeComponent();
        }
        ProveedoresLN oln = new ProveedoresLN();
        public Proveedores obj = new Proveedores();

        public void ListarProveedores(string val)
        {
            dataGridView1.DataSource = oln.ShowProveedoresFiltro(val);

        }

        public void Nuevo()
        {
            try
            {
                frmEditProveedores frm = new frmEditProveedores();
                frm.Text = "Insertar Proveedores";
                frm.label1.Text = "Insertar Proveedores";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    Proveedores oc = frm.CrearObjeto();
                    oln.InsertProveedores(oc);
                    frm.Close();
                    toolStripStatusLabel1.Text = "Proveedores ingresado correctamente";
                    ListarProveedores(textBox1.Text);
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al insertar Proveedores. " + ex.Message;
            }

        }

        public void Actualizar()
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    frmEditProveedores frm = new frmEditProveedores();
                    frm.Text = "Modificar Categoria";

                    Proveedores obj = dataGridView1.CurrentRow.DataBoundItem as Proveedores;
                    frm.setDatos(obj);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        Proveedores oe = frm.CrearObjeto();
                        oln.UpdateProveedores(oe);
                        ListarProveedores(textBox1.Text);
                        toolStripStatusLabel1.Text = "Proveedores actualizado correctamente";
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
                MessageBox.Show("Error al modificar Proveedores: " + ex.Message);
            }
        }

        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var resp = MessageBox.Show(
                        "¿Desea eliminar el Proveedores?",
                        "Eliminar Proveedores",
                        MessageBoxButtons.YesNo);

                    if (resp == DialogResult.Yes)
                    {
                        Proveedores obj = dataGridView1.CurrentRow.DataBoundItem as Proveedores;
                        oln.DeleteProveedores(obj);
                        ListarProveedores(textBox1.Text);
                        toolStripStatusLabel1.Text = "Proveedores eliminado correctamente";
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
                toolStripStatusLabel1.Text = "Error al eliminar Proveedores. " + ex.Message;
            }
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "...";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarProveedores(textBox1.Text);
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

        private void frmAdminProveedores_Load(object sender, EventArgs e)
        {
            ListarProveedores("");
        }
    }
}
