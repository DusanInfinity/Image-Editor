using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MMSP1.Models
{
    public enum ReduceType : byte
    {
        ReduceRedGreen = 1, // NormalBlue
        ReduceRedBlue = 2, // NormalGreen
        ReduceGreenBlue = 3, // NormalRed
    }

    public class ElfakBitmap
    {
        public byte[] Data { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public ReduceType ReduceType { get; private set; }

        public ElfakBitmap(int width, int height, byte[] data, ReduceType reduceType)
        {
            Width = width;
            Height = height;
            Data = data;
            ReduceType = reduceType;
        }

        public static ElfakBitmap FromBitmap(Bitmap bmp, ReduceType reduceType)
        {
            int width = bmp.Width;
            int height = bmp.Height;

            byte[] data = new byte[width * 3 * height];

            switch (reduceType)
            {
                case ReduceType.ReduceRedGreen:
                    {
                        int bytePos = 0;
                        for (int y = 0; y < bmp.Height; y++)
                        {
                            for (int x = 0; x < bmp.Width; x++)
                            {
                                Color currPixel = bmp.GetPixel(x, y);
                                if (x % 2 == 0)
                                {
                                    data[bytePos + 2] = currPixel.R;
                                    data[bytePos + 1] = currPixel.G;
                                    data[bytePos] = currPixel.B;
                                    bytePos += 3;
                                }
                                else
                                {
                                    data[bytePos] = currPixel.B;
                                    bytePos += 1;
                                }
                            }
                        }
                    }
                    break;
                case ReduceType.ReduceRedBlue:
                    {
                        int bytePos = 0;
                        for (int y = 0; y < bmp.Height; y++)
                        {
                            for (int x = 0; x < bmp.Width; x++)
                            {
                                Color currPixel = bmp.GetPixel(x, y);
                                if (x % 2 == 0)
                                {
                                    data[bytePos + 2] = currPixel.R;
                                    data[bytePos + 1] = currPixel.G;
                                    data[bytePos] = currPixel.B;
                                    bytePos += 3;
                                }
                                else
                                {
                                    data[bytePos] = currPixel.G;
                                    bytePos += 1;
                                }
                            }
                        }
                    }
                    break;
                case ReduceType.ReduceGreenBlue:
                    {
                        int bytePos = 0;
                        for (int y = 0; y < bmp.Height; y++)
                        {
                            for (int x = 0; x < bmp.Width; x++)
                            {
                                Color currPixel = bmp.GetPixel(x, y);
                                if (x % 2 == 0)
                                {
                                    data[bytePos + 2] = currPixel.R;
                                    data[bytePos + 1] = currPixel.G;
                                    data[bytePos] = currPixel.B;
                                    bytePos += 3;
                                }
                                else
                                {
                                    data[bytePos] = currPixel.R;
                                    bytePos += 1;
                                }
                            }
                        }
                    }
                    break;
            }

            ElfakBitmap elfakBitmap = new ElfakBitmap(width, height, data, reduceType);
            return elfakBitmap;
        }

        public Bitmap ToBitmap()
        {
            Bitmap bmp = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);

            switch (ReduceType)
            {
                case ReduceType.ReduceRedGreen:
                    {
                        int bytePos = 0;
                        for (int y = 0; y < bmp.Height; y++)
                        {
                            for (int x = 0; x < bmp.Width; x++)
                            {
                                if (x % 2 == 0)
                                {
                                    bmp.SetPixel(x, y, Color.FromArgb(Data[bytePos + 2], Data[bytePos + 1], Data[bytePos]));
                                    bytePos += 3;
                                }
                                else
                                {

                                    Color prevPixel = bmp.GetPixel(x - 1, y);
                                    bmp.SetPixel(x, y, Color.FromArgb(prevPixel.R, prevPixel.G, Data[bytePos]));
                                    bytePos += 1;
                                }
                            }
                        }
                    }
                    break;
                case ReduceType.ReduceRedBlue:
                    {
                        int bytePos = 0;
                        for (int y = 0; y < bmp.Height; y++)
                        {
                            for (int x = 0; x < bmp.Width; x++)
                            {
                                if (x % 2 == 0)
                                {
                                    bmp.SetPixel(x, y, Color.FromArgb(Data[bytePos + 2], Data[bytePos + 1], Data[bytePos]));
                                    bytePos += 3;
                                }
                                else
                                {

                                    Color prevPixel = bmp.GetPixel(x - 1, y);
                                    bmp.SetPixel(x, y, Color.FromArgb(prevPixel.R, Data[bytePos], prevPixel.B));
                                    bytePos += 1;
                                }
                            }
                        }
                    }
                    break;
                case ReduceType.ReduceGreenBlue:
                    {
                        int bytePos = 0;
                        for (int y = 0; y < bmp.Height; y++)
                        {
                            for (int x = 0; x < bmp.Width; x++)
                            {
                                if (x % 2 == 0)
                                {
                                    bmp.SetPixel(x, y, Color.FromArgb(Data[bytePos + 2], Data[bytePos + 1], Data[bytePos]));
                                    bytePos += 3;
                                }
                                else
                                {

                                    Color prevPixel = bmp.GetPixel(x - 1, y);
                                    bmp.SetPixel(x, y, Color.FromArgb(Data[bytePos], prevPixel.G, prevPixel.B));
                                    bytePos += 1;
                                }
                            }
                        }
                    }
                    break;
            }

            return bmp;
        }

        public bool Save(string fileName)
        {
            using (BinaryWriter sw = new BinaryWriter(File.OpenWrite(fileName)))
            {
                //sw.Write((byte)0x42);
                //sw.Write((byte)0x4D);
                sw.Write(Data.Length);
                sw.Write(Width);
                sw.Write(Height);
                sw.Write((byte)ReduceType);
                sw.Write(Data);
            }

            return true;
        }

        public static ElfakBitmap Load(string fileName)
        {
            ElfakBitmap elfakBitmap = null;

            using (BinaryReader sr = new BinaryReader(File.OpenRead(fileName)))
            {
                //sr.ReadByte(); // 0x42
                //sr.ReadByte(); // 0x4D
                int length = sr.ReadInt32();
                int width = sr.ReadInt32();
                int height = sr.ReadInt32();
                ReduceType reduceType = (ReduceType)sr.ReadByte();
                byte[] data = sr.ReadBytes(length);

                elfakBitmap = new ElfakBitmap(width, height, data, reduceType);
            }

            return elfakBitmap;
        }
    }
}
