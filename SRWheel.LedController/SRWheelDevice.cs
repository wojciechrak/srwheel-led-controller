using System;
using HidSharp;

namespace SRWheel.LedController
{
    /// <summary>
    /// Wrapper for actual HID device
    /// </summary>
    public class SRWheelDevice
    {
        private static int SRWHEEL_PID = 0x1410;
        private static int SRWHEEL_VID = 0x1038;

        private readonly HidDevice _device; 
        
        public SRWheelDevice()
        {
            _device = DeviceList.Local.GetHidDeviceOrNull(SRWHEEL_VID, SRWHEEL_PID);
        }

        public bool IsInitialized => _device != null;

        public bool Write(byte[] data)
        {
            if(!IsInitialized)
                throw new InvalidOperationException("SRWheel not found!");

            using (var s = _device.Open())
            {
                s.Write(data);
            }

            return true;
        }
    }
}