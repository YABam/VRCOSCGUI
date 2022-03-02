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

        bool connection = false;

        List<MSIData> OSCData;

        public void Action()
        {
            MSIABVisitor afterburner = new MSIABVisitor();
            OSCData = new List<MSIData>();
            while (true)
            {
                if (connection && OSCData != null && OSCData.Count > 0)
                {
                    List<ABReportDataGroup> abReport = new List<ABReportDataGroup>(afterburner.GetReportArray());
                    foreach (ABReportDataGroup abReportDataGroup in abReport)
                    {
                        foreach (MSIData msid in OSCData)
                        {
                            if (abReportDataGroup.dataName == msid.name)
                            {
                                float data;
                                if (float.TryParse(abReportDataGroup.dataValue, out data))
                                {
                                    float dmin;
                                    float dmax;
                                    if (float.TryParse(msid.min, out dmin) && float.TryParse(msid.max, out dmax))
                                    {
                                        if (data <= dmin) data = dmin;
                                        if (data >= dmax) data = dmax;
                                        data = (data - dmin) / (dmax - dmin); // 0~1 
                                        msid.data = data.ToString();
                                    }
                                }
                                break;
                            }
                        }
                    }

                    foreach (MSIData msid in OSCData)
                    {
                        SendOSCRequest(msid.address, msid.data, typeof(float));
                        ConsolePrint(msid.name + " OSC sent to " + msid.address);
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
            settings = new Form1(time, OSCData);
            settings.Show();
            settings.FormClosing += FinishSetting;
        }

        private void FinishSetting(object sender, FormClosingEventArgs e)
        {
            time = settings.time;
            OSCData = settings.data;
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

    public class MSIData
    {
        public string name;
        public string address;
        public string data;
        public string min;
        public string max;

        public MSIData(string iname, string iaddr, string idata, string imin, string imax)
        {
            name = iname;
            address = iaddr;
            data = idata;
            min = imin;
            max = imax;
        }
    }
}
