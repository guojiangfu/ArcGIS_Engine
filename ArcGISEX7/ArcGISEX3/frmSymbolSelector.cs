using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
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
    public partial class frmSymbolSelector : Form
    {
        ILayer layer;
        public IStyleGalleryItem m_styleGalleryItem;
        public static frmSymbolSelector frmSymbolSelector1;
        public frmSymbolSelector(ILayer layer)
        {
            InitializeComponent();
            this.layer = layer;
            frmSymbolSelector1 = this;
        }

        private void frmSymbolSelector_Load(object sender, EventArgs e)
        {
            string sInstall = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path;
            axSymbologyControl1.LoadStyleFile(sInstall + "\\Styles\\ESRI.ServerStyle");

            IFeatureLayer featurelayer = layer as IFeatureLayer;
            if (featurelayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint)
            {
                axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassMarkerSymbols;
            }
            else if (featurelayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassLineSymbols;
            }
            else if (featurelayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassFillSymbols;
            }
        }

        public IStyleGallery GetItem(ref esriSymbologyStyleClass styleClass, ISymbol symbol)
        {
            m_styleGalleryItem = null;
            axSymbologyControl1.StyleClass = styleClass;
            ISymbologyStyleClass symbologyStyleClass = axSymbologyControl1.GetStyleClass(styleClass);
            ServerStyleGalleryItem styleGalleryItem = new ServerStyleGalleryItem();
            styleGalleryItem.Item = symbol;
            styleGalleryItem.Name = "mySymbol";
            symbologyStyleClass.AddItem(styleGalleryItem, 0);
            symbologyStyleClass.SelectItem(0);
            this.ShowDialog();
            return (IStyleGallery)m_styleGalleryItem;
        }

        /*private*/
        public  Bitmap Sym2Bitmap(ISymbol sym, int width, int height)
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
            IGeometry geom = MakeGeometry(sym,bounds);
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

        private void buttonColor_Click(object sender, EventArgs e)
        {
            ColorDialog coldig = new ColorDialog();
            coldig.ShowDialog();

            if (axSymbologyControl1.StyleClass == esriSymbologyStyleClass.esriStyleClassMarkerSymbols)
            {
                ISymbol symbol = m_styleGalleryItem.Item as ISymbol;
                IMarkerSymbol mark = (IMarkerSymbol)symbol;
                IRgbColor rgbcolor = new RgbColorClass();
                rgbcolor = ConvertoIcolor(coldig.Color);
                IColor color = rgbcolor as IColor;
                mark.Color = color;
                ISymbologyStyleClass symbologyStyleClass;
                symbologyStyleClass = axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass);
                stdole.IPictureDisp picture;
                picture = symbologyStyleClass.PreviewItem(m_styleGalleryItem, pictureBox1.Width, pictureBox1.Height);
                Image image;
                image = Image.FromHbitmap(new IntPtr(picture.Handle));
                pictureBox1.Image = image;

            }
            else if (axSymbologyControl1.StyleClass == esriSymbologyStyleClass.esriStyleClassLineSymbols)
            {

                ILineSymbol symbol = m_styleGalleryItem.Item as ILineSymbol;
                IRgbColor rgbcolor = new RgbColorClass();
                rgbcolor = ConvertoIcolor(coldig.Color);
                IColor color = rgbcolor as IColor;
                symbol.Color = color;
                ISymbologyStyleClass symbologyStyleClass;
                symbologyStyleClass = axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass);
                stdole.IPictureDisp picture;
                picture = symbologyStyleClass.PreviewItem(m_styleGalleryItem, pictureBox1.Width, pictureBox1.Height);
                Image image;
                image = Image.FromHbitmap(new IntPtr(picture.Handle));
                pictureBox1.Image = image;

            }
            else if (axSymbologyControl1.StyleClass == esriSymbologyStyleClass.esriStyleClassFillSymbols)
            {
                IFillSymbol symbol = m_styleGalleryItem.Item as IFillSymbol;
                IRgbColor rgbcolor = new RgbColorClass();
                rgbcolor = ConvertoIcolor(coldig.Color);
                IColor color = rgbcolor as IColor;
                symbol.Color = color;
                ISymbologyStyleClass symbologyStyleClass;
                symbologyStyleClass = axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass);
                stdole.IPictureDisp picture;
                picture = symbologyStyleClass.PreviewItem(m_styleGalleryItem, pictureBox1.Width, pictureBox1.Height);
                Image image;
                image = Image.FromHbitmap(new IntPtr(picture.Handle));
                pictureBox1.Image = image;

            }
        }
        public IRgbColor ConvertoIcolor(Color color)
        {
            IRgbColor RGB = new RgbColorClass();
            RGB.RGB = color.B * 65336 + color.G * 256 + color.R;
            return RGB;
        }

        private void axSymbologyControl1_OnMouseDown(object sender, ISymbologyControlEvents_OnMouseDownEvent e)
        {
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ISymbol pSym = (ISymbol)m_styleGalleryItem.Item;
            if (axSymbologyControl1.StyleClass == esriSymbologyStyleClass.esriStyleClassMarkerSymbols)
            {
                IMarkerSymbol mark = (IMarkerSymbol)pSym;
                mark.Size = (double)numericUpDown1.Value;
            }
            else if (axSymbologyControl1.StyleClass == esriSymbologyStyleClass.esriStyleClassLineSymbols)
            {
                ILineSymbol symbol = (ILineSymbol)pSym;
                symbol.Width = (double)numericUpDown1.Value;
            }
            else if(axSymbologyControl1.StyleClass == esriSymbologyStyleClass.esriStyleClassFillSymbols)
            {
                IFillSymbol fillSymbol = (IFillSymbol)pSym;
            }
            Bitmap b = Sym2Bitmap(pSym, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = (Image)b;
            pictureBox1.Refresh();
        }

        private void axSymbologyControl1_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            m_styleGalleryItem = e.styleGalleryItem as IStyleGalleryItem;
            ISymbologyStyleClass symbologyStyleClass;
            symbologyStyleClass = axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass);
            stdole.IPictureDisp picture;
            picture = symbologyStyleClass.PreviewItem(m_styleGalleryItem, pictureBox1.Width, pictureBox1.Height);
            Image image;
            image = Image.FromHbitmap(new IntPtr(picture.Handle));
            pictureBox1.Image = image;
            if (axSymbologyControl1.StyleClass == esriSymbologyStyleClass.esriStyleClassMarkerSymbols)
            {
                ISymbol symbol = m_styleGalleryItem.Item as ISymbol;
                IMarkerSymbol mark = (IMarkerSymbol)symbol;
                numericUpDown1.Value = (int)mark.Size;
            }
            else if (axSymbologyControl1.StyleClass == esriSymbologyStyleClass.esriStyleClassLineSymbols)
            {
                ILineSymbol symbol = m_styleGalleryItem.Item as ILineSymbol;
                numericUpDown1.Value = (int)symbol.Width;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
