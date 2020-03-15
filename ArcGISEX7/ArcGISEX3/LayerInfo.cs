using ESRI.ArcGIS.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcGISEX3
{
    public partial class LayerInfo : Form
    {
        private string name;
        private string type;
        public LayerInfo(string name,string type)
        {
            this.name = name;
            this.type = type;
            InitializeComponent();
        }

        private void LayerInfo_Load(object sender, EventArgs e)
        {
            textBox1.Text = name;
            textBox2.Text = type;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
