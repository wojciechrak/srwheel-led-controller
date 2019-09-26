using System;

namespace SRWheel.LedController
{
    public class LedController
    {
        private SRWheelDevice _wheel;

        /// <summary>
        /// Initializes controller by finding SRWheel among HID devices
        /// </summary>
        /// <returns>Boolean value indicating whether device was found or not</returns>
        public bool InitDevice()
        {
            _wheel = new SRWheelDevice();
            return _wheel.IsInitialized;
        }
        
        /// <summary>
        /// Sends data to device
        /// </summary>
        /// <param name="ledValue">Bit-mask of LEDs to be set</param>
        /// <returns>Operation succeed?</returns>
        /// <exception cref="InvalidOperationException">Device could not be initialized</exception>
        public bool SetLed(LedNumber ledValue)
        {
            if (_wheel?.IsInitialized != true && !InitDevice())
                throw new InvalidOperationException("SRWheel not initialized.");
            
            var payload = PayloadHelper.PreparePayload(ledValue);
            return _wheel.Write(payload);
        }

        /// <summary>
        /// Sends data to device
        /// </summary>
        /// <param name="section">Bit-mask of LED sections to be set</param>
        /// <returns>Operation succeed?</returns>
        /// <exception cref="InvalidOperationException">Device could not be initialized</exception>
        public bool SetSection(LedSection section)
        {
            return SetLed((LedNumber) section);
        }

        /// <summary>
        /// Turns all LEDs off. Has the same effect as <see cref="SetLed"/> with argument <code>0</code>
        /// </summary>
        /// <returns>Operation succeed?</returns>
        public bool Reset()
        {
            return SetLed(0);
        }
    }
}