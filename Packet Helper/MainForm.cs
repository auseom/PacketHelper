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
using System.Collections;

namespace Packet_Helper
{
    public partial class MainForm : Form
    {
        public static List<ICaptureDevice> deviceList;
        private static ICaptureDevice device;
        public ArrayList sensitiveDataArr;
        CapturePacket capturePacket;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            listDevicesToComboBox();
            capturePacket = new CapturePacket(this);
            sensitiveDataArr = new ArrayList();
            toolStripMenuItem_tray_activate.Enabled = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.Activate();
        }

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

        private void comboBox_DevList_SelectedIndexChanged(object sender, EventArgs e)
        {
            device = deviceList[comboBox_DevList.SelectedIndex];
            capturePacket.listingPackets(device);

            toolStripMenuItem_tray_activate.Enabled = true;
            notifyIcon1.Text = "Packet Helper: Activated";
            toolStripMenuItem_tray_activate.Text = "Stop";
        }

        private void button_CaptureRestart_Click(object sender, EventArgs e)
        {
            resumeCaptureRoutine();
        }

        private void button_CaptureStop_Click(object sender, EventArgs e)
        {
            stopCaptureRoutine();
        }

        /* Testing for detecting user registered sensitive data */
        private void button_register_Click(object sender, EventArgs e)
        {
            sensitiveDataArr.Add(textBox_registerText.Text);
            /* It will be used in CapturePacket.cs */
        }

        /* Listing what data is registered */
        private void button_sDataList_Click(object sender, EventArgs e)
        {

        }

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

        /*
         * User Defined Methods
         **/
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
                    //"FrindlyName:" 부터 "\n" 사이의 문자열 파싱
                    //제공되는 속성들에는 FriendlyName만 따로 주는 건 없음
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
            toolStripMenuItem_tray_activate.Text = "Resume";
            notifyIcon1.Text = "Packet Helper: Activated";
        }

        private void stopCaptureRoutine()
        {
            capturePacket.stopCapture();
            toolStripMenuItem_tray_activate.Text = "Stop";
            notifyIcon1.Text = "Packet Helper: Deactivated";
        }

        private void appExit()
        {
            if (CapturePacket.BackgroundThreadStop)
            {
                capturePacket.device.StopCapture();
                CapturePacket.BackgroundThreadStop = true;
                capturePacket.device.Close();
                capturePacket.backgroundThread.Abort();
            }

            notifyIcon1.Visible = false;
            Application.ExitThread();
            Environment.Exit(0);
        }
    }
}