namespace Web_Proxy.UC
{
    partial class UCPlugin
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPlugin));
            this.labelName = new System.Windows.Forms.Label();
            this.labelDiscription = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelPrintLine = new System.Windows.Forms.Label();
            this.pbIco = new System.Windows.Forms.PictureBox();
            this.labelFrom = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbIco)).BeginInit();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.labelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.labelName.Location = new System.Drawing.Point(180, 35);
            this.labelName.Margin = new System.Windows.Forms.Padding(0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(155, 34);
            this.labelName.TabIndex = 3;
            this.labelName.Text = "插件名称";
            this.labelName.Click += new System.EventHandler(this.labelName_Click);
            this.labelName.DoubleClick += new System.EventHandler(this.UCPlugin_Click);
            this.labelName.MouseEnter += new System.EventHandler(this.labelName_MouseEnter);
            this.labelName.MouseLeave += new System.EventHandler(this.labelName_MouseLeave);
            // 
            // labelDiscription
            // 
            this.labelDiscription.AutoSize = true;
            this.labelDiscription.Font = new System.Drawing.Font("宋体", 9F);
            this.labelDiscription.ForeColor = System.Drawing.Color.Gray;
            this.labelDiscription.Location = new System.Drawing.Point(182, 115);
            this.labelDiscription.Margin = new System.Windows.Forms.Padding(0);
            this.labelDiscription.Name = "labelDiscription";
            this.labelDiscription.Size = new System.Drawing.Size(133, 30);
            this.labelDiscription.TabIndex = 4;
            this.labelDiscription.Text = "插件描述";
            this.labelDiscription.Click += new System.EventHandler(this.UCPlugin_Click);
            this.labelDiscription.DoubleClick += new System.EventHandler(this.UCPlugin_Click);
            // 
            // labelSize
            // 
            this.labelSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSize.AutoSize = true;
            this.labelSize.Font = new System.Drawing.Font("宋体", 10F);
            this.labelSize.ForeColor = System.Drawing.Color.Gray;
            this.labelSize.Location = new System.Drawing.Point(1412, 68);
            this.labelSize.Margin = new System.Windows.Forms.Padding(0);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(100, 34);
            this.labelSize.TabIndex = 5;
            this.labelSize.Text = "200KB";
            this.labelSize.Click += new System.EventHandler(this.UCPlugin_Click);
            this.labelSize.DoubleClick += new System.EventHandler(this.UCPlugin_Click);
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("宋体", 10F);
            this.labelTime.ForeColor = System.Drawing.Color.Gray;
            this.labelTime.Location = new System.Drawing.Point(1598, 68);
            this.labelTime.Margin = new System.Windows.Forms.Padding(0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(168, 34);
            this.labelTime.TabIndex = 6;
            this.labelTime.Text = "2018-8-20";
            this.labelTime.Click += new System.EventHandler(this.UCPlugin_Click);
            this.labelTime.DoubleClick += new System.EventHandler(this.UCPlugin_Click);
            // 
            // labelVersion
            // 
            this.labelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("宋体", 10F);
            this.labelVersion.ForeColor = System.Drawing.Color.Gray;
            this.labelVersion.Location = new System.Drawing.Point(1222, 68);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(0);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(100, 34);
            this.labelVersion.TabIndex = 7;
            this.labelVersion.Text = "1.0.0";
            this.labelVersion.Click += new System.EventHandler(this.UCPlugin_Click);
            this.labelVersion.DoubleClick += new System.EventHandler(this.UCPlugin_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 166);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(8);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1835, 2);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // labelPrintLine
            // 
            this.labelPrintLine.BackColor = System.Drawing.Color.WhiteSmoke;
            this.labelPrintLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelPrintLine.Location = new System.Drawing.Point(0, 168);
            this.labelPrintLine.Margin = new System.Windows.Forms.Padding(0);
            this.labelPrintLine.Name = "labelPrintLine";
            this.labelPrintLine.Size = new System.Drawing.Size(1835, 2);
            this.labelPrintLine.TabIndex = 8;
            // 
            // pbIco
            // 
            this.pbIco.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbIco.BackgroundImage")));
            this.pbIco.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbIco.Location = new System.Drawing.Point(40, 38);
            this.pbIco.Margin = new System.Windows.Forms.Padding(8);
            this.pbIco.Name = "pbIco";
            this.pbIco.Size = new System.Drawing.Size(100, 100);
            this.pbIco.TabIndex = 10;
            this.pbIco.TabStop = false;
            this.pbIco.Click += new System.EventHandler(this.UCPlugin_Click);
            this.pbIco.DoubleClick += new System.EventHandler(this.UCPlugin_DoubleClick);
            // 
            // labelFrom
            // 
            this.labelFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFrom.AutoSize = true;
            this.labelFrom.Font = new System.Drawing.Font("宋体", 10F);
            this.labelFrom.ForeColor = System.Drawing.Color.Gray;
            this.labelFrom.Location = new System.Drawing.Point(988, 68);
            this.labelFrom.Margin = new System.Windows.Forms.Padding(0);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(83, 34);
            this.labelFrom.TabIndex = 11;
            this.labelFrom.Text = "本地";
            this.labelFrom.DoubleClick += new System.EventHandler(this.UCPlugin_Click);
            // 
            // UCPlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.pbIco);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.labelPrintLine);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.labelDiscription);
            this.Controls.Add(this.labelName);
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "UCPlugin";
            this.Size = new System.Drawing.Size(1835, 170);
            this.Click += new System.EventHandler(this.UCPlugin_Click);
            this.DoubleClick += new System.EventHandler(this.UCPlugin_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.pbIco)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelDiscription;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label labelPrintLine;
        private System.Windows.Forms.PictureBox pbIco;
        private System.Windows.Forms.Label labelFrom;
    }
}
