using Proxy.Common;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WebProxy.Plugin;

namespace Web_Proxy.UC
{
    public partial class UCPlugin : UserControl
    {
        private string id;
        public delegate void SelectedChanged(UCPlugin uc);
        public SelectedChanged SelectedChange;

        public UCPlugin()
        {
            InitializeComponent();
            this.labelName.Text = "";
            this.labelDiscription.Text = "";
            this.labelVersion.Text = "";
            this.labelSize.Text = "";
            this.labelTime.Text = "";
            this.labelFrom.Text = "";
        }

        public UCPlugin(PluginConfig plugin) : this()
        {
            this.id = plugin.Plugin.ID;
            this.Tag = plugin.Path;
            this.PluginName = plugin.Name;
            this.Discription = plugin.Discription;
            this.Version = plugin.Plugin.Version;
            if (plugin.UpdateTime != null && plugin.UpdateTime > DateTime.MinValue)
            {
                this.Time = plugin.UpdateTime.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                this.Time = plugin.CreateTime.ToString("yyyy-MM-dd");
            }

            this.From = EnumHelper.GetDescription(typeof(PluginFrom),(int)plugin.From);
            if (plugin.From == PluginFrom.Local)
            {
                this.pbIco.BackgroundImage = Properties.Resources.dll;
            }
        }

        private bool selected;
        /// <summary>
        /// 获取或者甚至是否选中
        /// </summary>
        public bool Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                this.selected = value;
                if (this.selected)
                {
                    this.UCPlugin_Click(this, null);
                }
            }
        }

        public string GetPath()
        {
            return this.Tag.ToString();
        }

        /// <summary>
        /// 设置或获取插件的名称
        /// </summary>
        [Localizable(true)]
        [Description("设置或获取插件的名称"), Browsable(true), Category("扩展")]
        public string PluginName
        {
            get
            {
                return this.labelName.Text;
            }
            set
            {
                this.labelName.Text = value;
            }
        }
        /// <summary>
        /// 设置或获取插件的名称
        /// </summary>
        [Localizable(true)]
        [Description("设置或获取插件的名称"), Browsable(true), Category("扩展")]
        public string Discription
        {
            get
            {
                return this.labelDiscription.Text;
            }
            set
            {
                this.labelDiscription.Text = value;
            }
        }

        /// <summary>
        /// 设置或获取插件的版本
        /// </summary>
        [Localizable(true)]
        [Description("设置或获取插件的版本"), Browsable(true), Category("扩展")]
        public string Version
        {
            get
            {
                return this.labelVersion.Text;
            }
            set
            {
                this.labelVersion.Text = value;
            }
        }

        /// <summary>
        /// 设置或获取插件的大小
        /// </summary>
        [Localizable(true)]
        [Description("设置或获取插件的大小"), Browsable(true), Category("扩展")]
        public string DllSize
        {
            get
            {
                return this.labelSize.Text;
            }
            set
            {
                this.labelSize.Text = value;
            }
        }

        /// <summary>
        /// 设置或获取插件的名称
        /// </summary>
        [Localizable(true)]
        [Description("设置或获取插件的名称"), Browsable(true), Category("扩展")]
        public string Time
        {
            get
            {
                return this.labelTime.Text;
            }
            set
            {
                this.labelTime.Text = value;
            }
        }

        /// <summary>
        /// 设置或获取插件的来源
        /// </summary>
        [Localizable(true)]
        [Description("设置或获取插件的来源"), Browsable(true), Category("扩展")]
        public string From
        {
            get
            {
                return this.labelFrom.Text;
            }
            set
            {
                this.labelFrom.Text = value;
            }
        }

        private void UCPlugin_Click(object sender, System.EventArgs e)
        {
            if (SelectedChange != null)
            {
                SelectedChange(this);
            }
        }

        private void UCPlugin_DoubleClick(object sender, EventArgs e)
        {
            var path = this.GetPath();
            if (!File.Exists(path))
            {
                return;
            }
            Process.Start("explorer.exe", $"/select,{path}");            
        }

        private void labelName_MouseEnter(object sender, EventArgs e)
        {
            this.labelName.ForeColor = Color.FromArgb(0, 0, 240);
        }

        private void labelName_MouseLeave(object sender, EventArgs e)
        {
            this.labelName.ForeColor = Color.FromArgb(70, 70, 70);
        }

        private void labelName_Click(object sender, EventArgs e)
        {
            if (this.id == null)
            {
                return;
            }
            var form = new FormConfig(this.id);           
            form.Show(this);
        }
    }
}
