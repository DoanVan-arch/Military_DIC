using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TUDIEN
{
    public partial class help : Form
    {
        bool check;
        public help(bool CHECK)
        {
            check = CHECK;
            InitializeComponent();
        }

        private void help_Load(object sender, EventArgs e)
        {
           

            // Determine whether the user selected a file from the OpenFileDialog. 
           
                // Load the contents of the file into the RichTextBox.
                if(check == true)
             richTextBox1.LoadFile(@"Help.rtf");
                else
                richTextBox1.LoadFile(@"Info.rtf");

        }
    }
}
