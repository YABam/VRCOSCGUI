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
        public List<MSIData> data;
        public int time;
        public Form1(int itime, List<MSIData> idata)
        {
            InitializeComponent();
            time = itime;
            data = idata;
            
            tbTime.Text = time.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbTime.Text, out time))
            {
                if (dgv_OSCTarget.Rows.Count - 1 > 0)
                {
                    data.Clear();
                    for (int i = 0; i < dgv_OSCTarget.Rows.Count - 1; i++)
                    {
                        data.Add(new MSIData(
                            dgv_OSCTarget.Rows[i].Cells[0].Value.ToString(),
                            dgv_OSCTarget.Rows[i].Cells[1].Value.ToString(),
                            null,
                            dgv_OSCTarget.Rows[i].Cells[2].Value.ToString(),
                            dgv_OSCTarget.Rows[i].Cells[3].Value.ToString()));
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Input");
            }
        }

        private void dgv_OSCTarget_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgv_OSCTarget.Rows[dgv_OSCTarget.Rows.Count - 2].Cells[1].Value = "/avatar/parameters/";
        }
    }
}
