using System.Drawing;
using System.Drawing.Imaging;

namespace MMSP1.Models
{
    class ConvMatrix7x7 : ConvMatrix
    {
        public override bool Convert(Bitmap bitmap)
        {
            // Avoid divide by zero errors
            if (Factor == 0) return false;

            Bitmap bSrc = (Bitmap)bitmap.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            int stride2 = stride * 2;
            int stride3 = stride * 3;
            int stride4 = stride * 4;
            int stride5 = stride * 5;
            int stride6 = stride * 6;

            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = stride - bitmap.Width * 3;
                // 4 piksela manje jer se od trenutnog uzimaju 3 sa leve i desne strane
                int nWidth = bitmap.Width - 4;
                int nHeight = bitmap.Height - 4;

                int nPixel;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        for (int col = 0; col < 3; col++) // col=0 - crvena, col=1 - zelena, col=2 - plava
                        {
                            nPixel =
                                (((pSrc[2 - col] * a) +
                                (pSrc[5 - col] * a) +
                                (pSrc[8 - col] * 0) +
                                (pSrc[11 - col] * b) +
                                (pSrc[14 - col] * b) +
                                (pSrc[17 - col] * 0) +
                                (pSrc[20 - col] * c) +

                                (pSrc[2 + stride - col] * 0) +
                                (pSrc[5 + stride - col] * a) +
                                (pSrc[8 + stride - col] * a) +
                                (pSrc[11 + stride - col] * b) +
                                (pSrc[14 + stride - col] * b) +
                                (pSrc[17 + stride - col] * c) +
                                (pSrc[20 + stride - col] * c) +

                                (pSrc[2 + stride2 - col] * d) +
                                (pSrc[5 + stride2 - col] * d) +
                                (pSrc[8 + stride2 - col] * a) +
                                (pSrc[11 + stride2 - col] * b) +
                                (pSrc[14 + stride2 - col] * c) +
                                (pSrc[17 + stride2 - col] * c) +
                                (pSrc[20 + stride2 - col] * 0) +

                                (pSrc[2 + stride3 - col] * d) +
                                (pSrc[5 + stride3 - col] * d) +
                                (pSrc[8 + stride3 - col] * d) +
                                (pSrc[11 + stride3 - col] * e) +
                                (pSrc[14 + stride3 - col] * f) +
                                (pSrc[17 + stride3 - col] * f) +
                                (pSrc[20 + stride3 - col] * f) +


                                (pSrc[2 + stride4 - col] * 0) +
                                (pSrc[5 + stride4 - col] * g) +
                                (pSrc[8 + stride4 - col] * g) +
                                (pSrc[11 + stride4 - col] * h) +
                                (pSrc[14 + stride4 - col] * i) +
                                (pSrc[17 + stride4 - col] * f) +
                                (pSrc[20 + stride4 - col] * f) +

                                (pSrc[2 + stride5 - col] * g) +
                                (pSrc[5 + stride5 - col] * g) +
                                (pSrc[8 + stride5 - col] * h) +
                                (pSrc[11 + stride5 - col] * h) +
                                (pSrc[14 + stride5 - col] * i) +
                                (pSrc[17 + stride5 - col] * i) +
                                (pSrc[20 + stride5 - col] * 0) +

                                (pSrc[2 + stride6 - col] * g) +
                                (pSrc[5 + stride6 - col] * 0) +
                                (pSrc[8 + stride6 - col] * h) +
                                (pSrc[11 + stride6 - col] * h) +
                                (pSrc[14 + stride6 - col] * 0) +
                                (pSrc[17 + stride6 - col] * i) +
                                (pSrc[20 + stride6 - col] * i)) / Factor) + Offset;

                            if (nPixel < 0) nPixel = 0;
                            if (nPixel > 255) nPixel = 255;

                            p[11 + stride3 - col] = (byte)nPixel;
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
