﻿using System.Drawing;
using System.Drawing.Imaging;

namespace MMSP1.Models
{
    public class ConvMatrix
    {
        public int a = 0, b = 0, c = 0; // TopLeft = 0, TopMid = 0, TopRight = 0;
        public int d = 0, e = 1, f = 0;//MidLeft = 0, Pixel = 1, MidRight = 0;
        public int g = 0, h = 0, i = 0; // BottomLeft = 0, BottomMid = 0, BottomRight = 0;
        public int Factor = 1;
        public int Offset = 0;
        public void SetAll(int nVal)
        {
            a = b = c = d = e = f = g = h = i = nVal;
        }

        public virtual bool Convert(Bitmap bitmap)
        {
            // Avoid divide by zero errors
            if (Factor == 0) return false;

            Bitmap bSrc = (Bitmap)bitmap.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            int stride2 = stride * 2;
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = stride - bitmap.Width * 3;
                int nWidth = bitmap.Width - 2;
                int nHeight = bitmap.Height - 2;

                int nPixel;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        for (int col = 0; col < 3; col++) // col=0 - crvena, col=1 - zelena, col=2 - plava
                        {
                            nPixel =
                                (((pSrc[2 - col] * a) +
                                (pSrc[5 - col] * b) +
                                (pSrc[8 - col] * c) +
                                (pSrc[2 + stride - col] * d) +
                                (pSrc[5 + stride - col] * e) +
                                (pSrc[8 + stride - col] * f) +
                                (pSrc[2 + stride2 - col] * g) +
                                (pSrc[5 + stride2 - col] * h) +
                                (pSrc[8 + stride2 - col] * i)) / Factor) + Offset;

                            if (nPixel < 0) nPixel = 0;
                            if (nPixel > 255) nPixel = 255;

                            p[5 + stride - col] = (byte)nPixel;
                        }

                        p += 3;
                        pSrc += 3;
                    }
                    p += nOffset;
                    pSrc += nOffset;
                }
            }

            bitmap.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return true;
        }
    }
}
