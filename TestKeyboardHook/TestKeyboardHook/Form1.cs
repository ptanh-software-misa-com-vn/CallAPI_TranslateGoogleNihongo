using Anh.Translate;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anh.KeyboardHook
{
    public partial class Form1 : Form
    {
        ActionF1 ActionF1;
        public Form1()
        {
            ActionF1 = new ActionF1();
            InterceptKeys.MyHook += ObserverHook;
            InitializeComponent();
        }

        private async void ObserverHook(InterceptKeys.KeysData m, EventArgs e)
        {
            string text = CopyTextFromFocusedControl();
            if (!string.IsNullOrEmpty(text))
            {
                //await Task.Run(() => { });
                label1.Text = text;
                var jj = await ActionF1.GetSingle(text);
                if (jj == null)
                {
                    return;
                }
                JToken j_0 = jj[0];
                if (j_0 == null)
                {
                    return;
                }
                int iLen = j_0.Count();
                JToken spell = j_0[iLen - 1];
                if (j_0[0] == null || j_0[0][0] == null)
                {
                    if (spell == null || spell[3] == null)
                    {
                        return;
                    }
                    label1.Text = label1.Text + Environment.NewLine + spell[3].ToString();
                }
                else
                {
                    if (spell == null || spell[3] == null)
                    {
                        return;
                    }
                    label1.Text = label1.Text + Environment.NewLine + j_0[0][0].ToString() + Environment.NewLine + spell[3].ToString();
                }

                this.WindowState = FormWindowState.Minimized;
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
           
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        //Get the text of the focused control
        private  string CopyTextFromFocusedControl()
        {
            return new TextSelectionReader().TryGetSelectedTextFromActiveControl();
        }
    }
}
