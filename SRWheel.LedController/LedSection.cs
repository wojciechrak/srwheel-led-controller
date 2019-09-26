using System;

namespace SRWheel.LedController
{
    /// <summary>
    /// Helper flags with pre-set sections of LEDs (red, green, blue, all)
    /// </summary>
    [Flags]
    public enum LedSection : short
    {
        Red = LedNumber.R1 | LedNumber.R2 | LedNumber.R3 | LedNumber.R4 | LedNumber.R5,
        Green = LedNumber.G6 | LedNumber.G7 | LedNumber.G8 | LedNumber.G9 | LedNumber.G10,
        Blue = LedNumber.B11 | LedNumber.B12 | LedNumber.B13 | LedNumber.B14 | LedNumber.B15,
        
        All = Red | Green | Blue,
    }
}