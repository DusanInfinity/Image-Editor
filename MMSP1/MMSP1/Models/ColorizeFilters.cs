using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MMSP1.Models
{
    public static class ColorizeFilters
    {
        private static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        private static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }


        public static bool CrossDomainColorize(Bitmap inputBitmap, short newHue, double newSaturation, out Bitmap generatedBitmap)
        {
            generatedBitmap = (Bitmap)inputBitmap.Clone();

            BitmapData bmData = generatedBitmap.LockBits(new Rectangle(0, 0, generatedBitmap.Width, generatedBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            double h;
            double s;
            double v;

            int width = generatedBitmap.Width;
            int height = generatedBitmap.Height;

            double hueToSet = (newHue + 1) * 60;

            int quickX;
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - width * 3;

                for (int x = 0; x < width - 1; ++x)
                {
                    quickX = x * 3;
                    for (int y = 0; y < height - 1; ++y)
                    { // bgr 
                        int index = quickX + y * stride;

                        ColorToHSV(Color.FromArgb(p[index + 2], p[index + 1], p[index + 0]), out h, out s, out v);
                        Color newColor = ColorFromHSV(hueToSet, newSaturation != -1 ? newSaturation : s, v);

                        p[index + 2] = newColor.R; // r
                        p[index + 1] = newColor.G; // g
                        p[index] = newColor.B; // b

                        //p += 3;
                    }
                    //p += nOffset;
                }
            }

            generatedBitmap.UnlockBits(bmData);

            return true;
        }

        public static bool SimpleColorize(Bitmap inputBitmap, short newHue, double newSaturation, out Bitmap generatedBitmap)
        {
            generatedBitmap = (Bitmap)inputBitmap.Clone();

            BitmapData bmData = generatedBitmap.LockBits(new Rectangle(0, 0, generatedBitmap.Width, generatedBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            double h;
            double s;
            double v;

            int width = generatedBitmap.Width;
            int height = generatedBitmap.Height;

            double hueToSet = (newHue + 1) * 60;

            int quickX;
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - width * 3;

                for (int x = 0; x < width - 1; ++x)
                {
                    quickX = x * 3;
                    for (int y = 0; y < height - 1; ++y)
                    { // bgr 
                        int index = quickX + y * stride;

                        ColorToHSV(Color.FromArgb(p[index + 2], p[index + 1], p[index + 0]), out h, out s, out v);
                        Color newColor = ColorFromHSV(hueToSet, newSaturation != -1 ? newSaturation : s, v);

                        p[index + 2] = newColor.R; // r
                        p[index + 1] = newColor.G; // g
                        p[index] = newColor.B; // b

                        //p += 3;
                    }
                    //p += nOffset;
                }
            }

            generatedBitmap.UnlockBits(bmData);

            return true;
        }
    }
}
