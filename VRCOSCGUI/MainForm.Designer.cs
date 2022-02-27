namespace VRCOSCGUI
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClearConsole = new System.Windows.Forms.Button();
            this.OSCLocalIP = new OSCOperatorSpace.OSCOperator();
            this.OSCRemoteIP = new OSCOperatorSpace.OSCOperator();
            this.tcConsole = new VRCOSCGUI.TextConsole();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pluginsToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(672, 25);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // pluginsToolStripMenuItem
            // 
            this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
            this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.pluginsToolStripMenuItem.Text = "Plugins";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Local";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Remote";
            // 
            // btnClearConsole
            // 
            this.btnClearConsole.Location = new System.Drawing.Point(584, 184);
            this.btnClearConsole.Name = "btnClearConsole";
            this.btnClearConsole.Size = new System.Drawing.Size(75, 23);
            this.btnClearConsole.TabIndex = 6;
            this.btnClearConsole.Text = "Clear";
            this.btnClearConsole.UseVisualStyleBackColor = true;
            this.btnClearConsole.Click += new System.EventHandler(this.btnClearConsole_Click);
            // 
            // OSCLocalIP
            // 
            this.OSCLocalIP.IPAddr = "127.0.0.1";
            this.OSCLocalIP.Location = new System.Drawing.Point(14, 44);
            this.OSCLocalIP.Name = "OSCLocalIP";
            this.OSCLocalIP.Port = 8989;
            this.OSCLocalIP.Size = new System.Drawing.Size(348, 70);
            this.OSCLocalIP.TabIndex = 2;
            this.OSCLocalIP.OnIpSet += new OSCOperatorSpace.IpSet(this.OSCLocalIP_OnIpSet);
            // 
            // OSCRemoteIP
            // 
            this.OSCRemoteIP.IPAddr = "127.0.0.1";
            this.OSCRemoteIP.Location = new System.Drawing.Point(12, 120);
            this.OSCRemoteIP.Name = "OSCRemoteIP";
            this.OSCRemoteIP.Port = 9000;
            this.OSCRemoteIP.Size = new System.Drawing.Size(348, 68);
            this.OSCRemoteIP.TabIndex = 1;
            this.OSCRemoteIP.OnIpSet += new OSCOperatorSpace.IpSet(this.OSCRemoteIP_OnIpSet);
            // 
            // tcConsole
            // 
            this.tcConsole.Location = new System.Drawing.Point(12, 213);
            this.tcConsole.Multiline = true;
            this.tcConsole.Name = "tcConsole";
            this.tcConsole.ReadOnly = true;
            this.tcConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tcConsole.Size = new System.Drawing.Size(648, 178);
            this.tcConsole.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 403);
            this.Controls.Add(this.btnClearConsole);
            this.Controls.Add(this.tcConsole);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OSCLocalIP);
            this.Controls.Add(this.OSCRemoteIP);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem pluginsToolStripMenuItem;
        private OSCOperatorSpace.OSCOperator OSCRemoteIP;
        private OSCOperatorSpace.OSCOperator OSCLocalIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private TextConsole tcConsole;
        private System.Windows.Forms.Button btnClearConsole;
    }
}

