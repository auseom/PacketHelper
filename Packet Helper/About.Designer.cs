namespace Packet_Helper
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.textBox_about = new System.Windows.Forms.TextBox();
            this.button_close = new System.Windows.Forms.Button();
            this.label_visitGibhub = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_about
            // 
            this.textBox_about.Location = new System.Drawing.Point(12, 12);
            this.textBox_about.Multiline = true;
            this.textBox_about.Name = "textBox_about";
            this.textBox_about.ReadOnly = true;
            this.textBox_about.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_about.Size = new System.Drawing.Size(260, 208);
            this.textBox_about.TabIndex = 0;
            this.textBox_about.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_close
            // 
            this.button_close.Location = new System.Drawing.Point(197, 236);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 1;
            this.button_close.Text = "Close";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // label_visitGibhub
            // 
            this.label_visitGibhub.AutoSize = true;
            this.label_visitGibhub.Location = new System.Drawing.Point(10, 223);
            this.label_visitGibhub.Name = "label_visitGibhub";
            this.label_visitGibhub.Size = new System.Drawing.Size(65, 12);
            this.label_visitGibhub.TabIndex = 2;
            this.label_visitGibhub.Text = "initial label";
            this.label_visitGibhub.MouseClick += new System.Windows.Forms.MouseEventHandler(this.label_visitGibhub_MouseClick);
            this.label_visitGibhub.MouseLeave += new System.EventHandler(this.label_visitGibhub_MouseLeave);
            this.label_visitGibhub.MouseHover += new System.EventHandler(this.label_visitGibhub_MouseHover);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 271);
            this.Controls.Add(this.label_visitGibhub);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.textBox_about);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "About";
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_about;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Label label_visitGibhub;
    }
}