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
    public partial class frmAdminHistorialClientes : Form
    {
        public frmAdminHistorialClientes()
        {
            InitializeComponent();
        }
        HistorialClientesLN oln = new HistorialClientesLN();
        public HistorialClientes obj = new HistorialClientes();

        public void ListarHistorialClientes(string val)
        {
            dataGridView1.DataSource = oln.ShowHistorialClientesFiltro(val);

        }

        public void Nuevo()
        {
            try
            {
                frmEditHistorialClientes frm = new frmEditHistorialClientes();
                frm.Text = "Insertar HistorialCliente";
                frm.label1.Text = "Insertar HistorialCliente";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    HistorialClientes oc = frm.CrearObjeto();
                    oln.InsertHistorialCliente(oc);
                    frm.Close();
                    toolStripStatusLabel1.Text = "HistorialCliente ingresado correctamente";
                    ListarHistorialClientes(textBox1.Text);
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al insertar HistorialCliente. " + ex.Message;
            }

        }

        public void Actualizar()
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    frmEditHistorialClientes frm = new frmEditHistorialClientes();
                    frm.Text = "Modificar Categoria";

                    HistorialClientes obj = dataGridView1.CurrentRow.DataBoundItem as HistorialClientes;
                    frm.setDatos(obj);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        HistorialClientes oe = frm.CrearObjeto();
                        oln.UpdateHistorialCliente(oe);
                        ListarHistorialClientes(textBox1.Text);
                        toolStripStatusLabel1.Text = "HistorialCliente actualizado correctamente";
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
                MessageBox.Show("Error al modificar HistorialCliente: " + ex.Message);
            }
        }

        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var resp = MessageBox.Show(
                        "¿Desea eliminar el HistorialCliente?",
                        "Eliminar HistorialCliente",
                        MessageBoxButtons.YesNo);

                    if (resp == DialogResult.Yes)
                    {
                        HistorialClientes obj = dataGridView1.CurrentRow.DataBoundItem as HistorialClientes;
                        oln.DeleteHistorialCliente(obj);
                        ListarHistorialClientes(textBox1.Text);
                        toolStripStatusLabel1.Text = "HistorialCliente eliminado correctamente";
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
                toolStripStatusLabel1.Text = "Error al eliminar HistorialCliente. " + ex.Message;
            }
        }
        private void frmAdminHistorialClientes_Load(object sender, EventArgs e)
        {
            ListarHistorialClientes("");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "...";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarHistorialClientes(textBox1.Text);
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
