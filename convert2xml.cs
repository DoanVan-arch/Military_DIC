
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

using Spire.Xls;
using Worksheet = Spire.Xls.Worksheet;

namespace TUDIEN
{
    public class convert2xml
    {
        public bool CreateXltoXML(string XmlFile, string XlFile, string RowName)
        {
            bool IsCreated = false;
            try
            {
                DataTable dt = GetTableDataXl(XlFile);
                XmlTextWriter writer = new XmlTextWriter(XmlFile, System.Text.Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartElement("tbl_" + RowName);
                List<string> ColumnNames = dt.Columns.Cast<DataColumn>().ToList().Select(x => x.ColumnName).ToList(); // Column Names List  
                List<DataRow> RowList = dt.Rows.Cast<DataRow>().ToList();
                foreach (DataRow dr in RowList)
                {
                    writer.WriteStartElement(RowName);
                    for (int i = 0; i < ColumnNames.Count; i++) // Getting Node Names from DataTable Column Names  
                    {
                        writer.WriteStartElement(ColumnNames[i]);
                        writer.WriteString(dr.ItemArray[i].ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
                if (File.Exists(XmlFile))
                    IsCreated = true;
            }
            catch (Exception ex)
            {
            }

            return IsCreated;

        }
        public DataTable Convertxls2data(string path)
        {
            String name = "Items";
            String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                           path +
                            ";Extended Properties='Excel 8.0;HDR=YES;';";

            OleDbConnection con = new OleDbConnection(constr);
            OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
            con.Open();

            OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
            DataTable data = new DataTable();
            sda.Fill(data);
            return data;
        }
        public DataTable Convertxlsx2data(string path)
        {
            Spire.Xls.Workbook workbook = new Spire.Xls.Workbook();

            workbook.LoadFromFile(path);

            Worksheet sheet = workbook.Worksheets[0];

            return sheet.ExportDataTable();


            // Close the file stream to free all resources

        }
        public string ConvertDatatableToXML(DataTable dt,string type)
        {
            MemoryStream str = new MemoryStream();
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
          
            StreamReader sr = new StreamReader(str);
            string xmlstr;
            xmlstr = sr.ReadToEnd();
            
          //  xmlstr.Trim().Remove(0,18).Insert(0, "<DocumentElement type=\"" + type + "\">");
            return xmlstr;
        }
        public DataTable GetTableDataXl(string XlFile)
        {
            DataTable dt = new DataTable();
            try
            {
                string Ext = Path.GetExtension(XlFile);
                string connectionString = "";
                if (Ext == ".xls")
                {   //For Excel 97-03  
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source =" + XlFile + "; Extended Properties = 'Excel 8.0;HDR=YES'";
                }
                else if (Ext == ".xlsx")
                {    //For Excel 07 and greater  
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source =" + XlFile + "; Extended Properties = 'Excel 8.0;HDR=YES'";
                }
                OleDbConnection conn = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();

                cmd.Connection = conn;
                //Fetch Fisrt Sheet Name  
                conn.Open();
                DataTable dtSchema;
                dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string ExcelSheetName = dtSchema.Rows[0]["TABLE_NAME"].ToString();
                conn.Close();
                //Read all data from the Sheet to a Data Table  
                conn.Open();
                cmd.CommandText = "SELECT * From [" + ExcelSheetName + "]";
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dt); // Fill Sheet Data to Datatable  
                conn.Close();
            }
            catch (Exception ex)
            { }

            return dt;
        }
        public DataTable Doc2data(string path)
        {
            Document doc = new Document();
            doc.LoadFromFile(path);
            Section section = doc.Sections[0];

            Table table = section.Tables[0] as Table;
            DataTable dt = new DataTable();
            int count = 0;
            foreach (TableRow row in table.Rows)
            {
                if(count == 0)
                foreach (TableCell cell in row.Cells)
                {
                        foreach (Paragraph paragraph in cell.Paragraphs)
                        {
                            dt.Columns.Add(paragraph.Text);
                        }
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    int temp = 0;
                    foreach (TableCell cell in row.Cells)
                    {
                        foreach (Paragraph paragraph in cell.Paragraphs)
                        {
                            dr[temp] = paragraph.Text;
                        }
                        temp++;
                    }
                    dt.Rows.Add(dr);
                }
                count++;
            }
            return dt;//    StreamReader str = new StreamReader(path);
        }
    }
}

