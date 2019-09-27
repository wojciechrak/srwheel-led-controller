# srwheel-led-controller
.NET library that can control LEDs on Simraceway SRW-S1 steering wheel controller

# Usage
```csharp
using SRWheel.LedController


// Instantiate controller
LedController ctr = new LedController();

// Init device (find SRWheel in HID devices)
bool deviceFound = ctr.InitDevice();

// Set individual LED
ctr.SetLed(LedNumber.R1); // first red LED
ctr.SetLed(LedNumber.R2); // second red LED; this will unset R1!

// Set many LEDs, use binary OR operator
ctr.SetLed(LedNumber.R1 | LedNumber.G6 | LedNumber.B11); // sets first red LED, first green (sixth overall) LED and first blue (11th overall) LED

// or set entire section
ctr.SetSection(LedSection.Red);

// unset all LEDs
ctr.Reset();

```

# Demo
See a short gif with demo application https://imgur.com/a/UgOV1rJ
