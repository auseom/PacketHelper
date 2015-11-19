using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Packet_Helper
{
    public partial class registerSensitiveData : Form
    {
        MainForm mainForm;
        private List<string> tempSensitiveDataList;
        private int count;

        public registerSensitiveData(MainForm main)
        {
            InitializeComponent();

            mainForm = main;
        }

        private void registerSensitiveData_Load(object sender, EventArgs e)
        {
            tempSensitiveDataList = new List<string>();
            count = 1;
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (textBox_sensitiveData.Text == string.Empty)
                return;

            var tempSensitiveData = textBox_sensitiveData.Text;

            ListViewItem tempItem = new ListViewItem(count.ToString());
            tempItem.SubItems.Add(tempSensitiveData);

            if (checkBox_hide.Checked)
            {
                tempItem.SubItems.Add("v");
                tempSensitiveData += mainForm.hideSignal;
            }

            listView_tempSensitiveData.Items.Add(tempItem);
            count++;

            tempSensitiveDataList.Add(tempSensitiveData);
            textBox_sensitiveData.Text = string.Empty;
        }

        private void button_closeAndRegister_Click(object sender, EventArgs e)
        {
            if (tempSensitiveDataList.Count == 0)
                this.Close();
            else
            {
                registerRoutine();
                this.Close();
            }
        }

        private void registerRoutine()
        {
            try
            {
                mainForm.sensitiveDataList.AddRange(tempSensitiveDataList);
            }
            catch (Exception _e)
            {
                MessageBox.Show("Error occured!\n" + _e);
            }

            var registerDataResult = string.Empty;
            for (int i = 0; i < tempSensitiveDataList.Count; i++)
            {
                var data = tempSensitiveDataList[i];
                data = mainForm.removeHideSignal(data);

                registerDataResult += data;
                if (i + 1 < tempSensitiveDataList.Count)
                    registerDataResult += ", ";
            }

            MessageBox.Show("Add to Sensitive Data List\n" + registerDataResult);
        }
    }
}