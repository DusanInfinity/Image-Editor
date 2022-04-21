using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace MMSP1.Models
{
    public static class DitheringFilters
    {
        public static bool OrderedDithering(Bitmap inputBitmap, out Bitmap generatedBitmap)
        {
            generatedBitmap = (Bitmap)inputBitmap.Clone();

            BitmapData bmData = generatedBitmap.LockBits(new Rectangle(0, 0, generatedBitmap.Width, generatedBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            int[,] matrix = new int[4, 4]
            {
                { 1, 9, 3, 11 },
                { 13, 5, 15, 17 },
                { 4, 12, 2, 10 },
                { 16, 8, 14, 6 },
            };

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - generatedBitmap.Width * 3;

                int col = 0;
                int row = 0;

                for (int y = 0; y < generatedBitmap.Height; ++y)
                {
                    row = y % 4;

                    for (int x = 0; x < generatedBitmap.Width; ++x)
                    { // bgr 
                        col = x % 4;

                        // Da bismo skalirali x od 1 do 16 gde x ima vrednost od 1 do 255
                        // izracunavamo po formuli: x*16/255

                        p[2] = (byte)(((float)p[2] * 16.0 / 255.0) > matrix[row, col] ? 255 : 0); // r
                        p[1] = (byte)(((float)p[1] * 16.0 / 255.0) > matrix[row, col] ? 255 : 0); // g
                        p[0] = (byte)(((float)p[0] * 16.0 / 255.0) > matrix[row, col] ? 255 : 0); // b

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            generatedBitmap.UnlockBits(bmData);

            return true;
        }

        #region JarvisJudiceNinkeDithering
        // Po ugledu na https://www.codeproject.com/Tips/739630/Dither-Floyd-Steinberg-Dithering
        // Link koji je dat u zadatku(https://gist.github.com/PhearTheCeal/6443667) ne radi
        private static byte PlusTruncate(byte a, int b)
        {
            if ((a & 0xff) + b < 0)
                return 0;
            else if ((a & 0xff) + b > 255)
                return (byte)255;
            else
                return (byte)(a + b);
        }
        private static Color FindNearestColor(Color color, Color[] palette)
        {
            int minDistanceSquared = 255 * 255 + 255 * 255 + 255 * 255 + 1;
            byte bestIndex = 0;
            for (int i = 0; i < palette.Length; i++)
            {
                int Rdiff = (color.R & 0xff) - (palette[i].R & 0xff);
                int Gdiff = (color.G & 0xff) - (palette[i].G & 0xff);
                int Bdiff = (color.B & 0xff) - (palette[i].B & 0xff);
                int distanceSquared = Rdiff * Rdiff + Gdiff * Gdiff + Bdiff * Bdiff;
                if (distanceSquared < minDistanceSquared)
                {
                    minDistanceSquared = distanceSquared;
                    bestIndex = (byte)i;
                }
            }
            return (palette[bestIndex] == Color.White) ? Color.Empty : palette[bestIndex];
        }

        public static bool JarvisJudiceNinkeDithering(Bitmap inputBitmap, out Bitmap generatedBitmap)
        {
            generatedBitmap = (Bitmap)inputBitmap.Clone();

            List<Color> paletteList = new List<Color>();
            paletteList.Add(Color.FromArgb(0, 0, 0)); // crna
            paletteList.Add(Color.FromArgb(255, 0, 0)); // r
            paletteList.Add(Color.FromArgb(0, 255, 0)); // g
            paletteList.Add(Color.FromArgb(0, 0, 255)); // b
            paletteList.Add(Color.FromArgb(255, 255, 0)); // zuta
            paletteList.Add(Color.FromArgb(255, 0, 255)); // ljubicasta
            paletteList.Add(Color.FromArgb(0, 255, 255)); // tirkizna
            paletteList.Add(Color.FromArgb(255, 255, 255)); // bela
            Color[] palette = paletteList.ToArray();

            int height = generatedBitmap.Height;
            int width = generatedBitmap.Width;


            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color currentPixel = generatedBitmap.GetPixel(x, y);
                    Color bestColor = FindNearestColor(currentPixel, palette);
                    generatedBitmap.SetPixel(x, y, bestColor);

                    int errorR = (currentPixel.R & 0xff) - (bestColor.R & 0xff);
                    int errorG = (currentPixel.G & 0xff) - (bestColor.G & 0xff);
                    int errorB = (currentPixel.B & 0xff) - (bestColor.B & 0xff);


                    // smanjenje(optimizacija) aritmetickih operacija
                    int[] error1 = new int[3] { errorR / 48, errorG / 48, errorB / 48 };
                    int[] error3 = new int[3] { (errorR * 3) / 48, (errorG * 3) / 48, (errorB * 3) / 48 };
                    int[] error5 = new int[3] { (errorR * 5) / 48, (errorG * 5) / 48, (errorB * 5) / 48 };
                    int[] error7 = new int[3] { (errorR * 7) / 48, (errorG * 7) / 48, (errorB * 7) / 48 };

                    if (x + 1 < width)
                    {
                        Color pixel = generatedBitmap.GetPixel(x + 1, y);
                        generatedBitmap.SetPixel(x + 1, y, Color.FromArgb(
                            PlusTruncate(pixel.R, error7[0]),
                            PlusTruncate(pixel.G, error7[1]),
                            PlusTruncate(pixel.B, error7[2])
                        ));

                        if (x + 2 < width)
                        {
                            pixel = generatedBitmap.GetPixel(x + 2, y);
                            generatedBitmap.SetPixel(x + 2, y, Color.FromArgb(
                                PlusTruncate(pixel.R, error5[0]),
                                PlusTruncate(pixel.G, error5[1]),
                                PlusTruncate(pixel.B, error5[2])
                            ));
                        }
                    }
                    if (y + 1 < height)
                    {
                        Color pixel;

                        if (x - 1 >= 0)
                        {
                            if (x - 2 >= 0)
                            {
                                pixel = generatedBitmap.GetPixel(x - 2, y + 1);
                                generatedBitmap.SetPixel(x - 2, y + 1, Color.FromArgb(
                                    PlusTruncate(pixel.R, error3[0]),
                                    PlusTruncate(pixel.G, error3[1]),
                                    PlusTruncate(pixel.B, error3[2])
                                ));
                            }

                            pixel = generatedBitmap.GetPixel(x - 1, y + 1);
                            generatedBitmap.SetPixel(x - 1, y + 1, Color.FromArgb(
                                PlusTruncate(pixel.R, error5[0]),
                                PlusTruncate(pixel.G, error5[1]),
                                PlusTruncate(pixel.B, error5[2])
                            ));
                        }

                        pixel = generatedBitmap.GetPixel(x, y + 1);
                        generatedBitmap.SetPixel(x, y + 1, Color.FromArgb(
                            PlusTruncate(pixel.R, error7[0]),
                            PlusTruncate(pixel.G, error7[1]),
                            PlusTruncate(pixel.B, error7[2])
                        ));


                        if (x + 1 < width)
                        {
                            pixel = generatedBitmap.GetPixel(x + 1, y + 1);
                            generatedBitmap.SetPixel(x + 1, y + 1, Color.FromArgb(
                                PlusTruncate(pixel.R, error5[0]),
                                PlusTruncate(pixel.G, error5[1]),
                                PlusTruncate(pixel.B, error5[2])
                            ));

                            if (x + 2 < width)
                            {
                                pixel = generatedBitmap.GetPixel(x + 2, y + 1);
                                generatedBitmap.SetPixel(x + 2, y + 1, Color.FromArgb(
                                    PlusTruncate(pixel.R, error3[0]),
                                    PlusTruncate(pixel.G, error3[1]),
                                    PlusTruncate(pixel.B, error3[2])
                                ));
                            }
                        }

                        // Treca vrsta
                        if (y + 2 < height)
                        {
                            if (x - 2 >= 0)
                            {
                                pixel = generatedBitmap.GetPixel(x - 2, y + 2);
                                generatedBitmap.SetPixel(x - 2, y + 2, Color.FromArgb(
                                    PlusTruncate(pixel.R, error1[0]),
                                    PlusTruncate(pixel.G, error1[1]),
                                    PlusTruncate(pixel.B, error1[2])
                                ));
                            }

                            if (x - 1 >= 0)
                            {
                                pixel = generatedBitmap.GetPixel(x - 1, y + 2);
                                generatedBitmap.SetPixel(x - 1, y + 2, Color.FromArgb(
                                    PlusTruncate(pixel.R, error3[0]),
                                    PlusTruncate(pixel.G, error3[1]),
                                    PlusTruncate(pixel.B, error3[2])
                                ));
                            }

                            pixel = generatedBitmap.GetPixel(x, y + 2);
                            generatedBitmap.SetPixel(x, y + 2, Color.FromArgb(
                                PlusTruncate(pixel.R, error5[0]),
                                PlusTruncate(pixel.G, error5[1]),
                                PlusTruncate(pixel.B, error5[2])
                            ));


                            if (x + 1 < width)
                            {
                                pixel = generatedBitmap.GetPixel(x + 1, y + 2);
                                generatedBitmap.SetPixel(x + 1, y + 2, Color.FromArgb(
                                    PlusTruncate(pixel.R, error3[0]),
                                    PlusTruncate(pixel.G, error3[1]),
                                    PlusTruncate(pixel.B, error3[2])
                                ));

                                if (x + 2 < width)
                                {
                                    pixel = generatedBitmap.GetPixel(x + 2, y + 2);
                                    generatedBitmap.SetPixel(x + 2, y + 2, Color.FromArgb(
                                        PlusTruncate(pixel.R, error1[0]),
                                        PlusTruncate(pixel.G, error1[1]),
                                        PlusTruncate(pixel.B, error1[2])
                                    ));
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        public static bool JarvisJudiceNinkeDitheringUnsafe(Bitmap inputBitmap, out Bitmap generatedBitmap)
        {
            generatedBitmap = (Bitmap)inputBitmap.Clone();

            List<Color> paletteList = new List<Color>();
            paletteList.Add(Color.FromArgb(0, 0, 0)); // crna
            paletteList.Add(Color.FromArgb(255, 0, 0)); // r
            paletteList.Add(Color.FromArgb(0, 255, 0)); // g
            paletteList.Add(Color.FromArgb(0, 0, 255)); // b
            paletteList.Add(Color.FromArgb(255, 255, 0)); // zuta
            paletteList.Add(Color.FromArgb(255, 0, 255)); // ljubicasta
            paletteList.Add(Color.FromArgb(0, 255, 255)); // tirkizna
            paletteList.Add(Color.FromArgb(255, 255, 255)); // bela
            Color[] palette = paletteList.ToArray();

            int height = generatedBitmap.Height;
            int width = generatedBitmap.Width;

            BitmapData bmData = generatedBitmap.LockBits(new Rectangle(0, 0, generatedBitmap.Width, generatedBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            int stride2 = bmData.Stride * 2;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - generatedBitmap.Width * 3;

                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    { // bgr 

                        Color currentPixel = Color.FromArgb(p[2], p[1], p[0]);
                        Color bestColor = FindNearestColor(currentPixel, palette);
                        p[2] = bestColor.R;
                        p[1] = bestColor.G;
                        p[0] = bestColor.B;

                        int errorR = (currentPixel.R & 0xff) - (bestColor.R & 0xff);
                        int errorG = (currentPixel.G & 0xff) - (bestColor.G & 0xff);
                        int errorB = (currentPixel.B & 0xff) - (bestColor.B & 0xff);


                        // smanjenje(optimizacija) aritmetickih operacija
                        int[] error1 = new int[3] { errorR / 48, errorG / 48, errorB / 48 };
                        int[] error3 = new int[3] { (errorR * 3) / 48, (errorG * 3) / 48, (errorB * 3) / 48 };
                        int[] error5 = new int[3] { (errorR * 5) / 48, (errorG * 5) / 48, (errorB * 5) / 48 };
                        int[] error7 = new int[3] { (errorR * 7) / 48, (errorG * 7) / 48, (errorB * 7) / 48 };

                        if (x + 1 < width) // Prva vrsta
                        {
                            p[5] = PlusTruncate(p[5], error7[0]);
                            p[4] = PlusTruncate(p[4], error7[1]);
                            p[3] = PlusTruncate(p[3], error7[2]);

                            if (x + 2 < width)
                            {
                                p[8] = PlusTruncate(p[8], error5[0]);
                                p[7] = PlusTruncate(p[7], error5[1]);
                                p[6] = PlusTruncate(p[6], error5[2]);
                            }
                        }
                        if (y + 1 < height) // Druga vrsta
                        {
                            int offset;

                            if (x - 1 >= 0)
                            {
                                if (x - 2 >= 0)
                                {
                                    // -3 * 2 = -6 => 2 piksela levo od trenutnog
                                    // +stride => naredna vrsta
                                    offset = -3 * 2 + stride;
                                    p[2 + offset] = PlusTruncate(p[2 + offset], error3[0]);
                                    p[1 + offset] = PlusTruncate(p[1 + offset], error3[1]);
                                    p[0 + offset] = PlusTruncate(p[0 + offset], error3[2]);
                                }

                                offset = -3 + stride;
                                p[2 + offset] = PlusTruncate(p[2 + offset], error5[0]);
                                p[1 + offset] = PlusTruncate(p[1 + offset], error5[1]);
                                p[0 + offset] = PlusTruncate(p[0 + offset], error5[2]);
                            }

                            offset = stride;
                            p[2 + offset] = PlusTruncate(p[2 + offset], error7[0]);
                            p[1 + offset] = PlusTruncate(p[1 + offset], error7[1]);
                            p[0 + offset] = PlusTruncate(p[0 + offset], error7[2]);

                            if (x + 1 < width)
                            {
                                offset = 3 + stride;
                                p[2 + offset] = PlusTruncate(p[2 + offset], error5[0]);
                                p[1 + offset] = PlusTruncate(p[1 + offset], error5[1]);
                                p[0 + offset] = PlusTruncate(p[0 + offset], error5[2]);

                                if (x + 2 < width)
                                {
                                    offset = 3 * 2 + stride;
                                    p[2 + offset] = PlusTruncate(p[2 + offset], error3[0]);
                                    p[1 + offset] = PlusTruncate(p[1 + offset], error3[1]);
                                    p[0 + offset] = PlusTruncate(p[0 + offset], error3[2]);
                                }
                            }

                            // Treca vrsta
                            if (y + 2 < height)
                            {
                                if (x - 2 >= 0)
                                {
                                    offset = -3 * 2 + stride2;
                                    p[2 + offset] = PlusTruncate(p[2 + offset], error1[0]);
                                    p[1 + offset] = PlusTruncate(p[1 + offset], error1[1]);
                                    p[0 + offset] = PlusTruncate(p[0 + offset], error1[2]);
                                }

                                if (x - 1 >= 0)
                                {
                                    offset = -3 + stride2;
                                    p[2 + offset] = PlusTruncate(p[2 + offset], error3[0]);
                                    p[1 + offset] = PlusTruncate(p[1 + offset], error3[1]);
                                    p[0 + offset] = PlusTruncate(p[0 + offset], error3[2]);
                                }

                                offset = stride2;
                                p[2 + offset] = PlusTruncate(p[2 + offset], error5[0]);
                                p[1 + offset] = PlusTruncate(p[1 + offset], error5[1]);
                                p[0 + offset] = PlusTruncate(p[0 + offset], error5[2]);


                                if (x + 1 < width)
                                {
                                    offset = 3 + stride2;
                                    p[2 + offset] = PlusTruncate(p[2 + offset], error3[0]);
                                    p[1 + offset] = PlusTruncate(p[1 + offset], error3[1]);
                                    p[0 + offset] = PlusTruncate(p[0 + offset], error3[2]);

                                    if (x + 2 < width)
                                    {
                                        offset = 3 * 2 + stride2;
                                        p[2 + offset] = PlusTruncate(p[2 + offset], error1[0]);
                                        p[1 + offset] = PlusTruncate(p[1 + offset], error1[1]);
                                        p[0 + offset] = PlusTruncate(p[0 + offset], error1[2]);
                                    }
                                }
                            }
                        }

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            generatedBitmap.UnlockBits(bmData);

            return true;
        }
        #endregion
    }
}
