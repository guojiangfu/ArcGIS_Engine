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
    public partial class PictureForm : Form
    {
        string PicturePath;
        string PictureName;
        public PictureForm(string picturePath)
        {
            InitializeComponent();
            this.PicturePath = @"E:\ArcGIS开发\实验数据\Ex8\PIC\" + picturePath + ".jpg";
            this.PictureName = picturePath;
        }

        private void PictureForm_Load(object sender, EventArgs e)
        {
            string[] BuildingName = { "凤起", "贡院考棚", "会泽院正面", "蛟腾", "科学馆", "南学楼",
                                                            "情人坡", "生物东楼", "图书馆", "文渊楼(东学楼)", "物理馆正面", "信息学院",
                                                               "映秋院", "映日海棠", "正门_阶梯", "至公堂", "钟楼" };
            if( BuildingName.Contains(PictureName))
            {
                pictureBox1.Image = Image.FromFile(PicturePath);
            }
           else
            {
                string Tips = "抱歉！尚未采集到" + PictureName + "的信息！";
                MessageBox.Show(Tips);
                this.Close();
            }
        }
    }
}
