using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRCOSCGUI.Interface
{
    public delegate void OSCSendRequest(string addr, string data, Type t);
    internal interface IOSCPlugin
    {
        //插件自述
        string WhoAmI();
        //插件入口，分配线程
        void Action(object sender, EventArgs e);
        //插件设置，可选
        void Settings(object sender, EventArgs e);

        //定义事件
        event OSCSendRequest sendOSCRequest;
    }
}
