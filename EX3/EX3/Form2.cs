using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;

namespace EX3
{
    public partial class Form2 : Form
    {
        public IMapControl3 axMapControl;
        private string spath;
       
        IFeatureWorkspace pFeatureWorkspace;
        IFeatureLayer pFeatureLayer = new FeatureLayerClass();
        IFeatureDataset pFeatureDataset;
        IWorkspace pWorkspace;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "打开*.mdb文件";
            openFileDialog1.Filter = "mdb文件(*.mdb*)|*.mdb*";
            spath = "E:\\ArcGIS开发\\实验数据\\EX3";
            openFileDialog1.InitialDirectory = spath;
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;
            IWorkspaceFactory pAccessWorkspaceFactory = new AccessWorkspaceFactoryClass();
            //string path = "E:\\yn.mdb";
            pWorkspace = pAccessWorkspaceFactory.OpenFromFile(path, 0);
            IEnumDataset pEnumDataset = pWorkspace.get_Datasets(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTAny);
            pEnumDataset.Reset();
            IDataset pDataset = pEnumDataset.Next();
            if (pDataset is IFeatureDataset)
            {
                pFeatureWorkspace = (IFeatureWorkspace)pWorkspace;
                IEnumDataset pEnumDataset1 = pDataset.Subsets;
                pEnumDataset1.Reset();
                IDataset pDataset1 = pEnumDataset1.Next();
                if (pDataset1 is IFeatureClass)
                {
                    pFeatureLayer = new FeatureLayerClass();
                    pFeatureLayer.FeatureClass = pFeatureWorkspace.OpenFeatureClass(pDataset1.Name);
                    pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;
                    axMapControl.Map.AddLayer(pFeatureLayer);
                    axMapControl.ActiveView.Refresh();
                }
            }
        }






    }
}
