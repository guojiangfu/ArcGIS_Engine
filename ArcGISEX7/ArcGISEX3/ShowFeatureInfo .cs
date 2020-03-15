using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
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
    public partial class ShowFeatureInfo : Form
    {
        AxMapControl axMapControl = Form1.form1.axMapControl1;
        IFeatureLayer featureLayer;
        ILayer layer;
        IFeatureClass FeatureClass;
        IGeometry bufferGeometry;

        public ShowFeatureInfo(IGeometry bufferGeometry)
        {
            ESRI.ArcGIS.RuntimeManager.BindLicense(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            this.bufferGeometry = bufferGeometry;
            InitializeComponent();
        }

        private void ShowFeatureInfo_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 2;
            dataGridView1.RowHeadersWidth = 60;
            dataGridView1.TopLeftHeaderCell.Value = "序号";
            dataGridView1.Columns[0].HeaderText = "字段";
            dataGridView1.Columns[1].HeaderText = "属性值";

            int i;
            for (i = Form1.form1.axMapControl1.LayerCount - 1; i >= 0; i--)
            {
                comboBox1.Items.Add(Form1.form1.axMapControl1.get_Layer(i).Name.ToString());
            }
            comboBox1.SelectedIndex = 0;
        }
        public enum emnSelectType : int
        {
            POINTSELECT = 1,
            CIRCLESELECT = 2,
            RECTSELECT = 3,
            POLYGONSELECT = 4
        }
        public void showFeature(IGeometry geometry)
        {
            //定义空间查询的过滤器
            ISpatialFilter spatialFilter = new SpatialFilterClass();
            spatialFilter.Geometry = geometry;
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            IFeatureCursor featureCursor = featureLayer.Search(spatialFilter, false);
            IFeature feature = featureCursor.NextFeature();
            while (feature != null)
            {
                axMapControl.FlashShape(feature.Shape);
                showAttribute(feature);
                feature = featureCursor.NextFeature();
            }
            axMapControl.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
        }
        public void showAttribute(IFeature feature)
        {
            int num = feature.Fields.FieldCount;
            dataGridView1.RowCount = num;
            int i = 0; for (i = 0; i < num; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = i.ToString();
                dataGridView1[0, i].Value = feature.Fields.get_Field(i).Name.ToString();
                if (feature.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                {
                    string type = feature.Shape.GeometryType.ToString();
                    switch (type)
                    {
                        case "esriGeometryPoint":
                            dataGridView1[1, i].Value = "点";
                            break;
                        case "esriGeometryPolyline":
                            dataGridView1[1, i].Value = "线";
                            break;
                        case "esriGeometryPolygon":
                            dataGridView1[1, i].Value = "面";
                            break;
                    }
                }
                else
                {
                    dataGridView1[1, i].Value = feature.Value[i].ToString();
                }
            }
            this.dataGridView1.Text = "查询的要素共有" + feature.Fields.FieldCount.ToString() + "个字段";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            layer = Form1.form1.axMapControl1.get_Layer(comboBox1.SelectedIndex);
            featureLayer = layer as FeatureLayer;
            if (featureLayer == null)
                return;
            FeatureClass = featureLayer.FeatureClass;
            showFeature(bufferGeometry);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
