using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;

namespace ArcGISEX3
{
    /// <summary>
    /// Summary description for ZoomIn.
    /// </summary>
    [Guid("b70d5b78-ac4a-459d-a556-d7487237556f")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("ArcGISEX3.ZoomIn")]
    public sealed class ZoomIn : BaseTool
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
        private System.Drawing.Bitmap m_bitmap;
        private IntPtr m_hBitmap;
        private INewEnvelopeFeedback m_feedBack;
        private IPoint m_point;
        private Boolean m_isMouseDown;
        private System.Windows.Forms.Cursor m_zoomInCur;
        private System.Windows.Forms.Cursor m_moveZoomInCur;
        public ZoomIn()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text 
            base.m_caption = "·Å´ó";  //localizable text 
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

            // TODO:  Add ZoomIn.OnCreate implementation
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add ZoomIn.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ZoomIn.OnMouseDown implementation
            if (m_hookHelper.ActiveView == null)
            {
                return;
            }
            if (m_hookHelper.ActiveView is IPageLayout)
            {
                IPoint pPoint = (IPoint)(m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y));
                IMap pMap = m_hookHelper.ActiveView.HitTestMap(pPoint);
                if (pMap == null)
                {
                    return;
                }
                if (pMap != m_hookHelper.FocusMap)
                {
                    m_hookHelper.ActiveView.FocusMap = pMap;
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                }
            }
            IActiveView pActiveView = (IActiveView)m_hookHelper.FocusMap;
            m_point = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            m_isMouseDown = true;
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ZoomIn.OnMouseMove implementation
            if (!m_isMouseDown)
            {
                return;
            }
            IActiveView pActiveView = (IActiveView)m_hookHelper.FocusMap;
            if (m_feedBack == null)
            {
                m_feedBack = new NewEnvelopeFeedbackClass();
                m_feedBack.Display = pActiveView.ScreenDisplay;
                m_feedBack.Start(m_point);
            }
            m_feedBack.MoveTo(pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y));
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ZoomIn.OnMouseUp implementation
            if (!m_isMouseDown)
            {
                return;
            }
            IActiveView pActiveView = (IActiveView)m_hookHelper.FocusMap;
            IEnvelope pEnvelope = default(IEnvelope);
            if (m_feedBack == null)
            {
                pEnvelope = pActiveView.Extent;
                pEnvelope.Expand(0.5, 0.5, true);
                pEnvelope.CenterAt(m_point);
            }
            else
            {
                pEnvelope = m_feedBack.Stop();
                if (pEnvelope.Width == 0 || pEnvelope.Height == 0)
                {
                    m_feedBack = null;
                    m_isMouseDown = false;
                }
            }
            pActiveView.Extent = pEnvelope;
            pActiveView.Refresh();
            m_feedBack = null;
            m_isMouseDown = false;
        }
        #endregion

    }
}
