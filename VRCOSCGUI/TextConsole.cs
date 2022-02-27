using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VRCOSCGUI
{
    internal class TextConsole : TextBox
    {
        public TextConsole()
        { 
            this.ReadOnly = true;
        }

        public void WriteLine(string input)
        {
            this.Text += DateTime.Now.ToString("[HH:mm:ss] ");
            this.Text += input;
            this.Text += "\r\n";
        }

        protected override void OnTextChanged(EventArgs e)
        {
            this.SelectionStart = this.Text.Length;//设置光标在末尾 
            this.ScrollToCaret();
        }

        public void ClearConsole()
        { 
            this.Text = string.Empty;
        }
    }
}
