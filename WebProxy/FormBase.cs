using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Web_Proxy
{
    public partial class FormBase : Form
    {
        public delegate void Form_Closed();
        public Form_Closed FormClosedEvent;

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);


        public FormBase()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.labelTitle.Text = value;
            }
        }

        #region --关闭操作
        private void imgClose_Click(object sender, EventArgs e)
        {
            this.Close();
            if (FormClosedEvent != null)
            {
                FormClosedEvent();
            }
        }

        private void imgClose_MouseEnter(object sender, EventArgs e)
        {
            this.imgClose.BackColor = Color.Red;
        }

        private void imgClose_MouseLeave(object sender, EventArgs e)
        {
            this.imgClose.BackColor = Color.Transparent;
        }
        #endregion

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
    }
}
