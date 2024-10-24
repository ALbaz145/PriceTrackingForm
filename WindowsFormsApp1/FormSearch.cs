using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace WindowsFormsApp1
{
    public partial class FormSearch : Form
    {
        public FormSearch()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string amazonUrl = textBoxLink.Text;
            while (true)
            {
                string price = await GetAmazonPrice(amazonUrl);
                if (price != null)
                {
                    label1.Text = price;
                }

                // Increase delay to avoid being blocked
                await Task.Delay(10000); // 10 seconds delay
            }
        }
        private async Task<string> GetAmazonPrice(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");

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
