using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Packet_Helper
{
    public partial class registerSensitiveData : Form
    {
        MainForm mainForm;
        private List<string> tempSensitiveDataList;

        public registerSensitiveData(MainForm main)
        {
            InitializeComponent();

            mainForm = main;
            tempSensitiveDataList = new List<string>();
        }

        private void button_register_Click(object sender, EventArgs e)
        {
            tempSensitiveDataList.Add(textBox_sensitiveData.Text);
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            mainForm.sensitiveDataList.AddRange(tempSensitiveDataList);

            this.Close();
        }
    }
}
