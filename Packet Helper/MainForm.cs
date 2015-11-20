/* Packet Helper
 * 사용자의 민감한 정보를 미리 프로그램에 등록시켜놓은 뒤,
 * 전달하는 패킷에 해당 민감한 정보가 있을 시 사용자에게 알림
 * 패킷 캡쳐 라이브러리: http://www.codeproject.com/Articles/12458/SharpPcap-A-Packet-Capture-Framework-for-NET
 * icon 이미지 출처: http://www.flaticon.com/free-icon/antenna_71311
 * DLL 생성 관련 참고 자료 1: http://liesm.tistory.com/entry/C-Exe-%ED%8C%8C%EC%9D%BC%EC%97%90-DllOcx-%ED%8C%8C%EC%9D%BC-%ED%8F%AC%ED%95%A8%ED%95%98%EC%97%AC-%EC%BB%B4%ED%8C%8C%EC%9D%BC%ED%95%98%EA%B8%B0
 * DLL 생성 관련 참고 자료 2: http://devilchen.tistory.com/4720
 **/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using SharpPcap;

namespace Packet_Helper
{
    public partial class MainForm : Form
    {
        private static ICaptureDevice device;
        private CapturePacket capturePacket;
        private bool existPacketDotNetDll;
        private bool existShapPcapDll;

        public static List<ICaptureDevice> deviceList;
        public List<string> sensitiveDataList;
        public List<string> sensitiveDataListWithoutHide;

        public string hideSignal = ", HiDe";


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\WinPcapInst");
            if (reg.ValueCount == 0)
            {
                MessageBox.Show("Install WinPcap First!");
                appExit();
            }

            existPacketDotNetDll = true;
            existShapPcapDll = true;

            if (!createDLLFiles())
            {
                MessageBox.Show("Creating DLL Files Error!");
                appExit();
            }
            if (!existPacketDotNetDll || !existShapPcapDll)
            {
                System.Diagnostics.Process.Start(Application.ExecutablePath);
                appExit();
            }

            listDevicesToComboBox();
            capturePacket = new CapturePacket(this);
            sensitiveDataList = new List<string>();
            sensitiveDataListWithoutHide = new List<string>();
            toolStripMenuItem_tray_activate.Enabled = false;

            saveFileDialog_saveUserData.InitialDirectory = Application.StartupPath;
            saveFileDialog_saveUserData.FileName = "";
            saveFileDialog_saveUserData.Filter = "dat(*.dat)|*.dat";

            openFileDialog_openUserData.InitialDirectory = Application.StartupPath;
            openFileDialog_openUserData.FileName = "";
            openFileDialog_openUserData.Filter = "dat(*.dat)|*.dat";

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
            this.Visible = true;
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            this.Activate();
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
            if (openFileDialog_openUserData.ShowDialog() == DialogResult.OK)
            {
                sensitiveDataList.Clear();
                sensitiveDataListWithoutHide.Clear();

                var fullPath = openFileDialog_openUserData.FileName;

                var fs = new FileStream(fullPath, FileMode.Open);
                var bf = new BinaryFormatter();
                sensitiveDataListWithoutHide = (List<string>)bf.Deserialize(fs);

                fs.Close();

                foreach (var data in sensitiveDataListWithoutHide)
                {
                    if (data.Contains(hideSignal))
                        sensitiveDataList.Add(removeHideSignal(data));
                    else
                        sensitiveDataList.Add(data);
                }

                refreshSensitiveDataListView();
            }
        }

        private void toolStripMenuItem_Save_Click(object sender, EventArgs e)
        {
            if (saveFileDialog_saveUserData.ShowDialog() == DialogResult.OK)
            {
                var fullPath = saveFileDialog_saveUserData.FileName;

                var fs = new FileStream(fullPath, FileMode.Create);
                var bf = new BinaryFormatter();
                bf.Serialize(fs, sensitiveDataListWithoutHide);

                fs.Close();
            }
        }

        private void toolStripMenuItem_Close_Click(object sender, EventArgs e)
        {
            appExit();
        }

        private void toolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            About aboutForm = new About();
            var aboutFormThread = new Thread(delegate ()
            {
                aboutForm.ShowDialog();
            });
            aboutFormThread.Start();
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
            registerSensitiveData registerSDataForm = new registerSensitiveData(this);
            var registerSDataFormThread = new Thread(delegate ()
            {
                registerSDataForm.ShowDialog();

                refreshSensitiveDataListView();
            });
            registerSDataFormThread.Start();
        }

        private void button_deleteSData_Click(object sender, EventArgs e)
        {
            if (listView_sensitiveData.SelectedIndices.Count == 0)
                return;

            var removeDataIndex = listView_sensitiveData.SelectedItems[0].Index;

            sensitiveDataList.RemoveAt(removeDataIndex);
            sensitiveDataListWithoutHide.RemoveAt(removeDataIndex);

            listView_sensitiveData.Items.Clear();

            refreshSensitiveDataListView();
        }

        /* ListView Events */
        private void listView_PacketActivity_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = listView_PacketActivity.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void listView_sensitiveData_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = listView_PacketActivity.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        /* User Define Methods */
        private bool createDLLFiles()
        {
            try
            {
                var _assembly = Assembly.GetExecutingAssembly();
                var resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();

                var _dllPacketDotNetStream = _assembly.GetManifestResourceStream(resourceNames[4]);
                var _dllSharpPcapStream = _assembly.GetManifestResourceStream(resourceNames[5]);

                var strPacketDotNetPath = Application.ExecutablePath.Replace("/", "\\");
                var strSharpPcapPath = Application.ExecutablePath.Replace("/", "\\");

                var intPos = strPacketDotNetPath.LastIndexOf("\\");
                if (intPos >= 1) strPacketDotNetPath = strPacketDotNetPath.Substring(0, intPos).Trim('\\');
                intPos = strSharpPcapPath.LastIndexOf("\\");
                if (intPos >= 1) strSharpPcapPath = strSharpPcapPath.Substring(0, intPos).Trim('\\');

                strPacketDotNetPath += "\\PacketDotNet.dll";
                strSharpPcapPath += "\\SharpPcap.dll";

                var fileInfoPacketDotNet = new FileInfo(strPacketDotNetPath);
                var fileInfoSharpPcap = new FileInfo(strSharpPcapPath);

                if (fileInfoPacketDotNet.Exists == false)
                {
                    var PacketDotNetBytes = new byte[_dllPacketDotNetStream.Length];

                    using (var ms = new MemoryStream())
                    {
                        int read;
                        while ((read = _dllPacketDotNetStream.Read(PacketDotNetBytes, 0, PacketDotNetBytes.Length)) > 0)
                            ms.Write(PacketDotNetBytes, 0, read);
                    }

                    var fileStream = new FileStream(fileInfoPacketDotNet.FullName, FileMode.CreateNew);
                    fileStream.Write(PacketDotNetBytes, 0, PacketDotNetBytes.Length);
                    fileStream.Close();

                    existPacketDotNetDll = false;
                }
                if (fileInfoSharpPcap.Exists == false)
                {
                    var SharpPcapBytes = new byte[_dllSharpPcapStream.Length];

                    using (var ms = new MemoryStream())
                    {
                        int read;
                        while ((read = _dllSharpPcapStream.Read(SharpPcapBytes, 0, SharpPcapBytes.Length)) > 0)
                            ms.Write(SharpPcapBytes, 0, read);
                    }

                    var fileStream = new FileStream(fileInfoSharpPcap.FullName, FileMode.CreateNew);
                    fileStream.Write(SharpPcapBytes, 0, SharpPcapBytes.Length);
                    fileStream.Close();

                    existShapPcapDll = false;
                }
                return true;
            }
            catch
            {
                return false;
            }
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

        public string removeHideSignal(string originalString)
        {
            string[] separatorArr = { hideSignal };

            if (originalString.Contains(hideSignal))
            {
                var index = originalString.IndexOf(',');
                if (originalString[index + 1].Equals(' '))
                {
                    var tempStringArr = originalString.Split(separatorArr, StringSplitOptions.None);
                    var removedHideSignalString = tempStringArr[0];

                    return removedHideSignalString;
                }
            }

            return originalString;
        }

        public bool containsHideSignal(string originalString)
        {
            if (originalString.Contains(hideSignal))
                return true;
            else
                return false;
        }

        public void refreshSensitiveDataListView()
        {
            var curSensitiveDataListCount = sensitiveDataList.Count;
            var asteriskString = string.Empty;
            var asteriskCount = 0;

            listView_sensitiveData.Items.Clear();

            for (int i = 0; i < sensitiveDataList.Count; i++)
            {
                ListViewItem newItem = new ListViewItem((i + 1).ToString());
                var sensitiveDataContent = sensitiveDataListWithoutHide[i];
                if (containsHideSignal(sensitiveDataContent))
                {
                    asteriskCount = sensitiveDataList[i].Length;
                }

                if (asteriskCount == 0)
                    newItem.SubItems.Add(sensitiveDataList[i]);
                else
                {
                    for (int j = 0; j < asteriskCount; j++)
                        asteriskString += '*';
                    newItem.SubItems.Add(asteriskString);

                    asteriskString = string.Empty;
                    asteriskCount = 0;
                }

                listView_sensitiveData.Items.Add(newItem);
            }
        }
    }
}