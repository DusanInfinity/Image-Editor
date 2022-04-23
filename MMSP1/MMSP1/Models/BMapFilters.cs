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


        // https://www.mechanikadesign.com/2014/09/kuwahara-filter-in-c/
        public static bool KuwaharaFilter(Bitmap inputBitmap, int Size, out Bitmap generatedBitmap) // size = 13
        {
            int width = inputBitmap.Width;
            int height = inputBitmap.Height;
            generatedBitmap = new Bitmap(width, height);

            int[] ApetureMinX = { -(Size / 2), 0, -(Size / 2), 0 };
            int[] ApetureMaxX = { 0, (Size / 2), 0, (Size / 2) };
            int[] ApetureMinY = { -(Size / 2), -(Size / 2), 0, 0 };
            int[] ApetureMaxY = { 0, 0, (Size / 2), (Size / 2) };


            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    int[] RValues = { 0, 0, 0, 0 };
                    int[] GValues = { 0, 0, 0, 0 };
                    int[] BValues = { 0, 0, 0, 0 };
                    int[] NumPixels = { 0, 0, 0, 0 };
                    int[] MaxRValue = { 0, 0, 0, 0 };
                    int[] MaxGValue = { 0, 0, 0, 0 };
                    int[] MaxBValue = { 0, 0, 0, 0 };
                    int[] MinRValue = { 255, 255, 255, 255 };
                    int[] MinGValue = { 255, 255, 255, 255 };
                    int[] MinBValue = { 255, 255, 255, 255 };
                    for (int i = 0; i < 4; ++i)
                    {
                        for (int x2 = ApetureMinX[i]; x2 < ApetureMaxX[i]; ++x2)
                        {
                            int TempX = x + x2;
                            if (TempX >= 0 && TempX < width)
                            {
                                for (int y2 = ApetureMinY[i]; y2 < ApetureMaxY[i]; ++y2)
                                {
                                    int TempY = y + y2;
                                    if (TempY >= 0 && TempY < height)
                                    {
                                        Color TempColor = inputBitmap.GetPixel(TempX, TempY);
                                        RValues[i] += TempColor.R;
                                        GValues[i] += TempColor.G;
                                        BValues[i] += TempColor.B;

                                        if (TempColor.R > MaxRValue[i])
                                            MaxRValue[i] = TempColor.R;
                                        else if (TempColor.R < MinRValue[i])
                                            MinRValue[i] = TempColor.R;

                                        if (TempColor.G > MaxGValue[i])
                                            MaxGValue[i] = TempColor.G;
                                        else if (TempColor.G < MinGValue[i])
                                            MinGValue[i] = TempColor.G;

                                        if (TempColor.B > MaxBValue[i])
                                            MaxBValue[i] = TempColor.B;
                                        else if (TempColor.B < MinBValue[i])
                                            MinBValue[i] = TempColor.B;

                                        ++NumPixels[i];
                                    }
                                }
                            }
                        }
                    }
                    int j = 0;
                    int MinDifference = 10000;
                    for (int i = 0; i < 4; ++i)
                    {
                        int CurrentDifference = (MaxRValue[i] - MinRValue[i]) + (MaxGValue[i] - MinGValue[i]) + (MaxBValue[i] - MinBValue[i]);
                        if (CurrentDifference < MinDifference && NumPixels[i] > 0)
                        {
                            j = i;
                            MinDifference = CurrentDifference;
                        }
                    }

                    Color MeanPixel = Color.FromArgb(RValues[j] / NumPixels[j],
                        GValues[j] / NumPixels[j],
                        BValues[j] / NumPixels[j]);
                    generatedBitmap.SetPixel(x, y, MeanPixel);
                }
            }

            return true;
        }

        public static bool KuwaharaFilterUnsafe(Bitmap inputBitmap, int Size, out Bitmap generatedBitmap) // size = 13
        {
            int width = inputBitmap.Width;
            int height = inputBitmap.Height;


            generatedBitmap = new Bitmap(width, height);

            int[] ApetureMinX = { -(Size / 2), 0, -(Size / 2), 0 };
            int[] ApetureMaxX = { 0, (Size / 2), 0, (Size / 2) };
            int[] ApetureMinY = { -(Size / 2), -(Size / 2), 0, 0 };
            int[] ApetureMaxY = { 0, 0, (Size / 2), (Size / 2) };

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = generatedBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = inputBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmSrc.Stride;

            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = stride - width * 3;

                for (int x = 0; x < width; ++x)
                {
                    for (int y = 0; y < height; ++y)
                    {
                        int[] RValues = { 0, 0, 0, 0 };
                        int[] GValues = { 0, 0, 0, 0 };
                        int[] BValues = { 0, 0, 0, 0 };
                        int[] NumPixels = { 0, 0, 0, 0 };
                        int[] MaxRValue = { 0, 0, 0, 0 };
                        int[] MaxGValue = { 0, 0, 0, 0 };
                        int[] MaxBValue = { 0, 0, 0, 0 };
                        int[] MinRValue = { 255, 255, 255, 255 };
                        int[] MinGValue = { 255, 255, 255, 255 };
                        int[] MinBValue = { 255, 255, 255, 255 };
                        for (int i = 0; i < 4; ++i)
                        {
                            for (int x2 = ApetureMinX[i]; x2 < ApetureMaxX[i]; ++x2)
                            {
                                int TempX = x + x2;
                                if (TempX >= 0 && TempX < width)
                                {
                                    TempX *= 3; // svaki piksel zauzima po 3 bajta
                                    for (int y2 = ApetureMinY[i]; y2 < ApetureMaxY[i]; ++y2)
                                    {
                                        int TempY = y + y2;
                                        if (TempY >= 0 && TempY < height)
                                        {
                                            int index = TempY * stride + TempX;
                                            byte red = pSrc[index + 2];
                                            byte green = pSrc[index + 1];
                                            byte blue = pSrc[index];

                                            RValues[i] += red;
                                            GValues[i] += green;
                                            BValues[i] += blue;

                                            if (red > MaxRValue[i])
                                                MaxRValue[i] = red;
                                            else if (red < MinRValue[i])
                                                MinRValue[i] = red;

                                            if (green > MaxGValue[i])
                                                MaxGValue[i] = green;
                                            else if (green < MinGValue[i])
                                                MinGValue[i] = green;

                                            if (blue > MaxBValue[i])
                                                MaxBValue[i] = blue;
                                            else if (blue < MinBValue[i])
                                                MinBValue[i] = blue;

                                            ++NumPixels[i];
                                        }
                                    }
                                }
                            }
                        }
                        int j = 0;
                        int MinDifference = 10000;
                        for (int i = 0; i < 4; ++i)
                        {
                            int CurrentDifference = (MaxRValue[i] - MinRValue[i]) + (MaxGValue[i] - MinGValue[i]) + (MaxBValue[i] - MinBValue[i]);
                            if (CurrentDifference < MinDifference && NumPixels[i] > 0)
                            {
                                j = i;
                                MinDifference = CurrentDifference;
                            }
                        }

                        int destIndex = y * stride + x * 3;
                        p[destIndex + 2] = (byte)(RValues[j] / NumPixels[j]);
                        p[destIndex + 1] = (byte)(GValues[j] / NumPixels[j]);
                        p[destIndex] = (byte)(BValues[j] / NumPixels[j]);
                    }
                }
            }

            generatedBitmap.UnlockBits(bmData);
            inputBitmap.UnlockBits(bmSrc);

            return true;
        }

        private static double GetColorSimilarity(Color c1, Color c2)
        {
            int Rdiff = c1.R - c2.R;
            int Gdiff = c1.G - c2.G;
            int Bdiff = c1.B - c2.B;

            return Math.Sqrt(Rdiff * Rdiff + Gdiff * Gdiff + Bdiff * Bdiff);
        }

        //public static bool UnificationOfSimilarColoredZonesUnsafe(Bitmap inputBitmap, Color colorX, int x, int y, double simThreshold, out Bitmap generatedBitmap)
        //{
        //    generatedBitmap = (Bitmap)inputBitmap.Clone();

        //    int width = generatedBitmap.Width;
        //    int height = generatedBitmap.Height;

        //    if (x < 0 || x >= width || y < 0 || y >= height) return false;

        //    BitmapData bmData = generatedBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

        //    int stride = bmData.Stride;
        //    System.IntPtr Scan0 = bmData.Scan0;

        //    unsafe
        //    {
        //        byte* p = (byte*)(void*)Scan0;

        //        int clickedPixelOffset = y * stride + x * 3;

        //        int offset = clickedPixelOffset;

        //        byte yRed = p[offset + 2];
        //        byte yGreen = p[offset + 1];
        //        byte yBlue = p[offset];

        //        p[offset + 2] = colorX.R;
        //        p[offset + 1] = colorX.G;
        //        p[offset] = colorX.B;

        //        if (x - 1 >= 0) // leva strana
        //        {
        //            offset = offset - 3; // levo - sredina
        //            if (GetColorSimilarity(Color.FromArgb(p[offset + 2], p[offset + 2], p[offset + 2]), colorX) <= simThreshold)
        //            {
        //                p[offset + 2] = colorX.R;
        //                p[offset + 1] = colorX.G;
        //                p[offset] = colorX.B;
        //            }

        //            if (y - 1 >= 0)
        //            {
        //                offset = offset - stride; // levo - gore
        //                if (GetColorSimilarity(Color.FromArgb(p[offset + 2], p[offset + 2], p[offset + 2]), colorX) <= simThreshold)
        //                {
        //                    p[offset + 2] = colorX.R;
        //                    p[offset + 1] = colorX.G;
        //                    p[offset] = colorX.B;
        //                }
        //            }

        //            if (y + 1 < height)
        //            {
        //                offset = offset + 2 * stride; // levo - dole
        //                if (GetColorSimilarity(Color.FromArgb(p[offset + 2], p[offset + 2], p[offset + 2]), colorX) <= simThreshold)
        //                {
        //                    p[offset + 2] = colorX.R;
        //                    p[offset + 1] = colorX.G;
        //                    p[offset] = colorX.B;
        //                }
        //            }
        //        }

        //        offset = clickedPixelOffset;
        //        if (x + 1 < width) // desna strana
        //        {
        //            offset = offset + 3; // desno - sredina
        //            if (GetColorSimilarity(Color.FromArgb(p[offset + 2], p[offset + 2], p[offset + 2]), colorX) <= simThreshold)
        //            {
        //                p[offset + 2] = colorX.R;
        //                p[offset + 1] = colorX.G;
        //                p[offset] = colorX.B;
        //            }

        //            if (y - 1 >= 0)
        //            {
        //                offset = offset - stride; // desno - gore
        //                if (GetColorSimilarity(Color.FromArgb(p[offset + 2], p[offset + 2], p[offset + 2]), colorX) <= simThreshold)
        //                {
        //                    p[offset + 2] = colorX.R;
        //                    p[offset + 1] = colorX.G;
        //                    p[offset] = colorX.B;
        //                }
        //            }

        //            if (y + 1 < height)
        //            {
        //                offset = offset + 2 * stride; // desno - dole
        //                if (GetColorSimilarity(Color.FromArgb(p[offset + 2], p[offset + 2], p[offset + 2]), colorX) <= simThreshold)
        //                {
        //                    p[offset + 2] = colorX.R;
        //                    p[offset + 1] = colorX.G;
        //                    p[offset] = colorX.B;
        //                }
        //            }
        //        }

        //        offset = clickedPixelOffset;
        //        if (y - 1 >= 0) // gore
        //        {
        //            offset = offset - stride; // gore - sredina
        //            if (GetColorSimilarity(Color.FromArgb(p[offset + 2], p[offset + 2], p[offset + 2]), colorX) <= simThreshold)
        //            {
        //                p[offset + 2] = colorX.R;
        //                p[offset + 1] = colorX.G;
        //                p[offset] = colorX.B;
        //            }
        //        }

        //        offset = clickedPixelOffset;
        //        if (y + 1 < height) // dole
        //        {
        //            offset = offset + stride; // dole - sredina
        //            if (GetColorSimilarity(Color.FromArgb(p[offset + 2], p[offset + 2], p[offset + 2]), colorX) <= simThreshold)
        //            {
        //                p[offset + 2] = colorX.R;
        //                p[offset + 1] = colorX.G;
        //                p[offset] = colorX.B;
        //            }
        //        }

        //    }

        //    generatedBitmap.UnlockBits(bmData);

        //    return true;
        //}

        public static bool UnificationOfSimilarColoredZones(Bitmap inputBitmap, Color colorX, int x, int y, double simThreshold, out Bitmap generatedBitmap)
        {
            generatedBitmap = (Bitmap)inputBitmap.Clone();

            int width = generatedBitmap.Width;
            int height = generatedBitmap.Height;

            if (x < 0 || x >= width || y < 0 || y >= height) return false;

            Color colorY = generatedBitmap.GetPixel(x, y);
            generatedBitmap.SetPixel(x, y, colorX);

            bool[,] processed = new bool[width, height];
            processed[x, y] = true;

            Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>();

            stack.Push(new Tuple<int, int>(x - 1, y));
            stack.Push(new Tuple<int, int>(x - 1, y - 1));
            stack.Push(new Tuple<int, int>(x - 1, y + 1));
            stack.Push(new Tuple<int, int>(x + 1, y));
            stack.Push(new Tuple<int, int>(x + 1, y - 1));
            stack.Push(new Tuple<int, int>(x + 1, y + 1));
            stack.Push(new Tuple<int, int>(x, y - 1));
            stack.Push(new Tuple<int, int>(x, y + 1));

            while (stack.Count > 0)
            {
                Tuple<int, int> coords = stack.Pop();

                int currX = coords.Item1;
                int currY = coords.Item2;

                if (currX < 0 || currX >= width || currY < 0 || currY >= height || processed[currX, currY])
                    continue;

                processed[currX, currY] = true;

                Color currColor = generatedBitmap.GetPixel(currX, currY);
                if (GetColorSimilarity(currColor, colorY) > simThreshold)
                    continue;

                generatedBitmap.SetPixel(currX, currY, colorX);

                stack.Push(new Tuple<int, int>(currX - 1, currY)); // levo sredina
                stack.Push(new Tuple<int, int>(currX - 1, currY - 1)); // levo gore
                stack.Push(new Tuple<int, int>(currX - 1, currY + 1)); // levo dole
                stack.Push(new Tuple<int, int>(currX + 1, currY)); // desno sredina
                stack.Push(new Tuple<int, int>(currX + 1, currY - 1)); // desno gore
                stack.Push(new Tuple<int, int>(currX + 1, currY + 1)); // desno dole
                stack.Push(new Tuple<int, int>(currX, currY - 1)); // gore - sredina
                stack.Push(new Tuple<int, int>(currX, currY + 1)); // dole - sredina
            }

            return true;
        }
    }
}
