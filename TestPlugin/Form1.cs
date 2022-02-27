using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPlugin
{
    public partial class Form1 : Form
    {
        public int start;
        public int end;
        public int time;
        public string addr;
        public Form1(int istart, int iend, int itime, string iaddr)
        {
            InitializeComponent();
            start = istart;
            end = iend;
            time = itime;
            addr = iaddr;

            tbStart.Text = istart.ToString();
            tbEnd.Text = iend.ToString();
            tbSleepTime.Text = itime.ToString();
            tbAddress.Text = iaddr;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbStart.Text, out start) &&
            int.TryParse(tbEnd.Text, out end) &&
            int.TryParse(tbSleepTime.Text, out time))
            {
                addr = tbAddress.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Input");
            }
        }
    }
}
