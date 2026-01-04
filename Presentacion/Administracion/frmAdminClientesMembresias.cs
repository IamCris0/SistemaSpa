using Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logica.Administracion;

namespace Presentacion.Administracion
{
    public partial class frmAdminClientesMembresias : Form
    {
        public frmAdminClientesMembresias()
        {
            InitializeComponent();
        }
       ClienteMembresiasLN oln = new ClienteMembresiasLN();
       public ClientesMembresias obj = new ClientesMembresias();

       public void ListarClientesMembresias(string val)
       {
           dataGridView1.DataSource = oln.ShowClientesMembresiasFiltro(val);

       }

       public void Nuevo()
       {
           try
           {
               frmEditClientesMembresias frm = new frmEditClientesMembresias();
               frm.Text = "Insertar Clientes Membresias";
               frm.label1.Text = "Insertar Clientes Membresias";
               frm.ShowDialog();

               if (frm.DialogResult == DialogResult.OK)
               {
                   ClientesMembresias oc = frm.CrearObjeto();
                   oln.InsertClienteMembresias(oc);
                   frm.Close();
                   toolStripStatusLabel1.Text = "Cliente ingresado correctamente";
                   ListarClientesMembresias(textBox1.Text);
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
                   frmEditClientesMembresias frm = new frmEditClientesMembresias();
                   frm.Text = "Modificar Categoria";

                   ClientesMembresias obj = dataGridView1.CurrentRow.DataBoundItem as ClientesMembresias;
                   frm.setDatos(obj);
                   frm.ShowDialog();
                   if (frm.DialogResult == DialogResult.OK)
                   {
                       ClientesMembresias oe = frm.CrearObjeto();
                       oln.UpdateClienteMembresias(oe);
                       ListarClientesMembresias(textBox1.Text);
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
                       ClientesMembresias obj = dataGridView1.CurrentRow.DataBoundItem as ClientesMembresias;
                       oln.DeleteClienteMembresias(obj);
                       ListarClientesMembresias(textBox1.Text);
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
        private void frmAdminClientesMembresias_Load(object sender, EventArgs e)
        {
            ListarClientesMembresias("");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "...";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarClientesMembresias(textBox1.Text);
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
