using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace OSCOperatorSpace
{
    public delegate void FallBackEvent(IpInputBox box, int flag);
    public delegate void IpSet(object sender, bool e);
    
    public partial class OSCOperator : UserControl
    {
        IPAddress _ipAddress;
        IPEndPoint _iPEndPoint;
        int _port;

        bool _ipSet = false;

        string _ipAddr;
        public string IPAddr
        {
            get => _ipAddr;
            set => _ipAddr = value;
        }
        public int Port
        { 
            get { return _port; }
            set { _port = value; }
        }

        public IPEndPoint EndPoint
        {
            get
            {
                if (_ipSet == true)
                {
                    return _iPEndPoint;
                }
                else
                {
                    return null;
                }
            }
        }

        public event IpSet OnIpSet;

        public OSCOperator()
        {
            InitializeComponent();
        }

        private void OSCOperator_Load(object sender, EventArgs e)
        {
            if (_ipAddr == null)
            {
                ipBox.UpdateIpaddress("127.0.0.1");
            }
            else
            {
                ipBox.UpdateIpaddress(_ipAddr);
            }

            tbPort.Text = _port.ToString();
        }

        private void tbPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if ((e.KeyChar >= '0' && e.KeyChar <= '9')|| e.KeyChar == (char) 8)
            {
                e.Handled = false; return;
            }
        }

        private void btnSetIP_Click(object sender, EventArgs e)
        {
            if (_ipSet == false)
            {
                try
                {
                    if (IPAddress.TryParse(ipBox.IpAddressString, out _ipAddress) && int.TryParse(tbPort.Text, out _port))
                    {
                        _iPEndPoint = new IPEndPoint(_ipAddress, _port);
                        _ipAddr = _ipAddress.ToString();
                        _ipSet = true;
                        ipBox.Enabled = false;
                        tbPort.Enabled = false;
                        btnSetIP.Text = "Release";
                        OnIpSet(sender, true);
                    }
                }
                catch
                {
                    MessageBox.Show("IP or Port error");
                }
            }
            else
            {
                _iPEndPoint = null;
                _ipAddr = string.Empty;
                _ipSet = false;
                ipBox.Enabled = true;
                tbPort.Enabled= true;
                btnSetIP.Text = "Set IP";
                OnIpSet(sender, false);
                GC.Collect();
            }       
        }
    }
}
