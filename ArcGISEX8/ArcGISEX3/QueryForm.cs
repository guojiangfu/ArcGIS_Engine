using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
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
    public partial class QueryForm : Form
    {
        IFeatureClass featureClass;
        IFields pfields;
        IField pfield;
        ILayer layer;
        IFeatureLayer pfeaturelayer;
        IQueryFilter pQueryFilter;
        public QueryForm()
        {
            InitializeComponent();
        }

        private void QueryForm_Load(object sender, EventArgs e)
        {
            //遍历地图控件中的图层
            int i;
            for (i = Form1.form1.axMapControl1.LayerCount - 1; i >= 0; i--)
            {
                comboBox1.Items.Add(Form1.form1.axMapControl1.get_Layer(i).Name.ToString());
            }

        }


        //查找要素类中别名与字段名称不同的字段 
        public void DisplayDistinctFieldAliasNames(IFeatureClass featureClass)
        {
            //从要素类中获取字段集
            IFields fields = featureClass.Fields;
            IField field = null;
            //遍历字段集中所有的字段
            for (int i = 0; i < fields.FieldCount; i++)
            {
                field = fields.get_Field(i);
                field = fields.Field[i];
                if (field.Name != field.AliasName)
                {
                    Console.WriteLine("{0}:{1}", field.Name, field.AliasName);
                }
            }
        }

        //获取字段名
        public IField GetFieldByName(IFeatureClass featureClass, string fieldName)
        { 
            // 从要素类获取字体集  
            IFields fields = featureClass.Fields;
            // 根据字段名称获取字段的索引
            int fieldIndex = fields.FindField(fieldName);
            //根据索引得到字段Field  
            IField field = fields.get_Field(fieldIndex);
            //IField field = fields.Field[fieldIndex];
            return field;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            layer = Form1.form1.axMapControl1.get_Layer(comboBox1.SelectedIndex);
            pfeaturelayer = layer as FeatureLayer;
            if (pfeaturelayer == null)
                return;
            featureClass = pfeaturelayer.FeatureClass;
            pfields = featureClass.Fields;
            for (int i = 0; i < pfields.FieldCount; i++)
            {
                pfield = pfields.get_Field(i);
                comboBox2.Items.Add(pfield.Name.ToString());
                if (pfield.Name.ToString() == "Shape")
                {
                    comboBox2.Items.RemoveAt(i);
                    continue;
                }
                if (pfield.Name.ToString() == "OBJECTID")
                    comboBox2.SelectedIndex = i;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (featureClass == null)
            {
                return;
            }
            IGeoDataset geoDataset = (IGeoDataset)featureClass;
            IGeometry geometryBag = new GeometryBagClass();
            //添加元素到包前指定坐标系. 
            geometryBag.SpatialReference = geoDataset.SpatialReference;
            //遍历要素类中所有要素 
            IFeatureCursor featureCursor = featureClass.Search(null, false);
            //接口转换到 IGeometryCollection 
            IGeometryCollection geometryCollection = (IGeometryCollection)geometryBag;
            IFeature currentFeature = featureCursor .NextFeature();
            while (currentFeature != null)
            {  //在几何图形集末尾添加要素的几何图形,AddGeometry 的后两个参数为 Type.missing(之前和之后的 几何对象)  
               //so the currentFeature.Shape IGeometry is added to the end of the geometryCollection.  
                object missing = Type.Missing;
                geometryCollection.AddGeometry(currentFeature.Shape, missing, missing);
                currentFeature = featureCursor.NextFeature();
            }
            Form1.form1.axMapControl1.Extent= geometryBag.Envelope;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pfield = pfields.get_Field(comboBox2.SelectedIndex);
            string fieldName = comboBox2.SelectedItem.ToString();
            pfield = GetFieldByName(featureClass, fieldName);
            ArrayList arrValues = new ArrayList();
            IQueryFilter pQueryFilter = new QueryFilterClass();
            IFeatureCursor pFeatureCursor = null;
            pQueryFilter.SubFields = pfield.Name;
            pFeatureCursor = featureClass.Search(pQueryFilter, true);
            IDataStatistics pDataStati = new DataStatisticsClass();
            pDataStati.Field = pfield.Name;
            pDataStati.Cursor = (ICursor)pFeatureCursor;
            IEnumerator pEnumerator = pDataStati.UniqueValues;
            pEnumerator.Reset();
            while (pEnumerator.MoveNext())
            {
                object pObj = pEnumerator.Current;
                string strvalue = pObj.ToString();
                if (pfield.Type == esriFieldType.esriFieldTypeString)
                {
                    strvalue = "'" + strvalue + "'";
                }
                arrValues.Add(strvalue);
            }
            arrValues.Sort();
            comboBox4.Items.AddRange(arrValues.ToArray());
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public IQueryFilter Query()
        {
            IQueryFilter pQueryFilter = new QueryFilterClass();
            pQueryFilter.WhereClause = comboBox2.SelectedItem.ToString() + " " + comboBox3.SelectedItem.ToString() + " " + comboBox4.SelectedItem.ToString();
            return pQueryFilter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //定义筛选器
            pQueryFilter = Query();
            IFeatureCursor fcursor = featureClass.Search(pQueryFilter, true);
            IFeature feature = fcursor.NextFeature();
            while (feature != null)
            {
                Form1.form1.axMapControl1.FlashShape(feature.Shape);
                feature = fcursor.NextFeature();
            }
            Form1.form1.axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
        }
    }
}
