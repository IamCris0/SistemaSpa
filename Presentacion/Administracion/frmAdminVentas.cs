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
    public partial class frmAdminVentas : Form
    {
        public frmAdminVentas()
        {
            InitializeComponent();
        }
        VentasLN oln = new VentasLN();
        public Ventas obj = new Ventas();

        public void ListarVentas(string val)
        {
            dataGridView1.DataSource = oln.ShowVentasFiltro(val);

        }

        public void Nuevo()
        {
            try
            {
                frmEditVentas frm = new frmEditVentas();
                frm.Text = "Insertar Venta";
                frm.label1.Text = "Insertar Venta";
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.OK)
                {
                    Ventas oc = frm.CrearObjeto();
                    oln.InsertVenta(oc);
                    frm.Close();
                    toolStripStatusLabel1.Text = "Venta ingresado correctamente";
                    ListarVentas(textBox1.Text);
                    timer1.Start();
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "Error al insertar Venta. " + ex.Message;
            }

        }

        public void Actualizar()
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    frmEditVentas frm = new frmEditVentas();
                    frm.Text = "Modificar Categoria";

                    Ventas obj = dataGridView1.CurrentRow.DataBoundItem as Ventas;
                    frm.setDatos(obj);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        Ventas oe = frm.CrearObjeto();
                        oln.UpdateVenta(oe);
                        ListarVentas(textBox1.Text);
                        toolStripStatusLabel1.Text = "Venta actualizado correctamente";
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
                MessageBox.Show("Error al modificar Venta: " + ex.Message);
            }
        }

        public void Eliminar()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var resp = MessageBox.Show(
                        "¿Desea eliminar el Venta?",
                        "Eliminar Venta",
                        MessageBoxButtons.YesNo);

                    if (resp == DialogResult.Yes)
                    {
                        Ventas obj = dataGridView1.CurrentRow.DataBoundItem as Ventas;
                        oln.DeleteVenta(obj);
                        ListarVentas(textBox1.Text);
                        toolStripStatusLabel1.Text = "Venta eliminado correctamente";
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
                toolStripStatusLabel1.Text = "Error al eliminar Venta. " + ex.Message;
            }
        }
        private void frmAdminVentas_Load(object sender, EventArgs e)
        {
            ListarVentas("");
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
            ListarVentas(textBox1.Text);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
