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
    public partial class frmAdminDetalleCitas : Form
    {
        public frmAdminDetalleCitas()
        {
            InitializeComponent();
        }
        DetalleCitasLN oln = new DetalleCitasLN();
        public DetalleCitas obj = new DetalleCitas();

        public void ListarDetalleCitas(string val)
        {
            dataGridView1.DataSource = oln.ShowDetalleCitasFiltro(val);

        }

        public void Nuevo()
        {
            try
            {
                frmEditDetalleCitas frm = new frmEditDetalleCitas();
                frm.Text = "Insertar DetalleCitas";
                frm.label1.Text = "Insertar DetalleCitas";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    DetalleCitas oc = frm.CrearObjeto();
                    oln.InsertDetalleCitas(oc);
                    frm.Close();
                    toolStripStatusLabel1.Text = "DetalleCitas ingresado correctamente";
                    ListarDetalleCitas(textBox1.Text);
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al insertar DetalleCitas. " + ex.Message;
            }

        }

        public void Actualizar()
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    frmEditDetalleCitas frm = new frmEditDetalleCitas();
                    frm.Text = "Modificar Categoria";

                    DetalleCitas obj = dataGridView1.CurrentRow.DataBoundItem as DetalleCitas;
                    frm.setDatos(obj);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        DetalleCitas oe = frm.CrearObjeto();
                        oln.UpdateDetalleCitas(oe);
                        ListarDetalleCitas(textBox1.Text);
                        toolStripStatusLabel1.Text = "DetalleCitas actualizado correctamente";
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
                MessageBox.Show("Error al modificar DetalleCitas: " + ex.Message);
            }
        }

        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var resp = MessageBox.Show(
                        "¿Desea eliminar el DetalleCitas?",
                        "Eliminar DetalleCitas",
                        MessageBoxButtons.YesNo);

                    if (resp == DialogResult.Yes)
                    {
                        DetalleCitas obj = dataGridView1.CurrentRow.DataBoundItem as DetalleCitas;
                        oln.DeleteDetalleCitas(obj);
                        ListarDetalleCitas(textBox1.Text);
                        toolStripStatusLabel1.Text = "DetalleCitas eliminado correctamente";
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
                toolStripStatusLabel1.Text = "Error al eliminar DetalleCitas. " + ex.Message;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "...";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarDetalleCitas(textBox1.Text);
        }

        private void frmAdminDetalleCitas_Load(object sender, EventArgs e)
        {
            ListarDetalleCitas("");
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
