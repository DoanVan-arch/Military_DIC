using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Text.RegularExpressions;

namespace TUDIEN
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        Form1 FRM;
        public XtraForm1(Form1 frm)
        {
            FRM = frm;
            InitializeComponent();
        }
        convert2xml conver2xml = new convert2xml();
        private void TextBox1_DoubleClick(object sender, EventArgs e)
        {
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            try
            {
                if (comboBox1.Text.Contains(".xlsx"))
                {
                    XtraOpenFileDialog sfd = new XtraOpenFileDialog();
                    sfd.Filter = "XLSX (*.xlsx)|*.xlsx";
                    //   sfd.FileName = "Output.xlsx";
                    bool fileError = false;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        textBox1.Text = sfd.FileName;
                        if (!fileError)
                        {
                            gridControl1.DataSource = conver2xml.Convertxlsx2data(sfd.FileName);
                            //  FRM.comboBox1.Items.Add("Từ điển Việt-Anh");
                        }
                    }
                }
                else
                {
                    XtraOpenFileDialog sfd = new XtraOpenFileDialog();
                    sfd.Filter = "DOCX (*.docx)|*.docx";
                    //   sfd.FileName = "Output.xlsx";
                    bool fileError = false;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        textBox1.Text = sfd.FileName;
                        if (!fileError)
                        {
                            gridControl1.DataSource = conver2xml.Doc2data(sfd.FileName);
                        }
                    }
                }
            }
            catch { }
        }
        
        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)gridControl1.DataSource;
                dt.TableName = "data";
              
                string FileToSave = Application.StartupPath + "/data/" + textBox2.Text + ".xml";
                if (File.Exists(FileToSave))
                {
                    FileToSave = FRM.GetNewFileName(FileToSave);
                }
                string data = "";
                if (comboBox2.Text == "Từ điển Anh - Việt")
                {
                    data = conver2xml.ConvertDatatableToXML(dt, "1");

                }
                else
                {
                    data = conver2xml.ConvertDatatableToXML(dt, "0");


                    //  File.WriteAllText(Application.StartupPath + "//1.txt", data);
                    //     data.Replace(@"<DocumentElement", "<DocumentElement type=\"0\"");
                    // MessageBox.Show(data.Substring(0, 100));
                }
                File.WriteAllText(Application.StartupPath + "/data/" + textBox2.Text + ".xml", data);
                data = File.ReadAllText(Application.StartupPath + "/data/" + textBox2.Text + ".xml");
                if (comboBox2.Text == "Từ điển Anh - Việt")
                {
                    data = data.Replace(@"<DocumentElement", "<DocumentElement type=\"1\"");

                }
                else
                {
                    data = data.Replace(@"<DocumentElement", "<DocumentElement type=\"0\"");


                    //  File.WriteAllText(Application.StartupPath + "//1.txt", data);
                    //     data.Replace(@"<DocumentElement", "<DocumentElement type=\"0\"");
                    // MessageBox.Show(data.Substring(0, 100));
                }
                File.WriteAllText(Application.StartupPath + "/data/" + textBox2.Text + ".xml", data);
                MessageBox.Show("Đã thêm dữ liệu mới");
                FRM.get_list_item();
                this.Close();
                this.Dispose();
            }
            catch { }
         // File.WriteAllText(Application.StartupPath,conver2xml.ConvertDatatableToXML((DataTable)gridControl1.DataSource));
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {

        }

        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}