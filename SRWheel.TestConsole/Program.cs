using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SRWheel.LedController;
using static System.Console;

namespace SRWheel.TestConsole
{
    internal class Program
    {
        private static LedController.LedController _controller;
        private const int LED_INTERVAL_MS = 100;
        private static Random _random = new Random();
        
        public static void Main(string[] args)
        {
            _controller = new LedController.LedController();
            
            if (_controller.InitDevice())
            {
                Write("Found SRWheel");
            }
            else
            {
                Error.WriteLine("SRWheel not found");
                Environment.Exit(1);
            }
            
            WriteLine("Resetting LEDs");
            _controller.Reset();

            WriteLine("Program will now perform 6 test sequences in a loop. Press <ESC> to stop");
            
            var cts = new CancellationTokenSource();
            
            Task.Run(() =>
            {
                while (true)
                {
                    TestSequence();
                }
            }, cts.Token);

            while (ReadKey(true).Key != ConsoleKey.Escape)
            {
                Thread.Sleep(10);
            }

            cts.Cancel();

            WriteLine("Test sequence interrupted");
            
            WriteLine("Resetting LEDs");
            _controller.Reset();

            WriteLine("Program will now exit");
        }

        private static void TestSequence()
        {
            // TEST #1
            WriteLine("Test sequence #1 - light up each single LED");
            var allLeds = Enum.GetValues(typeof(LedNumber)).Cast<LedNumber>().ToArray();

            foreach (var led in allLeds)
            {
                _controller.SetLed(led);
                WaitShort();
            }

            WaitLong();
            _controller.Reset();
            WaitLong();

            // TEST #2
            WriteLine("Test sequence #2 - light up all consecutive LEDs");

            LedNumber sum = 0;
            foreach (var led in allLeds)
            {
                sum |= led;
                _controller.SetLed(sum);
                
                WaitShort();
            }

            WaitLong();
            _controller.Reset();
            WaitLong();

            // TEST #3
            WriteLine("Test sequence #3 - light up each LED section");
            
            var sections = new []{LedSection.Red, LedSection.Green, LedSection.Blue};
            foreach (var section in sections)
            {
                _controller.SetSection(section);

                WaitLong();
            }

            WaitLong();
            _controller.Reset();
            WaitLong();

            // TEST #4
            WriteLine("Test sequence #4 - light up each LED section consecutively");

            LedSection cumulativeSections = 0;
            foreach (var section in sections)
            {
                cumulativeSections|= section;
                _controller.SetSection(cumulativeSections);

                WaitLong();
            }
            
            WaitLong();
            _controller.Reset();
            WaitLong();

            // TEST #5
            WriteLine("Test sequence #5 - blink all LEDs");

            for (int i = 0; i < 10; i++)
            {
                _controller.SetSection(LedSection.All);
                WaitShort();
                _controller.Reset();
                WaitShort();
            }
            
            WaitLong();
            _controller.Reset();
            WaitLong();
            
            // TEST #6
            WriteLine("Test sequence #6 - blink all LEDs randomly for 5s");

            var stopwatch = Stopwatch.StartNew();
            while(stopwatch.ElapsedMilliseconds < 5_000)
            {
                var b = new byte[2];
                _random.NextBytes(b);
                var leds = (LedNumber)BitConverter.ToInt16(b,0);
                _controller.SetLed(leds);
                WaitVeryShort();
            }

            _controller.Reset();
            
            WriteLine("All test sequences completed");
        }

        private static void WaitVeryShort() => Thread.Sleep(LED_INTERVAL_MS / 5);
        private static void WaitShort() => Thread.Sleep(LED_INTERVAL_MS);
        private static void WaitLong() => Thread.Sleep(LED_INTERVAL_MS * 5);


    }
}