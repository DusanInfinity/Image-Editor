using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

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
        public static Bitmap CalculateChannelByMaskUnsafe(Bitmap img, byte[] mask)
        {
            Bitmap b = (Bitmap)img.Clone();

            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    { // bgr 
                        p[2] &= mask[0]; // r
                        p[1] &= mask[1]; // g
                        p[0] &= mask[2]; // b

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

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

            for (int i = 0; i < generatedBitmap.Width; i++)
            {
                for (int j = 0; j < generatedBitmap.Height; j++)
                {
                    System.Drawing.Color c = generatedBitmap.GetPixel(i, j);
                    generatedBitmap.SetPixel(i, j, Color.FromArgb(redGamma[c.R], greenGamma[c.G], blueGamma[c.B]));
                }
            }

            return true;
        }
        public static bool GammaUnsafe(Bitmap inputBitmap, double red, double green, double blue, out Bitmap generatedBitmap)
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

        public static bool Sharpen(Bitmap inputBitmap, int nWeight /* default to 11*/, out Bitmap generatedBitmap, string matrixSize = "3x3")
        {
            generatedBitmap = (Bitmap)inputBitmap.Clone();
            ConvMatrix m;
            if (matrixSize == "5x5")
                m = new ConvMatrix5x5();
            else if (matrixSize == "7x7")
                m = new ConvMatrix7x7();
            else
                m = new ConvMatrix();

            m.SetAll(0);
            m.e = nWeight;
            m.b = m.d = m.f = m.h = -2;
            m.Factor = nWeight - 8;

            return m.Convert(generatedBitmap);
        }


        private static bool OffsetFilter(Bitmap b, Point[,] offset)
        {
            Bitmap bSrc = (Bitmap)b.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int scanline = bmData.Stride;

            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = bmData.Stride - b.Width * 3;
                int nWidth = b.Width;
                int nHeight = b.Height;

                int xOffset, yOffset;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        xOffset = offset[x, y].X;
                        yOffset = offset[x, y].Y;

                        if (y + yOffset >= 0 && y + yOffset < nHeight && x + xOffset >= 0 && x + xOffset < nWidth)
                        {
                            p[0] = pSrc[((y + yOffset) * scanline) + ((x + xOffset) * 3)];
                            p[1] = pSrc[((y + yOffset) * scanline) + ((x + xOffset) * 3) + 1];
                            p[2] = pSrc[((y + yOffset) * scanline) + ((x + xOffset) * 3) + 2];
                        }

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return true;
        }
        public static bool Pixelate(Bitmap inputBitmap, out Bitmap outputBitmap, short pixelSize = 15)
        {
            outputBitmap = (Bitmap)inputBitmap.Clone();
            int nWidth = inputBitmap.Width;
            int nHeight = inputBitmap.Height;

            Point[,] pt = new Point[nWidth, nHeight];

            int newX, newY;

            for (int x = 0; x < nWidth; ++x)
            {
                for (int y = 0; y < nHeight; ++y)
                {
                    newX = pixelSize - x % pixelSize;

                    if (/*bGrid && */newX == pixelSize)
                        pt[x, y].X = -x;
                    else if (x + newX > 0 && x + newX < nWidth)
                        pt[x, y].X = newX;
                    else
                        pt[x, y].X = 0;

                    newY = pixelSize - y % pixelSize;

                    if (/*bGrid && */newY == pixelSize)
                        pt[x, y].Y = -y;
                    else if (y + newY > 0 && y + newY < nHeight)
                        pt[x, y].Y = newY;
                    else
                        pt[x, y].Y = 0;
                }
            }

            OffsetFilter(outputBitmap, pt);
            return true;
        }

        public static bool EdgeEnhance(Bitmap inputBitmap, byte nThreshold, out Bitmap outputBitmap)
        {
            outputBitmap = (Bitmap)inputBitmap.Clone();
            Bitmap b2 = (Bitmap)inputBitmap.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = outputBitmap.LockBits(new Rectangle(0, 0, outputBitmap.Width, outputBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmData2 = b2.LockBits(new Rectangle(0, 0, outputBitmap.Width, outputBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr Scan02 = bmData2.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* p2 = (byte*)(void*)Scan02;

                int nOffset = stride - outputBitmap.Width * 3;
                int nWidth = outputBitmap.Width * 3;

                int nPixel = 0, nPixelMax = 0;

                p += stride;
                p2 += stride;

                for (int y = 1; y < outputBitmap.Height - 1; ++y)
                {
                    p += 3;
                    p2 += 3;

                    for (int x = 3; x < nWidth - 3; ++x)
                    {
                        nPixelMax = Math.Abs((p2 - stride + 3)[0] - (p2 + stride - 3)[0]); // C-G

                        nPixel = Math.Abs((p2 + stride + 3)[0] - (p2 - stride - 3)[0]); // I-A

                        if (nPixel > nPixelMax)
                            nPixelMax = nPixel;

                        nPixel = Math.Abs((p2 + stride)[0] - (p2 - stride)[0]); // H-B

                        if (nPixel > nPixelMax)
                            nPixelMax = nPixel;

                        nPixel = Math.Abs((p2 + 3)[0] - (p2 - 3)[0]); // F-D

                        if (nPixel > nPixelMax)
                            nPixelMax = nPixel;

                        if (nPixelMax > nThreshold && nPixelMax > p[0])
                            p[0] = (byte)Math.Max(p[0], nPixelMax);

                        ++p;
                        ++p2;
                    }

                    p += nOffset + 3;
                    p2 += nOffset + 3;
                }
            }

            outputBitmap.UnlockBits(bmData);
            b2.UnlockBits(bmData2);

            return true;
        }

        public static bool Create256ColorBMP(Bitmap inputBitmap, out Bitmap generatedBitmap)
        {
            //generatedBitmap = inputBitmap.Clone(new Rectangle(0, 0, inputBitmap.Width, inputBitmap.Height), PixelFormat.Format8bppIndexed);

            int height = inputBitmap.Height;
            int width = inputBitmap.Width;

            generatedBitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            Dictionary<Color, int> dict = new Dictionary<Color, int>();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    System.Drawing.Color c = inputBitmap.GetPixel(x, y);
                    if (dict.ContainsKey(c))
                        dict[c]++;
                    else
                        dict.Add(c, 1);
                }
            }

            List<KeyValuePair<Color, int>> colors = dict.OrderByDescending(x => x.Value).ToList();
            int count = colors.Count > 256 ? 256 : colors.Count;
            var palette = generatedBitmap.Palette;
            for (int i = 0; i < count; i++)
            {
                Color c = colors[i].Key;
                palette.Entries[i] = Color.FromArgb(c.R, c.G, c.B);
            }
            generatedBitmap.Palette = palette;


            BitmapData bmData = generatedBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            BitmapData bmSrc = inputBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride8 = bmData.Stride;
            int stride = bmSrc.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            List<Color> paletteEntriesList = palette.Entries.ToList();

            unsafe
            {
                byte* p8 = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = stride - width * 3;

                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    { // bgr 

                        Color currentPixelColor = Color.FromArgb(pSrc[2], pSrc[1], pSrc[0]);
                        int colorIndex = paletteEntriesList.IndexOf(currentPixelColor);
                        if (colorIndex != -1)
                            p8[y * stride8 + x] = (byte)colorIndex;
                        else
                        {
                            int minDistance = int.MaxValue;
                            colorIndex = 0;
                            for (int ind = 0; ind < 256; ind++)
                            {
                                Color col = paletteEntriesList[ind];
                                int dist = Math.Abs(col.R - currentPixelColor.R) + Math.Abs(col.G - currentPixelColor.G) + Math.Abs(col.B - currentPixelColor.B);
                                if (dist < minDistance)
                                {
                                    minDistance = dist;
                                    colorIndex = ind;
                                    if (dist == 1)
                                        break;
                                }
                            }
                            p8[y * stride8 + x] = (byte)colorIndex;
                        }

                        pSrc += 3;
                    }
                    pSrc += nOffset;
                }
            }

            generatedBitmap.UnlockBits(bmData);
            inputBitmap.UnlockBits(bmSrc);
            return true;
        }
    }
}
