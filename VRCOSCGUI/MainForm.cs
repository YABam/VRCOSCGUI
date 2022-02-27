using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;        
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VRCOSCGUI.Interface;
using System.Threading;

namespace VRCOSCGUI
{
    public partial class MainForm : Form
    {
        //List of loaded plugins
        List<IOSCPlugin> _loadedPlugins = new List<IOSCPlugin>();
        //count
        int _countPluginsLoaded = 0;
        int _countPluginsFailed = 0;
        bool remoteIPSet = false;

        event HolderStatusChange OnHolderStatusChange;

        //UDP client
        private UdpClient udpSend = null;

        //Thread List
        List<Thread> _pluginThreads = new List<Thread>();

        public MainForm()
        {
            InitializeComponent();
            //udpSend = new UdpClient(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9001));
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            List<string> list = new List<string>();

            //Scan plugin
            string path = System.IO.Path.Combine(System.Environment.CurrentDirectory + @"\plugins\");
            DirectoryInfo dir = new DirectoryInfo(path);
            //if not exist, create
            if (dir.Exists == false)
            { 
                Directory.CreateDirectory(path);
            }
            FileInfo[] fInfo = dir.GetFiles();
            foreach (FileInfo fi in fInfo)
            {
                if (fi.Extension.Equals(".dll"))
                {
                    list.Add(fi.FullName);
                }
            }

            //Load plugin
            if (list.Count > 0)
            {
                foreach (string s in list)
                {
                    try
                    {
                        IOSCPlugin tempPlugin;
                        Assembly ass = Assembly.LoadFile(s);
                        Type[] pluginType = ass.GetTypes();
                        foreach (Type t in pluginType)
                        {
                            //check interface
                            if (t.GetInterface("IOSCPlugin") != null)
                            {
                                tempPlugin = (IOSCPlugin)Activator.CreateInstance(ass.GetType(t.FullName));
                                _loadedPlugins.Add(tempPlugin);
                                tempPlugin.SendOSCRequest += new OSCSendRequest(HolderOSCSend);
                                tempPlugin.ConsolePrint += new PluginConsoleRequest(HolderConsolePrint);
                                OnHolderStatusChange += new HolderStatusChange(tempPlugin.OnHolderStatusChange);
                                //Add Menu
                                ToolStripMenuItem newItem = new ToolStripMenuItem();
                                newItem.Text = tempPlugin.WhoAmI().Name;
                                newItem.Click += tempPlugin.Settings;
                                ((ToolStripMenuItem)mainMenuStrip.Items[0]).DropDownItems.Add(newItem);
                                //count
                                _countPluginsLoaded++;

                                //Chreate Thread for this Plugin
                                Thread _thread = new Thread(new ThreadStart(tempPlugin.Action));
                                _thread.IsBackground = true;
                                _pluginThreads.Add(_thread);        
                            }
                        }
                    }
                    catch
                    {
                        _countPluginsFailed++;
                    }
                }
            }
            else
            {
                //Menu
                ToolStripMenuItem newItem = new ToolStripMenuItem();
                newItem.Text = "No Plugin";
                newItem.Enabled = false;
                ((ToolStripMenuItem)mainMenuStrip.Items[0]).DropDownItems.Add(newItem);
            }
            //Notice
            MessageBox.Show(_countPluginsLoaded.ToString() + "个插件成功载入，" + _countPluginsFailed.ToString() + "个插件加载失败。");
            tcConsole.WriteLine(_countPluginsLoaded.ToString() + "个插件成功载入，" + _countPluginsFailed.ToString() + "个插件加载失败。");

            for (int i = 0; i < _pluginThreads.Count; i++)
            {
                try
                {
                    _pluginThreads[i].Start();
                    tcConsole.WriteLine(_loadedPlugins[i].WhoAmI().Name + " Start!");
                }
                catch
                {
                    _pluginThreads[i].Abort();
                    _pluginThreads.RemoveAt(i);
                    _loadedPlugins.RemoveAt(i);
                    tcConsole.WriteLine("Some Plugin Start error, they will be aborted");       
                }
            }
        }

        private void HolderConsolePrint(string msg)
        {
            if (tcConsole.InvokeRequired)
            {
                tcConsole.Invoke(new Action(() => tcConsole.WriteLine(msg)));
            }
            else
            {
                tcConsole.WriteLine(msg);
            }
        }

        private void HolderOSCSend(string addr, string data, Type t)
        {
            if (udpSend != null)
            {
                byte[] oscArr;
                if (OSCProtocols.ConvertToOSCArray(addr, data, t, out oscArr))
                {
                    if (OSCRemoteIP.EndPoint != null && remoteIPSet)
                    {
                        udpSend.Send(oscArr, oscArr.Length, OSCRemoteIP.EndPoint);
                    }
                }
            }
        }

        private void OSCLocalIP_OnIpSet(object sender, bool e)
        {
            if (e == true)
            {
                udpSend = new UdpClient(OSCLocalIP.EndPoint);
            }
            else
            {
                //udpSend.Close();
                udpSend.Dispose();
            }
            RefreshStatus();
        }

        private void btnClearConsole_Click(object sender, EventArgs e)
        {
            tcConsole.Clear();
        }

        private void OSCRemoteIP_OnIpSet(object sender, bool e)
        {
            if (e == true)
            {
                remoteIPSet = true;
            }
            else
            { 
                remoteIPSet = false;
            }
            RefreshStatus();
        }

        private void RefreshStatus()
        {
            //Checked valid connection
            bool connection = false;
            if (udpSend != null && OSCLocalIP.EndPoint != null & OSCRemoteIP.EndPoint != null && remoteIPSet == true)
            {
                connection = true;
            }
            OnHolderStatusChange(new HolderStatus(
                OSCLocalIP.EndPoint,
                OSCRemoteIP.EndPoint,
                connection
                ));
        }
    }   
}
