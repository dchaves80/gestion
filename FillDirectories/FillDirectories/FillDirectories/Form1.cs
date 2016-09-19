using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FillDirectories
{
    public partial class Form1 : Form
    {

        public string CurrentPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbl_CurrentPath.Text = Application.StartupPath;
            Application.DoEvents();
            txtConsole.Text = "";
            ExaminePath(Application.StartupPath);
        }

        private void ExaminePath(String Path) 
        {
            lbl_CurrentPath.Text = Path;
            Application.DoEvents();
            if (Directory.Exists(Path)) 
            {
                string[] MyFiles = Directory.GetFiles(Path);
                if (MyFiles != null && MyFiles.Length > 0)
                {

                }
                else 
                {
                    txtConsole.Text += "\r\n" + Path;
                    FileStream FS = File.Create(Path + "\\gitkeep.git");
                    FS.Close();
                    StreamWriter SW = new StreamWriter(Path + "\\gitkeep.git");
                    SW.WriteLine("#Auto generate file");
                    SW.Close();
                    Application.DoEvents();
                }

                string[] MyDirectories = Directory.GetDirectories(Path);
                if (MyDirectories != null && MyDirectories.Length > 0) 
                {
                    for (int a = 0; a < MyDirectories.Length; a++) 
                    {
                        ExaminePath(MyDirectories[a]);
                    }
                }

            }
            
        }
    }
}
