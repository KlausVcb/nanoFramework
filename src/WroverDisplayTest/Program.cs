using nanoFramework.Hardware.Esp32;
using nanoFramework.Presentation.Media;
using nanoFramework.UI;
using System;
using System.Threading;

namespace WroverDisplayTest
{
    public class Program
    {
        public static void Main()
        {
            const int backLightPin = 5;
            const int chipSelect = 22;
            const int dataCommand = 21;
            const int reset = 18;
            const int screenWidth = 320;
            const int screenHeight = 240;

            Configuration.SetPinFunction(25, DeviceFunction.SPI1_MISO);
            Configuration.SetPinFunction(23, DeviceFunction.SPI1_MOSI);
            Configuration.SetPinFunction(19, DeviceFunction.SPI1_CLOCK);

            DisplayControl.Initialize(
                new SpiConfiguration(1, chipSelect, dataCommand, reset, backLightPin),
                new ScreenConfiguration(0, 0, screenWidth, screenHeight), 2 * 1024 * 1024);

            Bitmap fullScreenBitmap = DisplayControl.FullScreen;
            Font font = Resources.GetFont(Resources.FontResources.DefaultFont);
            Color color = ColorUtility.ColorFromRGB(255, 0, 0);
            Color white = ColorUtility.ColorFromRGB(255, 255, 255);

            fullScreenBitmap.Clear();
            fullScreenBitmap.Flush();

            while (true)
            {
                fullScreenBitmap.DrawText($"Time is {DateTime.UtcNow:HH:mm:ss}", font, color, 30, 10);
                fullScreenBitmap.Flush();
                Thread.Sleep(500);
            }
        }
    }
}
