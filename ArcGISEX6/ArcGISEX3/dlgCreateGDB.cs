using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesFile;
using System.IO;

namespace ArcGISEX3
{
    public partial class dlgCreateGDB : Form
    {
        public dlgCreateGDB()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fldDlog = new FolderBrowserDialog();
            fldDlog.ShowDialog();
            fldDlog.ShowNewFolderButton = true;
            textBox1.Text = fldDlog.SelectedPath;
        }

        private void dlgCreateGDB_Load(object sender, EventArgs e)
        {
            textBox1.Text = "显示文件夹名称";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
        }

        public IWorkspace CreateAccessGDBWorkspace()
        {
            IWorkspaceFactory worksoceFactory = new AccessWorkspaceFactoryClass();
            IWorkspaceName workspaceName = worksoceFactory.Create(textBox1.Text,textBox2.Text,null,0);
            IName Name = (IName)workspaceName;
            IWorkspace workspace = (IWorkspace)Name.Open();
            return workspace;
        }
        public IWorkspace CreateFileGDBWorkspace()
        {
            IWorkspaceFactory worksoceFactory = new FileGDBWorkspaceFactoryClass();
            IWorkspaceName workspaceName = worksoceFactory.Create(textBox1.Text, textBox2.Text, null, 0);
            IName Name = (IName)workspaceName;
            IWorkspace workspace = (IWorkspace)Name.Open();
            return workspace;
        }
        public IWorkspace CreateShapefileGDBWorkspace()
        {
            IWorkspaceFactory2 worksoceFactory = (IWorkspaceFactory2)new ShapefileWorkspaceFactoryClass();
            IWorkspaceName workspaceName = worksoceFactory.Create(textBox1.Text, textBox2.Text, null, 0);
            IName Name = (IName)workspaceName;
            IWorkspace workspace = (IWorkspace)Name.Open();
            return workspace;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                CreateAccessGDBWorkspace();
                MessageBox.Show("成功创建了AccessGDB数据库！");
            }
            else if (radioButton2.Checked == true)
            {
                CreateFileGDBWorkspace();
                MessageBox.Show("成功创建了FileGDB数据库！");
            }
            else if (radioButton3.Checked == true)
            {
                CreateShapefileGDBWorkspace();
                MessageBox.Show("成功创建了ShapefileGDB数据库！");
            }
            else
                MessageBox.Show("请先选择需要创建的数据库的类型！");
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            string path = @"E:\ArcGIS开发\实验数据\EX6Test\"+ textBox2.Text;
            if (radioButton1.Checked == true && File.Exists(path+".mdb")&&File.Exists(path + ".ldb"))
            {
                File.Delete(path+".mdb");
                File.Delete(path+ ".ldb");
                MessageBox.Show("AeecsssGDB删除成功！");
            }
            else if (radioButton2.Checked ==true && Directory.Exists(path+".gdb"))
            {
                Directory.Delete(path+".gdb",true);
                MessageBox.Show("FileGDB删除成功！");
            }
            else if (radioButton3.Checked == true && Directory.Exists(path))
            {
                Directory.Delete(path,true);
                MessageBox.Show("ShapeFileGDB删除成功！");
            }
            else
            {
                MessageBox.Show("工作空间不存在或没有选择需要删除的数据库类型！");
            }
        }
    }
}
