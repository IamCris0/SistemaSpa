using Entidades.Administracion;
using Logica.Administracion;
using System;
using System.Windows.Forms;


namespace Presentacion.Administracion
{
    public partial class frmEditDetalleCompras : Form
    {
        // ================== LOGICA ==================
        CompraLN olCompras = new CompraLN();
        ProductosLN olProductos = new ProductosLN();

        // ================== CONSTRUCTOR ==================
        public frmEditDetalleCompras()
        {
            InitializeComponent();

            MostrarVentas();
            MostrarProductos();

            // Subtotal solo visual
            textBox4.ReadOnly = true;

            // Eventos para calcular subtotal
            textBox2.TextChanged += textBox2_TextChanged;
            textBox3.TextChanged += textBox3_TextChanged;
        }

        // ================== MOSTRAR VENTAS ==================
        private void MostrarVentas()
        {
            comboBox1.DataSource = olCompras.ShowComprasFiltro("");
            comboBox1.DisplayMember = "ComprasID";
            comboBox1.ValueMember = "CompraID";
            comboBox1.SelectedIndex = 0;
        }

        // ================== MOSTRAR PRODUCTOS ==================
        private void MostrarProductos()
        {
            comboBox2.DataSource = olProductos.ShowProductosFiltro("");
            comboBox2.DisplayMember = "NombreProducto";
            comboBox2.ValueMember = "ProductoID";
            comboBox2.SelectedIndex = 0;
        }

        // ================== CALCULAR SUBTOTAL ==================
        private void CalcularSubtotal()
        {
            if (int.TryParse(textBox2.Text, out int cantidad) &&
                decimal.TryParse(textBox3.Text, out decimal precio))
            {
                textBox4.Text = (cantidad * precio).ToString("0.00");
            }
            else
            {
                textBox4.Text = "0.00";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CalcularSubtotal();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            CalcularSubtotal();
        }

        // ================== CREAR OBJETO ==================
        public DetalleCompras CrearObjeto()
        {
            return new DetalleCompras(
                int.Parse(textBox1.Text),
                (int)comboBox1.SelectedValue,
                (int)comboBox2.SelectedValue,
                int.Parse(textBox2.Text),
                decimal.Parse(textBox3.Text)
            );
        }

        // ================== SET DATOS (MODIFICAR) ==================
        public void setDatos(DetalleCompras detalle)
        {
            textBox1.Text = detalle.DetalleCompraID.ToString();
            comboBox1.SelectedValue = detalle.CompraID;
            comboBox2.SelectedValue = detalle.ProductoID;
            textBox2.Text = detalle.Cantidad.ToString();
            textBox3.Text = detalle.PrecioUnitario.ToString();
            textBox4.Text = detalle.Subtotal.ToString("0.00");
        }

        // ================== VALIDAR ==================
        private bool ValidarDatos()
        {
            return textBox1.Text.Trim() != "" &&
                   textBox2.Text.Trim() != "" &&
                   textBox3.Text.Trim() != "" &&
                   comboBox1.SelectedIndex >= 0 &&
                   comboBox2.SelectedIndex >= 0;
        }

        // ================== GUARDAR ==================
        private void Guardar()
        {
            if (ValidarDatos())
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Todos los campos son obligatorios");
            }
        }

        // ================== BOTONES ==================
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Guardar();
            this.Hide();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmEditDetalleCompras_Load(object sender, EventArgs e)
        {

        }
    }
}
