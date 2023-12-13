
namespace Web_Proxy
{
    partial class FormHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHome));
            this.panelTop = new System.Windows.Forms.Panel();
            this.pbHome = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pbSetting = new System.Windows.Forms.PictureBox();
            this.imgClose = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.labelRemove = new System.Windows.Forms.Label();
            this.labelLocalImp = new System.Windows.Forms.Label();
            this.panelPlugin = new System.Windows.Forms.Panel();
            this.ucHeader1 = new Web_Proxy.UC.UCHeader();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgClose)).BeginInit();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            this.panelTop.Controls.Add(this.pbHome);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.pbSetting);
            this.panelTop.Controls.Add(this.imgClose);
            this.panelTop.Controls.Add(this.labelTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(700, 36);
            this.panelTop.TabIndex = 0;
            // 
            // pbHome
            // 
            this.pbHome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbHome.BackColor = System.Drawing.Color.Transparent;
            this.pbHome.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbHome.BackgroundImage")));
            this.pbHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbHome.Location = new System.Drawing.Point(594, 0);
            this.pbHome.Name = "pbHome";
            this.pbHome.Size = new System.Drawing.Size(36, 36);
            this.pbHome.TabIndex = 5;
            this.pbHome.TabStop = false;
            this.pbHome.MouseEnter += new System.EventHandler(this.control_MouseEnter);
            this.pbHome.MouseLeave += new System.EventHandler(this.control_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(117, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "1.0.7";
            // 
            // pbSetting
            // 
            this.pbSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSetting.BackColor = System.Drawing.Color.Transparent;
            this.pbSetting.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbSetting.BackgroundImage")));
            this.pbSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbSetting.Location = new System.Drawing.Point(629, 0);
            this.pbSetting.Name = "pbSetting";
            this.pbSetting.Size = new System.Drawing.Size(36, 36);
            this.pbSetting.TabIndex = 3;
            this.pbSetting.TabStop = false;
            this.pbSetting.Click += new System.EventHandler(this.pbSetting_Click);
            this.pbSetting.MouseEnter += new System.EventHandler(this.control_MouseEnter);
            this.pbSetting.MouseLeave += new System.EventHandler(this.control_MouseLeave);
            // 
            // imgClose
            // 
            this.imgClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgClose.BackColor = System.Drawing.Color.Transparent;
            this.imgClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("imgClose.BackgroundImage")));
            this.imgClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.imgClose.Location = new System.Drawing.Point(664, 0);
            this.imgClose.Name = "imgClose";
            this.imgClose.Size = new System.Drawing.Size(36, 36);
            this.imgClose.TabIndex = 2;
            this.imgClose.TabStop = false;
            this.imgClose.Click += new System.EventHandler(this.imgClose_Click);
            this.imgClose.MouseEnter += new System.EventHandler(this.imgClose_MouseEnter);
            this.imgClose.MouseLeave += new System.EventHandler(this.imgClose_MouseLeave);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(37, 7);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(83, 20);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Web-Proxy";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.labelRemove);
            this.panelMain.Controls.Add(this.labelLocalImp);
            this.panelMain.Controls.Add(this.panelPlugin);
            this.panelMain.Controls.Add(this.ucHeader1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 36);
            this.panelMain.Margin = new System.Windows.Forms.Padding(0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(700, 444);
            this.panelMain.TabIndex = 3;
            // 
            // labelRemove
            // 
            this.labelRemove.AutoSize = true;
            this.labelRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelRemove.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelRemove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.labelRemove.Location = new System.Drawing.Point(88, 8);
            this.labelRemove.Name = "labelRemove";
            this.labelRemove.Size = new System.Drawing.Size(37, 20);
            this.labelRemove.TabIndex = 4;
            this.labelRemove.Text = "移除";
            this.labelRemove.Click += new System.EventHandler(this.labelRemove_Click);
            // 
            // labelLocalImp
            // 
            this.labelLocalImp.AutoSize = true;
            this.labelLocalImp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelLocalImp.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelLocalImp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(240)))));
            this.labelLocalImp.Location = new System.Drawing.Point(12, 8);
            this.labelLocalImp.Name = "labelLocalImp";
            this.labelLocalImp.Size = new System.Drawing.Size(65, 20);
            this.labelLocalImp.TabIndex = 3;
            this.labelLocalImp.Text = "本地导入";
            this.labelLocalImp.Click += new System.EventHandler(this.labelLocalImp_Click);
            // 
            // panelPlugin
            // 
            this.panelPlugin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPlugin.Location = new System.Drawing.Point(0, 38);
            this.panelPlugin.Name = "panelPlugin";
            this.panelPlugin.Size = new System.Drawing.Size(700, 406);
            this.panelPlugin.TabIndex = 1;
            // 
            // ucHeader1
            // 
            this.ucHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucHeader1.Location = new System.Drawing.Point(0, 0);
            this.ucHeader1.Margin = new System.Windows.Forms.Padding(8);
            this.ucHeader1.Name = "ucHeader1";
            this.ucHeader1.Size = new System.Drawing.Size(700, 38);
            this.ucHeader1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(82)))));
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(6, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(26, 26);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // FormHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.ClientSize = new System.Drawing.Size(700, 480);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelTop);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormSet_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgClose)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.PictureBox imgClose;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.PictureBox pbSetting;
        private System.Windows.Forms.Panel panelPlugin;
        private UC.UCHeader ucHeader1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbHome;
        private System.Windows.Forms.Label labelLocalImp;
        private System.Windows.Forms.Label labelRemove;
    }
}

