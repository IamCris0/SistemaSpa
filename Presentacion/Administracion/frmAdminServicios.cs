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
    public partial class frmAdminServicios : Form
    {
        public frmAdminServicios()
        {
            InitializeComponent();
        }
        ServiciosLN oln = new ServiciosLN();
        public Servicios obj = new Servicios();

        public void ListarServicios(string val)
        {
            dataGridView1.DataSource = oln.ShowServiciosFiltro(val);

        }

        public void Nuevo()
        {
            try
            {
                frmEditServicios frm = new frmEditServicios();
                frm.Text = "Insertar Servicios";
                frm.label1.Text = "Insertar Servicios";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    Servicios oc = frm.CrearObjeto();
                    oln.InsertServicios(oc);
                    frm.Close();
                    toolStripStatusLabel1.Text = "Servicios ingresado correctamente";
                    ListarServicios(textBox1.Text);
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al insertar Servicios. " + ex.Message;
            }

        }

        public void Actualizar()
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    frmEditServicios frm = new frmEditServicios();
                    frm.Text = "Modificar Categoria";

                    Servicios obj = dataGridView1.CurrentRow.DataBoundItem as Servicios;
                    frm.setDatos(obj);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        Servicios oe = frm.CrearObjeto();
                        oln.UpdateServicios(oe);
                        ListarServicios(textBox1.Text);
                        toolStripStatusLabel1.Text = "Servicios actualizado correctamente";
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
                MessageBox.Show("Error al modificar Servicios: " + ex.Message);
            }
        }

        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var resp = MessageBox.Show(
                        "¿Desea eliminar el Servicios?",
                        "Eliminar Servicios",
                        MessageBoxButtons.YesNo);

                    if (resp == DialogResult.Yes)
                    {
                        Servicios obj = dataGridView1.CurrentRow.DataBoundItem as Servicios;
                        oln.DeleteServicios(obj);
                        ListarServicios(textBox1.Text);
                        toolStripStatusLabel1.Text = "Servicios eliminado correctamente";
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
                toolStripStatusLabel1.Text = "Error al eliminar Servicios. " + ex.Message;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "...";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarServicios(textBox1.Text);
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

        private void frmAdminServicios_Load(object sender, EventArgs e)
        {
            ListarServicios("");
        }
    }
}
