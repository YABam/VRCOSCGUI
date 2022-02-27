# VRCOSCGUI
This is a OSCSender for VRChat based on Plugins. By using interface can add more plugins and send OSC by the holder application. Each plugin don't need to manage OSC by itself.

# Plugin Interface 

        PluginInfo WhoAmI();
        //Main Thread for the plugin. Holder will give a single run this method.
        void Action();
        //Settings about the plugin
        void Settings(object sender, EventArgs e);
        //event on holder status changed
        void OnHolderStatusChange(HolderStatus hs);

        //events
        //request print on console
        event PluginConsoleRequest ConsolePrint;
        //request osc sender
        event OSCSendRequest SendOSCRequest;
