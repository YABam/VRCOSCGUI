using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace OSCOperatorSpace
{
    public class IpInputBox : TextBox
    {
        private SubIpInputBox _box1 = new SubIpInputBox("");
        private SubIpInputBox _box2 = new SubIpInputBox("");
        private SubIpInputBox _box3 = new SubIpInputBox("");
        private SubIpInputBox _box4 = new SubIpInputBox("");

        private Label _dotLabel1 = new Label();
        private Label _dotLabel2 = new Label();
        private Label _dotLabel3 = new Label();

        private string ipAddress = string.Empty;

        public IpInputBox()
        {
            this.Size = new System.Drawing.Size(150, 25);

            _dotLabel1.Text = ".";
            _dotLabel2.Text = ".";
            _dotLabel3.Text = ".";

            _dotLabel1.Size = new System.Drawing.Size(10, 25);
            _dotLabel2.Size = new System.Drawing.Size(10, 25);
            _dotLabel3.Size = new System.Drawing.Size(10, 25);

            _box1.Flag = 1;
            _box2.Flag = 2;
            _box3.Flag = 3;
            _box4.Flag = 4;


            this.Controls.Add(_box1);
            this.Controls.Add(_dotLabel1);


            this.Controls.Add(_box2);
            this.Controls.Add(_dotLabel2);


            this.Controls.Add(_box3);
            this.Controls.Add(_dotLabel3);

            this.Controls.Add(_box4);

            this.Font = new System.Drawing.Font(this.Font.Name, 11);
            _box1.Location = new System.Drawing.Point(-1, 2);
            _dotLabel1.Location = new System.Drawing.Point(29, 2);
            _box2.Location = new System.Drawing.Point(39, 2);
            _dotLabel2.Location = new System.Drawing.Point(69, 2);
            _box3.Location = new System.Drawing.Point(79, 2);
            _dotLabel3.Location = new System.Drawing.Point(109, 2);
            _box4.Location = new System.Drawing.Point(119, 2);

            _box1.Box = this;
            _box2.Box = this;
            _box3.Box = this;
            _box4.Box = this;

            this.EnabledChanged += m_OnEnabledChanged;
        }

        private void m_OnEnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled == true)
            {
                _dotLabel1.Enabled = true;
                _dotLabel1.BackColor = SystemColors.Window;
                _dotLabel2.Enabled = true;
                _dotLabel2.BackColor = SystemColors.Window;
                _dotLabel3.Enabled = true;
                _dotLabel3.BackColor = SystemColors.Window;
            }
            else
            {
                _dotLabel1.Enabled = false;
                _dotLabel1.BackColor = SystemColors.Control;
                _dotLabel2.Enabled = false;
                _dotLabel2.BackColor = SystemColors.Control;
                _dotLabel3.Enabled = false;
                _dotLabel3.BackColor = SystemColors.Control;
            }
        }

        public void FallBackEventFun(IpInputBox box, int flag)
        {
            switch (flag)
            {
                case 1:
                    _box1.Focus();
                    break;
                case 2:
                    _box1.Focus();
                    break;
                case 3:
                    _box2.Focus(); ;
                    break;
                case 4:
                    _box3.Focus(); ;
                    break;
            }
        }

        private string _ipAddress = string.Empty;
        public void UpdateIpaddress()
        {
            try
            {
                string[] sArray = ipAddress.Split(new char[] { '.' });
                _box1.Text = sArray[0];
                _box2.Text = sArray[1];
                _box3.Text = sArray[2];
                _box4.Text = sArray[3];
            }
            catch (Exception exp)
            {
                MessageBox.Show("更新字符串失败:" + exp.Message);
            }
        }

        public void UpdateIpaddress(string inIp)
        {
            ipAddress = inIp;
            UpdateIpaddress();
        }

        /// <summary> 
        /// 获取ip地址 
        /// </summary> 
        public string IpAddressString
        {
            get
            {
                string _ipStr1 = _box1.Text;
                string _ipStr2 = _box2.Text;
                string _ipStr3 = _box3.Text;
                string _ipStr4 = _box4.Text;
                string _ipDotStr = ".";
                _ipAddress = _ipStr1 + _ipDotStr + _ipStr2 + _ipDotStr + _ipStr3 + _ipDotStr + _ipStr4;
                return _ipAddress;
            }
            set
            {
                _ipAddress = value;
            }
        }
    }
}
