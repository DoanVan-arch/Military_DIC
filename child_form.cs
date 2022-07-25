using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DevExpress.XtraRichEdit.API.Native;
using System.Net;

namespace TUDIEN
{
    public partial class child_form : Form
    {
        string SEARCH;
        Form1 FRM_MAIN;
        bool CHECK ;
        public child_form(string search,Form1 frm_main,bool check)
        {
            SEARCH = search;
            FRM_MAIN = frm_main;
            CHECK = check;
            InitializeComponent();
        }
        public class TimedWebClient : WebClient
        {
            // Timeout in milliseconds, default = 600,000 msec
            public int Timeout { get; set; }

            public TimedWebClient()
            {
                this.Timeout = 600000;
            }

            protected override System.Net.WebRequest GetWebRequest(Uri address)
            {
                var objWebRequest = base.GetWebRequest(address);
                objWebRequest.Timeout = this.Timeout;
                return objWebRequest;
            }
        }
        private void font(int start,int end,Color color)
        {
            DocumentRange range = rich_data.Document.CreateRange(start, end);

            CharacterProperties prop = rich_data.Document.BeginUpdateCharacters(range);

            prop.ForeColor = color;
            rich_data.Document.EndUpdateCharacters(prop);
        }
        Dictionary<string,Dictionary<string,string>> lst_key = new Dictionary<string, Dictionary<string, string>>();
        List<XElement> rootElements;
        private void child_form_Load(object sender, EventArgs e)
        {
            this.Text = SEARCH;
            label1.Text = SEARCH;
            this.Text = SEARCH;
            label1.Text = SEARCH;
            comboBox1.DataSource = FRM_MAIN.filenames;
            rootElements = new List<XElement>();
            foreach (var temp in FRM_MAIN.filenames)
            {
                XElement root_temp;
                root_temp = XElement.Load(Application.StartupPath + "/data/" + temp + ".xml");
                rootElements.Add(root_temp);
            }
          
                foreach (XElement child in rootElements[0].Elements("data"))
                {
                   
                    //   Text_search.Text = lst_term.SelectedItem.ToString();
                    if (child.Elements().ToList()[0].Value.ToString().ToUpper().Contains(SEARCH.Trim().ToString().ToUpper()))
                    {
                        listBox1.Items.Add(child.Elements().ToList()[0].Value.ToString());
                       
                        Dictionary<string, string> lst_temp = new Dictionary<string, string>();
                        for (int h = 0; h < child.Elements().ToList().Count; h++)
                        {
                            lst_temp.Add(child.Elements().ToList()[h].Name.ToString().Replace("_x0020_", " "), child.Elements().ToList()[h].Value.ToString());
                        }
                    try
                    {
                        lst_key.Add(child.Elements().ToList()[0].Value.ToString().ToLower(), lst_temp);
                    }
                    catch { }

                    }
                    // if (listBox1.Items.Count == 0) { rich_data.Text = "Không tìm thấy dữ liệu"; }
                }
            
            if (listBox1.Items.Count == 0) { rich_data.Text = "Không tìm thấy dữ liệu"; }
        }

        private void PanelControl1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void ReplaceAll(string strFind, string strReplace)
        {
            if (!String.IsNullOrEmpty(strFind))
            {
                DocumentRange[] ranges = rich_data.Document.FindAll(strFind, SearchOptions.None, rich_data.Document.Range);
                for (int i = 0; i < ranges.Length; i++)
                {
                    if (strReplace == "null")
                        strReplace = String.Empty;
                    rich_data.Document.Replace(ranges[i], strReplace);
                    CharacterProperties cp = rich_data.Document.BeginUpdateCharacters(ranges[i]);
                    // cp.BackColor = System.Drawing.Color.FromArgb(180, 201, 233);
                    rich_data.Document.EndUpdateCharacters(cp);
                };
            }
        }
            private void ListBox1_Click(object sender, EventArgs e)
            {

                string cellValue = listBox1.SelectedItem.ToString();
            label3.Text = cellValue;
            if (rich_data.Text.Length > 0)
            {
                rich_data.Document.SelectAll();
                DocumentRange selection = rich_data.Document.Selection;
                //  Richtext_data.Document.Replace
                // ReplaceAll(Richtext_data.Document.GetText(selection),text_temp);
                ReplaceAll(rich_data.Document.GetText(selection), "");
            }
            foreach (var child in lst_key)
                {
                    //   Text_search.Text = lst_term.SelectedItem.ToString();
                    if (cellValue.ToString().Trim().ToLower() == child.Key.ToString().Trim().ToLower())
                    {
                        // changeFontColorItem1.SelectedColor = Color.Red;
                        foreach(var item in child.Value)
                        {
                            if (String.IsNullOrEmpty(item.Value.ToString())) continue;
                            string text_temp = "\n " + item.Key.ToString().Replace("_x0020_", " ") + ": \n     " + item.Value.ToString();
                            //  font(0, text_temp.Length, Color.Black);

                            rich_data.Document.AppendText(text_temp);
                        }
                       // label1.Text = child.ENG.ToString().Trim().ToLower();
                    
                    }
                    
                }
             
          
            rich_data.Document.Selection = rich_data.Document.CreateRange(0, 0);
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            int count = 0;
            try
            {
                foreach (var item in comboBox1.Items)
                {
                    if (comboBox1.Text == item.ToString())
                    {
                        break;
                    }
                    count++;
                }
             
            lst_key = new Dictionary<string, Dictionary<string, string>>();
            rich_data.Text = "";
                listBox1.Items.Clear();
            foreach (XElement child in rootElements[count].Elements("data"))
            {

                //   Text_search.Text = lst_term.SelectedItem.ToString();
                if (child.Elements().ToList()[0].Value.ToString().ToUpper().Contains(SEARCH.Trim().ToString().ToUpper()))
                {
                    listBox1.Items.Add(child.Elements().ToList()[0].Value.ToString().ToLower());
                    // data temp_data = new data();
                    // temp_data.ENG = child.Element("EnTerm").Value.ToString().ToLower();
                    // temp_data.VN = child.Element("VnMean").Value.ToString().ToLower();
                    Dictionary<string, string> lst_temp = new Dictionary<string, string>();
                    for (int h = 0; h < child.Elements().ToList().Count; h++)
                    {
                        lst_temp.Add(child.Elements().ToList()[h].Name.ToString().Replace("_x0020_", " "), child.Elements().ToList()[h].Value.ToString());
                    }
                    lst_key.Add(child.Elements().ToList()[0].Value.ToString().ToLower(), lst_temp);

                }
                // if (listBox1.Items.Count == 0) { rich_data.Text = "Không tìm thấy dữ liệu"; }
            }

            if (listBox1.Items.Count == 0) { rich_data.Text = "Không tìm thấy dữ liệu"; }
            }
            catch { }
        }
    }
 }


