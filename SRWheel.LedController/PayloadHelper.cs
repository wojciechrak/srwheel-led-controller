using System;

namespace SRWheel.LedController
{
    public static class PayloadHelper
    {
        /// <summary>
        /// Common data frame for setting LEDs
        /// </summary>
        private static readonly byte[] _commonPayload = new byte[4] {0x00, 0x40, 0x00, 0x00};
        
        /// <summary>
        /// Returns complete byte array to write directly to device.
        /// </summary>
        /// <param name="ledValue">Bit-mask of LEDs to set</param>
        /// <returns>Byte array to write to device</returns>
        public static byte[] PreparePayload(LedNumber ledValue)
        {
            var ledBytes = BitConverter.GetBytes((short)ledValue);

            var payload = new byte[4];
            Array.Copy(_commonPayload, payload, 4);
            
            payload[2] = ledBytes[0];
            payload[3] = ledBytes[1];
            
            return payload;
        }
    }
}