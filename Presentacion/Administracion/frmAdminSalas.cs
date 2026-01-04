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
    public partial class frmAdminSalas : Form
    {
        public frmAdminSalas()
        {
            InitializeComponent();
        }
        SalasLN oln = new SalasLN();
        public Salas obj = new Salas();

        public void ListarSalas(string val)
        {
            dataGridView1.DataSource = oln.ShowSalasFiltro(val);

        }

        public void Nuevo()
        {
            try
            {
                frmEditSalas frm = new frmEditSalas();
                frm.Text = "Insertar Salas";
                frm.label1.Text = "Insertar Salas";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    Salas oc = frm.CrearObjeto();
                    oln.InsertSala(oc);
                    frm.Close();
                    toolStripStatusLabel1.Text = "Salas ingresado correctamente";
                    ListarSalas(textBox1.Text);
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al insertar Salas. " + ex.Message;
            }

        }

        public void Actualizar()
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    frmEditSalas frm = new frmEditSalas();
                    frm.Text = "Modificar Categoria";

                    Salas obj = dataGridView1.CurrentRow.DataBoundItem as Salas;
                    frm.setDatos(obj);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        Salas oe = frm.CrearObjeto();
                        oln.UpdateSala(oe);
                        ListarSalas(textBox1.Text);
                        toolStripStatusLabel1.Text = "Salas actualizado correctamente";
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
                MessageBox.Show("Error al modificar Salas: " + ex.Message);
            }
        }

        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var resp = MessageBox.Show(
                        "¿Desea eliminar el Salas?",
                        "Eliminar Salas",
                        MessageBoxButtons.YesNo);

                    if (resp == DialogResult.Yes)
                    {
                        Salas obj = dataGridView1.CurrentRow.DataBoundItem as Salas;
                        oln.DeleteSala(obj);
                        ListarSalas(textBox1.Text);
                        toolStripStatusLabel1.Text = "Salas eliminado correctamente";
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
                toolStripStatusLabel1.Text = "Error al eliminar Salas. " + ex.Message;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "...";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListarSalas(textBox1.Text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
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

        private void frmAdminSalas_Load(object sender, EventArgs e)
        {
            ListarSalas("");
        }
    }
}
