using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;

namespace EX3
{
    /// <summary>
    /// Summary description for OpenDocument.
    /// </summary>
    [Guid("3ffd7cd4-e0ed-453e-b033-a36b1a1d908f")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("EX3.OpenDocument")]
    public sealed class OpenDocument : BaseCommand
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
        public IMapControl3 axMapControl;
            
        public OpenDocument()//�򿪵�ͼ�ĵ�
        {
            //string bitmapResourceName = this.GetType().Name + ".bmp";
            //base.m_bitmap = new Bitmap(this.GetType(), bitmapResourceName);
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text
            base.m_caption = "�򿪵�ͼ�ĵ�";  //localizable text
            base.m_message = "";  //localizable text 
            base.m_toolTip = "";  //localizable text 
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;

            // TODO:  Add other initialization code
            //axMapControl = (IMapControl3)hook;
            axMapControl = ((IToolbarControl)hook).Buddy as IMapControl3;
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add OpenDocument.OnClick implementation
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "��*.mxd�ļ�";
            openFileDialog1.Filter = "�ı��ļ�(*.txt)|*.txt|excel�ļ�(*.xls)|*.xls|mxd��ͼ�ĵ�(*.mxd*)|*.mxd*";
            openFileDialog1.InitialDirectory = "C:\\Program Files (x86)\\ArcGIS\\DeveloperKit10.4\\Samples\\data\\World ";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filesName = System.IO.Path.GetFileName(openFileDialog1.FileName);//�õ��ļ���������·��
                string pathName = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);//�õ�·��
                string mxdpath = pathName + "\\" + filesName;
                axMapControl.LoadMxFile(mxdpath);
            }
        }

        #endregion
    }
}
