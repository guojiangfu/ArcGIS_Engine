using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geometry;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace ArcGISEX3
{
    /// <summary>
    /// Summary description for cmdProjectLayer.
    /// </summary>
    [Guid("3aa908df-a669-4c2e-8afb-50d9d8c770fd")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("ArcGISEX3.cmdProjectLayer")]
    public sealed class cmdProjectLayer : BaseCommand
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
        string targetWorkspacePath;
        IWorkspaceFactory pAccessWorkspaceFactory = new AccessWorkspaceFactoryClass();

        public cmdProjectLayer()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text
            base.m_caption = "投影图层为新数据";  //localizable text
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
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            ILayer pLayer = default(ILayer);
            pLayer = (ILayer)Form1.form1.axMapControl1.CustomProperty;
            IFeatureLayer layer = (IFeatureLayer)pLayer;
            IFeatureClass pInFeatureClass = layer.FeatureClass;
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "(*.mdb)|*.mdb";
            openfile.RestoreDirectory = true;
            openfile.Multiselect = false;
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                targetWorkspacePath = openfile.FileName;
            }
            IWorkspace pWorkspace;
            pWorkspace = pAccessWorkspaceFactory.OpenFromFile(targetWorkspacePath, 0);
            IFeatureWorkspace workspace = pWorkspace as IFeatureWorkspace;
            IEnumDatasetName pEnumDatasetName;
            IDatasetName pDatasetName;
            pEnumDatasetName = pWorkspace.get_DatasetNames(esriDatasetType.esriDTFeatureClass ^ esriDatasetType.esriDTFeatureDataset);
            pEnumDatasetName.Reset();
            pDatasetName = pEnumDatasetName.Next();
            while (pDatasetName != null)
            {
                if (pDatasetName.Name.IndexOf("ExportLayer") >= 0)
                {
                    IFeatureWorkspaceManage pWorkspaceManager = workspace as IFeatureWorkspaceManage;
                    pWorkspaceManager.DeleteByName(pDatasetName);
                }
                pDatasetName = pEnumDatasetName.Next();
            }
            ExportFeature(pInFeatureClass, targetWorkspacePath);
            IWorkspaceFactory pworkspacefactory = new AccessWorkspaceFactoryClass();
            IWorkspace pWorksapce = pworkspacefactory.OpenFromFile(targetWorkspacePath, 0);
            IFeatureWorkspace pfeatureworkspace = (IFeatureWorkspace)pWorksapce;
            IFeatureLayer mfeaturelayer = new FeatureLayerClass();
            mfeaturelayer.FeatureClass = pfeatureworkspace.OpenFeatureClass("ExportLayer");
            MapWindow map = new MapWindow(mfeaturelayer);
            map.Show();
            // TODO: Add cmdProjectLayer.OnClick implementation

        }

        public void ExportFeature(IFeatureClass pInFeatureClass, string targetWorkspacePath)
        {
            IDataset indataset = pInFeatureClass as IDataset;
            IWorkspace sourceWorkspace = indataset.Workspace;
            IWorkspaceFactory sourceWorkspaceFactory = new AccessWorkspaceFactory();
            IFeatureClassName pinfeaturename = indataset.FullName as IFeatureClassName;
            IWorkspaceFactory targetWorkspaceFactory = new AccessWorkspaceFactory();
            IWorkspace targetWorkspace = targetWorkspaceFactory.OpenFromFile(targetWorkspacePath, 0);
            // Cast the workspaces to the IDataset interface and get name objects. 
            IDataset sourceWorkspaceDataset = (IDataset)sourceWorkspace;
            IDataset targetWorkspaceDataset = (IDataset)targetWorkspace;
            IName sourceWorkspaceDatasetName = sourceWorkspaceDataset.FullName;
            IName targetWorkspaceDatasetName = targetWorkspaceDataset.FullName;
            IWorkspaceName sourceWorkspaceName = (IWorkspaceName)sourceWorkspaceDatasetName;
            IWorkspaceName targetWorkspaceName = (IWorkspaceName)targetWorkspaceDatasetName;
            // Create a name object for the shapefile and cast it to the IDatasetName interface. 
            IFeatureClassName sourceFeatureClassName = pinfeaturename;
            IDatasetName sourceDatasetName = (IDatasetName)sourceFeatureClassName;
            sourceDatasetName.WorkspaceName = sourceWorkspaceName;
            // Create a name object for the FGDB feature class and cast it to the IDatasetName interface. 
            IFeatureClassName targetFeatureClassName = new FeatureClassNameClass();
            IDatasetName targetDatasetName = (IDatasetName)targetFeatureClassName;
            targetDatasetName.Name = "ExportLayer";
            targetDatasetName.WorkspaceName = targetWorkspaceName;
            // Open source feature class to get field definitions. 
            IName sourceName = (IName)sourceFeatureClassName;
            IFeatureClass sourceFeatureClass = (IFeatureClass)sourceName.Open();
            // Create the objects and references necessary for field validation. 
            IFieldChecker fieldChecker = new FieldCheckerClass();
            IFields sourceFields = sourceFeatureClass.Fields;
            IFields targetFields = null;
            IEnumFieldError enumFieldError = null;
            // Set the required properties for the IFieldChecker interface. 
            fieldChecker.InputWorkspace = sourceWorkspace;
            fieldChecker.ValidateWorkspace = targetWorkspace;
            // Validate the fields and check for errors. 
            fieldChecker.Validate(sourceFields, out enumFieldError, out targetFields);
            if (enumFieldError != null)
            {
                // Handle the errors in a way appropriate to your application. 
                Console.WriteLine("Errors were encountered during field validation.");
            }
            // Find the shape field. 
            string shapeFieldName = sourceFeatureClass.ShapeFieldName;
            int shapeFieldIndex = sourceFeatureClass.FindField(shapeFieldName);
            IField shapeField = sourceFields.Field[shapeFieldIndex];
            // Get the geometry definition from the shape field and clone it. 
            IGeometryDef geometryDef = shapeField.GeometryDef;
            IClone geometryDefClone = (IClone)geometryDef;
            IClone targetGeometryDefClone = geometryDefClone.Clone();
            IGeometryDef targetGeometryDef = (IGeometryDef)targetGeometryDefClone;
            // Cast the IGeometryDef to the IGeometryDefEdit interface. 
            IGeometryDefEdit targetGeometryDefEdit = (IGeometryDefEdit)targetGeometryDef;
            ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
            IProjectedCoordinateSystem projectedCoordinateSystem = spatialReferenceFactory.CreateProjectedCoordinateSystem((int)esriSRProjCSType.esriSRProjCS_Beijing1954GK_17N);
            ISpatialReference spatialReference = projectedCoordinateSystem as ISpatialReference;
            IControlPrecision2 controlPrecision = spatialReference as IControlPrecision2;
            controlPrecision.IsHighPrecision = true;
            ISpatialReferenceResolution spatialReferenceResolution = (ISpatialReferenceResolution)spatialReference;
            //These three methods are the keys, construct horizon, then set the default x,y resolution and tolerance. 
            spatialReferenceResolution.ConstructFromHorizon();
            //Set the default x,y resolution value. 
            spatialReferenceResolution.SetDefaultXYResolution();
            //Set the default x,y tolerance value. 
            ISpatialReferenceTolerance spatialReferenceTolerance = (ISpatialReferenceTolerance)spatialReference;
            spatialReferenceTolerance.SetDefaultXYTolerance();
            targetGeometryDefEdit.SpatialReference_2 = spatialReference;
            // Set the IGeometryDefEdit properties. 
            //  targetGeometryDefEdit.GridCount_2 = 1 
            //  targetGeometryDefEdit.GridSize_2(0) = 0.75 
            // Create a query filter to only select cities with a province (PROV) value of 'NS'. 
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = "";
            // Create the converter and run the conversion. 
            IFeatureDataConverter featureDataConverter = new FeatureDataConverterClass();
            IEnumInvalidObject enumInvalidObject = featureDataConverter.ConvertFeatureClass(sourceFeatureClassName, queryFilter, null, targetFeatureClassName, targetGeometryDef, targetFields, "", 1000, 0);
            // Check for errors. 
            IInvalidObjectInfo invalidObjectInfo = null;
            enumInvalidObject.Reset();
            while ((invalidObjectInfo = enumInvalidObject.Next()) != null)
            {
                Console.WriteLine("Errors occurred for the following feature: {0}", invalidObjectInfo.InvalidObjectID);
            }
        }
        #endregion
    }
}
