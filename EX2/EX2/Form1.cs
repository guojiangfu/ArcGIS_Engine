using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EX2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cmdLoadShpf_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog( );
            openFileDialog1.InitialDirectory = "E:\\ArcGIS开发\\实验数据";
            openFileDialog1.Filter = "txt files(*.txt)| *.txt |All Files(*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filesName = openFileDialog1.FileName;
                //System.IO.Path.GetFileName(openFieDialog1.FileName);
                //System.IO.Path.GetDirectoryName(openFieDialog1.FileName);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
