using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VRCOSCGUI.Interface;

namespace SystemInfoCollector
{
    public class Collector : IOSCPlugin
    {
        public event PluginConsoleRequest ConsolePrint;
        public event OSCSendRequest SendOSCRequest;

        Form1 settings;

        int time = 2;
        string addresss = "/avatar/parameters/CPULoad";

        bool connection = false;

        float data;

        public void Action()
        {
            //[DllImport(@("DownloadPlaintext.dll", EntryPoint = "DownloadPlaintext"))]
            MSIABVisitor afterburner = new MSIABVisitor();
            
            while (true)
            {       
                if (connection)
                {
                    List<ABReportDataGroup> abReport = new List<ABReportDataGroup>(afterburner.GetReportArray());
                    foreach (ABReportDataGroup abReportDataGroup in abReport)
                    {
                        if (abReportDataGroup.dataName == "CPU usage")
                        {
                            if (float.TryParse(abReportDataGroup.dataValue, out data))
                            {
                                if (data <= 10) data = 10;
                                if (data >= 25) data = 25;
                                data = (data - 10) / 15; // 0~1
                                SendOSCRequest(addresss, data.ToString("0.0"), typeof(float));
                                ConsolePrint("CPU usage OSC sent!");        
                                break;
                            }
                        }
                    }
                }
                Thread.Sleep(time * 1000);
            }
        }

        public void OnHolderStatusChange(HolderStatus hs)
        {
            connection = hs.UdpEstablished;
        }

        public void Settings(object sender, EventArgs e)
        {
            settings = new Form1(time,addresss);
            settings.Show();                        
            settings.FormClosing += FinishSetting;
        }

        private void FinishSetting(object sender, FormClosingEventArgs e)
        {
            time = settings.time;
            addresss = settings.address;
        }

        public PluginInfo WhoAmI()
        {
            return new PluginInfo(
                "SystemInfoCollector",
                "1.0.0",
                "This is used to collect system info from MSI Afterburner");
        }

        public void OnHolderOSCReceived(string addr, string data, Type t)
        {
            //Do Nothing
            //ConsolePrint?.Invoke(t.ToString() + " OSC Received by Collector at " + addr + ", " + data);
        }
    }
}
