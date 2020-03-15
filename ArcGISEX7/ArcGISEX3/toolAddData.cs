using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

namespace ArcGISEX3
{
    /// <summary>
    /// Summary description for toolAddData.
    /// </summary>
    [Guid("54fec2c0-4b90-479a-898a-f22253d32e65")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("ArcGISEX3.toolAddData")]
    public sealed class toolAddData : BaseTool
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper;
        private IFeatureClass featureclass;
        public static IFeature  addfeature;
        public toolAddData()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text 
            base.m_caption = "Ìí¼ÓÒªËØ";  //localizable text 
            base.m_message = "";  //localizable text
            base.m_toolTip = "";  //localizable text
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;

            // TODO:  Add toolAddData.OnCreate implementation
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add toolAddData.OnClick implementation
            EditEnvSingleton.workspaceEdit.StartEditing(true);
            EditEnvSingleton.workspaceEdit.StartEditOperation();
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add toolAddData.OnMouseDown implementation
            featureclass = EditEnvSingleton.targetFeatClass;
            if (featureclass.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                CreateFeature(featureclass, Form1.form1.axMapControl1.TrackLine());
            }
            else if (featureclass.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                CreateFeature(featureclass, Form1.form1.axMapControl1.TrackPolygon()); 
            }
            else if (featureclass.ShapeType == esriGeometryType.esriGeometryPoint)
            {
                CreateFeature(featureclass, Form1.form1.axMapControl1.ToMapPoint(X, Y));
            }
        }
        public IFeature CreateFeature(IFeatureClass featureclass, IGeometry geometry)
        {
            addfeature = featureclass.CreateFeature();
            addfeature.Shape = geometry;
            Form1.form1.axMapControl1.Map.SelectFeature(EditEnvSingleton.TargetLayer, addfeature);
            Form1.form1.axMapControl1.Refresh();
            return addfeature;
        }
        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add toolAddData.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add toolAddData.OnMouseUp implementation
        }
        #endregion
    }
}
