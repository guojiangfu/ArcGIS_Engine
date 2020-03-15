using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
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
    public partial class Form1 : Form
    {

        public static Form1 form1;

        public ILayer pSeletLayer = null;
        esriTOCControlItem pItem = new esriTOCControlItem();
        IBasicMap pMap = null;
        ILayer pLayer = null;
        object pOther = new object();
        object pIndex = new object();
        public int toIndex;
        public Form1()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            form1 = this;
            InitializeComponent();
        }

        private void 新建地图文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand cmdNew = new CreateNewDocument();
            cmdNew.OnCreate(axMapControl1.Object);
            cmdNew.OnClick();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            axTOCControl1.SetBuddyControl(axMapControl1);
            axToolbarControl1.SetBuddyControl(axMapControl1);
            axToolbarControl1.AddItem(new OpenDocument(), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);//栈不对称
            axToolbarControl1.AddItem(new SaveDocument(), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl1.AddItem(new ZoomIn(), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl1.AddItem(new FullExtent(), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl1.AddItem(new LoadLayer(), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl1.AddItem(new Query(), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl1.AddItem(new myIdentifyTool(), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);
        }

        private void axToolbarControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IToolbarControlEvents_OnMouseDownEvent e)
        {

        }

        private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {

        }

        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button == 1)
            {
                return;
            }
            if (e.button == 2)
            {
                axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pMap, ref pLayer, ref pOther, ref pIndex);
                string name = pLayer.Name;
                string type = ((IFeatureLayer2)pLayer).ShapeType.ToString();
                Form myform = new LayerInfo( name ,type);
                myform.ShowDialog();
            }
        }
    }
}
