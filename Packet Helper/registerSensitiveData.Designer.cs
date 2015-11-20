namespace Packet_Helper
{
    partial class registerSensitiveData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(registerSensitiveData));
            this.button_add = new System.Windows.Forms.Button();
            this.textBox_sensitiveData = new System.Windows.Forms.TextBox();
            this.button_closeAndRegister = new System.Windows.Forms.Button();
            this.checkBox_hide = new System.Windows.Forms.CheckBox();
            this.listView_tempSensitiveData = new System.Windows.Forms.ListView();
            this.columnHeader_temp_No = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_temp_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_temp_checkHide = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(12, 199);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(75, 23);
            this.button_add.TabIndex = 0;
            this.button_add.Text = "Add";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // textBox_sensitiveData
            // 
            this.textBox_sensitiveData.Location = new System.Drawing.Point(12, 24);
            this.textBox_sensitiveData.Name = "textBox_sensitiveData";
            this.textBox_sensitiveData.Size = new System.Drawing.Size(260, 21);
            this.textBox_sensitiveData.TabIndex = 1;
            // 
            // button_closeAndRegister
            // 
            this.button_closeAndRegister.Location = new System.Drawing.Point(137, 199);
            this.button_closeAndRegister.Name = "button_closeAndRegister";
            this.button_closeAndRegister.Size = new System.Drawing.Size(135, 23);
            this.button_closeAndRegister.TabIndex = 2;
            this.button_closeAndRegister.Text = "Close and Register";
            this.button_closeAndRegister.UseVisualStyleBackColor = true;
            this.button_closeAndRegister.Click += new System.EventHandler(this.button_closeAndRegister_Click);
            // 
            // checkBox_hide
            // 
            this.checkBox_hide.AutoSize = true;
            this.checkBox_hide.Location = new System.Drawing.Point(177, 51);
            this.checkBox_hide.Name = "checkBox_hide";
            this.checkBox_hide.Size = new System.Drawing.Size(95, 16);
            this.checkBox_hide.TabIndex = 3;
            this.checkBox_hide.Text = "Hide at main";
            this.checkBox_hide.UseVisualStyleBackColor = true;
            // 
            // listView_tempSensitiveData
            // 
            this.listView_tempSensitiveData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_temp_No,
            this.columnHeader_temp_name,
            this.columnHeader_temp_checkHide});
            this.listView_tempSensitiveData.FullRowSelect = true;
            this.listView_tempSensitiveData.Location = new System.Drawing.Point(12, 73);
            this.listView_tempSensitiveData.Name = "listView_tempSensitiveData";
            this.listView_tempSensitiveData.Size = new System.Drawing.Size(260, 120);
            this.listView_tempSensitiveData.TabIndex = 4;
            this.listView_tempSensitiveData.UseCompatibleStateImageBehavior = false;
            this.listView_tempSensitiveData.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_temp_No
            // 
            this.columnHeader_temp_No.Text = "#";
            this.columnHeader_temp_No.Width = 20;
            // 
            // columnHeader_temp_name
            // 
            this.columnHeader_temp_name.Text = "Content";
            this.columnHeader_temp_name.Width = 195;
            // 
            // columnHeader_temp_checkHide
            // 
            this.columnHeader_temp_checkHide.Text = "Hide";
            this.columnHeader_temp_checkHide.Width = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Input your sensitive data";
            // 
            // registerSensitiveData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 234);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView_tempSensitiveData);
            this.Controls.Add(this.checkBox_hide);
            this.Controls.Add(this.button_closeAndRegister);
            this.Controls.Add(this.textBox_sensitiveData);
            this.Controls.Add(this.button_add);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "registerSensitiveData";
            this.Text = "Register Data";
            this.Load += new System.EventHandler(this.registerSensitiveData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.TextBox textBox_sensitiveData;
        private System.Windows.Forms.Button button_closeAndRegister;
        private System.Windows.Forms.CheckBox checkBox_hide;
        private System.Windows.Forms.ListView listView_tempSensitiveData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader_temp_No;
        private System.Windows.Forms.ColumnHeader columnHeader_temp_name;
        private System.Windows.Forms.ColumnHeader columnHeader_temp_checkHide;
    }
}