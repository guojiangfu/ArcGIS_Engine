using ESRI.ArcGIS.Carto;
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
    public partial class MapWindow : Form
    {
        public ILayer layer;
        public IGeometry poly1;
        public IClone poly2;
        public MapWindow(IFeatureLayer FeatureLayer)
        {
            ESRI.ArcGIS.RuntimeManager.BindLicense(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            InitializeComponent();
            layer = (ILayer)FeatureLayer;
        }

        private void MapWindow_Load(object sender, EventArgs e)
        {
            axMapControl1.AddLayer(layer);
            axMapControl1.Refresh();
        }

        private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            poly1 = this.axMapControl1.TrackPolygon();
            DrawMapShape(poly1);
            axMapControl1.Refresh(esriViewDrawPhase.esriViewGeography, null, null);
            IClone clone = (IClone)poly1;
            poly2 = clone.Clone();
            IPolygon poly = (IPolygon)poly2;
            poly.SpatialReference = Form1.form1.axMapControl1.SpatialReference;
            Form1.form1.axMapControl1.DrawShape(poly);
        }
        private void DrawMapShape(IGeometry poly)
        {
            IRgbColor color;
            color = new RgbColorClass();
            color.Red = 255;
            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbol();
            simpleFillSymbol.Color = color;
            object o = simpleFillSymbol;
            axMapControl1.DrawShape(poly, ref o);
        }
    }
}
