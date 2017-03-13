namespace Packet_Helper
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.listView_PacketActivity = new System.Windows.Forms.ListView();
            this.columnHeader_No = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_SrcIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_DestIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Protocol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_SrcPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_DestPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Length = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Info = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Info = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox_DevList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_CaptureRestart = new System.Windows.Forms.Button();
            this.button_CaptureStop = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip_tray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_tray_activate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_tray_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.button_deleteSData = new System.Windows.Forms.Button();
            this.listView_sensitiveData = new System.Windows.Forms.ListView();
            this.columnHeader_sDataList_No = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_sDataList_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label_sensitiveDataList = new System.Windows.Forms.Label();
            this.button_registerSData = new System.Windows.Forms.Button();
            this.openFileDialog_openUserData = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog_saveUserData = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip_tray.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView_PacketActivity
            // 
            this.listView_PacketActivity.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_No,
            this.columnHeader_Time,
            this.columnHeader_SrcIP,
            this.columnHeader_DestIP,
            this.columnHeader_Protocol,
            this.columnHeader_SrcPort,
            this.columnHeader_DestPort,
            this.columnHeader_Length,
            this.columnHeader_Info});
            this.listView_PacketActivity.FullRowSelect = true;
            this.listView_PacketActivity.GridLines = true;
            this.listView_PacketActivity.Location = new System.Drawing.Point(198, 51);
            this.listView_PacketActivity.MultiSelect = false;
            this.listView_PacketActivity.Name = "listView_PacketActivity";
            this.listView_PacketActivity.Size = new System.Drawing.Size(860, 498);
            this.listView_PacketActivity.TabIndex = 0;
            this.listView_PacketActivity.UseCompatibleStateImageBehavior = false;
            this.listView_PacketActivity.View = System.Windows.Forms.View.Details;
            this.listView_PacketActivity.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listView_PacketActivity_ColumnWidthChanging);
            // 
            // columnHeader_No
            // 
            this.columnHeader_No.Text = "No.";
            this.columnHeader_No.Width = 45;
            // 
            // columnHeader_Time
            // 
            this.columnHeader_Time.Text = "DateTime";
            this.columnHeader_Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader_Time.Width = 171;
            // 
            // columnHeader_SrcIP
            // 
            this.columnHeader_SrcIP.Text = "Source IP";
            this.columnHeader_SrcIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader_SrcIP.Width = 120;
            // 
            // columnHeader_DestIP
            // 
            this.columnHeader_DestIP.Text = "Destination IP";
            this.columnHeader_DestIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader_DestIP.Width = 120;
            // 
            // columnHeader_Protocol
            // 
            this.columnHeader_Protocol.Text = "Protocol";
            this.columnHeader_Protocol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader_SrcPort
            // 
            this.columnHeader_SrcPort.Text = "Src Port";
            // 
            // columnHeader_DestPort
            // 
            this.columnHeader_DestPort.Text = "Dst Port";
            // 
            // columnHeader_Length
            // 
            this.columnHeader_Length.Text = "Length";
            // 
            // columnHeader_Info
            // 
            this.columnHeader_Info.Text = "Info";
            this.columnHeader_Info.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader_Info.Width = 160;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Menu,
            this.toolStripMenuItem_Info});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1069, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem_Menu
            // 
            this.toolStripMenuItem_Menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Open,
            this.toolStripMenuItem_Save,
            this.toolStripSeparator2,
            this.toolStripMenuItem_Close});
            this.toolStripMenuItem_Menu.Name = "toolStripMenuItem_Menu";
            this.toolStripMenuItem_Menu.Size = new System.Drawing.Size(50, 20);
            this.toolStripMenuItem_Menu.Text = "Menu";
            // 
            // toolStripMenuItem_Open
            // 
            this.toolStripMenuItem_Open.Name = "toolStripMenuItem_Open";
            this.toolStripMenuItem_Open.Size = new System.Drawing.Size(155, 22);
            this.toolStripMenuItem_Open.Text = "Open User Info";
            this.toolStripMenuItem_Open.Click += new System.EventHandler(this.toolStripMenuItem_Open_Click);
            // 
            // toolStripMenuItem_Save
            // 
            this.toolStripMenuItem_Save.Name = "toolStripMenuItem_Save";
            this.toolStripMenuItem_Save.Size = new System.Drawing.Size(155, 22);
            this.toolStripMenuItem_Save.Text = "Save User Info";
            this.toolStripMenuItem_Save.Click += new System.EventHandler(this.toolStripMenuItem_Save_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(152, 6);
            // 
            // toolStripMenuItem_Close
            // 
            this.toolStripMenuItem_Close.Name = "toolStripMenuItem_Close";
            this.toolStripMenuItem_Close.Size = new System.Drawing.Size(155, 22);
            this.toolStripMenuItem_Close.Text = "Exit";
            this.toolStripMenuItem_Close.Click += new System.EventHandler(this.toolStripMenuItem_Close_Click);
            // 
            // toolStripMenuItem_Info
            // 
            this.toolStripMenuItem_Info.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_About});
            this.toolStripMenuItem_Info.Name = "toolStripMenuItem_Info";
            this.toolStripMenuItem_Info.Size = new System.Drawing.Size(40, 20);
            this.toolStripMenuItem_Info.Text = "Info";
            // 
            // toolStripMenuItem_About
            // 
            this.toolStripMenuItem_About.Name = "toolStripMenuItem_About";
            this.toolStripMenuItem_About.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItem_About.Text = "About Packet Helper";
            this.toolStripMenuItem_About.Click += new System.EventHandler(this.toolStripMenuItem_About_Click);
            // 
            // comboBox_DevList
            // 
            this.comboBox_DevList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_DevList.FormattingEnabled = true;
            this.comboBox_DevList.Location = new System.Drawing.Point(14, 66);
            this.comboBox_DevList.Name = "comboBox_DevList";
            this.comboBox_DevList.Size = new System.Drawing.Size(178, 20);
            this.comboBox_DevList.TabIndex = 2;
            this.comboBox_DevList.SelectedIndexChanged += new System.EventHandler(this.comboBox_DevList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select the Network Device";
            // 
            // button_CaptureRestart
            // 
            this.button_CaptureRestart.Location = new System.Drawing.Point(14, 92);
            this.button_CaptureRestart.Name = "button_CaptureRestart";
            this.button_CaptureRestart.Size = new System.Drawing.Size(75, 23);
            this.button_CaptureRestart.TabIndex = 4;
            this.button_CaptureRestart.Text = "Restart";
            this.button_CaptureRestart.UseVisualStyleBackColor = true;
            this.button_CaptureRestart.Click += new System.EventHandler(this.button_CaptureRestart_Click);
            // 
            // button_CaptureStop
            // 
            this.button_CaptureStop.Location = new System.Drawing.Point(117, 92);
            this.button_CaptureStop.Name = "button_CaptureStop";
            this.button_CaptureStop.Size = new System.Drawing.Size(75, 23);
            this.button_CaptureStop.TabIndex = 5;
            this.button_CaptureStop.Text = "Stop";
            this.button_CaptureStop.UseVisualStyleBackColor = true;
            this.button_CaptureStop.Click += new System.EventHandler(this.button_CaptureStop_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip_tray;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Packet Helper";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // contextMenuStrip_tray
            // 
            this.contextMenuStrip_tray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_tray_activate,
            this.toolStripSeparator1,
            this.toolStripMenuItem_tray_exit});
            this.contextMenuStrip_tray.Name = "contextMenuStrip1";
            this.contextMenuStrip_tray.Size = new System.Drawing.Size(217, 54);
            // 
            // toolStripMenuItem_tray_activate
            // 
            this.toolStripMenuItem_tray_activate.Name = "toolStripMenuItem_tray_activate";
            this.toolStripMenuItem_tray_activate.Size = new System.Drawing.Size(216, 22);
            this.toolStripMenuItem_tray_activate.Text = "Select the Network Device";
            this.toolStripMenuItem_tray_activate.Click += new System.EventHandler(this.toolStripMenuItem_tray_activate_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(213, 6);
            // 
            // toolStripMenuItem_tray_exit
            // 
            this.toolStripMenuItem_tray_exit.Name = "toolStripMenuItem_tray_exit";
            this.toolStripMenuItem_tray_exit.Size = new System.Drawing.Size(216, 22);
            this.toolStripMenuItem_tray_exit.Text = "Exit";
            this.toolStripMenuItem_tray_exit.Click += new System.EventHandler(this.toolStripMenuItem_tray_exit_Click);
            // 
            // button_deleteSData
            // 
            this.button_deleteSData.Location = new System.Drawing.Point(117, 526);
            this.button_deleteSData.Name = "button_deleteSData";
            this.button_deleteSData.Size = new System.Drawing.Size(75, 23);
            this.button_deleteSData.TabIndex = 9;
            this.button_deleteSData.Text = "Delete";
            this.button_deleteSData.UseVisualStyleBackColor = true;
            this.button_deleteSData.Click += new System.EventHandler(this.button_deleteSData_Click);
            // 
            // listView_sensitiveData
            // 
            this.listView_sensitiveData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_sDataList_No,
            this.columnHeader_sDataList_name});
            this.listView_sensitiveData.FullRowSelect = true;
            this.listView_sensitiveData.Location = new System.Drawing.Point(10, 265);
            this.listView_sensitiveData.MultiSelect = false;
            this.listView_sensitiveData.Name = "listView_sensitiveData";
            this.listView_sensitiveData.Size = new System.Drawing.Size(180, 255);
            this.listView_sensitiveData.TabIndex = 10;
            this.listView_sensitiveData.UseCompatibleStateImageBehavior = false;
            this.listView_sensitiveData.View = System.Windows.Forms.View.Details;
            this.listView_sensitiveData.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.listView_sensitiveData_ColumnWidthChanging);
            this.listView_sensitiveData.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.listView_sensitiveData_ItemMouseHover);
            this.listView_sensitiveData.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView_sensitiveData_MouseMove);
            // 
            // columnHeader_sDataList_No
            // 
            this.columnHeader_sDataList_No.Text = "#";
            this.columnHeader_sDataList_No.Width = 20;
            // 
            // columnHeader_sDataList_name
            // 
            this.columnHeader_sDataList_name.Text = "Content";
            this.columnHeader_sDataList_name.Width = 150;
            // 
            // label_sensitiveDataList
            // 
            this.label_sensitiveDataList.AutoSize = true;
            this.label_sensitiveDataList.Location = new System.Drawing.Point(12, 250);
            this.label_sensitiveDataList.Name = "label_sensitiveDataList";
            this.label_sensitiveDataList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_sensitiveDataList.Size = new System.Drawing.Size(109, 12);
            this.label_sensitiveDataList.TabIndex = 11;
            this.label_sensitiveDataList.Text = "Sensitive Data List";
            // 
            // button_registerSData
            // 
            this.button_registerSData.Location = new System.Drawing.Point(10, 526);
            this.button_registerSData.Name = "button_registerSData";
            this.button_registerSData.Size = new System.Drawing.Size(75, 23);
            this.button_registerSData.TabIndex = 12;
            this.button_registerSData.Text = "Register";
            this.button_registerSData.UseVisualStyleBackColor = true;
            this.button_registerSData.Click += new System.EventHandler(this.button_registerSData_Click);
            // 
            // openFileDialog_openUserData
            // 
            this.openFileDialog_openUserData.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 561);
            this.Controls.Add(this.button_registerSData);
            this.Controls.Add(this.label_sensitiveDataList);
            this.Controls.Add(this.listView_sensitiveData);
            this.Controls.Add(this.button_deleteSData);
            this.Controls.Add(this.button_CaptureStop);
            this.Controls.Add(this.button_CaptureRestart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_DevList);
            this.Controls.Add(this.listView_PacketActivity);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Packet Helper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip_tray.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Menu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Save;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Close;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Info;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_About;
        private System.Windows.Forms.ComboBox comboBox_DevList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader_No;
        private System.Windows.Forms.ColumnHeader columnHeader_Time;
        private System.Windows.Forms.ColumnHeader columnHeader_SrcIP;
        private System.Windows.Forms.ColumnHeader columnHeader_DestIP;
        private System.Windows.Forms.ColumnHeader columnHeader_Protocol;
        private System.Windows.Forms.ColumnHeader columnHeader_Length;
        private System.Windows.Forms.ColumnHeader columnHeader_Info;
        public System.Windows.Forms.ListView listView_PacketActivity;
        private System.Windows.Forms.Button button_CaptureRestart;
        private System.Windows.Forms.Button button_CaptureStop;
        private System.Windows.Forms.ColumnHeader columnHeader_SrcPort;
        private System.Windows.Forms.ColumnHeader columnHeader_DestPort;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_tray;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_tray_exit;
        public System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_tray_activate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Open;
        private System.Windows.Forms.Button button_deleteSData;
        private System.Windows.Forms.Label label_sensitiveDataList;
        private System.Windows.Forms.Button button_registerSData;
        public System.Windows.Forms.ListView listView_sensitiveData;
        private System.Windows.Forms.ColumnHeader columnHeader_sDataList_No;
        private System.Windows.Forms.ColumnHeader columnHeader_sDataList_name;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.OpenFileDialog openFileDialog_openUserData;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_saveUserData;
    }
}

