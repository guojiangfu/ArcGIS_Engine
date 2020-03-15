using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections;
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
    public partial class LoadLayerForm : Form
    {
        private string spath;
        IFeatureWorkspace pFeatureWorkspace;
        IWorkspaceFactory pAccessWorkspaceFactory;
        IWorkspace pWorkspace;
        IFeatureDataset pFeatureDataset;
        IFeatureLayer pFeatureLayer;
        IEnumDataset pEnumDataset;
        IFeatureWorkspace featureWorkspace;
        IRasterWorkspaceEx pRasterWorkspace;
        AxMapControl axMapControl = Form1.form1.axMapControl1;
        public LoadLayerForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //创建工作空间
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "打开*.mdb文件";
            openFileDialog1.Filter = "mdb文件(*.mdb*)|*.mdb*";
            spath = "E:\\ArcGIS开发\\实验数据\\EX3";
            openFileDialog1.InitialDirectory = spath;
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;
            pAccessWorkspaceFactory = new AccessWorkspaceFactoryClass();
            pWorkspace = pAccessWorkspaceFactory.OpenFromFile(path, 0);
            textBox1.Text = path;
            radioButton1.Checked = true;
        }

        //根据工作区得到要素集名称列表             要素集名称
        private ArrayList GeoDatasetNames(IWorkspace workspace)
        {
            IEnumDatasetName enumDatasetName = workspace.DatasetNames[esriDatasetType.esriDTFeatureDataset];
            IDatasetName datasetName = enumDatasetName.Next();
            ArrayList alist = new ArrayList();
            while (datasetName != null)
            {
                alist.Add(datasetName.Name);
                datasetName = enumDatasetName.Next();
            }
            return alist;
        }

        public ArrayList getDatasetNames(IWorkspace workspace)
        {
            IEnumDatasetName enumDatasetName = workspace.get_DatasetNames(esriDatasetType.esriDTRasterDataset);
            ArrayList alist = new ArrayList();
            IDatasetName datasetName = enumDatasetName.Next();
            while (datasetName != null)
            {
                alist.Add(datasetName.Name);
                datasetName = enumDatasetName.Next();
            }
            return alist;
        }

        //根据要素集得到其中的要素类名称列表          要素类名称
        private ArrayList getFeatClassNames(FeatureDataset featDataset)
        {
            IEnumDataset enumDataset = featDataset.Subsets;
            IDataset dataset = enumDataset.Next();
            ArrayList alist = new ArrayList();
            while (dataset != null)
            {
                if (dataset.Type == esriDatasetType.esriDTFeatureClass)
                {
                    alist.Add(dataset.Name);
                    dataset = enumDataset.Next();
                }
            }
            return alist;
        }


        //根据工作区和要素集名称得到要素集          要素集
        private IFeatureDataset getFeatDataset(string Name, IWorkspace workspace)
        {
            IFeatureWorkspace pFeatureWorkspace;
            pFeatureWorkspace = (IFeatureWorkspace)workspace;
            IFeatureDataset featDataset;
            featDataset = pFeatureWorkspace.OpenFeatureDataset(Name);
            return featDataset;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string curItem = listBox1.SelectedItem.ToString();
            IFeatureDataset featDS;
            if (pWorkspace != null)
            {
                featDS = getFeatDataset(curItem, pWorkspace);
                listBox2.Items.AddRange(getFeatClassNames((FeatureDataset)featDS).ToArray());
                //listBox2.DataSource = getFeatClassNames((IFeatureDataset)featDS);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
        }

        private void LoadLayerForm_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("显示要素集");
            listBox2.Items.Add("显示要素类");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Visible = false;
            listBox2.Items.Clear();
            string path = textBox1.Text;
            IWorkspace pWorkspace = pAccessWorkspaceFactory.OpenFromFile(path, 0);
            IEnumDatasetName enumDatasetName = pWorkspace.DatasetNames[esriDatasetType.esriDTFeatureClass];
            IDatasetName datasetName = enumDatasetName.Next();
            ArrayList alist = new ArrayList();
            while (datasetName != null)
            {
                alist.Add(datasetName.Name);
                datasetName = enumDatasetName.Next();
            }
            listBox2.Items.AddRange(alist.ToArray());
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Visible = true;
            listBox2.Items.Clear();
            string path = textBox1.Text;
            IWorkspace pWorkspace = pAccessWorkspaceFactory.OpenFromFile(path, 0);
            listBox1.DataSource = GeoDatasetNames(pWorkspace);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Visible = false;
            listBox2.Items.Clear();
            string path = textBox1.Text;
            IWorkspaceFactory pWorkspaceFactory = new AccessWorkspaceFactoryClass();
            pRasterWorkspace = pWorkspaceFactory.OpenFromFile(path, 0) as IRasterWorkspaceEx;
            listBox2.Items.AddRange(getDatasetNames(pRasterWorkspace as IWorkspace).ToArray());
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath;
            filePath = textBox1.Text+"\\"+listBox2.SelectedItem.ToString();
            IRasterLayer pRasterLy = new RasterLayerClass( );
            pRasterLy.CreateFromFilePath(filePath);
            axMapControl.Map.AddLayer(pRasterLy );
            MessageBox.Show("图层加载成功!");
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string filePath;
            filePath = listBox2.SelectedItem.ToString();
            pFeatureWorkspace = (IFeatureWorkspace)pWorkspace;
            pFeatureLayer = new FeatureLayerClass();
            pFeatureLayer.FeatureClass = pFeatureWorkspace.OpenFeatureClass(filePath);
            pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;
            axMapControl.Map.AddLayer(pFeatureLayer );
            MessageBox.Show("图层加载成功!");
            this.Close();
        }
    }
}
