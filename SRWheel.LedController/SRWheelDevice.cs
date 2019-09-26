using System;
using System.Linq;
using HidLibrary;

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
            _device = HidDevices
                .Enumerate()
                .FirstOrDefault(x => x.Attributes.ProductId == SRWHEEL_PID && x.Attributes.VendorId == SRWHEEL_VID);
        }

        public bool IsInitialized => _device != null;

        public bool Write(byte[] data)
        {
            if(!IsInitialized)
                throw new InvalidOperationException("SRWheel not found!");
            
            return _device.Write(data);
        }
    }
}