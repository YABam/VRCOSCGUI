using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VRCOSCGUI.Interface
{
    public delegate void OSCSendRequest(string addr, string data, Type t);
    public delegate void PluginConsoleRequest(string msg);
    public delegate void HolderStatusChange(HolderStatus hs);
    public delegate void HolderOSCReceived(string addr, string data, Type t);
    public interface IOSCPlugin
    {
        //插件自述      
        PluginInfo WhoAmI();
        //插件入口，分配线程
        void Action();
        //插件设置，可选
        void Settings(object sender, EventArgs e);
        //宿主状态改变事件
        void OnHolderStatusChange(HolderStatus hs);
        //收OSC信息
        void OnHolderOSCReceived(string addr, string data, Type t);

        //定义事件
        //写日志
        event PluginConsoleRequest ConsolePrint;
        //发信息
        event OSCSendRequest SendOSCRequest;
    }

    public class PluginInfo
    {
        string _name;
        string _version;
        string _description;

        public string Name { get => _name; }
        public string Version { get => _version; }
        public string Description { get => _description; }

        public PluginInfo(string iName, string iVersion, string iDescription)
        {
            _name = iName;
            _version = iVersion;
            _description = iDescription;
        }
    }

    public class HolderStatus
    {
        IPEndPoint localEndPoint;
        IPEndPoint remoteEndPoint;

        bool _udpEstablished;

        public HolderStatus(IPEndPoint local, IPEndPoint remote, bool _udp)
        {
            localEndPoint = local;
            remoteEndPoint = remote;
            _udpEstablished = _udp;
        }

        public IPEndPoint LocalEndPoint { get => localEndPoint; }
        public IPEndPoint RemoteEndPoint { get => remoteEndPoint; }
        public bool UdpEstablished { get => _udpEstablished; }
    }
}
