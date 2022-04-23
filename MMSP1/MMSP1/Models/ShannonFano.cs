using MMSP1.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MMSP1.Models
{
    // Iskoriscena implementacija za laboratorijsku vezbu iz predmeta Projektovanje i analiza algoritama

    // Izmenjena za potrebe ove lab. vezbe
    public class ShannonFano
    {
        public byte Char { get; set; }
        public string Code { get; set; }
        public int AppearenceNum { get; set; }
        public float Frequency { get; set; }

        public ShannonFano(byte ch)
        {
            Char = ch;
            AppearenceNum = 1;
            Frequency = 0.0f;
            Code = "";
        }

        #region Shannon Fano
        public static void CompressFile(string fileName, byte[] data)
        {
            /*byte[] data;// = new byte[] { 5, 8, 3, 5, 2, 1, 5, 8 };
            using (var ms = new MemoryStream())
            {
                File.OpenRead(fileName).CopyTo(ms);
                data = ms.ToArray();
            } */

            Dictionary<byte, ShannonFano> AllCharacters = new Dictionary<byte, ShannonFano>();
            int charactersNum = 0;
            foreach (byte ch in data)
            {
                charactersNum++;

                if (AllCharacters.TryGetValue(ch, out ShannonFano sf))
                {
                    sf.AppearenceNum++;
                    continue;
                }

                AllCharacters.Add(ch, new ShannonFano(ch));
            }

            int charNum = AllCharacters.Count;
            SortList sortedChars = new SortList(charNum);

            foreach (var kvp in AllCharacters)
            {
                ShannonFano sf = kvp.Value;
                sf.Frequency = (float)sf.AppearenceNum / charactersNum;
                sortedChars.Add(sf);
            }

            SfRecursive(sortedChars);

            StringBuilder sbEncodedText = new StringBuilder();
            int bitsNum = 0;
            foreach (byte ch in data)
            {
                sbEncodedText.Append(AllCharacters[ch].Code);
                bitsNum += AllCharacters[ch].Code.Length;
            }
            string encodedStr = sbEncodedText.ToString();


            StringBuilder allCharCodeSB = new StringBuilder();
            foreach (var kvp in AllCharacters)
            {
                allCharCodeSB.Append($"{kvp.Value.Char}={kvp.Value.Code} ");
            }
            allCharCodeSB.Append(bitsNum);

            byte[] stringAsBytes = Encoding.Unicode.GetBytes(allCharCodeSB.ToString());


            using (BinaryWriter sw = new BinaryWriter(File.Open(fileName, FileMode.Create), Encoding.ASCII))
            {
                sw.Write(stringAsBytes.Length);
                sw.Write(stringAsBytes);

                byte[] array = GetBytes(encodedStr);
                sw.Write(array);
            }
        }

        public static byte[] DecompressFile(string fileName)
        {
            StringBuilder data = new StringBuilder();
            Dictionary<string, byte> AllCharacters = new Dictionary<string, byte>();

            int readedBytes = 0;
            int bitNum = 0;
            using (BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.Open), Encoding.ASCII))
            {
                int codesBytesNum = br.ReadInt32();
                byte[] allCharCodeBytes = br.ReadBytes(codesBytesNum);

                string str = Encoding.Unicode.GetString(allCharCodeBytes);
                string[] textSplit = str.Split(' ');

                foreach (string ch in textSplit)
                {
                    string[] sfArray = ch.Split('=');
                    if (sfArray.Length == 2)
                    {
                        AllCharacters.Add(sfArray[1], Convert.ToByte(sfArray[0]));
                    }
                }
                bitNum = int.Parse(textSplit[textSplit.Length - 1]);

                int bytesNum = bitNum / 8 + ((bitNum % 8 != 0) ? 1 : 0);
                while (readedBytes < bytesNum && br.BaseStream.Position < br.BaseStream.Length)
                {
                    data.Append(Convert.ToString(br.ReadByte(), 2).PadLeft(8, '0'));
                    readedBytes++;
                }
            }

            int dataLength = data.Length;

            string code = "";
            List<byte> decompressedData = new List<byte>();
            for (int i = 0; i < dataLength && i < bitNum; i++)
            {
                code += data[i];
                if (AllCharacters.TryGetValue(code, out byte ch))
                {
                    decompressedData.Add(ch);
                    code = ""; // resetujemo kod
                }
            }

            return decompressedData.ToArray();
        }

        public static byte[] GetBytes(string bitString)
        { // https://stackoverflow.com/questions/2989695/how-to-convert-a-string-of-bits-to-byte-array
            int strlen = bitString.Length;
            int size = strlen / 8 + ((strlen % 8 != 0) ? 1 : 0); // strlen/8 zbog bajtova i ukoliko strlen%8 nije 0 to znaci da imamo jos jedan bajt koji nije celi(manje od 8 bitova) pa ga dopunjavamo nulama
            byte[] output = new byte[size];

            for (int i = 0; i < size; i++)
            {
                for (int b = 0; b <= 7; b++)
                {
                    char ch = (strlen > i * 8 + b) ? bitString[i * 8 + b] : '0'; // ukoliko je prekoracenje, postavljamo 0 (za poslednji bajt, ukoliko strlen%8 nije 0
                    output[i] |= (byte)((ch == '1' ? 1 : 0) << (7 - b));
                }
            }

            return output;
        }

        public static void SfRecursive(SortList list)
        {
            int elNum = list.Count;
            if (elNum < 2)
                return;

            SortList leftList = new SortList(elNum);
            SortList rightList = new SortList(elNum);

            ShannonFano firstElement = list[0];

            leftList.Add(firstElement); // prvi cvor je najveci i sigurno ide u levu listu
            firstElement.Code += "0"; // kod 0

            int addedLeft = 1;
            float freqLeft = firstElement.Frequency;
            float freqRight = 0;

            int i = elNum - 1; // indeks poslednjeg elementa u listi
            while (i >= addedLeft)
            {
                ShannonFano currEl = list[i]; // naredni element sa desne nalevo
                float elementFreq = currEl.Frequency;

                ShannonFano nextEl = list[addedLeft]; // naredni element sa leve strane
                float nextElFreq = nextEl.Frequency;

                float addToLeftDiff = freqLeft + nextElFreq; // vrednost ukupne frekvencije ako dodamo naredni levi element u levu listu
                float addToRightDiff = freqRight + elementFreq; // vrednost ukupne frekvencije ako dodamo naredni desni element u desnu listu

                if (Math.Abs(addToLeftDiff - freqRight) > Math.Abs(freqLeft - addToRightDiff))
                { // ako je razlika izmedju leve i desne liste u slucaju dodavanja u levu listu veca od razlike prilikom dodavanja u desnu listu, dodajemo u desnu
                    rightList.Add(currEl);
                    freqRight += elementFreq;
                    currEl.Code += "1";
                    i--; // dekrementiramo i, jer smo dodali dati element u listu
                }
                else
                {
                    leftList.Add(nextEl);
                    freqLeft += nextElFreq;
                    nextEl.Code += "0";
                    addedLeft++;
                }
            }

            SfRecursive(leftList);
            SfRecursive(rightList);
        }

        #endregion
    }


}
