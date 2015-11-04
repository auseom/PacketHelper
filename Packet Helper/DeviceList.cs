using System;
using System.Collections.Generic;
using SharpPcap;

namespace Packet_Helper
{
    /// <summary>
    /// Obtaining the device list
    /// </summary>
    public class DeviceList
    {
        public static String GetSharpPcapVersion()
        {
            String ver = SharpPcap.Version.VersionString;

            return ver;
        }

        public static List<ICaptureDevice> GetDevices()
        {
            List<ICaptureDevice> deviceList = new List<ICaptureDevice>();
            var devices = CaptureDeviceList.Instance;

            if (devices.Count < 1)
            {
                return null;
            }

            foreach (ICaptureDevice dev in devices)
            {
                deviceList.Add(dev);
            }

            return deviceList;
        }
    }
}
