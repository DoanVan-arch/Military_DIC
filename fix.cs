using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TUDIEN
{
    public partial class fix : Form
    {
        Form1 FRM;
        public fix(Form1 frm)
        {
            InitializeComponent();
            FRM = frm;
        }

        private void btn_DEL_Click(object sender, EventArgs e)
        {

        }

        private void btn_FIX_Click(object sender, EventArgs e)
        {

        }

        private void lst_VIEW_Click(object sender, EventArgs e)
        {

        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {

        }
        public List<XElement> rootElements;
        List<string> ENV;
        private void Load_data()
        {
            try
            {
                comboBox1.DataSource = FRM.filenames;
                rootElements = new List<XElement>();
                foreach (var temp in FRM.filenames)
                {
                    XElement root_temp;
                    root_temp = XElement.Load(Application.StartupPath + "/data/" + temp + ".xml");
                    rootElements.Add(root_temp);
                }
                ENV = new List<string>();
                var EnTerm = from a in rootElements[0].Elements("data") select a.Elements().ToList()[0].Value.ToString().ToLower().Trim();
                ENV = new List<string>();
                ENV = EnTerm.ToList();
                ENV.Sort();
                listBoxControl1.DataSource = ENV;
            }
            catch { }
        }
        private void Fix_Load(object sender, EventArgs e)
        {
           
            Load_data();
        }
        List<string> list_temp = new List<string>();
        private void ListBoxControl1_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (var item in comboBox1.Items)
            {
                if (comboBox1.Text == item.ToString())
                {
                    break;
                }
                count++;
            }
            txt_EN.Text = listBoxControl1.SelectedItem.ToString();
            foreach (XElement child in rootElements[count].Elements())
            {
                if (txt_EN.Text.ToString() == child.Elements().ToList()[0].Value.ToString().Trim().ToLower())
                {

                    // txt_TYPE.Text = child.Element("Type").Value.ToString();
                  //  textBox1.Text = null;
                    list_temp.Clear();
                    comboBox2.Items.Clear();
                    comboBox2.Text = null;
                    textBox1.Text = null;
                    txt_MEANVN.Text = child.Elements().ToList()[1].Value.ToString();
                    for (int k=2;k < child.Elements().ToList().Count;k++)
                    {
                        comboBox2.Items.Add(child.Elements().ToList()[k].Name.ToString().Replace("_x0020_", " "));
                        list_temp.Add(child.Elements().ToList()[k].Value);
                    }
                //    txt_EXAMPLE.Text = child.Element("Example").Value.ToString();
                    break;
                }
            }
        }

        private void Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Btn_FIX_Click_1(object sender, EventArgs e)
        {
            if(tabControl1.SelectedTab.Name == "tabPage2")
            {
                int count = 0;
                foreach (var item in comboBox1.Items)
                {
                    if (comboBox1.Text == item.ToString())
                    {
                        break;
                    }
                    count++;
                }

                XElement root = new XElement("data");
                root.Add(new XElement(FRM.lst_rootElements[count].Descendants("data").Elements().ToList()[0].Name, textBox4.Text));
                root.Add(new XElement(FRM.lst_rootElements[count].Descendants("data").Elements().ToList()[1].Name, textBox3.Text));
             //   MessageBox.Show(FRM.lst_rootElements[count].Descendants("data").ToList()[0].Elements().ToList().Count.ToString());
                for(int i = 2;i< FRM.lst_rootElements[count].Descendants("data").ToList()[0].Elements().ToList().Count; i++)
                {
                    root.Add(new XElement(FRM.lst_rootElements[count].Descendants("data").ToList()[0].Elements().ToList()[i].Name, " "));
                }
                 FRM.lst_rootElements[count].Add(root);

                FRM.lst_rootElements[count].Save(Application.StartupPath + "/data/" + comboBox1.Text + ".xml");
               // comboBox1.DataSource = FRM.filenames;
                rootElements = new List<XElement>();
                foreach (var temp in FRM.filenames)
                {
                    XElement root_temp;
                    root_temp = XElement.Load(Application.StartupPath + "/data/" + temp + ".xml");
                    rootElements.Add(root_temp);
                }
                ENV = new List<string>();
                var EnTerm = from a in rootElements[count].Elements("data") select a.Elements().ToList()[0].Value.ToString().ToLower().Trim();
                ENV = new List<string>();
                ENV = EnTerm.ToList();
                ENV.Sort();
                listBoxControl1.DataSource = ENV;
                textBox3.Text = null;
                textBox4.Text = null;
                return;
            }
           
            try
            {
                int count = 0;
                foreach (var item in comboBox1.Items)
                {
                    if (comboBox1.Text == item.ToString())
                    {
                        break;
                    }
                    count++;
                }
                var items = from item in FRM.lst_rootElements[count].Descendants("data")
                            where item.Elements().ToList()[0].Value.ToString().Trim().ToLower() == txt_EN.Text.ToString().ToLower()
                            select item;
                foreach (XElement itemElement in items)
                {

                    itemElement.SetElementValue(itemElement.Elements().ToList()[1].Name, txt_MEANVN.Text);
                    for (int k = 2; k < itemElement.Elements().ToList().Count; k++)
                    {
                        itemElement.SetElementValue(itemElement.Elements().ToList()[k].Name,list_temp[k-2]);
                    }

                    }
                FRM.lst_rootElements[count].Save(Application.StartupPath + "/data/" + comboBox1.Text + ".xml");
                Load_data();
                txt_EN.Text = null;
                txt_MEANVN.Text = null;
                comboBox2.Text = null;

                textBox1.Text = null;
                comboBox1.Text = comboBox1.Items[count].ToString();
                //  textBox1.Text = list_temp[count].ToString();
            }
            catch { }

        }

        private void ListBoxControl1_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                foreach (var item in comboBox2.Items)
                {
                    if (comboBox2.Text == item.ToString())
                    {
                        break;
                    }
                    count++;
                }
                textBox1.Text = list_temp[count].ToString();
            }
            catch { }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
          
            txt_EN.Text = null;
            txt_MEANVN.Text = null;
            comboBox2.Text = null;
            textBox1.Text = null;
        }

        private void Btn_DEL_Click_1(object sender, EventArgs e)
        {
          
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                foreach (var item in comboBox2.Items)
                {
                    if (comboBox2.Text == item.ToString())
                    {
                        break;
                    }
                    count++;
                }
                list_temp[count] = textBox1.Text;
            }
            catch { }
        }

        private void PictureEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void PictureEdit2_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                foreach (var item in comboBox1.Items)
                {
                    if (comboBox1.Text == item.ToString())
                    {
                        break;
                    }
                    count++;
                }
                var items = from item in FRM.lst_rootElements[count].Descendants("data")
                            where item.Elements().ToList()[0].Value.ToString().Trim().ToLower() == txt_EN.Text.ToString().ToLower()
                            select item;
                items.First().Remove();
                FRM.lst_rootElements[count].Save(Application.StartupPath + "/data/" + comboBox1.Text + ".xml");
                Load_data();
                txt_EN.Text = null;
                txt_MEANVN.Text = null;
                comboBox2.Text = null;
                textBox1.Text = null;
                return;
            }
            catch { }
        }

        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(comboBox1.Text)) return;
              int count = 0;
                foreach (var item in comboBox1.Items)
                {
                    if (comboBox1.Text == item.ToString())
                    {
                        break;
                    }
                    count++;
                }
                ENV = new List<string>();
                var EnTerm = from a in rootElements[count].Elements("data") select a.Elements().ToList()[0].Value.ToString().ToLower().Trim();
                ENV = new List<string>();
                ENV = EnTerm.ToList();
                ENV.Sort();
                listBoxControl1.DataSource = ENV;
            }
            catch { }
        }
    }
}
