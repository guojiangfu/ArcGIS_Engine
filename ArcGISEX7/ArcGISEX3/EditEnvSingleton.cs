﻿using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcGISEX3
{
    public sealed class EditEnvSingleton
    {

        static readonly EditEnvSingleton m_instance = new EditEnvSingleton();
        static IWorkspaceEdit m_workspace = null;
        static IFeatureLayer m_TargetLayer;

        static bool m_InEditing = false;
        public EditEnvSingleton()
        {

        }

        public static EditEnvSingleton Instance
        {
            get { return m_instance; }
        }

        public static IFeatureLayer TargetLayer
        {   //设置/获取当前编辑的目标图层 
            get { return m_TargetLayer; }
            set { m_TargetLayer = value; }
        }
        public static IWorkspaceEdit workspaceEdit
        {   //设置/获取当前工作区 
            get { return m_workspace; }
            set { m_workspace = value; }
        }
        public static IFeatureClass targetFeatClass
        {//获取当前在编辑的要素类 
            get
            {
                if (m_TargetLayer != null)
                {
                    return m_TargetLayer.FeatureClass;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
