using System.Drawing;

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
    }
}
