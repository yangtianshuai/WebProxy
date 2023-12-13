using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WebProxy.Plugin;

namespace Web_Proxy
{
    public partial class FormConfig : PopForm
    {
        private string plugin_id;
        public FormConfig(string plugin_id)
        {
            InitializeComponent();
            this.plugin_id = plugin_id;           
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (this.tbText.Text.Length == 0) return;
            try
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(this.tbText.Text);
                if (PluginManager.Current.SetSetting(this.plugin_id, dict))
                {
                    MessageBox.Show("保存成功");
                }
                else
                {
                    MessageBox.Show("保存失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败："+ ex.Message);
            }            
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {         
            var setting = PluginManager.Current.GetSetting(this.plugin_id);
            if (setting == null)
            {
                return;                
            }
            var dict = setting.Get();
            if (dict == null)
            {
                return;
            }
            this.tbText.Text = JsonConvert.SerializeObject(dict);
        }
    }
}
