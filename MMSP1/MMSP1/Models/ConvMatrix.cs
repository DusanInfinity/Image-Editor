using System.Drawing;
using System.Drawing.Imaging;

namespace MMSP1.Models
{
    public class ConvMatrix
    {
        public int TopLeft = 0, TopMid = 0, TopRight = 0;
        public int MidLeft = 0, Pixel = 1, MidRight = 0;
        public int BottomLeft = 0, BottomMid = 0, BottomRight = 0;
        public int Factor = 1;
        public int Offset = 0;
        public void SetAll(int nVal)
        {
            TopLeft = TopMid = TopRight = MidLeft = Pixel = MidRight = BottomLeft = BottomMid = BottomRight = nVal;
        }

        public bool Conv3x3(Bitmap b)
        {
            // Avoid divide by zero errors
            if (0 == Factor) return false;

            Bitmap bSrc = (Bitmap)b.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            int stride2 = stride * 2;
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width - 2;
                int nHeight = b.Height - 2;

                int nPixel;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nPixel = ((((pSrc[2] * TopLeft) + (pSrc[5] * TopMid) + (pSrc[8] * TopRight) +
                            (pSrc[2 + stride] * MidLeft) + (pSrc[5 + stride] * Pixel) + (pSrc[8 + stride] * MidRight) +
                            (pSrc[2 + stride2] * BottomLeft) + (pSrc[5 + stride2] * BottomMid) + (pSrc[8 + stride2] * BottomRight)) / Factor) + Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[5 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[1] * TopLeft) + (pSrc[4] * TopMid) + (pSrc[7] * TopRight) +
                            (pSrc[1 + stride] * MidLeft) + (pSrc[4 + stride] * Pixel) + (pSrc[7 + stride] * MidRight) +
                            (pSrc[1 + stride2] * BottomLeft) + (pSrc[4 + stride2] * BottomMid) + (pSrc[7 + stride2] * BottomRight)) / Factor) + Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[4 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[0] * TopLeft) + (pSrc[3] * TopMid) + (pSrc[6] * TopRight) +
                            (pSrc[0 + stride] * MidLeft) + (pSrc[3 + stride] * Pixel) + (pSrc[6 + stride] * MidRight) +
                            (pSrc[0 + stride2] * BottomLeft) + (pSrc[3 + stride2] * BottomMid) + (pSrc[6 + stride2] * BottomRight)) / Factor) + Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[3 + stride] = (byte)nPixel;

                        p += 3;
                        pSrc += 3;
                    }
                    p += nOffset;
                    pSrc += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return true;
        }
    }
}
