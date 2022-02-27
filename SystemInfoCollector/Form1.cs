using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemInfoCollector
{
    public partial class Form1 : Form
    {
        public string address;
        public int time;
        public Form1(int itime, string iaddress)
        {
            InitializeComponent();
            time = itime;
            address = iaddress;
            
            tbTime.Text = time.ToString();
            tbAddr.Text = address;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbTime.Text, out time))
            {
                address = tbAddr.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Input");
            }
        }
    }
}
