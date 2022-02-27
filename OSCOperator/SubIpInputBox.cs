using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace OSCOperatorSpace
{
    public class SubIpInputBox : TextBox
    {
        private bool _isSendKey = false;
        //private System.ComponentModel.IContainer components;
        private int _flag = 0;

        public int Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

        /// <summary> 
        ///  
        /// </summary> 
        public event FallBackEvent TextFallBackEvent;


        //定义事件函数 
        public void FallBackEventFun(int flag)
        {
            if (TextFallBackEvent != null)
            {
                TextFallBackEvent(box, this.Flag);
            }
        }

        /// <summary> 
        /// 构造函数 
        /// </summary> 
        public SubIpInputBox()
        {
            box = new IpInputBox();
            this.Font = new System.Drawing.Font(this.Font.Name, 11);
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;//去掉边框 
            this.TextAlign = HorizontalAlignment.Center;//字体居中 
            this.Size = new System.Drawing.Size(30, 25);
            this.MaxLength = 3;
        }

        public SubIpInputBox(string str)
        {
            this.Text = str;
            this.Size = new System.Drawing.Size(30, 25);
            this.MaxLength = 3;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;//去掉边框 
            this.TextAlign = HorizontalAlignment.Center;//字体居中 
        }

        private IpInputBox box;
        public IpInputBox Box
        {
            get { return box; }
            set { box = value; }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);


            if (this.Text == "")
            {
                if (e.KeyCode.ToString() == "Back")
                {
                    this.TextFallBackEvent += new FallBackEvent(box.FallBackEventFun);
                    this.FallBackEventFun(this.Flag);
                }
            }
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            //阻止从键盘输入键 
            e.Handled = true;


            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == (char)8))
            {

                if ((e.KeyChar == (char)8))
                {
                    e.Handled = false; return;
                }
                else
                {
                    int len = this.Text.Length;
                    if (len < 4)
                    {
                        if (len == 0 && e.KeyChar != '0')
                        {
                            e.Handled = false; return;
                        }
                        else if (len == 0)
                        {
                            if (this.Flag == 1)
                            {
                                return;
                            }
                        }
                        e.Handled = false; return;
                    }
                    else
                    {
                        // MessageBox.Show("编号最多只能输入3位数字！"); 
                    }
                }
            }
            else if (e.KeyChar == '.')
            {
                //MessageBox.Show("编号只能输入数字！"); 
                if (this.Text.Length != 0)
                {
                    // 如果输入 . 就执行 TAB 键  
                    SendKeys.SendWait("{TAB}");
                }
            }
            else if (this._isSendKey)
            {
                this.SelectAll();
            }

        }


        protected override void OnTextChanged(EventArgs e)
        {

            try
            {
                string currentStr = this.Text;
                int currentNumber = Convert.ToInt32(currentStr);
                this.Text = currentNumber.ToString();
                this.SelectionStart = currentStr.Length;//设置光标在末尾 

                if (currentNumber > 255)
                {
                    MessageBox.Show("你输入的" + currentStr + "不是有效数值，请指定一个介于0到255之间的数值!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    this.Text = "255";

                    _isSendKey = true;
                    this.Focus();
                    this.SelectionStart = currentStr.Length;//设置光标在末尾 
                    this.SelectAll();
                }
                else
                {
                    if (currentStr.Length == 3 && _isSendKey == false)
                    {// 当输入的字符个数为3时，跳入另外一个输入框 
                     //MessageBox.Show("输入完毕"); 
                        if (currentNumber == 0)
                        {
                            this.Text = "";
                            MessageBox.Show("000");
                        }
                        //SendKeys.SendWait("{TAB}");
                    }
                }
            }
            catch
            {
                // 异常处理 
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //  
            // SubIpInputBox 
            //  
            this.TabIndexChanged += new System.EventHandler(this.SubTextBox_TabIndexChanged);
            this.ResumeLayout(false);
        }
        private void SubTextBox_TabIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

