using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Sendlers;
using ExchangeSystem.Requests.Sendlers.Close;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionSettings connectionSettings = new ConnectionSettings("127.0.0.1", 80);
                var message = new ExchangeSystem.Requests.Objects.Message("Чеб такого написать чтоп пш пш по приколу");
                var pack = new NewMessage(message);
                var aesRsaSender = new AesRsaSendler(connectionSettings);
                var response = await aesRsaSender.SendRequest(pack);

                textBox1.Text += response.Status + " ";
            }
            catch { textBox1.Text += "BAD" + " "; }
        }
    }
}
