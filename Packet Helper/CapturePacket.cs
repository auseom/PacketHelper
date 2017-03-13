using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Text;
using SharpPcap;

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
                        /* http://www.netmanias.com/ko/post/blog/5372/ethernet-ip-ip-routing-network-protocol/packet-header-ethernet-ip-tcp-ip
                         * 알아내고자 하는 것
                         * 1. 패킷 내 사용자 민감 데이터 여부
                         * 2. 각 헤더 내 정보
                         * 
                         * 사용한 방법
                         * - 캡쳐되는 Raw 패킷(Ethernet 헤더가 포함된 패킷)으로부터 IP 헤더, TCP 헤더와 TCP 페이로드 값 확인
                         * (1) IP 헤더로부터 출발지 IP와 도착지 IP, 프로토콜 확인
                         * (2) TCP 헤더로부터 출발지 포트, 도착지 포트 확인
                         * (3) TCP 페이로드로부터 민감 데이터 여부 확인
                        */

                        // Ethernet 패킷의 Data 부분에 IP 헤더와 TCP 헤더 및 페이로드가 포함되어 있기 때문에 Ethernet 타입에 맞게 패킷 변환
                        var thisPacket = PacketDotNet.Packet.ParsePacket(packet.LinkLayerType, packet.Data);

                        // IP 헤더와 프로토콜이 TCP 인 지 알기 위해 IP 패킷 추출
                        var ipPacket = (PacketDotNet.IpPacket)thisPacket.Extract(typeof(PacketDotNet.IpPacket));

                        if (ipPacket != null)
                        {
                            var protocol = ipPacket.Protocol;
                            if (protocol == PacketDotNet.IPProtocolType.TCP)
                            {
                                // 프로토콜이 TCP인 경우 사용자 민감 데이터 검사 및 리스트뷰에 아이템 추가

                                ListViewItem newItem = new ListViewItem(count.ToString());

                                var packetLength = packet.Data.Length;
                                var time = TimeZoneInfo.ConvertTime(packet.Timeval.Date, TimeZoneInfo.Local);

                                // TCP 헤더와 페이로드 부분을 알기 위해 TCP 패킷 추출
                                var tcpPacket = (PacketDotNet.TcpPacket)thisPacket.Extract(typeof(PacketDotNet.TcpPacket));

                                // TCP 페이로드로부터 민감 데이터 여부 확인
                                var containsSensitiveData = detectSensitiveData(tcpPacket.PayloadData);

                                var srcIp = ipPacket.SourceAddress.ToString();
                                var dstIp = ipPacket.DestinationAddress.ToString();
                                var srcPort = tcpPacket.SourcePort;
                                var dstPort = tcpPacket.DestinationPort;

                                if (srcIp.ToString().Equals("255.255.255.255") && dstIp.ToString().Equals("255.255.255.255") || (srcPort == 0 && dstPort == 0))
                                {
                                    continue;
                                }
                                else
                                {
                                    newItem.SubItems.Add(time.ToString("G"));
                                    newItem.SubItems.Add(srcIp);
                                    newItem.SubItems.Add(dstIp);
                                    newItem.SubItems.Add(protocol.ToString());
                                    newItem.SubItems.Add(srcPort.ToString());
                                    newItem.SubItems.Add(dstPort.ToString());
                                    newItem.SubItems.Add(packetLength.ToString());
                                    newItem.SubItems.Add("");
                                    if (containsSensitiveData)
                                    {
                                        newItem.BackColor = System.Drawing.Color.OrangeRed;
                                    }

                                    Monitor.Enter(MainForm.LVItemQueue);
                                    MainForm.LVItemQueue.Enqueue(newItem);

                                    if (MainForm.LVItemQueue.Count > 0)
                                        Monitor.Pulse(MainForm.LVItemQueue);
                                    Monitor.Exit(MainForm.LVItemQueue);

                                    count++;
                                }
                            }
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
        //private static void showHexDump(byte[] payload, int packetLength)
        //{
        //    char ch;
        //    string outputHexDump = "Data Payload \n";
        //    string outputASCII = "ASCII \n";

        //    var payloadHexDump = BitConverter.ToString(payload).Replace("-", string.Empty);
        //    var payloadASCII = Encoding.ASCII.GetString(payload);

        //    var payloadHexDumpArr = payloadHexDump.ToCharArray();
        //    var payloadASCIIArr = payloadASCII.ToCharArray();

        //    var charNumIndex = 0;

        //    for (int i = 0; i < packetLength; i++)
        //    {
        //        ch = payloadHexDumpArr[i];

        //        outputHexDump += ch;
        //        charNumIndex++;

        //        if (charNumIndex % 2 == 0)
        //            outputHexDump += " ";

        //        if (charNumIndex % 30 == 0)
        //            outputHexDump += " \n";
        //    }

        //    for (int i = 0; i < packetLength; i++)
        //    {
        //        ch = payloadASCIIArr[i];

        //        /* Is it work? */
        //        ch = (ch >= 32 && ch <= 128) ? ch : '.';

        //        outputASCII += ch;
        //        charNumIndex++;

        //        if (charNumIndex % 2 == 0)
        //            outputASCII += " ";

        //        if (charNumIndex % 30 == 0)
        //            outputASCII += " \n";
        //    }

        //    mainForm.textBox_showPayload.Text = outputHexDump + "\n\n" + outputASCII;
        //}

        private static bool detectSensitiveData(byte[] payload)
        {
            try {
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
                    new Thread(new ThreadStart(new Action(() => { MessageBox.Show("Sending sensitive data detected!\nContent: " + detectedDataResult); }))).Start();

                    return true;
                }
                else
                    return false;
            }
            catch (Exception _e)
            {
                MessageBox.Show(_e.Message);
                return false;
            }
        }
    }
}
