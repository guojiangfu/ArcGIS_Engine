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

namespace ArcGIS_EX1
{
    public partial class Form1 : Form
    {
        IMapDocument mapDocument;
        IToolbarMenu m_pMenuLayer;

        public ILayer pSeletLayer = null;
        esriTOCControlItem pItem = new esriTOCControlItem();
        IBasicMap pMap = null;
        ILayer pLayer = null;
        object pOther = new object();
        object pIndex = new object();
        public int toIndex;
        public Form1()
        {
            ESRI.ArcGIS.RuntimeManager.BindLicense(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.axTOCControl1.SetBuddyControl(this.axMapControl1);
            this.axToolbarControl1.SetBuddyControl(this.axMapControl1);

            // TOCControl控件的右键菜单          
            m_pMenuLayer = new ToolbarMenuClass();
            m_pMenuLayer.AddItem(new RemoveLayer(), -1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_pMenuLayer.AddItem(new ZoomToLayer(), -1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_pMenuLayer.SetHook(axMapControl1);

        }

        private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 1)
            {
                axMapControl1.Extent = axMapControl1.TrackRectangle();
            }
            else
            {
                contextMenuStrip1.Show(sender as Control, e.x, e.y);
            }
        }

        private void 显示全图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.Extent = axMapControl1.FullExtent;
        }

        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.Extent = axMapControl1.TrackRectangle();
        }

        private void cmdLoadShpf_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "E:\\ArcGIS开发\\实验数据";
            openFileDialog1.Filter = "txt files(*.txt)| *.txt |All Files(*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filesName = System.IO.Path.GetFileName(openFileDialog1.FileName);//得到文件明不包括路径
                string pathName = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);//得到路径
                axMapControl1.AddShapeFile(pathName, filesName);
                //string MxdPath = openFileDialog1.FileName;
                //axMapControl1.LoadMxFile(MxdPath);

            }
        }

        private void cmdClearLayers_Click(object sender, EventArgs e)
        {
            axMapControl1.ClearLayers();
            axTOCControl1.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = @"E:\ArcGIS开发\实验数据\Lab2\EX1.mxd";
            if (axMapControl1.CheckMxFile(filePath))
            {
                axMapControl1.LoadMxFile(filePath, Type.Missing, Type.Missing);
            }
        }

        private void open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "打开*.mxd文件";
            openFileDialog1.InitialDirectory = "C:\\Program Files (x86)\\ArcGIS\\DeveloperKit10.4\\Samples\\data\\World ";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filesName = System.IO.Path.GetFileName(openFileDialog1.FileName);//得到文件明不包括路径
                string pathName = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);//得到路径
                string mxdpath = pathName + "\\" + filesName;
                axMapControl1.LoadMxFile(mxdpath);
            }
        }

        private void saveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Title = "地图保存为";
            saveFileDialog1.InitialDirectory = "E:\\ArcGIS开发\\实验数据";
            saveFileDialog1.Filter = "Map Document(*.mxd)|*.mxd";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sFilePath = saveFileDialog1.FileName;
                mapDocument = new MapDocumentClass();
                mapDocument.New(sFilePath);
                mapDocument.ReplaceContents(axMapControl1.Map as IMxdContents);
                mapDocument.Save(true, true);
                mapDocument.Close();
                MessageBox.Show("保存成功~");
            }
        }


        private void axTOCControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
        {
            //图层左击操作
            if (e.button == 1)
            {
                this.axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pMap, ref pLayer, ref pOther, ref pIndex);
                if (pItem == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    pSeletLayer = pLayer;
                    this.axTOCControl1.SelectItem(pLayer, null);
                }
                //刷新SelectLayer控件
            }
            //右击 菜单删除和缩放图层菜单
            if (e.button == 2)
            {
                axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pMap, ref pLayer, ref pOther, ref pIndex);
                if (pItem == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    axTOCControl1.SelectItem(pLayer, null);
                }
                axMapControl1.CustomProperty = pLayer;
                if (pItem == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    m_pMenuLayer.PopupMenu(e.x, e.y, axMapControl1.hWnd);
                }
            }
        }

        private void axTOCControl1_OnMouseUp(object sender, ITOCControlEvents_OnMouseUpEvent e)
        {
            if (e.button == 1)
            {
                esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap map = null;
                ILayer layer = null;
                object other = null;
                object index = null;

                this.axTOCControl1.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);
                IMap pMap = this.axMapControl1.ActiveView.FocusMap;
                if (item == esriTOCControlItem.esriTOCControlItemLayer || layer != null)
                {
                    if (pSeletLayer != null)
                    {
                        ILayer pTempLayer;
                        for (int i = 0; i < pMap.LayerCount; i++)
                        {
                            pTempLayer = pMap.get_Layer(i);
                            if (pTempLayer == layer)
                            {
                                toIndex = i;
                            }
                        }
                        pMap.MoveLayer(pSeletLayer, toIndex);
                        axMapControl1.ActiveView.Refresh();
                        this.axTOCControl1.Update();
                    }
                }
            }
        }

        private void axTOCControl1_OnMouseMove(object sender, ITOCControlEvents_OnMouseMoveEvent e)
        {
           
        }

        private void OnEndLabelEdit(object sender, ITOCControlEvents_OnEndLabelEditEvent e)
        {
            string newLabel = e.newLabel;
            if (newLabel.Trim()=="" )
            {
                e.canEdit = false;
            }
        }

        private void OnBeginLabelEdit(object sender, ITOCControlEvents_OnBeginLabelEditEvent e)
        {
            
        }
    }
 }
