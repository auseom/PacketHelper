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
            this.button_register = new System.Windows.Forms.Button();
            this.textBox_sensitiveData = new System.Windows.Forms.TextBox();
            this.button_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_register
            // 
            this.button_register.Location = new System.Drawing.Point(12, 138);
            this.button_register.Name = "button_register";
            this.button_register.Size = new System.Drawing.Size(75, 23);
            this.button_register.TabIndex = 0;
            this.button_register.Text = "등록";
            this.button_register.UseVisualStyleBackColor = true;
            this.button_register.Click += new System.EventHandler(this.button_register_Click);
            // 
            // textBox_sensitiveData
            // 
            this.textBox_sensitiveData.Location = new System.Drawing.Point(12, 111);
            this.textBox_sensitiveData.Name = "textBox_sensitiveData";
            this.textBox_sensitiveData.Size = new System.Drawing.Size(260, 21);
            this.textBox_sensitiveData.TabIndex = 1;
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(197, 138);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 2;
            this.button_close.Text = "닫기";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // registerSensitiveData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 173);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.textBox_sensitiveData);
            this.Controls.Add(this.button_register);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "registerSensitiveData";
            this.Text = "Register Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_register;
        private System.Windows.Forms.TextBox textBox_sensitiveData;
        private System.Windows.Forms.Button button_close;
    }
}