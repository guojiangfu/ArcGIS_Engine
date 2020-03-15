﻿using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
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

        private IToolbarMenu m_pMenuLayer;
        private IToolbarMenu m_pMenuMap;

        public IPoint newPt;

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
            m_pMenuMap = new ToolbarMenuClass();
            m_pMenuLayer = new ToolbarMenuClass();
            m_pMenuMap.AddItem(new cmdSetMapSR(), -1, 0, false);
            //m_pMenuLayer.AddItem(new cmdProjectLayer(), -1, 0, true, esriCommandStyles.esriCommandStyleTextOnly);
            m_pMenuLayer.SetHook(axMapControl1);
            m_pMenuMap.SetHook(axMapControl1);
            axToolbarControl1.AddItem(new cmdCreateGBD(), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl1.AddItem(new cmdCreateFC(),-1,-1,true,0,esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl1.AddItem(new cmdEditStart(), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl1.AddItem(new toolAddData(), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl1.AddItem(new cmdEditStop(), -1, -1, true, 0, esriCommandStyles.esriCommandStyleIconAndText);
        }

        private void axToolbarControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IToolbarControlEvents_OnMouseDownEvent e)
        {

        }

        private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            newPt = axMapControl1.ToMapPoint(e.x,e.y);
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
                //string name = pLayer.Name;
                //string type = ((IFeatureLayer2)pLayer).ShapeType.ToString();
                //Form myform = new LayerInfo(name, type);
                //myform.ShowDialog();

                if ((pItem == esriTOCControlItem.esriTOCControlItemLayer))
                {
                    axTOCControl1.SelectItem(pLayer, null);
                    axMapControl1.CustomProperty = pLayer;
                    if (axMapControl1.CustomProperty is ILayer)
                    {
                        m_pMenuLayer.AddItem(new cmdProjectLayer(), -1, 0, false);
                        m_pMenuLayer.AddItem(new cmdLayerProperty(pLayer), -1, 0, false);
                        string name = pLayer.Name;
                        string type = ((IFeatureLayer2)pLayer).ShapeType.ToString();
                        m_pMenuLayer.AddItem(new cmdLayerInfo(name, type), -1, 0, false);
                        m_pMenuLayer.PopupMenu(e.x, e.y, axTOCControl1.hWnd);
                        m_pMenuLayer.RemoveAll();
                    }
                }
                else if ((pItem == esriTOCControlItem.esriTOCControlItemMap))
                {
                    axTOCControl1.SelectItem(pMap, null);
                    axMapControl1.CustomProperty = pMap;
                   // m_pMenuMap.AddItem(new cmdLayerProperty(pLayer), -1, 0, false);
                    m_pMenuMap.PopupMenu(e.x, e.y, axTOCControl1.hWnd);
                }
            }
        }
    }
}
