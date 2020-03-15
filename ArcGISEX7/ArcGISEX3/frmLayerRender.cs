using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
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
    public partial class frmLayerRender : Form
    {
        ILayer layer;
        IUniqueValueRenderer m_unrend;
        ImageList imagelist1 = new ImageList();
        ISymbol[] m_Symbols;
        string[] m_Labels;
        public frmLayerRender(ILayer layer)
        {
            this.layer = layer;
            InitializeComponent();
        }

        private void frmLayerRender_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("单一符号");
            listBox1.Items.Add("唯一值符号");
            listBox1.Items.Add("分级符号");

            IFeatureLayer featurelayer = layer as IFeatureLayer;
            for (int i = 0; i < featurelayer.FeatureClass.Fields.FieldCount; i++)
            {
                string filedname = featurelayer.FeatureClass.Fields.Field[i].Name.ToString();
                comboBox1.Items.Add(filedname);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form symbolForm = new frmSymbolSelector(layer);
            symbolForm.ShowDialog();
            pictureBox1.Image = frmSymbolSelector.frmSymbolSelector1.pictureBox1.Image;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                ISymbol pSym = (ISymbol)frmSymbolSelector.frmSymbolSelector1. m_styleGalleryItem.Item;
                ISimpleRenderer simpleRenderer = new SimpleRendererClass();
                simpleRenderer.Symbol = pSym;
                IGeoFeatureLayer m_featlayer = layer as IGeoFeatureLayer;
                m_featlayer.Renderer = simpleRenderer as IFeatureRenderer;
                Form1.form1.axMapControl1.Refresh();
                Form1.form1.axTOCControl1.Update();
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                IGeoFeatureLayer m_featlayer = layer as IGeoFeatureLayer;
                m_featlayer.Renderer = m_unrend as IFeatureRenderer;
                Form1.form1.axMapControl1.Refresh();
                Form1.form1.axTOCControl1.Update();
            }
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                ISymbol pSym = (ISymbol)frmSymbolSelector.frmSymbolSelector1.m_styleGalleryItem.Item;
                ISimpleRenderer simpleRenderer = new SimpleRendererClass();
                simpleRenderer.Symbol = pSym;
                IGeoFeatureLayer m_featlayer = layer as IGeoFeatureLayer;
                m_featlayer.Renderer = simpleRenderer as IFeatureRenderer;
                Form1.form1.axMapControl1.Refresh();
                Form1.form1.axTOCControl1.Update();
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                IGeoFeatureLayer m_featlayer = layer as IGeoFeatureLayer;
                m_featlayer.Renderer = m_unrend as IFeatureRenderer;
                Form1.form1.axMapControl1.Refresh();
                Form1.form1.axTOCControl1.Update();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filename = comboBox1.SelectedItem.ToString();
            UpdateListView(filename);
        }
        private void UpdateListView(string sFld)
        {
            ISymbol pSym;
            ListViewItem item;
            listView1.Items.Clear();
            m_unrend = CreateUVRen(sFld);
            int vCount = m_unrend.ValueCount;
            m_Symbols = new ISymbol[1000];
            m_Labels = new string[1000];
            imagelist1.Images.Clear();
            int i;
            for (i = 0; i < vCount; i++)
            {
                string sValue = m_unrend.get_Value(i);
                pSym = m_unrend.get_Symbol(sValue);
                Bitmap b = Sym2Bitmap((ISymbol)pSym, 15, 15);
                imagelist1.Images.Add(b);
                item = new ListViewItem(sValue);
                item.ImageIndex = i;
                listView1.Items.Add(item);
            }
            listView1.LargeImageList = imagelist1;
        }
        public Bitmap Sym2Bitmap(ISymbol sym, int width, int height)
        {
            Bitmap b = new Bitmap(width + 3, height + 3);
            IDisplayTransformation dispTrans = new DisplayTransformationClass();
            tagRECT r = new tagRECT();
            r.left = 0;
            r.top = 0;
            r.bottom = b.Height;
            r.right = b.Width;
            dispTrans.set_DeviceFrame(r);
            IEnvelope bounds = new EnvelopeClass();

            bounds.PutCoords(0, 0, b.Width, b.Height);
            dispTrans.Bounds = bounds;
            IGeometry geom = MakeGeometry(sym, bounds);
            Graphics g = Graphics.FromImage(b);
            IntPtr hDC = g.GetHdc();
            sym.SetupDC(hDC.ToInt32(), dispTrans);
            sym.Draw(geom);
            sym.ResetDC();
            g.ReleaseHdc(hDC);
            return b;
        }
        private IGeometry MakeGeometry(ISymbol sym, IEnvelope env)
        {
            if (sym is IMarkerSymbol)
            {
                return ((IArea)env).Centroid;
            }
            else if (sym is ILineSymbol)
            {
                object missing = Type.Missing;
                IPointCollection pc = new PolylineClass() as IPointCollection;
                pc.AddPoint(env.LowerLeft, missing, missing);
                pc.AddPoint(env.UpperRight, missing, missing);
                return (IGeometry)pc;
            }
            else if (sym is IFillSymbol)
            {
                ISegmentCollection sc = new PolygonClass();
                sc.SetRectangle(env);
                return (IGeometry)sc;
            }
            else
            {
                MessageBox.Show("Exception");
                return null;
            }
        }
        private System.Collections.IEnumerator SortTable(IFeatureLayer pFeatureLayer, string sFieldName)
        {
            ITableSort pTablesort = new TableSortClass();
            pTablesort.Fields = sFieldName;
            pTablesort.set_Ascending(sFieldName, true);
            pTablesort.set_CaseSensitive(sFieldName, false);
            pTablesort.QueryFilter = null;
            pTablesort.Table = pFeatureLayer as ITable;
            pTablesort.Sort(null);
            ICursor pCursor = pTablesort.Rows;
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = sFieldName;
            pDataStatistics.Cursor = pCursor;
            return pDataStatistics.UniqueValues;
        }
        private IUniqueValueRenderer CreateUVRen(string sField)
        {

            IFeatureLayer featurelayer = layer as IFeatureLayer;
            int nnClasses = 0;
            System.Collections.IEnumerator pEnum = SortTable((IFeatureLayer)layer, sField);
            pEnum.Reset();
            while (pEnum.MoveNext())
            {
                object myObject = pEnum.Current;
                Math.Max(System.Threading.Interlocked.Increment(ref nnClasses), nnClasses - 1);
            }
            IColorRamp colorRamp = new RandomColorRampClass();
            colorRamp.Size = nnClasses;
            bool createRamp;
            colorRamp.CreateRamp(out createRamp);
            IEnumColors enumColors = colorRamp.Colors;
            enumColors.Reset();
            IUniqueValueRenderer pUVRenderer = new UniqueValueRendererClass();
            if (featurelayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint)
            {

                ISimpleMarkerSymbol pSym;
                pUVRenderer.FieldCount = 1;
                pUVRenderer.set_Field(0, sField);
                System.Collections.IEnumerator pEnum2 = SortTable((IFeatureLayer)layer, sField);
                pEnum2.Reset();
                string value;
                object myObj;
                while (pEnum2.MoveNext())
                {
                    pSym = new SimpleMarkerSymbolClass();
                    pSym.Size = 7;
                    pSym.Style = esriSimpleMarkerStyle.esriSMSCircle;
                    pSym.Color = enumColors.Next();
                    pSym.Outline = true;
                    pSym.OutlineSize = 0.4;
                    myObj = pEnum2.Current;
                    value = myObj.ToString();
                    pUVRenderer.AddValue(value, null, (ISymbol)pSym);

                }
            }
            else if (featurelayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                ISimpleLineSymbol pSym;
                pUVRenderer.FieldCount = 1;
                pUVRenderer.set_Field(0, sField);
                System.Collections.IEnumerator pEnum2 = SortTable((IFeatureLayer)layer, sField);
                pEnum2.Reset();
                string value;
                object myObj;
                while (pEnum2.MoveNext())
                {
                    pSym = new SimpleLineSymbolClass();
                    pSym.Width = 1.5;
                    pSym.Style = esriSimpleLineStyle.esriSLSSolid;
                    pSym.Color = enumColors.Next();
                    myObj = pEnum2.Current;
                    value = myObj.ToString();
                    pUVRenderer.AddValue(value, null, (ISymbol)pSym);
                }
            }
            else if (featurelayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                ISimpleFillSymbol pSym;
                pUVRenderer.FieldCount = 1;
                pUVRenderer.set_Field(0, sField);
                System.Collections.IEnumerator pEnum2 = SortTable((IFeatureLayer)layer, sField);
                pEnum2.Reset();
                string value;
                object myObj;
                while (pEnum2.MoveNext())
                {
                    pSym = new SimpleFillSymbolClass();
                    pSym.Style = esriSimpleFillStyle.esriSFSSolid;
                    pSym.Color = enumColors.Next();
                    myObj = pEnum2.Current;
                    value = myObj.ToString();
                    pUVRenderer.AddValue(value, null, (ISymbol)pSym);
                }
            }
            return pUVRenderer;
        }
    }
}
