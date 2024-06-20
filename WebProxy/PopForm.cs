using System;
using System.Drawing;
using System.Windows.Forms;

namespace Web_Proxy
{
    public partial class PopForm : FormBase
    {
        private FormBG formBG;

        public PopForm()
        {
            InitializeComponent();
        }

        private void PopForm_Load(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {                
                if (formBG == null || formBG.IsDisposed)
                {
                    formBG = new FormBG();
                }
                formBG.DoubleClick -= new EventHandler(formBG_DoubleClick);
                formBG.DoubleClick += new EventHandler(formBG_DoubleClick);
                formBG.Show();
                formBG.Size = this.Owner.Size;
                formBG.Location = this.Owner.PointToScreen(new Point(0, 0));
                formBG.BringToFront();
                formBG.Owner = this.Owner;
                this.Owner = formBG;
            }
        }

        #region --窗体关闭
        private void PopForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.formBG != null)
            {
                this.formBG.Dispose();
                this.formBG = null;                
            }           
        }
        #endregion  

        #region --双击关闭窗体
        private void formBG_DoubleClick(object sender, EventArgs e)
        {
            this.formBG.Close();
        }
        #endregion
    }
}
