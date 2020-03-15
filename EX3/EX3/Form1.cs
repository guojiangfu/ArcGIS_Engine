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

namespace EX3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            //ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Engine);      出错
            InitializeComponent();          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.axTOCControl1.SetBuddyControl(this.axMapControl1);
            axTOCControl1.SetBuddyControl(axMapControl1);
            axToolbarControl1.SetBuddyControl(axMapControl1);
            axToolbarControl1.AddItem(new CreateNewDocument(), -1,-1,true,0, esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl1.AddItem(new OpenDocument(), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);//栈不对称
            axToolbarControl1.AddItem(new ZoomIn(), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl1.AddItem(new FullExtent(), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl1.AddItem(new LoadLayer(), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);
        }

        private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {

        }

        private void axToolbarControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IToolbarControlEvents_OnMouseDownEvent e)
        {

        }

        private void axTOCControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
        {

        }

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 新建地图文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand cmdNew = new CreateNewDocument();
            cmdNew.OnCreate(axMapControl1.Object);
            cmdNew.OnClick();
        }

        private void 打开地图文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand cmdOpen = new OpenDocument();
            cmdOpen.OnCreate(axMapControl1.Object);
            cmdOpen.OnClick();
        }

        private void 地图放大功能ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand cmdZoom = new ZoomIn();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
