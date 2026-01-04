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
    public partial class frmAdminMembresia : Form
    {
        public frmAdminMembresia()
        {
            InitializeComponent();
        }
        MembresiasLN oln = new MembresiasLN();
        public Membresias obj = new Membresias();

        public void ListarMembresias(string val)
        {
            dataGridView1.DataSource = oln.ShowMembresiasFiltro(val);

        }

        public void Nuevo()
        {
            try
            {
                frmEditMembresias frm = new frmEditMembresias();
                frm.Text = "Insertar Membresias";
                frm.label1.Text = "Insertar Membresias";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    Membresias oc = frm.CrearObjeto();
                    oln.InsertCliente(oc);
                    frm.Close();
                    toolStripStatusLabel1.Text = "Membresia ingresado correctamente";
                    ListarMembresias(textBox1.Text);
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al insertar cliente. " + ex.Message;
            }

        }

        public void Actualizar()
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    frmEditMembresias frm = new frmEditMembresias();
                    frm.Text = "Modificar Categoria";

                    Membresias obj = dataGridView1.CurrentRow.DataBoundItem as Membresias;
                    frm.setDatos(obj);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        Membresias oe = frm.CrearObjeto();
                        oln.UpdateCliente(oe);
                        ListarMembresias(textBox1.Text);
                        toolStripStatusLabel1.Text = "Cliente actualizado correctamente";
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
                MessageBox.Show("Error al modificar Cliente: " + ex.Message);
            }
        }

        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var resp = MessageBox.Show(
                        "¿Desea eliminar el cliente?",
                        "Eliminar Cliente",
                        MessageBoxButtons.YesNo);

                    if (resp == DialogResult.Yes)
                    {
                        Membresias obj = dataGridView1.CurrentRow.DataBoundItem as Membresias;
                        oln.DeleteCliente(obj);
                        ListarMembresias(textBox1.Text);
                        toolStripStatusLabel1.Text = "Cliente eliminado correctamente";
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
                toolStripStatusLabel1.Text = "Error al eliminar cliente. " + ex.Message;
            }
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarMembresias(textBox1.Text);
        }
    }
}
