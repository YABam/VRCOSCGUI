namespace SystemInfoCollector
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.dgv_OSCTarget = new System.Windows.Forms.DataGridView();
            this.Col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_addr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Min = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Max = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_OSCTarget)).BeginInit();
            this.SuspendLayout();
            // 
            // tbTime
            // 
            this.tbTime.Location = new System.Drawing.Point(128, 22);
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(100, 21);
            this.tbTime.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Collect data per ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "second(s)";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(374, 73);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dgv_OSCTarget
            // 
            this.dgv_OSCTarget.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgv_OSCTarget.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_OSCTarget.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_Name,
            this.Col_addr,
            this.Col_Min,
            this.Col_Max});
            this.dgv_OSCTarget.Location = new System.Drawing.Point(12, 102);
            this.dgv_OSCTarget.Name = "dgv_OSCTarget";
            this.dgv_OSCTarget.RowTemplate.Height = 23;
            this.dgv_OSCTarget.Size = new System.Drawing.Size(648, 646);
            this.dgv_OSCTarget.TabIndex = 6;
            this.dgv_OSCTarget.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgv_OSCTarget_RowsAdded);
            // 
            // Col_Name
            // 
            this.Col_Name.HeaderText = "Name";
            this.Col_Name.Name = "Col_Name";
            // 
            // Col_addr
            // 
            this.Col_addr.HeaderText = "Address";
            this.Col_addr.Name = "Col_addr";
            this.Col_addr.Width = 300;
            // 
            // Col_Min
            // 
            this.Col_Min.HeaderText = "Min";
            this.Col_Min.Name = "Col_Min";
            // 
            // Col_Max
            // 
            this.Col_Max.HeaderText = "Max";
            this.Col_Max.Name = "Col_Max";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 757);
            this.Controls.Add(this.dgv_OSCTarget);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_OSCTarget)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridView dgv_OSCTarget;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_addr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Min;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Max;
    }
}