using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Sendlers;
using ExchangeSystem.Requests.Sendlers.Close;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static ConnectionSettings connectionSettings = new ConnectionSettings("127.0.0.1", 80);
        private void button1_Click(object sender, EventArgs e)
        {
            var message = new ExchangeSystem.Requests.Objects.Message("Чеб такого написать чтоп пш пш по приколу");
            var pack = new NewMessage(message);
            var aesRsaSender = new AesRsaSendler(connectionSettings);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var response = aesRsaSender.SendRequest(pack);
            stopwatch.Stop();
            textBox1.Text += response.Status + " ";
            //Console.WriteLine("Response status: {0}", response.Status);
            //Console.WriteLine("Server error message: {0}", response.ErrorMessage);
            //Console.WriteLine("Response as string {0}", (string)response.ResponseData);
            //Console.WriteLine("Response received in {0} millisecond", stopwatch.ElapsedMilliseconds);
        }
    }
}
