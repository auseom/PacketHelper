﻿using System;
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
            tempSensitiveDataList = new List<string>();
            count = 1;
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            var tempSensitiveData = textBox_sensitiveData.Text;

            ListViewItem tempItem = new ListViewItem(count.ToString());
            tempItem.SubItems.Add(tempSensitiveData);
            listView_tempSensitiveData.Items.Add(tempItem);
            count++;

            if (checkBox_hide.Checked)
                tempSensitiveData += ", HiDe!!";

            tempSensitiveDataList.Add(tempSensitiveData);
            textBox_sensitiveData.Text = string.Empty;
        }

        private void button_closeAndRegister_Click(object sender, EventArgs e)
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

                registerDataResult += data;
                if (i + 1 < tempSensitiveDataList.Count)
                    registerDataResult += ", ";
            }

            MessageBox.Show("Add to Sensitive Data List\n" + registerDataResult);

            this.Close();
        }
    }
}