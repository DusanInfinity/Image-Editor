using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MMSP1.Models
{
    static class BMapFilters
    {
        public static Bitmap CalculateChannelByMask(Bitmap img, byte[] mask)
        {
            Bitmap b = (Bitmap)img.Clone();

            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    System.Drawing.Color c = b.GetPixel(i, j);
                    b.SetPixel(i, j, Color.FromArgb(c.R & mask[0], c.G & mask[1], c.B & mask[2]));
                }
            }

            return b;
        }

        public static bool Gamma(Bitmap inputBitmap, double red, double green, double blue, out Bitmap generatedBitmap)
        {

            generatedBitmap = (Bitmap)inputBitmap.Clone();
            // S = 5, 1/S = 1/5 = 0.2
            if (red < 0.2 || red > 5 || green < 0.2 || green > 5 || blue < 0.2 || blue > 5) return false;

            byte[] redGamma = new byte[256];
            byte[] greenGamma = new byte[256];
            byte[] blueGamma = new byte[256];

            for (int i = 0; i < 256; ++i)
            {
                redGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / red)) + 0.5));
                greenGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / green)) + 0.5));
                blueGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / blue)) + 0.5));
            }

            BitmapData bmData = generatedBitmap.LockBits(new Rectangle(0, 0, generatedBitmap.Width, generatedBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - generatedBitmap.Width * 3;

                for (int y = 0; y < generatedBitmap.Height; ++y)
                {
                    for (int x = 0; x < generatedBitmap.Width; ++x)
                    { // bgr 
                        p[2] = redGamma[p[2]]; // r
                        p[1] = greenGamma[p[1]]; // g
                        p[0] = blueGamma[p[0]]; // b

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
