using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
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
    public partial class frmEditStart : Form
    {
        public frmEditStart()
        {
            InitializeComponent();
        }

        private void frmEditStart_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Form1.form1.axMapControl1.LayerCount; i++)
            {
                listBox1.Items.Add(Form1.form1.axMapControl1.get_Layer(i).Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditEnvSingleton.TargetLayer = Form1.form1.axMapControl1.get_Layer(listBox1.SelectedIndex) as IFeatureLayer;
            IFeatureLayer featLayer = EditEnvSingleton.TargetLayer as IFeatureLayer;
            IFeatureClass oFC = featLayer.FeatureClass;
            IDataset oDataset;
            oDataset = oFC as IDataset;
            IWorkspaceEdit workspaceEdit = oDataset.Workspace as IWorkspaceEdit;
            workspaceEdit.StartEditing(true);
            EditEnvSingleton.workspaceEdit = workspaceEdit;
            this.Close();
        }
    }
}
