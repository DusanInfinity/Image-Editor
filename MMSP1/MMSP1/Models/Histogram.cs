using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace MMSP1.Models
{
    public enum ChannelColor
    {
        Red = 0,
        Green = 1,
        Blue = 2
    }

    public static class Histogram
    {
        public static List<int> GetHistogramValues(Bitmap inputBitmap, ChannelColor channel)
        {
            List<int> histogramValues = new List<int>(256);
            for (int i = 0; i < 256; i++)
                histogramValues.Add(0);

            switch (channel)
            {
                case ChannelColor.Red:
                    for (int i = 0; i < inputBitmap.Width; i++)
                    {
                        for (int j = 0; j < inputBitmap.Height; j++)
                        {
                            System.Drawing.Color c = inputBitmap.GetPixel(i, j);
                            histogramValues[c.R]++;
                        }
                    }
                    break;
                case ChannelColor.Green:
                    for (int i = 0; i < inputBitmap.Width; i++)
                    {
                        for (int j = 0; j < inputBitmap.Height; j++)
                        {
                            System.Drawing.Color c = inputBitmap.GetPixel(i, j);
                            histogramValues[c.G]++;
                        }
                    }
                    break;
                case ChannelColor.Blue:
                    for (int i = 0; i < inputBitmap.Width; i++)
                    {
                        for (int j = 0; j < inputBitmap.Height; j++)
                        {
                            System.Drawing.Color c = inputBitmap.GetPixel(i, j);
                            histogramValues[c.B]++;
                        }
                    }
                    break;
            }

            return histogramValues;
        }

        public static Dictionary<ChannelColor, List<int>> GetHistogramValues(Bitmap inputBitmap)
        {
            Dictionary<ChannelColor, List<int>> histogramValues = new Dictionary<ChannelColor, List<int>>(256);
            histogramValues[ChannelColor.Red] = new List<int>(256);
            histogramValues[ChannelColor.Green] = new List<int>(256);
            histogramValues[ChannelColor.Blue] = new List<int>(256);

            for (int i = 0; i < 256; i++)
            {
                histogramValues[ChannelColor.Red].Add(0);
                histogramValues[ChannelColor.Green].Add(0);
                histogramValues[ChannelColor.Blue].Add(0);
            }

            for (int i = 0; i < inputBitmap.Width; i++)
            {
                for (int j = 0; j < inputBitmap.Height; j++)
                {
                    System.Drawing.Color c = inputBitmap.GetPixel(i, j);
                    histogramValues[ChannelColor.Red][c.R]++;
                    histogramValues[ChannelColor.Green][c.G]++;
                    histogramValues[ChannelColor.Blue][c.B]++;
                }
            }

            return histogramValues;
        }

        public static bool MinMaxFilter(Bitmap inputBitmap, ChannelColor channel, byte min, byte max, out Bitmap generatedBitmap)
        {

            generatedBitmap = (Bitmap)inputBitmap.Clone();

            BitmapData bmData = generatedBitmap.LockBits(new Rectangle(0, 0, generatedBitmap.Width, generatedBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            int colorIndex = 2;
            switch (channel)
            {
                case ChannelColor.Red:
                    colorIndex = 2;
                    break;
                case ChannelColor.Green:
                    colorIndex = 1;
                    break;
                case ChannelColor.Blue:
                    colorIndex = 0;
                    break;
            }

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - generatedBitmap.Width * 3;
                for (int y = 0; y < generatedBitmap.Height; ++y)
                {
                    for (int x = 0; x < generatedBitmap.Width; ++x)
                    { // bgr 
                        if (p[colorIndex] < min)
                            p[colorIndex] = min;
                        else if (p[colorIndex] > max)
                            p[colorIndex] = max;

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            generatedBitmap.UnlockBits(bmData);

            return true;
        }
    }
}
