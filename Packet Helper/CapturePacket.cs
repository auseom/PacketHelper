using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using SharpPcap;
using System.Text;

namespace Packet_Helper
{
    class CapturePacket
    {
        private static MainForm mainForm;
        public ICaptureDevice device;

        public Thread backgroundThread;

        public static bool BackgroundThreadStop = false;
        private static object QueueLock = new object();
        private static List<RawCapture> PacketQueue = new List<RawCapture>();
        private static DateTime LastStatisticsOutput = DateTime.Now;
        private static TimeSpan LastStatisticsInterval = new TimeSpan(0, 0, 2);

        private static string detectedDataResult = string.Empty;
        public static int count = 1;
        

        public CapturePacket(MainForm main)
        {
            mainForm = main;
        }

        public void listingPackets(ICaptureDevice dev)
        {
            device = dev;
            int readTimeoutMilliseconds = 1000;

            backgroundThread = new Thread(BackgroundThread);
            backgroundThread.Start();

            device.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrival);
            device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);

            device.StartCapture();
        }

        private static void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            var Now = DateTime.Now;
            var interval = Now - LastStatisticsOutput;
            if (interval > LastStatisticsInterval)
            {
                LastStatisticsOutput = Now;
            }

            lock (QueueLock)
            {
                PacketQueue.Add(e.Packet);
            }
        }

        private static void BackgroundThread()
        {
            while (!BackgroundThreadStop)
            {
                bool shouldSleep = true;

                lock (QueueLock)
                {
                    if (PacketQueue.Count != 0)
                    {
                        shouldSleep = false;
                    }
                }

                if (shouldSleep)
                {
                    Thread.Sleep(250);
                }
                else
                {
                    List<RawCapture> ourQueue;
                    lock (QueueLock)
                    {
                        ourQueue = PacketQueue;
                        PacketQueue = new List<RawCapture>();
                    }

                    foreach (var packet in ourQueue)
                    {
                        /* Simple Packet Composition (Packet.Net) and It will be continually updated...

                        In case of TCP, A TCP packet captured on Ethernet may be "Ethernet packet -> IP packet -> TCP packet"
                        So in Packet.Net the TCP packet could be accessed like capturedPacket(Ethernet).PayloadPacket(IP).PayloadPacket(TCP)
                        The other way, the Ethernet packet could be accessed like tcpPacket.ParentPacket(IP).ParentPacket(Ethernet)

                        Each packet also have a payload part. And It's could be accessed like TcpPacket.PayloadData

                        Each two Hex (8bit) in Hex dump of the packet(e.g 90 9F 33 57 ...) is length 1 (1 byte)
                        */

                        ListViewItem newItem = new ListViewItem(count.ToString());

                        var packetLength = packet.Data.Length;
                        var time = packet.Timeval.Date;
                        var thisPacket = PacketDotNet.Packet.ParsePacket(packet.LinkLayerType, packet.Data);
                        var tcpPacket = (PacketDotNet.TcpPacket)thisPacket.Extract(typeof(PacketDotNet.TcpPacket));

                        /* Check if it contains sensitive data */
                        var containsSensitiveData = detectSensitiveData(packet.Data);

                        /* Show payload */
                        //showHexDump(packet.Data, packetLength);

                        System.Net.IPAddress srcIp = System.Net.IPAddress.None;
                        System.Net.IPAddress dstIp = System.Net.IPAddress.None;
                        int srcPort = 0;
                        int dstPort = 0;
                        PacketDotNet.IPProtocolType protocol = PacketDotNet.IPProtocolType.NONE;

                        if (tcpPacket != null)
                        {
                            var ipPacket = (PacketDotNet.IpPacket)tcpPacket.ParentPacket;
                            srcIp = ipPacket.SourceAddress;
                            dstIp = ipPacket.DestinationAddress;
                            protocol = ipPacket.Protocol;
                            srcPort = tcpPacket.SourcePort;
                            dstPort = tcpPacket.DestinationPort;
                        }

                        if (srcIp.ToString().Equals("255.255.255.255") && dstIp.ToString().Equals("255.255.255.255") || (srcPort == 0 && dstPort == 0))
                        {
                            continue;
                        }
                        else if (protocol != PacketDotNet.IPProtocolType.TCP)
                        {
                            continue;
                        }
                        else
                        {
                            var korTime = time.Hour - 3;
                            newItem.SubItems.Add(korTime + ":" + time.Minute + ":" + time.Second + "," + time.Millisecond);
                            newItem.SubItems.Add(srcIp.ToString());
                            newItem.SubItems.Add(dstIp.ToString());
                            newItem.SubItems.Add(protocol.ToString());
                            newItem.SubItems.Add(srcPort.ToString());
                            newItem.SubItems.Add(dstPort.ToString());
                            newItem.SubItems.Add(packetLength.ToString());
                            newItem.SubItems.Add("");
                            if (containsSensitiveData)
                            {
                                newItem.BackColor = System.Drawing.Color.OrangeRed;
                            }

                            mainForm.listView_PacketActivity.BeginUpdate();
                            mainForm.listView_PacketActivity.Items.Add(newItem);
                            mainForm.listView_PacketActivity.EndUpdate();

                            mainForm.listView_PacketActivity.EnsureVisible(mainForm.listView_PacketActivity.Items.Count - 1);
                            count++;
                        }
                    }
                }
            }
        }

        public void restartCapture()
        {
            device.StartCapture();
        }

        public void stopCapture()
        {
            device.StopCapture();
        }

        /* Not used for now */
        private static void showHexDump(byte[] payload, int packetLength)
        {
            char ch;
            string outputHexDump = "Data Payload \n";
            string outputASCII = "ASCII \n";

            var payloadHexDump = BitConverter.ToString(payload).Replace("-", string.Empty);
            var payloadASCII = Encoding.ASCII.GetString(payload);

            var payloadHexDumpArr = payloadHexDump.ToCharArray();
            var payloadASCIIArr = payloadASCII.ToCharArray();

            var charNumIndex = 0;

            for (int i = 0; i < packetLength; i++)
            {
                ch = payloadHexDumpArr[i];

                outputHexDump += ch;
                charNumIndex++;

                if (charNumIndex % 2 == 0)
                    outputHexDump += " ";

                if (charNumIndex % 30 == 0)
                    outputHexDump += " \n";
            }

            for (int i = 0; i < packetLength; i++)
            {
                ch = payloadASCIIArr[i];

                /* Is it work? */
                ch = (ch >= 32 && ch <= 128) ? ch : '.';

                outputASCII += ch;
                charNumIndex++;

                if (charNumIndex % 2 == 0)
                    outputASCII += " ";

                if (charNumIndex % 30 == 0)
                    outputASCII += " \n";
            }

            //mainForm.textBox_showPayload.Text = outputHexDump + "\n\n" + outputASCII;
        }

        private static bool detectSensitiveData(byte[] payload)
        {
            var payloadASCII = Encoding.ASCII.GetString(payload);
            var tempDetectedDataList = new List<string>();
            var detectedDataResult = string.Empty;

            foreach (var sData in mainForm.sensitiveDataList)
            {
                if (payloadASCII.Contains(sData))
                    tempDetectedDataList.Add(sData);
            }

            if (tempDetectedDataList.Count > 0)
            {
                for (int i = 0; i < tempDetectedDataList.Count; i++)
                {
                    var data = tempDetectedDataList[i];
                    //data = mainForm.removeHideSignal(data);

                    detectedDataResult += data;
                    if (i + 1 < tempDetectedDataList.Count)
                        detectedDataResult += ", ";
                }
                MessageBox.Show("Sending sensitive data detected!\nContent: " + detectedDataResult);
                return true;
            }
            else
                return false;
        }
    }
}
 