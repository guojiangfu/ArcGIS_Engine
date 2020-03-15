using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
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
    public partial class dlgCreateFC : Form
    {
        IWorkspace WorkSpace;
        public dlgCreateFC()
        {
            InitializeComponent();
        }

        private void dlgCreateFC_Load(object sender, EventArgs e)
        {
            Item esriFieldTypeSmallInteger = new Item(0, "esriFieldTypeSmallInteger");
            Item esriFieldTypeInteger = new Item(1, "esriFieldTypeInteger");
            Item  esriFieldTypeSingle = new Item(2, "esriFieldTypeSingle");
            Item esriFieldTypeDouble = new Item(3, "esriFieldTypeDouble");
            Item esriFieldTypeString = new Item(4, "esriFieldTypeString");
            Item esriFieldTypeDate = new Item(5, "esriFieldTypeDate");
            Item esriFieldTypeOID = new Item(6, "esriFieldTypeOID");
            Item esriFieldTypeGeometry = new Item(7, "esriFieldTypeGeometry");
            Item esriFieldTypeBlob = new Item(8, "esriFieldTypeBlob");
            Item esriFieldTypeRaster = new Item(9, "esriFieldTypeRaster");

            comboBox1.Items.Add(esriFieldTypeSmallInteger);
            comboBox1.Items.Add(esriFieldTypeInteger);
            comboBox1.Items.Add(esriFieldTypeSingle);
            comboBox1.Items.Add(esriFieldTypeDouble);
            comboBox1.Items.Add(esriFieldTypeString);
            comboBox1.Items.Add(esriFieldTypeDate);
            comboBox1.Items.Add(esriFieldTypeOID);
            comboBox1.Items.Add(esriFieldTypeGeometry);
            comboBox1.Items.Add(esriFieldTypeBlob);
            comboBox1.Items.Add(esriFieldTypeRaster);


            textBox2.Text = "显示打开的工作空间的名称";
            dataGridView1.ColumnCount = 3;
            dataGridView1.RowCount = 20;
            dataGridView1.RowHeadersWidth = 100;
            dataGridView1.TopLeftHeaderCell.Value = " ";
            dataGridView1. Columns[1].Width = 200;
            dataGridView1.Columns[0].HeaderText = "字段名称";
            dataGridView1.Columns[1].HeaderText = "字段类型";
            dataGridView1.Columns[2].HeaderText = "字段长度";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path;
            IWorkspaceFactory AccessWorkspaceFacetory = new AccessWorkspaceFactoryClass();
            OpenFileDialog OpenFiledialg = new OpenFileDialog();
            OpenFiledialg.InitialDirectory = @"E:\ArcGIS开发\实验数据";
            OpenFiledialg.Filter = "mdb files(*.mdb) | *.mdb";
            if (OpenFiledialg.ShowDialog() == DialogResult.OK)
            {
                path = OpenFiledialg.FileName;
                WorkSpace = AccessWorkspaceFacetory.OpenFromFile(path, 0);
                textBox2.Text = path;
            }
        }

        public IFeatureClass CreateFeatureClassWithSR(string featureClassName, IFeatureWorkspace featureWorkspace, ISpatialReference spatialReference)
        {
            IFeatureClassDescription fcDescription = new FeatureClassDescriptionClass();
            IObjectClassDescription ocDescription = (IObjectClassDescription)fcDescription;
            IFields fields = ocDescription.RequiredFields;
            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;
            IField field = new FieldClass();
            IFieldEdit fieldEdit = (IFieldEdit)field;
            fieldEdit.Name_2 = dataGridView1[0, 0].Value.ToString();
            int index = comboBox1.FindString(dataGridView1[1, 0].Value.ToString());
            Item item = (Item)comboBox1.Items[index];
            fieldEdit.Type_2 = (esriFieldType)item.value;
            fieldEdit.Length_2 = Convert.ToInt32(dataGridView1[2, 0].Value);
            fieldsEdit.AddField(field);
            IField field2 = new FieldClass();
            IFieldEdit fieldEdit2 = (IFieldEdit)field2;
            fieldEdit2.Name_2 = dataGridView1[0, 1].Value.ToString();
            int index2 = comboBox1.FindString(dataGridView1[1, 1].Value.ToString());
            Item item2 = (Item)comboBox1.Items[index];
            fieldEdit2.Type_2 = (esriFieldType)item.value;
            fieldEdit2.Length_2 = Convert.ToInt32(dataGridView1[2, 1].Value);
            fieldsEdit.AddField(field2);
            // 找到 Shape 字段，获取 GeometryDef 以设置空间体系 
            int shapeFieldIndex = fields.FindField(fcDescription.ShapeFieldName);
            IField shapefield = fields.Field[shapeFieldIndex];
            //或 get_Field(idx) 
            IGeometryDef geometryDef = shapefield.GeometryDef;
            IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
            if (radioButton1.Checked)
                geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            else if (radioButton2.Checked)
                geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryLine;
            else
                geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
            geometryDefEdit.SpatialReference_2 = spatialReference;
            IFieldChecker fieldChecker = new FieldCheckerClass();
            IEnumFieldError enumFieldError = null;
            IFields validatedFields = null;
            fieldChecker.ValidateWorkspace = (IWorkspace)featureWorkspace;
            fieldChecker.Validate(fields, out enumFieldError, out validatedFields);
            IFeatureClass featureClass = featureWorkspace.CreateFeatureClass
                (featureClassName, validatedFields, ocDescription.InstanceCLSID,
                ocDescription.ClassExtensionCLSID, esriFeatureType.esriFTSimple, fcDescription.ShapeFieldName, "");
            return featureClass;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            IFeatureWorkspace featurework = WorkSpace as IFeatureWorkspace;
            IFeatureClass featureclass = CreateFeatureClassWithSR(textBox1.Text, featurework, Form1.form1.axMapControl1.SpatialReference);
            IFeatureLayer layer = new FeatureLayerClass();
            layer.Name = textBox1.Text;
            layer.FeatureClass = featureclass;
            Form1.form1.axMapControl1.AddLayer(layer as ILayer);
        }
        int i;
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1[0, i].Value = textBox3.Text;
            dataGridView1[1, i].Value = comboBox1.SelectedItem.ToString();
            dataGridView1[2, i].Value = textBox4.Text;
            i = i + 1;
        }
    }
    public class Item
    {
        public string text;
        public int value;
        public Item(int v, string t)
        {
            text = t;
            value = v;
        }
        public override string ToString()
        {
            return this.text;
        }
    }

}
