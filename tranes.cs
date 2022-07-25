using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TUDIEN
{
    public partial class tranes : Form
    {
        string INPUT;
        bool check;
        public tranes(string input,bool CHECK)
        {
            check = CHECK;
            INPUT = input;
            InitializeComponent();
        }

        private void Tranes_Load(object sender, EventArgs e)
        {
            this.Text = INPUT;
            string Output = TranslateText(INPUT, check);
            textBox1.Text = Output;
        }
        public class TimedWebClient : WebClient
        {
            // Timeout in milliseconds, default = 600,000 msec
            public int Timeout { get; set; }

            public TimedWebClient()
            {
                this.Timeout = 600000;
            }

            protected override WebRequest GetWebRequest(Uri address)
            {
                var objWebRequest = base.GetWebRequest(address);
                objWebRequest.Timeout = this.Timeout;
                return objWebRequest;
            }
        }
        public string TranslateText(string input, bool check)
        {
            //   richTextBox1.Text = url;
            try
            {
                string url;
                if (check == true)
                {
                    url = String.Format("http://translate.google.com.tr/m?hl=en&sl=en&tl=vi&ie=UTF-8&prev=_m&q={0}", input);
                }
                else
                {
                    url = String.Format("https://translate.google.com.tr/m?hl=en&sl=vn&tl=en&ie=UTF-8&prev=_m&q={0}", input);
                }
                //  string url = String.Format("https://translate.google.com.tr/?hl=en&sl=en&tl=vi&ie=UTF-8&prev=_m&q={0}", input);
                WebClient webClient = new WebClient();
                webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
                webClient.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
                webClient.Encoding = Encoding.UTF8;
                //  webClient.Encoding = System.Text.Encoding.UTF8;
                string result = webClient.DownloadString(url);
                //   string result = new TimedWebClient{Timeout=10000 }.DownloadString(url);
                // TranslationClient client = TranslationClient.Create("tudien-205002",TranslationModel.ServiceDefault);
                // string result=  client.TranslateText(input, LanguageCodes.English, LanguageCodes.Vietnamese).TranslatedText;
                result = result.Remove(0, result.IndexOf("<div dir=\"ltr\" class=\"t0\">")).Replace("<div dir=\"ltr\" class=\"t0\">", "");
                // result = result.Remove(0, result.IndexOf("TRANSLATED_TEXT"));
                int last = result.IndexOf("</div>");
                result = result.Remove(last, result.Length - last);

                //   result = result.Substring(result.IndexOf("id=result_box"), 500);


                //result = result.Substring(0, result.IndexOf("</span"));




                // return result;
                return result;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); return "không có kết nối mạng"; }


        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }
    }
}
