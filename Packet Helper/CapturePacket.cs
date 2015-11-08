using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        public static int count = 1;

        public CapturePacket(MainForm main)
        {
            mainForm = main;
        }

        public void listingPackets(ICaptureDevice dev)
        {
            device = dev;
            int readTimeoutMilliseconds = 1000;

            backgroundThread = new System.Threading.Thread(BackgroundThread);
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

            lock(QueueLock)
            {
                PacketQueue.Add(e.Packet);
            }
        }

        private static void BackgroundThread()
        {
            while(!BackgroundThreadStop)
            {
                bool shouldSleep = true;

                lock(QueueLock)
                {
                    if (PacketQueue.Count != 0)
                    {
                        shouldSleep = false;
                    }
                }

                if (shouldSleep)
                {
                    System.Threading.Thread.Sleep(250);
                }
                else
                {
                    List<RawCapture> ourQueue;
                    lock(QueueLock)
                    {
                        ourQueue = PacketQueue;
                        PacketQueue = new List<RawCapture>();
                    }

                    foreach(var packet in ourQueue)
                    {
                        ListViewItem newItem = new ListViewItem(count.ToString());
                        
                        var packetLength = packet.Data.Length;
                        var time = packet.Timeval.Date;
                        var thisPacket = PacketDotNet.Packet.ParsePacket(packet.LinkLayerType, packet.Data);
                        var tcpPacket = (PacketDotNet.TcpPacket)thisPacket.Extract(typeof(PacketDotNet.TcpPacket));

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
                        else if ((protocol != PacketDotNet.IPProtocolType.TCP) && (protocol != PacketDotNet.IPProtocolType.UDP))
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
    }
}