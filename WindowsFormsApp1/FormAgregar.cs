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
using System.Net.Http;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Reflection.Emit;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class FormAgregar : Form
    {
        SqlConnection conexion = new SqlConnection(@"Data Source=DESKTOP-B5QJSTA\SQLEXPRESS; Initial Catalog=PriceTracking; integrated security=true");

        public FormAgregar()
        {
            InitializeComponent();
        }

        private void FormAgregar_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'priceTrackingDataSet2.Objetos' table. You can move, or remove it, as needed.
            this.objetosTableAdapter.Fill(this.priceTrackingDataSet2.Objetos);
            // TODO: This line of code loads data into the 'priceTrackingDataSet1.Objetos' table. You can move, or remove it, as needed.
           // this.objetosTableAdapter1.Fill(this.priceTrackingDataSet1.Objetos);
            // TODO: This line of code loads data into the 'priceTrackingDataSet.Objetos' table. You can move, or remove it, as needed.
            //this.objetosTableAdapter.Fill(this.priceTrackingDataSet.Objetos);

        }
        CancellationTokenSource cts = new CancellationTokenSource();
        private async void button1_Click(object sender, EventArgs e)
        {
            string amazonUrl = textBox2.Text;

            // Check if the string exceeds the maximum length allowed by the database
            if (amazonUrl.Length > 255)
            {
                amazonUrl = amazonUrl.Substring(0, 255); // Truncate to fit
            }
            while (true)
            {
                if (cts.Token.IsCancellationRequested)
                {
                    break;
                }
                string price = await GetAmazonPrice(amazonUrl);
                if (price != null)
                {
                    conexion.Open();
                    SqlCommand altas = new SqlCommand(
                        "insert into Objetos (Nombre, Tienda, Link, Precio) " +
                        "values(@Nombre, @Tienda, @Link, @Precio)", conexion);

                    altas.Parameters.AddWithValue("Nombre", textBox1.Text);
                    altas.Parameters.AddWithValue("Tienda", comboBox1.Text);
                    altas.Parameters.AddWithValue("Link", textBox2.Text);
                    altas.Parameters.AddWithValue("Precio", price);
                    altas.ExecuteNonQuery();
                    conexion.Close();

                    textBox1.Text = "";
                    comboBox1.Text = "";
                    textBox2.Text = "";
                    this.objetosTableAdapter.Fill(this.priceTrackingDataSet2.Objetos);
                    MessageBox.Show("Articulo registrado");
                    cts.Cancel();

                }

                // Increase delay to avoid being blocked
                await Task.Delay(10000); // 10 seconds delay
            }

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

        private async Task<string> GetAmazonPrice(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36");

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string pageContents = await response.Content.ReadAsStringAsync();
                        HtmlDocument doc = new HtmlDocument();
                        doc.LoadHtml(pageContents);

                        var priceElements = doc.DocumentNode.SelectNodes("//span[contains(@class, 'a-price-whole')]");
                        if (priceElements != null && priceElements.Count > 0)
                        {
                            return priceElements[0].InnerText.Trim();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return null;
        }
    }
}
