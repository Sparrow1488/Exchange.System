using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Sendlers;
using ExchangeSystem.Requests.Sendlers.Close;
using ExchangeSystem.Requests.Sendlers.Open;
using System;
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
                var letter = new Letter() { Title = "Рандомный заголовок", Text = "Большого ума текст", Type = LetterType.Offer};
                var pack = new NewLetter(letter);
                var aesRsaSender = new RequestSendler(connectionSettings);
                var response = await aesRsaSender.SendRequest(pack);

                string responseReport = string.Format("(Status: {0}, Error: {1}, Data(message): {2})\n", response.Status, response.ErrorMessage, (string)response.ResponseData);
                textBox1.Text += responseReport;
            }
            catch { textBox1.Text += "BAD" + " "; }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await DefaultRequest();
        }
        private async Task DefaultRequest()
        {
            ConnectionSettings connectionSettings = new ConnectionSettings("127.0.0.1", 80);
            var passport = new UserPassport("Sparrow", "1488");
            var pack = new Authorization(passport);
            var sendler = new AesRsaSendler(connectionSettings);
            var response = await sendler.SendRequest(pack);
            var authUser = (User)response.ResponseData;

            string responseReport = string.Format("(Status: {0}, Error: {1}, Data(message): {2})\n", response.Status, response.ErrorMessage, authUser.Passport.Login);
            textBox1.Text += responseReport;
        }
    }
}
