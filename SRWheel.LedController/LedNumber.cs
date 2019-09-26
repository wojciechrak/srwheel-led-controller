using System;

namespace SRWheel.LedController
{
    /// <summary>
    /// Flags for composing bit-mask of LEDs to be set
    /// </summary>
    [Flags]
    public enum LedNumber : short
    {
        R1 = 0x01,
        R2 = 0x02,
        R3 = 0x04,
        R4 = 0x08,
        R5 = 0x10,

        G6 = 0x20,
        G7 = 0x40,
        G8 = 0x80,
        G9 = 0x0100,
        G10 = 0x0200,

        B11 = 0x0400,
        B12 = 0x0800,
        B13 = 0x1000,
        B14 = 0x2000,
        B15 = 0x4000,
    }
}