using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Speech.Synthesis;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using DevExpress.XtraRichEdit.API.Native;
using System.Threading;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using DevExpress.Utils.Paint;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using System.IO;
using DevExpress.XtraBars.Ribbon;
using System.Speech.Recognition;
using System.Collections.ObjectModel;
using System.Speech.Recognition.SrgsGrammar;
using DevExpress.XtraBars;

namespace TUDIEN
{
    public partial class Form1 : Form
    {
        Boolean _backspace = false;
        List<string> ENV;
        List<string> ENV1;
        List<string> ENV2;
        List<string> lst_History;
        int temp_cout = 0;
        List<int> temp_cout1=new List<int>(); 
        string output_term;
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += (sender, e) =>
            {
                if (waiting)
                    stop.Set();
            };

        }
        bool history_check = false;
        bool waiting = false;
        AutoResetEvent stop = new AutoResetEvent(false);
        public XElement rootElement;
        public XElement rootElement1;
        public XElement rootElement2;
    
        public XDocument xdochistory;
        SpeechSynthesizer synthesizer;
        //  Tesseract.TesseractEngine OCR;
        List<List<string>> temp;
         List<List<string>> temp1;
        public List<XElement> lst_rootElements;
        Shape myPicture;
        DataTable gridData ;
        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }
        public void get_list_item()
        {
            comboBox1.Items.Clear();
        //    comboBox1.Items.Add("Từ điển Anh-Việt ");
        //    comboBox1.Items.Add("Từ viết tắt Anh-Việt quân sự ");
        //    comboBox1.Items.Add("Từ điển Việt-Anh 11 ");
            lst_rootElements = new List<XElement>();
            filenames = new List<string>();
            filenames = getlistfilename();
            foreach (var item in filenames)
            {
               
                comboBox1.Items.Add(item);
                XElement rootElement_temp = XElement.Load(Application.StartupPath + @"/data/" + item + ".xml");
                lst_rootElements.Add(rootElement_temp);
                List<string> temp = new List<string>();
                temp1.Add(temp);
                temp_cout1.Add(0);
            }
        }
        string dic_EV_default;
      //  Dictionary<string, string> default_EV = new Dictionary<string, string>();

        private void Form1_Load(object sender, EventArgs e)
        {
          
           
            // searchControl1.SetFilter();
            //   Richtext_data.ContextMenuStrip = contextMenuStrip1;
            //  Richtext_data.ContextMenuStrip.Enabled = false;
            temp = new List<List<string>>() ;
            temp1 = new List<List<string>>();
            //toolStripStatusLabel1.Text = DateTime.Now.TimeOfDay.ToString();
            // this.Richtext_data.ActiveView.BackColor = Color.WhiteSmoke;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            //  xdoc = XDocument.Load(@"DATA.xml");
            Thread t1 = new Thread(() =>
            {
               lst_rootElements = new List<XElement>();
          
            filenames = getlistfilename();
            rootElement = XElement.Load(Application.StartupPath + @"/EV_DF.xml");
            rootElement1 = XElement.Load(Application.StartupPath + @"/VE_DF.xml");
               // int count_dic = 0;
                foreach (var item in filenames)
                {

                    this.comboBox1.Invoke(new Action(() =>
                    {
                        comboBox1.Items.Add(item);
                    }));
                    XElement rootElement_temp = XElement.Load(Application.StartupPath + @"/data/" + item + ".xml");
                    lst_rootElements.Add(rootElement_temp);
                    List<string> temp = new List<string>();
                    temp1.Add(temp);
                    temp_cout1.Add(0);
                  //  count_dic++;
                }
              
                synthesizer = new SpeechSynthesizer();
                synthesizer.Volume = 100;  // 0...100
                synthesizer.Rate = -2;
             //   rootElement = XElement.Load(@"book.xml");
           //     rootElement1 = XElement.Load(@"book1.xml");
            //    rootElement2 = XElement.Load(@"book3.xml");
                //  xdochistory = XDocument.Load(@"history.xml");
                //  scientists_main();
                //  if (comboBox1.Text == "Từ điển Việt-Anh 11")
                //   {
                //var EnTerm = from a in rootElement.Elements("tblAVDic") select a.Element("EnTerm").Value.ToString().ToLower();
                //ENV = new List<string>();
                //lst_History = new List<string>();
                //ENV = EnTerm.ToList();
                //ENV.Sort();

                gridData = new DataTable();
                //gridData.Columns.Add("ENV");

                //foreach (string listItem in ENV)
                //{
                //    gridData.Rows.Add(listItem);
                //}
                //gridControl1.Invoke(new Action(() => { 
                //gridControl1.DataSource = gridData;
                //}));
   
                this.Richtext_data.Invoke(new Action(() =>
               {
                   this.Richtext_data.Document.DefaultCharacterProperties.FontName = "Times New Roman";
                   this.Richtext_data.Document.DefaultCharacterProperties.FontSize = 25;
               }));
               // return;

         //   }
            
            });
            t1.Start();
        }
        private void scientists_main()
            {
            if (comboBox1.Text == "Từ điển Việt-Anh 11")
            {
                var EnTerm = from a in rootElement1.Elements("tblVADic") select a.Element("VnTerm").Value.ToString().ToLower();
                ENV1 = new List<string>();
                //lst_History = new List<string>();
                ENV1 = EnTerm.ToList();
                ENV1.Sort();
                //   lst_Terms.DataSource = ENV1;
                DataTable gridData = new DataTable();
                gridData.Columns.Add("ENV");

                foreach (string listItem in ENV1)
                {
                    gridData.Rows.Add(listItem);
                }
                gridControl1.DataSource = ENV1;
              
                //  treeList1.DataSource = ENV;
                //rootElement.ReplaceAll(from a in rootElement.Elements() select new XElement(a.Element("EnTerm").Value.ToString().Replace(' ','-'),a.Element("Type").Value,a.Element("VnMean").Value,a.Element("Example").Value));
                //foreach (XElement child in rootElement.Elements())
                //{
                //   // child.Element("EnTerm").ReplaceWith(child.Element("EnTerm").Value.ToString().Replace(' ','-'));
                //  //  child.Element("EnTerm").Value = 
                //    lst_Terms.Items.Add(child.Element("EnTerm").Value.ToString());
                //   // child.Element("EnTerm").ReplaceWith(child.Element("EnTerm").Value);
                //}
                //rootElement.Save(@"book.xml");
                //  this.Richtext_data.Document.DefaultCharacterProperties.Italic = false;
                this.Richtext_data.Document.DefaultCharacterProperties.FontName = "Lucida Sans Unicode";
                this.Richtext_data.Document.DefaultCharacterProperties.FontSize = 25;
                return;
            }
            else
            {
                var EnTerm = from a in rootElement2.Elements("tblAVDic") select a.Element("EnTerm").Value.ToString().ToLower();
                ENV2 = new List<string>();
                lst_History = new List<string>();
                ENV2 = EnTerm.ToList();
                ENV2.Sort();
                DataTable gridData = new DataTable();
                gridData.Columns.Add("ENV");

                foreach (string listItem in ENV2)
                {
                    gridData.Rows.Add(listItem);
                }
                gridControl1.DataSource = gridData;
                //rootElement.ReplaceAll(from a in rootElement.Elements() select new XElement(a.Element("EnTerm").Value.ToString().Replace(' ','-'),a.Element("Type").Value,a.Element("VnMean").Value,a.Element("Example").Value));
                //foreach (XElement child in rootElement.Elements())
                //{
                //   // child.Element("EnTerm").ReplaceWith(child.Element("EnTerm").Value.ToString().Replace(' ','-'));
                //  //  child.Element("EnTerm").Value = 
                //    lst_Terms.Items.Add(child.Element("EnTerm").Value.ToString());
                //   // child.Element("EnTerm").ReplaceWith(child.Element("EnTerm").Value);
                //}
                //rootElement.Save(@"book.xml");
                //  this.Richtext_data.Document.DefaultCharacterProperties.Italic = false;
                this.Richtext_data.Document.DefaultCharacterProperties.FontName = "Lucida Sans Unicode";
                this.Richtext_data.Document.DefaultCharacterProperties.FontSize = 25;
                //rootElement.ReplaceAll(from a in rootElement.Elements() select new XElement(a.Element("EnTerm").Value.ToString().Replace(' ','-'),a.Element("Type").Value,a.Element("VnMean").Value,a.Element("Example").Value));
                //foreach (XElement child in rootElement.Elements())
                //{
                //   // child.Element("EnTerm").ReplaceWith(child.Element("EnTerm").Value.ToString().Replace(' ','-'));
                //  //  child.Element("EnTerm").Value = 
                //    lst_Terms.Items.Add(child.Element("EnTerm").Value.ToString());
                //   // child.Element("EnTerm").ReplaceWith(child.Element("EnTerm").Value);
                //}
                //rootElement.Save(@"book.xml");
                //  this.Richtext_data.Document.DefaultCharacterProperties.Italic = false;
                this.Richtext_data.Document.DefaultCharacterProperties.FontName = "Lucida Sans Unicode";
                this.Richtext_data.Document.DefaultCharacterProperties.FontSize = 25;
                return;

            }

        }
        private void lst_term_Click(object sender, EventArgs e)
        {
           // Richtext_data.Document.CaretPosition = Richtext_data.Document.Range.Start;
            string cellValue = "";
           
            if (comboBox1.Text == "Từ điển Việt-Anh 11")
            {
                int[] selectedRows = gridView1.GetSelectedRows();
                foreach (int rowHandle in selectedRows)
                {
                    if (rowHandle >= 0)
                        cellValue = gridView1.GetRowCellValue(rowHandle, "ENV").ToString();
                }
                if (Richtext_data.Document.Images[0] != null)
                    Richtext_data.Document.Delete(Richtext_data.Document.Images[0].Range);
                foreach (XElement child in rootElement1.Elements())
                {
                    //   Text_search.Text = lst_term.SelectedItem.ToString();
                    if (cellValue.ToString().Trim() == child.Element("VnTerm").Value.ToString().Trim().ToLower())
                    {
                        // changeFontColorItem1.SelectedColor = Color.Red;

                        lb_term.Text = child.Element("VnTerm").Value.ToString().ToLower();



                        string text_temp =  "\n Tiếng anh: \n     " + child.Element("EnMean").Value.ToString();
                        int k1 = text_temp.Length;
                        text_temp = text_temp + "\nPhiên âm:\n   "+GetPronunciationFromText(child.Element("EnMean").Value.ToString()).ToString().Trim().Replace(" ", "/ /").Replace("//", "") + "/";
                        // int K2 = text_temp.Length;
                        if (Richtext_data.Text == "")
                        {
                            Richtext_data.Document.AppendText(text_temp);
                            font(0, text_temp.Length, Color.Black);
                            font(0, 12, Color.Blue);
                            font(k1, 12, Color.Blue);
                            break;
                        }
                        Richtext_data.Document.SelectAll();
                        DocumentRange selection = Richtext_data.Document.Selection;
                        //  Richtext_data.Document.Replace
                        // ReplaceAll(Richtext_data.Document.GetText(selection),text_temp);
                        ReplaceAll(Richtext_data.Document.GetText(selection), text_temp);

                          font(0, text_temp.Length, Color.Black);
                        font(0, 12, Color.Blue);
                        font(k1, 10, Color.Blue);
                        //   Richtext_data.Document.AppendText(text_temp);

                        //   changeFontColorItem1.SelectedColor = Color.Black;

                        //  Richtext_data.Document.DefaultCharacterProperties.BackColor = Color.Transparent;
                        Richtext_data.Document.Selection = Richtext_data.Document.CreateRange(0, 0);
                        //DocumentRange range = Richtext_data.Document.CreateRange(0,7);
                        //DocumentRange range1 = Richtext_data.Document.CreateRange(Richtext_data.Document., 7); 
                        //CharacterProperties prop = Richtext_data.Document.BeginUpdateCharacters(range);
                        //CharacterProperties prop1 = Richtext_data.Document.BeginUpdateCharacters(range1);
                        //prop.ForeColor = Color.Red;
                        //prop1.ForeColor = Color.Red;
                        //Richtext_data.Document.EndUpdateCharacters(prop);
                        //Richtext_data.Document.EndUpdateCharacters(prop1);
                        break;

                        // Richtext_data.Document.AppendText(text_temp);


                    }
                }
              //  temp1.Add(lb_term.Text);

                //   Richtext_data.Document.AppendText("_______________________________________________");
                //temp_cout1[count] = temp1.Count - 1;
                return;
            }
            else
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
           //     var EnTerm = from a in lst_rootElements[count].Elements("data") select a.Elements().ToList()[0].Value.ToString().Trim();
                int[] selectedRows = gridView1.GetSelectedRows();
                foreach (int rowHandle in selectedRows)
                {
                    if (rowHandle >= 0)
                        cellValue = gridView1.GetRowCellValue(rowHandle, "ENV").ToString();
                }
                if (Richtext_data.Document.Images[0] != null)
                    Richtext_data.Document.Delete(Richtext_data.Document.Images[0].Range);
                foreach (XElement child in lst_rootElements[count].Elements())
                {
                    if (cellValue.ToString().Trim().ToLower() == child.Elements().ToList()[0].Value.ToString().Trim().ToLower())
                    {
                        if (Richtext_data.Text.Length > 0)
                        {
                            Richtext_data.Document.SelectAll();
                            DocumentRange selection = Richtext_data.Document.Selection;
                            //  Richtext_data.Document.Replace
                            // ReplaceAll(Richtext_data.Document.GetText(selection),text_temp);
                            ReplaceAll(Richtext_data.Document.GetText(selection), "");
                        }
                            lb_term.Text = child.Elements().ToList()[0].Value.ToString().Trim().ToLower();
                        for(int h =0;h < child.Elements().ToList().Count;h++)
                        {
                            if (String.IsNullOrEmpty(child.Elements().ToList()[h].Value.ToString())) continue;
                            string text_temp = "\n "+ child.Elements().ToList()[h].Name.ToString().Replace("_x0020_"," ") + ": \n     " + child.Elements().ToList()[h].Value.ToString();
                          //  font(0, text_temp.Length, Color.Black);
                           
                            Richtext_data.Document.AppendText(text_temp);
                           
                            font(Richtext_data.Document.Length - text_temp.Length, child.Elements().ToList()[h].Name.ToString().Length+1, Color.Blue);
                            font(Richtext_data.Document.Length - child.Elements().ToList()[h].Value.ToString().Length-1, child.Elements().ToList()[h].Value.ToString().Length, Color.Black);
                        }
                        if (toggleSwitch1.IsOn == true && (string)lst_rootElements[count].Attribute("type").Value =="1")
                        {
                            string text_temp = "\nPhiên âm:\n   " + "/" + GetPronunciationFromText(child.Elements().ToList()[0].Value.ToString().Replace('-', ' ').Replace('"', ' ')).ToString().Trim().Replace(" ", "/ /").Replace("//", "") + "/";
                           Richtext_data.Document.AppendText(text_temp);
                            font(Richtext_data.Document.Length - text_temp.Length, 9 + 1, Color.Blue);
                        }
                        if (toggleSwitch1.IsOn == true && (string)lst_rootElements[count].Attribute("type").Value == "0")
                        {
                            string text_temp = "\nPhiên âm:\n   " + "/" + GetPronunciationFromText(child.Elements().ToList()[1].Value.ToString().Replace('-', ' ').Replace('"', ' ')).ToString().Trim().Replace(" ", "/ /").Replace("//", "") + "/";
                            Richtext_data.Document.AppendText(text_temp);
                            font(Richtext_data.Document.Length - text_temp.Length, 9 + 1, Color.Blue);
                        }
                            break;
                    }
                   
                }
                temp1[count].Add(lb_term.Text);

                //   Richtext_data.Document.AppendText("_______________________________________________");
                temp_cout1[count] = temp1[count].Count - 1;
                return;
             }
        
          
            
        }
      
        private void font(int start, int end,Color color)
        {
            DocumentRange range = Richtext_data.Document.CreateRange(start, end);
           
            CharacterProperties prop = Richtext_data.Document.BeginUpdateCharacters(range);

            prop.ForeColor = color;
            Richtext_data.Document.EndUpdateCharacters(prop);
            
        }
        private void ReplaceAll(string strFind, string strReplace) {
        if(!String.IsNullOrEmpty(strFind)) {
            DocumentRange[] ranges = Richtext_data.Document.FindAll(strFind, SearchOptions.None, Richtext_data.Document.Range);
            for(int i = 0; i < ranges.Length; i++) {
                if(strReplace == "null")
                    strReplace = String.Empty;
                Richtext_data.Document.Replace(ranges[i], strReplace);
                CharacterProperties cp = Richtext_data.Document.BeginUpdateCharacters(ranges[i]);
               // cp.BackColor = System.Drawing.Color.FromArgb(180, 201, 233);
                Richtext_data.Document.EndUpdateCharacters(cp);
            };
        }
    }
        

        private void undoItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Richtext_data.Document.Selection = Richtext_data.Document.CreateRange(0, 0);
        }

        private void Richtext_data_DoubleClick(object sender, EventArgs e)
        {
           


        }

       

        private void Richtext_data_KeyPress(object sender, KeyPressEventArgs e)
        {
           // if (e.KeyCode == Keys.A || e.KeyCode == Keys.C) return;
           
            
        }

        private void redoItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Richtext_data.Document.Selection = Richtext_data.Document.CreateRange(0, 0);
        }

        private void Richtext_data_KeyDown(object sender, KeyEventArgs e)
        {
 
             
            
        }

        

     

        private void Text_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                _backspace = true;
            }
        }

       

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (comboBox1.Text == "Từ điển Anh-Việt 11")
            {
             //   synthesizer.Speak(lb_term.Text);

                // Asynchronous
                synthesizer.SpeakAsync(lb_term.Text);
                return;
            }
            if (comboBox1.Text == "Từ điển Việt-Anh 11")
            {
               // barButtonItem5.Enabled = false;
                return;
                //DocumentRange range = Richtext_data.Document.CreateRange(12,Richtext_data.Document.Text.Length);
                //synthesizer.Speak(Richtext_data.Document.GetText(range));
                //synthesizer.SpeakAsync(Richtext_data.Document.GetText(range));
                //return;
            }
            else
            {
                synthesizer.SpeakAsync(lb_term.Text);
                return;
            }
        }
        
        private void fileNewItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lb_term.Text = "";
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lb_term.Text = "";
            get_list_item();
         //   Translate_Text trantext = new Translate_Text(this,output_term);
          //  trantext.ShowDialog();
        }

        private void changeFontSizeItem1_EditValueChanged(object sender, EventArgs e)
        {
            Richtext_data.Document.SelectAll();
        }

        private void copyItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barEditItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

       
      /*  private void AddHistory()
        {
            var newElement  = new XElement("items", new XElement("Enterm", lb_term.Text.ToString())
                ,new XElement("Date",DateTime.Now.ToString("dd/MM/yyyy"))
                ,new XElement("Time",DateTime.Now.ToString("h:mm:ss")));
            xdochistory.Element("dataroot").Add(newElement);
            xdochistory.Save("history.xml");
        }*/
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            help frm_help = new help(true);
            frm_help.Show();
        }
       


        private void ribbonControl1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                RibbonControl ribbon = sender as RibbonControl;
                RibbonHitInfo hitInfo = ribbonControl1.CalcHitInfo(e.Location);
                // RibbonHitInfo hitInfo = ribbon.CalcHitInfo(e.Location);
                if (hitInfo == null || hitInfo.Page == null)
                {
                    return;
                }
                else
                {
                    if (hitInfo.Page.Text == @"Lịch sử")
                    {

                        var History = from a in xdochistory.Element("dataroot").Elements("items") select a.Element("Enterm").Value.ToString().ToLower();
                        lst_History = History.ToList();
                        gridData = new DataTable();
                        gridData.Columns.Add("ENV");

                        foreach (string listItem in lst_History)
                        {
                            gridData.Rows.Add(listItem);
                        }
                        gridControl1.DataSource = gridData;
                      //  lst_Terms.DataSource = lst_History;
                        reset();
                        lb_datehs.Caption = "Ngày tìm kiếm :      ";
                        lb_timehs.Caption = "Thời gian tìm kiếm : ";
                        history_check = true;
                    }
                    else
                    {
                        history_check = false;
                       // var EnTerm = from a in rootElement.Elements("tblAVDic") select a.Element("EnTerm").Value.ToString().ToLower();
                      ////  ENV = EnTerm.ToList();
                     //   ENV.Sort();
                    //    lst_Terms.DataSource = ENV;
                    }
                }
            }
            catch { }

        }

        private void lst_Terms_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      
        private void reset()
        {
            lb_term.Text = null;
            Richtext_data.Text = "";
        }
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xdochistory.Element("dataroot").Elements("items").Remove();
            reset();
            //lst_Terms.DataSource = null;
            xdochistory.Save("history.xml");
        }

        private void Text_search_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
          //  MessageBox.Show(temp_cout1[count].ToString());
                if (temp_cout1[count] == 0) { return; }
                foreach (XElement child in lst_rootElements[count].Elements())
                {
                    if (temp1[count][temp_cout1[count] - 1].ToString().ToLower() == child.Elements().ToList()[0].Value.ToString().ToLower())
                    {
                        if (Richtext_data.Text.Length > 0)
                        {
                            Richtext_data.Document.SelectAll();
                            DocumentRange selection = Richtext_data.Document.Selection;
                            //  Richtext_data.Document.Replace
                            // ReplaceAll(Richtext_data.Document.GetText(selection),text_temp);
                            ReplaceAll(Richtext_data.Document.GetText(selection), "");
                        }
                        lb_term.Text = child.Elements().ToList()[0].Value.ToString().Trim().ToLower();
                        for (int h = 0; h < child.Elements().ToList().Count; h++)
                        {
                            if (String.IsNullOrEmpty(child.Elements().ToList()[h].Value.ToString())) continue;
                            string text_temp = "\n " + child.Elements().ToList()[h].Name.ToString().Replace("_x0020_", " ") + ": \n     " + child.Elements().ToList()[h].Value.ToString();
                            //  font(0, text_temp.Length, Color.Black);

                            Richtext_data.Document.AppendText(text_temp);

                            font(Richtext_data.Document.Length - text_temp.Length, child.Elements().ToList()[h].Name.ToString().Length + 1, Color.Blue);
                            font(Richtext_data.Document.Length - child.Elements().ToList()[h].Value.ToString().Length - 1, child.Elements().ToList()[h].Value.ToString().Length, Color.Black);
                        }
                        if (toggleSwitch1.IsOn == true && (string)lst_rootElements[count].Attribute("type").Value == "1")
                        {
                            string text_temp = "\nPhiên âm:\n   " + "/" + GetPronunciationFromText(child.Elements().ToList()[0].Value.ToString().Replace('-', ' ').Replace('"', ' ')).ToString().Trim().Replace(" ", "/ /").Replace("//", "") + "/";
                            Richtext_data.Document.AppendText(text_temp);
                            font(Richtext_data.Document.Length - text_temp.Length, 9 + 1, Color.Blue);
                        }
                        if (toggleSwitch1.IsOn == true && (string)lst_rootElements[count].Attribute("type").Value == "0")
                        {
                            string text_temp = "\nPhiên âm:\n   " + "/" + GetPronunciationFromText(child.Elements().ToList()[1].Value.ToString().Replace('-', ' ').Replace('"', ' ')).ToString().Trim().Replace(" ", "/ /").Replace("//", "") + "/";
                            Richtext_data.Document.AppendText(text_temp);
                            font(Richtext_data.Document.Length - text_temp.Length, 9 + 1, Color.Blue);
                        }
                        break;
                    }
                }
                temp_cout1[count]--;

            
        }

        private void RendoItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                //   if (temp_cout1 == temp1.Count - 1) { return; }
                if (temp_cout1[count] == temp1[count].Count) { return; }
                foreach (XElement child in lst_rootElements[count].Elements())
                {
                    if (temp1[count][temp_cout1[count] + 1].ToString().ToLower() == child.Elements().ToList()[0].Value.ToString().ToLower())
                    {
                        if (Richtext_data.Text.Length > 0)
                        {
                            Richtext_data.Document.SelectAll();
                            DocumentRange selection = Richtext_data.Document.Selection;
                            //  Richtext_data.Document.Replace
                            // ReplaceAll(Richtext_data.Document.GetText(selection),text_temp);
                            ReplaceAll(Richtext_data.Document.GetText(selection), "");
                        }
                        lb_term.Text = child.Elements().ToList()[0].Value.ToString().Trim().ToLower();
                        for (int h = 0; h < child.Elements().ToList().Count; h++)
                        {
                            if (String.IsNullOrEmpty(child.Elements().ToList()[h].Value.ToString())) continue;
                            string text_temp = "\n " + child.Elements().ToList()[h].Name.ToString().Replace("_x0020_", " ") + ": \n     " + child.Elements().ToList()[h].Value.ToString();
                            //  font(0, text_temp.Length, Color.Black);

                            Richtext_data.Document.AppendText(text_temp);

                            font(Richtext_data.Document.Length - text_temp.Length, child.Elements().ToList()[h].Name.ToString().Length + 1, Color.Blue);
                            font(Richtext_data.Document.Length - child.Elements().ToList()[h].Value.ToString().Length - 1, child.Elements().ToList()[h].Value.ToString().Length, Color.Black);
                        }
                        if (toggleSwitch1.IsOn == true && (string)lst_rootElements[count].Attribute("type").Value == "1")
                        {
                            string text_temp = "\nPhiên âm:\n   " + "/" + GetPronunciationFromText(child.Elements().ToList()[0].Value.ToString().Replace('-', ' ').Replace('"', ' ')).ToString().Trim().Replace(" ", "/ /").Replace("//", "") + "/";
                            Richtext_data.Document.AppendText(text_temp);
                            font(Richtext_data.Document.Length - text_temp.Length, 9 + 1, Color.Blue);
                        }
                        if (toggleSwitch1.IsOn == true && (string)lst_rootElements[count].Attribute("type").Value == "0")
                        {
                            string text_temp = "\nPhiên âm:\n   " + "/" + GetPronunciationFromText(child.Elements().ToList()[1].Value.ToString().Replace('-', ' ').Replace('"', ' ')).ToString().Trim().Replace(" ", "/ /").Replace("//", "") + "/";
                            Richtext_data.Document.AppendText(text_temp);
                            font(Richtext_data.Document.Length - text_temp.Length, 9 + 1, Color.Blue);
                        }
                        break;
                    }
                }
                temp_cout1[count]++;
                //   temp_cout1++;

            }
            catch { }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
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
                    var EnTerm = from a in lst_rootElements[count].Elements("data") select a.Elements().ToList()[0].Value.ToString().Trim();
                    ENV2 = new List<string>();
                    //lst_History = new List<string>();
                    ENV2 = EnTerm.ToList();
                    ENV2.Sort();
                    //lst_Terms.DataSource = ENV2;
                    gridData = new DataTable();
                    gridData.Columns.Add("ENV");

                    foreach (string listItem in ENV2)
                    {
                        gridData.Rows.Add(listItem);
                    }
                    gridControl1.DataSource = gridData;
                    //rootElement.ReplaceAll(from a in rootElement.Elements() select new XElement(a.Element("EnTerm").Value.ToString().Replace(' ','-'),a.Element("Type").Value,a.Element("VnMean").Value,a.Element("Example").Value));
                    //foreach (XElement child in rootElement.Elements())
                    //{
                    //   // child.Element("EnTerm").ReplaceWith(child.Element("EnTerm").Value.ToString().Replace(' ','-'));
                    //  //  child.Element("EnTerm").Value = 
                    //    lst_Terms.Items.Add(child.Element("EnTerm").Value.ToString());
                    //   // child.Element("EnTerm").ReplaceWith(child.Element("EnTerm").Value);
                    //}
                    //rootElement.Save(@"book.xml");
                    //  this.Richtext_data.Document.DefaultCharacterProperties.Italic = false;
                    this.Richtext_data.Document.DefaultCharacterProperties.FontName = "Times New Roman";
                    this.Richtext_data.Document.DefaultCharacterProperties.FontSize = 25;
                    return;
                }
                catch {  }
            
        }


  

        

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            DocumentRange range = Richtext_data.Document.Selection;
           // synthesizer.Speak(Richtext_data.Document.GetText(range));
            synthesizer.SpeakAsync(Richtext_data.Document.GetText(range));
            return;
        }

     
     

 

        private void SearchControl1_TextChanged(object sender, EventArgs e)
        {
            //  MessageBox.Show(lst_Terms.ItemCount.ToString());
            gridView1.ActiveFilterString = "[ENV]= \"" + searchControl1.Text+"\"";
        }

   
        private void BarButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DocumentRange selection = Richtext_data.Document.Selection;
            child_form CHILD_FRM = new child_form(Richtext_data.Document.GetText(selection), this, true);
            CHILD_FRM.ShowDialog();
        }

        private void PopupMenu1_BeforePopup(object sender, CancelEventArgs e)
        {
            if(comboBox1.Text == "Từ điển Anh-Việt 11")
            {
                barButtonItem3.Enabled = false;
                barButtonItem11.Enabled = false;
                barButtonItem10.Enabled = true;
                barButtonItem8.Enabled = true;
                return;
            }
            if (comboBox1.Text == "Từ điển Việt-Anh 11")
            {
                barButtonItem10.Enabled = false;
                barButtonItem8.Enabled = false;
                barButtonItem3.Enabled = true;
                barButtonItem11.Enabled = true;
                return;
            }
            else
            {
                barButtonItem10.Enabled =true;
                barButtonItem8.Enabled = true;
                barButtonItem3.Enabled = true;
                barButtonItem11.Enabled = true;
                return;
            }
            
        }

        private void BarButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DocumentRange selection = Richtext_data.Document.Selection;
            child_form CHILD_FRM = new child_form(Richtext_data.Document.GetText(selection), this, false);
            CHILD_FRM.ShowDialog();
        }

        private void BarButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            DocumentRange range = Richtext_data.Document.Selection;
            string value = "không có dữ liệu";
            var EnTerm = from a in rootElement1.Elements("data") select a.Element("key").Value.ToString().ToLower();
            List<string> lsst_key = new List<string>();
            //   lst_History = new List<string>();
            lsst_key = EnTerm.ToList();

            int item = lsst_key.BinarySearch(Richtext_data.Document.GetText(range).ToString().Trim().ToLower());

            try
            {
                value = rootElement1.Elements().ToList()[item].Element("value").Value.ToString();
            }
            catch { }
            //   rootElement.Elements("data") select a.Elements().ToList()[0].Value.ToString().Trim();
            toolTipController1.ShowHint(value, "", MousePosition);
        }

        private void BarButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            DocumentRange range = Richtext_data.Document.Selection;
            string value = "không có dữ liệu";
            var EnTerm = from a in rootElement.Elements("data") select a.Element("key").Value.ToString().ToLower();
           List<string> lsst_key = new List<string>();
            //   lst_History = new List<string>();
            lsst_key = EnTerm.ToList();
             
            int item = lsst_key.BinarySearch(Richtext_data.Document.GetText(range).ToString().Trim().ToLower());
         
            try
            {
                value = rootElement.Elements().ToList()[item].Element("value").Value.ToString();
            }
            catch { }
            //   rootElement.Elements("data") select a.Elements().ToList()[0].Value.ToString().Trim();
            toolTipController1.ShowHint(value, "", MousePosition);
         
           
        }
     

        private void BarButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm1 form2 = new XtraForm1(this);
            form2.ShowDialog();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    StreamWriter writetext = new StreamWriter(Application.StartupPath + "\\path.txt");
        //    for (int i =0;i<ENV1.Count;i++)
        //    {
        //        if(ENV1[i].ToString().Length > 5)
        //            writetext.WriteLine(ENV1[i].ToString());
        //    }
        public static string recoPhonemes;
        public static string GetPronunciationFromText(string MyWord)
        {
            //this is a trick to figure out phonemes used by synthesis engine

            //txt to wav
            using (MemoryStream audioStream = new MemoryStream())
            {
                using (SpeechSynthesizer synth = new SpeechSynthesizer())
                {
                    synth.SetOutputToWaveStream(audioStream);
                    PromptBuilder pb = new PromptBuilder();
                    //pb.AppendBreak(PromptBreak.ExtraSmall); //'e' wont be recognized if this is large, or non-existent?
                    //synth.Speak(pb);
                    synth.Speak(MyWord);
                    //synth.Speak(pb);
                    synth.SetOutputToNull();
                    audioStream.Position = 0;

                    //now wav to txt (for reco phonemes)
                    recoPhonemes = String.Empty;
                    GrammarBuilder gb = new GrammarBuilder(MyWord);
                    Grammar g = new Grammar(gb); //TODO the hard letters to recognize are 'g' and 'e'
                    SpeechRecognitionEngine reco = new SpeechRecognitionEngine();
                    reco.SpeechHypothesized += new EventHandler<SpeechHypothesizedEventArgs>(reco_SpeechHypothesized);
                    reco.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(reco_SpeechRecognitionRejected);
                    reco.UnloadAllGrammars(); //only use the one word grammar
                    reco.LoadGrammar(g);
                    reco.SetInputToWaveStream(audioStream);
                    RecognitionResult rr = reco.Recognize();
                    reco.SetInputToNull();
                    if (rr != null)
                    {
                        recoPhonemes = StringFromWordArray(rr.Words, WordType.Pronunciation);
                    }
                    //txtRecoPho.Text = recoPhonemes;
                    return recoPhonemes.Replace("ɻ", "r").Replace("o", "əʊ").Replace("e͡i", "ei").Replace("a͡i","ai");
                }
            }
        }
        public static void reco_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            recoPhonemes = StringFromWordArray(e.Result.Words, WordType.Pronunciation);
        }

        public static void reco_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            recoPhonemes = StringFromWordArray(e.Result.Words, WordType.Pronunciation);
        }
        public static string StringFromWordArray(ReadOnlyCollection<RecognizedWordUnit> words, WordType type)
        {
            string text = "";
            foreach (RecognizedWordUnit word in words)
            {
                string wordText = "";
                if (type == WordType.Text || type == WordType.Normalized)
                {
                    wordText = word.Text;
                }
                else if (type == WordType.Lexical)
                {
                    wordText = word.LexicalForm;
                }
                else if (type == WordType.Pronunciation)
                {
                    wordText = word.Pronunciation;
                    //MessageBox.Show(word.LexicalForm);
                }
                else
                {
                    throw new InvalidEnumArgumentException(String.Format("[0}: is not a valid input", type));
                }
                //Use display attribute

                if ((word.DisplayAttributes & DisplayAttributes.OneTrailingSpace) != 0)
                {
                    wordText += " ";
                }
                if ((word.DisplayAttributes & DisplayAttributes.TwoTrailingSpaces) != 0)
                {
                    wordText += "  ";
                }
                if ((word.DisplayAttributes & DisplayAttributes.ConsumeLeadingSpaces) != 0)
                {
                    wordText = wordText.TrimStart();
                }
                if ((word.DisplayAttributes & DisplayAttributes.ZeroTrailingSpaces) != 0)
                {
                    wordText = wordText.TrimEnd();
                }

                text += wordText;

            }
            return text;
        }
        //}
        public enum WordType
        {
            Text,
            Normalized = Text,
            Lexical,
            Pronunciation
        }
         public List<string> filenames = new List<string>();
        List<string> getlistfilename()
        {
            DirectoryInfo d = new DirectoryInfo(Application.StartupPath + "\\data"); //Assuming Test is your Folder

            FileInfo[] Files = d.GetFiles("*.xml"); //Getting Text files
           List<string> str = new List<string>();

            foreach (FileInfo file in Files)
            {
                str.Add(file.Name.Split('.').ToList()[0]);
             //   MessageBox.Show(file.Name);
            }
            return str;
        }
        private void RibbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void BarButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fix fix_frm = new fix(this);
            fix_frm.ShowDialog();
        }

      

        private void BarButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string baseFolder = @"";
            XtraFolderBrowserDialog xdf = new XtraFolderBrowserDialog();
            if(xdf.ShowDialog() == DialogResult.OK)
            {
                baseFolder = xdf.SelectedPath;
            }
           
            string source =Application.StartupPath + "//data";
            string target = Path.Combine(baseFolder, DateTime.Now.ToString("yyyyMMddHHmmss"));
            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }
            foreach (var srcPath in Directory.GetFiles(source))
            {
                //Copy the file from sourcepath and place into mentioned target path, 
                //Overwrite the file if same file is exist in target path
                File.Copy(srcPath, srcPath.Replace(source, target), true);
            }
        }

        private void BarButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string baseFolder = @"";
                XtraFolderBrowserDialog xdf = new XtraFolderBrowserDialog();
                if (xdf.ShowDialog() == DialogResult.OK)
                {
                    baseFolder = xdf.SelectedPath;
                }

                string source = baseFolder;
                string target = Application.StartupPath + "/data";
                if (!Directory.Exists(target))
                {
                    Directory.CreateDirectory(target);
                }
                foreach (var srcPath in Directory.GetFiles(source))
                {
                    //Copy the file from sourcepath and place into mentioned target path, 
                    //Overwrite the file if same file is exist in target path
                    string FileToSave = srcPath.Replace(source, target);
                    if (File.Exists(FileToSave))
                    {
                        FileToSave = GetNewFileName(FileToSave);
                    }
                    try
                    {
                        File.Copy(srcPath, FileToSave, false);
                    }
                    catch (Exception)
                    {
                        //something went really wrong
                    }
                    get_list_item();
                }
            }
            catch { }
        }
         public string GetNewFileName(string oldFileName)
        {
            var counter = 0;
            var extension = Path.GetExtension(oldFileName);
            var directory = Path.GetDirectoryName(oldFileName);
            var fileName = Path.GetFileNameWithoutExtension(oldFileName);

            var newFileName = Path.Combine(directory,
               string.Format("{0}_{1}{2}", fileName, counter,extension));

            while (File.Exists(newFileName))
            {
                counter++;
                newFileName = Path.Combine(directory,
                  string.Format("{0}_{1}{2}", fileName, counter,extension));
            }

            return newFileName;
        }
        private void clearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                clearFolder(di.FullName);
                di.Delete();
            }
        }

        private void BarButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string message = "Bạn có muốn xóa toàn bộ dữ liệu";
            string title = "Xóa từ điển";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                gridControl1.DataSource = null;

                lst_rootElements = new List<XElement>();
                clearFolder(Application.StartupPath + "\\data");
                filenames = new List<string>();
                filenames = getlistfilename();
                comboBox1.Items.Clear();
        
                comboBox1.Text = null;
                foreach (var item in filenames)
                {

                    comboBox1.Items.Add(item);
                    XElement rootElement_temp = XElement.Load(Application.StartupPath + @"/data/" + item + ".xml");
                    lst_rootElements.Add(rootElement_temp);
                }
            }
            else
            {
                // Do something  
            }
           
        }

        private void BarButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string message = "Bạn có muốn xóa từ điển này";
            string title = "Xóa từ điển";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                gridControl1.DataSource = null;
         
                lst_rootElements = new List<XElement>();
                File.Delete(Application.StartupPath + @"/data/" + comboBox1.Text + ".xml");
                filenames = new List<string>();
                filenames = getlistfilename();
                comboBox1.Items.Clear();
         
                comboBox1.Text = null;
                foreach (var item in filenames)
                {

                    comboBox1.Items.Add(item);
                    XElement rootElement_temp = XElement.Load(Application.StartupPath + @"/data/" + item + ".xml");
                    lst_rootElements.Add(rootElement_temp);
                }
            }
            else
            {
                // Do something  
            }
        }

        private void BarButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // int durationMilliseconds = 10000;
         //   var obj = (BarButtonItem)sender;
            DocumentRange range = Richtext_data.Document.Selection;
            // synthesizer.Speak(Richtext_data.Document.GetText(range));
           // synthesizer.SpeakAsync(Richtext_data.Document.GetText(range));
            toolTipController1.ShowHint("/" + GetPronunciationFromText( Richtext_data.Document.GetText(range).ToString().Replace('-', ' ').Replace('"', ' ')).ToString().Trim().Replace(" ", "/ /").Replace("//", "") + "/","Phiên âm",MousePosition);
        }

        private void Richtext_data_TextChanged(object sender, EventArgs e)
        {
            toolTipController1.HideHint();
        }

        private void BarButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
            help frm_help = new help(false);
            frm_help.Show();
        }

        private void PanelControl3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}