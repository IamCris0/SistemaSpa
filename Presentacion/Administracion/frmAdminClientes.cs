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
    public partial class frmAdminClientes : Form
    {
        public frmAdminClientes()
        {
            InitializeComponent();
        }
        ClientesLN oln = new ClientesLN();
        public Clientes obj = new Clientes();

        public void ListarClientes(string val)
        {
            dataGridView1.DataSource = oln.ShowClientesFiltro(val);

        }

        public void Nuevo()
        {
            try
            {
                frmEditClientes frm = new frmEditClientes();
                frm.Text = "Insertar Cliente";
                frm.label1.Text = "Insertar Cliente";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    Clientes oc = frm.CrearObjeto();
                    oln.InsertCliente(oc);
                    frm.Close();
                    toolStripStatusLabel1.Text = "Cliente ingresado correctamente";
                    ListarClientes(textBox1.Text);
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
                    frmEditClientes frm = new frmEditClientes();
                    frm.Text = "Modificar Categoria";

                    Clientes obj = dataGridView1.CurrentRow.DataBoundItem as Clientes;
                    frm.setDatos(obj);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        Clientes oe = frm.CrearObjeto();
                        oln.UpdateCliente(oe);
                        ListarClientes(textBox1.Text);
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
                        Clientes obj = dataGridView1.CurrentRow.DataBoundItem as Clientes;
                        oln.DeleteCliente(obj);
                        ListarClientes(textBox1.Text);
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
        private void frmAdminClientes_Load(object sender, EventArgs e)
        {
            ListarClientes("");
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarClientes(textBox1.Text);
        }
    }
}
