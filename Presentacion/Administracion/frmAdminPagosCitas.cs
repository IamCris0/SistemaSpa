using Entidades.Administracion;
using Logica.Administracion;
using Logica.Vistas;
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
    public partial class frmAdminPagosCitas : Form
    {
        public frmAdminPagosCitas()
        {
            InitializeComponent();
        }
        PagosCitasLN oln = new PagosCitasLN();
        PagosCitas_Citas_ClientesLN oln2 = new PagosCitas_Citas_ClientesLN();
        public PagosCitas obj = new PagosCitas();

        public void ListarPagosCitasVista(string var)
        {
            dataGridView1.DataSource = oln.ShowPagosCitasFiltro(var);

        }

        public void Nuevo()
        {
            try
            {
                frmEditPagoCitas frm = new frmEditPagoCitas();
                frm.Text = "Insertar PagosCitas";
                frm.label1.Text = "Insertar PagosCitas";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    PagosCitas oc = frm.CrearObjeto();
                    oln.InsertPagosCitas(oc);
                    frm.Close();
                    toolStripStatusLabel1.Text = "PagosCitas ingresado correctamente";
                    ListarPagosCitasVista(textBox1.Text);
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al insertar PagosCitas. " + ex.Message;
            }

        }

        public void Actualizar()
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    frmEditPagoCitas frm = new frmEditPagoCitas();
                    frm.Text = "Modificar Categoria";

                    PagosCitas obj = dataGridView1.CurrentRow.DataBoundItem as PagosCitas;
                    frm.setDatos(obj);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        PagosCitas oe = frm.CrearObjeto();
                        oln.UpdatePagosCitas(oe);
                        ListarPagosCitasVista(textBox1.Text);
                        toolStripStatusLabel1.Text = "PagosCitas actualizado correctamente";
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
                MessageBox.Show("Error al modificar PagosCitas: " + ex.Message);
            }
        }

        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var resp = MessageBox.Show(
                        "¿Desea eliminar el PagosCitas?",
                        "Eliminar PagosCitas",
                        MessageBoxButtons.YesNo);

                    if (resp == DialogResult.Yes)
                    {
                        PagosCitas obj = dataGridView1.CurrentRow.DataBoundItem as PagosCitas;
                        oln.DeletePagosCitas(obj);
                        ListarPagosCitasVista(textBox1.Text);
                        toolStripStatusLabel1.Text = "PagosCitas eliminado correctamente";
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
                toolStripStatusLabel1.Text = "Error al eliminar PagosCitas. " + ex.Message;
            }
        }

        private void frmAdminPagosCitas_Load(object sender, EventArgs e)
        {
            ListarPagosCitasVista("");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "...";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarPagosCitasVista(textBox1.Text);
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
