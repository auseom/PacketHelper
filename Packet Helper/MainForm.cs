/* Packet Helper
 * 사용자의 민감한 정보를 미리 프로그램에 등록시켜놓은 뒤,
 * 전달하는 패킷에 해당 민감한 정보가 있을 시 사용자에게 알림
 * 패킷 캡쳐 라이브러리: http://www.codeproject.com/Articles/12458/SharpPcap-A-Packet-Capture-Framework-for-NET
 * icon 이미지 출처: http://www.flaticon.com/free-icon/antenna_71311
 **/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharpPcap;

namespace Packet_Helper
{
    public partial class MainForm : Form
    {
        private static ICaptureDevice device;
        private CapturePacket capturePacket;
        private int count;

        public static List<ICaptureDevice> deviceList;
        public List<string> sensitiveDataList;


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            listDevicesToComboBox();
            capturePacket = new CapturePacket(this);
            sensitiveDataList = new List<string>();
            toolStripMenuItem_tray_activate.Enabled = false;
            count = 1;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void comboBox_DevList_SelectedIndexChanged(object sender, EventArgs e)
        {
            device = deviceList[comboBox_DevList.SelectedIndex];
            capturePacket.listingPackets(device);

            toolStripMenuItem_tray_activate.Enabled = true;
            notifyIcon.Text = "Packet Helper: Activated";
            toolStripMenuItem_tray_activate.Text = "Stop";
        }

        /* Event about tray icon */
        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {

        }

        /* Tray Tool Strip Menu Items */
        private void toolStripMenuItem_tray_activate_Click(object sender, EventArgs e)
        {
            if (!toolStripMenuItem_tray_activate.Enabled)
                return;

            if (toolStripMenuItem_tray_activate.Text == "Resume")
            {
                resumeCaptureRoutine();
            }
            else if (toolStripMenuItem_tray_activate.Text == "Stop")
            {
                stopCaptureRoutine();
            }  
        }

        private void toolStripMenuItem_tray_exit_Click(object sender, EventArgs e)
        {
            appExit();
        }

        /* Tool Strip Menu Items */
        private void toolStripMenuItem_Open_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem_Save_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem_Close_Click(object sender, EventArgs e)
        {
            appExit();
        }

        private void toolStripMenuItem_Man_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem_About_Click(object sender, EventArgs e)
        {

        }

        /* Buttons */
        private void button_CaptureRestart_Click(object sender, EventArgs e)
        {
            resumeCaptureRoutine();
        }

        private void button_CaptureStop_Click(object sender, EventArgs e)
        {
            stopCaptureRoutine();
        }

        private void button_registerSData_Click(object sender, EventArgs e)
        {
            var curListCount = sensitiveDataList.Count;

            registerSensitiveData registerSDataForm = new registerSensitiveData(this);
            registerSDataForm.ShowDialog();

            for (int i = curListCount; i < sensitiveDataList.Count; i++)
            {
                ListViewItem newItem = new ListViewItem(count.ToString());
                newItem.SubItems.Add(sensitiveDataList[i]);

                listView_sensitiveData.Items.Add(newItem);
                count++;
            }
        }

        private void button_deleteSData_Click(object sender, EventArgs e)
        {

        }

        /* User Define Methods */
        private void listDevicesToComboBox()
        {
            deviceList = DeviceList.GetDevices();
            char[] delimiterChars = { ':', '\n', '\t' };
   
            String devInfoTemp;
            String[] devInfoSplitted;
            String devFriendlyName;

            if (deviceList.Count > 0)
            {
                foreach (ICaptureDevice dev in deviceList)
                {
                    //String parsing from "FrindlyName:" to "\n"
                    //There's no property supports only Friendly Name
                    devInfoTemp = dev.ToString();
                    devInfoSplitted = devInfoTemp.Split(delimiterChars);
                    devFriendlyName = devInfoSplitted[5];
                    
                    comboBox_DevList.Items.Add(devFriendlyName);
                }
            }
            else
            {
                comboBox_DevList.Items.Add("No Devices Found");
            }
        }

        private void resumeCaptureRoutine()
        {
            capturePacket.restartCapture();
            toolStripMenuItem_tray_activate.Text = "Stop";
            notifyIcon.Text = "Packet Helper: Activated";
        }

        private void stopCaptureRoutine()
        {
            capturePacket.stopCapture();
            toolStripMenuItem_tray_activate.Text = "Resume";
            notifyIcon.Text = "Packet Helper: Deactivated";
        }

        private void appExit()
        {
            notifyIcon.Visible = false;
            Application.ExitThread();
            Environment.Exit(0);
        }
    }
}