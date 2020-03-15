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
    public partial class MapSpatialRefDlg : Form
    {
        ISpatialReference Prj;
        public MapSpatialRefDlg()
        {
            InitializeComponent();
        }

        private void MapSpatialRefDlg_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add(new item(4214, "Beijing 1954 GCS"));
            comboBox1.Items.Add(new item(21415, "Beijing 1954 GK Zone 15"));
            comboBox1.Items.Add(new item(21416, "Beijing 1954 GK Zone 16 "));
            comboBox1.Items.Add(new item(21417, "Beijing 1954 GK Zone 17"));
            Prj = Form1.form1.axMapControl1.SpatialReference;
            if (Prj is IProjectedCoordinateSystem)
            {
                IProjectedCoordinateSystem5 PCS = Prj as IProjectedCoordinateSystem5;
                listBox1.Items.Add("Projection:" + PCS.Projection.Name.ToString());
                listBox1.Items.Add("False_Easting:" + PCS.FalseEasting.ToString());
                listBox1.Items.Add("False_Northing:" + PCS.FalseNorthing.ToString());
                listBox1.Items.Add("Central_Meridian:" + PCS.CentralMeridian[true].ToString());
                listBox1.Items.Add("Scale_Factor:" + PCS.ScaleFactor.ToString());
                listBox1.Items.Add("Latitude_Of_Origin:" + PCS.LatitudeOfOrigin.ToString());
                listBox1.Items.Add("Linear Unit:" + PCS.CoordinateUnit.Name.ToString());
                listBox1.Items.Add("Geographic Coordinate System:" + PCS.GeographicCoordinateSystem.Name.ToString());
                listBox1.Items.Add("Angular Unit:" + PCS.GeographicCoordinateSystem.CoordinateUnit.Name.ToString());
                listBox1.Items.Add("Prime Meridian:" + PCS.GeographicCoordinateSystem.PrimeMeridian.Name.ToString());
                listBox1.Items.Add("Datum:" + PCS.GeographicCoordinateSystem.Datum.Name.ToString());
                listBox1.Items.Add("Spheroid :" + PCS.GeographicCoordinateSystem.Datum.Spheroid.Name.ToString());
                listBox1.Items.Add("Semimajor Axis :" + PCS.GeographicCoordinateSystem.Datum.Spheroid.SemiMajorAxis.ToString());
                listBox1.Items.Add("Semiminor Axis :" + PCS.GeographicCoordinateSystem.Datum.Spheroid.SemiMinorAxis.ToString());
                listBox1.Items.Add("Inverse Flattening:" + PCS.GeographicCoordinateSystem.Datum.Spheroid.Flattening.ToString());
            }
            if (Prj is IGeographicCoordinateSystem)
            {
                IGeographicCoordinateSystem GCS = Prj as IGeographicCoordinateSystem;
                listBox1.Items.Add("Angular Unit:" + GCS.CoordinateUnit.Name.ToString());
                listBox1.Items.Add("Prime Meridian:" + GCS.PrimeMeridian.Name .ToString());
                listBox1.Items.Add("Datum:" + GCS.Datum.Name.ToString());
                listBox1.Items.Add("Spheroid:" + GCS.Datum.Spheroid.Name.ToString());
                listBox1.Items.Add("Semimajor Axis:" + GCS.Datum.Spheroid.SemiMajorAxis.ToString());
                listBox1.Items.Add("Semiminor Axis:" + GCS.Datum.Spheroid.SemiMinorAxis.ToString());
                listBox1.Items.Add("Inverse Flattening:" + GCS.Datum.Spheroid.Flattening.ToString());
            }
        }

        public ISpatialReference MakeSpatialReference(esriSRProjCSType coordinateSystem)
        {
            if (  (int)coordinateSystem == 4214 )//GCS
            {
                ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                ISpatialReferenceResolution spatialReferenceResolution = spatialReferenceFactory.CreateGeographicCoordinateSystem (System.Convert.ToInt32(coordinateSystem)) as ISpatialReferenceResolution;
                spatialReferenceResolution.ConstructFromHorizon();
                ISpatialReferenceTolerance spatialReferenceTolerance = spatialReferenceResolution as ISpatialReferenceTolerance;
                spatialReferenceTolerance.SetDefaultXYTolerance();
                ISpatialReference spatialReference = spatialReferenceResolution as ISpatialReference;
                return spatialReference;
            }
            else//PCS
            {
                ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                ISpatialReferenceResolution spatialReferenceResolution = spatialReferenceFactory.CreateProjectedCoordinateSystem(System.Convert.ToInt32(coordinateSystem)) as ISpatialReferenceResolution;
                spatialReferenceResolution.ConstructFromHorizon();
                ISpatialReferenceTolerance spatialReferenceTolerance = spatialReferenceResolution as ISpatialReferenceTolerance;
                spatialReferenceTolerance.SetDefaultXYTolerance();
                ISpatialReference spatialReference = spatialReferenceResolution as ISpatialReference;
                return spatialReference;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            int index = ((item)comboBox1.Items[comboBox1.SelectedIndex]).value;
            Form1.form1.axMapControl1.SpatialReference = MakeSpatialReference((esriSRProjCSType)index);
            // Form1.form1.axMapControl1.SpatialReference = MakeSpatialReference();
            Object selectedItem = comboBox1.SelectedItem;
          //  MakeSpatialReference(selectedItem);
            Form1.form1.axMapControl1.Refresh();
            MessageBox.Show("应用成功");
        }
    }
    public class item
    {
        public string text;
        public int value;
        public item (int v,string t)
        {
            this.value = v;
            this.text = t;
        }
        public override string ToString()
        {
            return this.text;
        }
    }
}
