using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace MMSP1.Models
{
    public class SimpleColorizeAlgorithm
    {
        public virtual Color GetColor(Color grayColor)
        {
            byte gray = grayColor.R;

            if (gray < 50)
                return Color.FromArgb(0, 0, 0); // crna
            else if (gray < 120)
                return Color.FromArgb(101, 67, 33); // braon
            else if (gray < 160)
                return Color.FromArgb(255, 105, 180); // roze
            else
                return Color.FromArgb(255, 255, 255); // bela
        }
    }

    public class SimpleColorizeAlgorithmAdvanced : SimpleColorizeAlgorithm
    {
        private readonly Dictionary<byte, Color> _mapping = new Dictionary<byte, Color>();

        public override Color GetColor(Color grayColor)
        {
            byte gray = grayColor.R;
            if (_mapping.TryGetValue(gray, out Color color))
                return color;

            return grayColor;
        }


        public static SimpleColorizeAlgorithmAdvanced GenerateMapping(Bitmap bitmap)
        {
            SimpleColorizeAlgorithmAdvanced algorithm = new SimpleColorizeAlgorithmAdvanced();

            int width = bitmap.Width;
            int height = bitmap.Height;

            BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            Dictionary<byte, Dictionary<Color, int>> colorMapping = new Dictionary<byte, Dictionary<Color, int>>();

            for (int i = 0; i < 256; i++)
                colorMapping.Add((byte)i, new Dictionary<Color, int>());

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - width * 3;

                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    { // bgr 
                        byte gray = Convert.ToByte(p[2] * 0.3 + p[1] * 0.59 + p[0] * 0.11);

                        Color currentColor = Color.FromArgb(p[2], p[1], p[0]);

                        if (colorMapping[gray].ContainsKey(currentColor))
                            colorMapping[gray][currentColor]++;
                        else
                            colorMapping[gray].Add(currentColor, 1);

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            bitmap.UnlockBits(bmData);

            // Inicijalizacija mapping-a
            for (int i = 0; i < 256; i++)
                algorithm._mapping.Add((byte)i, Color.FromArgb(i, i, i));


            foreach (var kvp in colorMapping)
            {
                byte gray = kvp.Key;
                Dictionary<Color, int> allColorsForThisShade = kvp.Value;

                // uzimamo neku od boja koja se pojavila najmanji broj puta
                // Prvobitna ideja je bila da uzmem boju koja se najvise puta mapirala na gray sivu nijansu, ali je to testiranjem dalo lose rezultate, mnogo bolje rezultate dobijam kada uzmem neke od boja koje se redje mapiraju, ocigledno da je problem do samih slika - uzoraka

                // Primer odlicnog bojenja je slika 'lena(1).png' i uzorak 'download.png' - spektar boja
                // lena(1).png: https://prnt.sc/a5FR9YzMdU75
                // download.png: https://prnt.sc/Oy_qw2yNO7Vz
                // Obojena lena(rezultat): https://prnt.sc/5QnSUwIyMD4v
                var colors = allColorsForThisShade.OrderBy(x => x.Value).ToList();
                if (colors.Count > 0)
                    algorithm._mapping[gray] = colors[0].Key;
            }

            return algorithm;
        }
    }
}
