using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class FormAgregar : Form
    {
        SqlConnection conexion = new SqlConnection(@"Data Source=DESKTOP-NSTVN1M\SQLEXPRESS; Initial Catalog=PriceTracking; integrated security=true");

        public FormAgregar()
        {
            InitializeComponent();
        }

        private void FormAgregar_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'priceTrackingDataSet.Objetos' table. You can move, or remove it, as needed.
            this.objetosTableAdapter.Fill(this.priceTrackingDataSet.Objetos);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conexion.Open();
            SqlCommand altas = new SqlCommand(
                "insert into Objetos (Nombre, Tienda, link) " +
                "values(@Nombre, @Tienda, @link)", conexion);

            altas.Parameters.AddWithValue("Nombre", textBox1.Text);
            altas.Parameters.AddWithValue("Tienda", comboBox1.Text);
            altas.Parameters.AddWithValue("link", textBox2.Text);
            altas.ExecuteNonQuery();

            textBox1.Text = "";
            comboBox1.Text = "";
            textBox2.Text = "";
            this.objetosTableAdapter.Fill(this.priceTrackingDataSet.Objetos);
            MessageBox.Show("Articulo registrado");
            conexion.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Font = new Font("Arial", 8, FontStyle.Italic);
            textBox1.ForeColor = Color.Black;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.Font = new Font("Arial", 8, FontStyle.Italic);
            textBox2.ForeColor = Color.Black;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox1.Font = new Font("Arial", 8, FontStyle.Italic);
            comboBox1.ForeColor = Color.Black;
        }
    }
}
