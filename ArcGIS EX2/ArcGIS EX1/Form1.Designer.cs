namespace ArcGIS_EX1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示全图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.放大ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdLoadShpf = new System.Windows.Forms.Button();
            this.cmdClearLayers = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.open = new System.Windows.Forms.Button();
            this.saveAs = new System.Windows.Forms.Button();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // axMapControl1
            // 
            this.axMapControl1.Location = new System.Drawing.Point(234, 81);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(657, 383);
            this.axMapControl1.TabIndex = 0;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(832, 43);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示全图ToolStripMenuItem,
            this.放大ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(139, 52);
            // 
            // 显示全图ToolStripMenuItem
            // 
            this.显示全图ToolStripMenuItem.Name = "显示全图ToolStripMenuItem";
            this.显示全图ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.显示全图ToolStripMenuItem.Text = "显示全图";
            this.显示全图ToolStripMenuItem.Click += new System.EventHandler(this.显示全图ToolStripMenuItem_Click);
            // 
            // 放大ToolStripMenuItem
            // 
            this.放大ToolStripMenuItem.Name = "放大ToolStripMenuItem";
            this.放大ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.放大ToolStripMenuItem.Text = "放大";
            this.放大ToolStripMenuItem.Click += new System.EventHandler(this.放大ToolStripMenuItem_Click);
            // 
            // cmdLoadShpf
            // 
            this.cmdLoadShpf.Location = new System.Drawing.Point(12, 43);
            this.cmdLoadShpf.Name = "cmdLoadShpf";
            this.cmdLoadShpf.Size = new System.Drawing.Size(127, 32);
            this.cmdLoadShpf.TabIndex = 2;
            this.cmdLoadShpf.Text = "加载shape文件";
            this.cmdLoadShpf.UseVisualStyleBackColor = true;
            this.cmdLoadShpf.Click += new System.EventHandler(this.cmdLoadShpf_Click);
            // 
            // cmdClearLayers
            // 
            this.cmdClearLayers.Location = new System.Drawing.Point(145, 43);
            this.cmdClearLayers.Name = "cmdClearLayers";
            this.cmdClearLayers.Size = new System.Drawing.Size(127, 32);
            this.cmdClearLayers.TabIndex = 3;
            this.cmdClearLayers.Text = "清空图层";
            this.cmdClearLayers.UseVisualStyleBackColor = true;
            this.cmdClearLayers.Click += new System.EventHandler(this.cmdClearLayers_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(278, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 32);
            this.button1.TabIndex = 4;
            this.button1.Text = "加载地图文档";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Location = new System.Drawing.Point(12, 81);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(216, 383);
            this.axTOCControl1.TabIndex = 5;
            this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
            this.axTOCControl1.OnMouseUp += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseUpEventHandler(this.axTOCControl1_OnMouseUp);
            this.axTOCControl1.OnMouseMove += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseMoveEventHandler(this.axTOCControl1_OnMouseMove);
            this.axTOCControl1.OnBeginLabelEdit += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnBeginLabelEditEventHandler(this.OnBeginLabelEdit);
            this.axTOCControl1.OnEndLabelEdit += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnEndLabelEditEventHandler(this.OnEndLabelEdit);
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(547, 43);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(127, 32);
            this.open.TabIndex = 6;
            this.open.Text = "打开";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // saveAs
            // 
            this.saveAs.Location = new System.Drawing.Point(680, 43);
            this.saveAs.Name = "saveAs";
            this.saveAs.Size = new System.Drawing.Size(127, 32);
            this.saveAs.TabIndex = 7;
            this.saveAs.Text = "存为";
            this.saveAs.UseVisualStyleBackColor = true;
            this.saveAs.Click += new System.EventHandler(this.saveAs_Click);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Location = new System.Drawing.Point(12, 5);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(863, 28);
            this.axToolbarControl1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 476);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.saveAs);
            this.Controls.Add(this.open);
            this.Controls.Add(this.axTOCControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmdClearLayers);
            this.Controls.Add(this.cmdLoadShpf);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axMapControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 显示全图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 放大ToolStripMenuItem;
        private System.Windows.Forms.Button cmdLoadShpf;
        private System.Windows.Forms.Button cmdClearLayers;
        private System.Windows.Forms.Button button1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private System.Windows.Forms.Button open;
        private System.Windows.Forms.Button saveAs;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
    }
}

