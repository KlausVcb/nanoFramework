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
            Configuration.SetPinFunction(25, DeviceFunction.SPI1_MISO);
            Configuration.SetPinFunction(23, DeviceFunction.SPI1_MOSI);
            Configuration.SetPinFunction(19, DeviceFunction.SPI1_CLOCK);

            Bitmap fullScreenBitmap = DisplayControl.FullScreen;
            Font font = Resources.GetFont(Resources.FontResources.DefaultFont);
            Color color = ColorUtility.ColorFromRGB(255, 0, 0);

            while (true)
            {
                fullScreenBitmap.Clear();
                fullScreenBitmap.DrawText($"Time is {DateTime.UtcNow:HH:mm:ss}", font, color, 30, 10);
                fullScreenBitmap.Flush();
                Thread.Sleep(100);
            }
        }
    }
}
