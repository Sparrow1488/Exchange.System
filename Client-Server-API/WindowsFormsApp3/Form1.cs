using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Objects.Packages.Default;
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
                var testLetter = (new Letter() { Title = "Source TITLE", Text = "Обращение", SenderId = 0, Type = LetterType.Complaint, Sources = new Source[] { new Source() { DateCreate = DateTime.Now, Base64Data = "1323123123", Extension = ".mp4" } } });
                var testPost = (new Publication() { Title = "NEW POST222!!!", Text = "КРУТОЙ ТwqeweЕКСТ ЙО", SenderId = 0, Type = NewsType.Important });
                var pack = new NewPublication(testPost);
                pack.UserToken = tokenTextBox.Text;
                var aesRsaSender = new RequestSendler(connectionSettings);
                var response = await aesRsaSender.SendRequest(pack);

                string responseReport = string.Format("(Status: {0}, Error: {1}\n", response.Status, response.ErrorMessage);
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
            var passport = new UserPassport("asd", "1234");
            var pack = new Authorization(passport);
            var sendler = new AesRsaSendler(connectionSettings);
            var response = await sendler.SendRequest(pack);
            var authUser = (User)response.ResponseData;

            textBox1.Text += string.Format("Status: {0}; Token: {1}", response.Status, authUser.Passport.Token) + "\n";
            tokenTextBox.Text = authUser.Passport.Token;
        }
    }
}
