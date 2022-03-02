using System;
using System.Threading;
using System.Windows.Forms;
using VRCOSCGUI.Interface;

namespace TestPlugin
{
    public class Class1 : IOSCPlugin
    {
        public event PluginConsoleRequest ConsolePrint;
        public event OSCSendRequest SendOSCRequest;

        bool connection = false;

		int start = 0;
		int end = 20;
		int time = 2;
		string address = "/test/testdata";

		Form1 settingsForm;

		public void Action()
        {
			int i = start;
			while (true)
			{
				if (connection == true)
				{
					ConsolePrint?.Invoke("This message is for test! Sending OSC int 0~10 at test/testdata");
					SendOSCRequest(address, i.ToString(), typeof(int));
					Thread.Sleep(time * 1000);
					i++;
					if (i > end)
					{
						i = start;
					}
				}
			}
		}

        public void OnHolderStatusChange(HolderStatus hs)
        {
			connection = hs.UdpEstablished;
        }

        public void Settings(object sender, EventArgs e)
        {
			settingsForm = new Form1(0, 20, 2, "/test/testdata");
			settingsForm.Show();
			settingsForm.FormClosing += SettingFinished;
		}

		private void SettingFinished(object sender, FormClosingEventArgs e)
		{
			start = settingsForm.start;
			end = settingsForm.end;
			time = settingsForm.time;
			address = settingsForm.addr;

		}
		public PluginInfo WhoAmI()
        {
			//PlugInInfo tempInfo = new PlugInInfo();
			return new PluginInfo(
				"testPlugin",
				"1.0.0",
				"This is a test plugin."
				);
		}

        public void OnHolderOSCReceived(string addr, string data, Type t)
        {
			ConsolePrint(t.ToString() + " OSC Received by testPlugin at " + addr + ", " + data);
        }
    }
}