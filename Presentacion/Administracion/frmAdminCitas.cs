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
    public partial class frmAdminCitas : Form
    {
        public frmAdminCitas()
        {
            InitializeComponent();
        }
        CitasLN oln = new CitasLN();
        public Citas obj = new Citas();

        public void ListarCitas(string val)
        {
            dataGridView1.DataSource = oln.ShowCitasFiltro(val);

        }

        public void Nuevo()
        {
            try
            {
                frmEditCitas frm = new frmEditCitas();
                frm.Text = "Insertar Citas";
                frm.label1.Text = "Insertar Citas";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    Citas oc = frm.CrearObjeto();
                    oln.InsertCita(oc);
                    frm.Close();
                    toolStripStatusLabel1.Text = "Citas ingresado correctamente";
                    ListarCitas(textBox1.Text);
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al insertar Citas. " + ex.Message;
            }

        }

        public void Actualizar()
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    frmEditCitas frm = new frmEditCitas();
                    frm.Text = "Modificar Categoria";

                    Citas obj = dataGridView1.CurrentRow.DataBoundItem as Citas;
                    frm.setDatos(obj);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        Citas oe = frm.CrearObjeto();
                        oln.UpdateCita(oe);
                        ListarCitas(textBox1.Text);
                        toolStripStatusLabel1.Text = "Citas actualizado correctamente";
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
                MessageBox.Show("Error al modificar Citas: " + ex.Message);
            }
        }

        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var resp = MessageBox.Show(
                        "¿Desea eliminar el Citas?",
                        "Eliminar Citas",
                        MessageBoxButtons.YesNo);

                    if (resp == DialogResult.Yes)
                    {
                        Citas obj = dataGridView1.CurrentRow.DataBoundItem as Citas;
                        oln.DeleteCita(obj);
                        ListarCitas(textBox1.Text);
                        toolStripStatusLabel1.Text = "Citas eliminado correctamente";
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
                toolStripStatusLabel1.Text = "Error al eliminar Citas. " + ex.Message;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "...";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarCitas(textBox1.Text);
        }

        private void frmAdminCitas_Load(object sender, EventArgs e)
        {
            ListarCitas("");
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
