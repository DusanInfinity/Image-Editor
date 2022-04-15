using System.Collections.Generic;
using System.Drawing;

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
    }
}
