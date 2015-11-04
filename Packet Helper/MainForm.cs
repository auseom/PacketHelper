using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpPcap;

namespace Packet_Helper
{
    public partial class MainForm : Form
    {
        public static List<ICaptureDevice> deviceList;
        private static ICaptureDevice device;
        CapturePacket capturePacket;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            listDevicesToComboBox();
            capturePacket = new CapturePacket(this);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CapturePacket.BackgroundThreadStop)
            {
                capturePacket.device.StopCapture();
                CapturePacket.BackgroundThreadStop = true;
                capturePacket.device.Close();
            }
        }

        private void comboBox_DevList_SelectedIndexChanged(object sender, EventArgs e)
        {
            device = deviceList[comboBox_DevList.SelectedIndex];
            capturePacket.listingPackets(device);
        }

        private void button_CaptureRestart_Click(object sender, EventArgs e)
        {
            capturePacket.restartCapture();
        }

        private void button_CaptureStop_Click(object sender, EventArgs e)
        {
            capturePacket.stopCapture();
        }

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
    }
}