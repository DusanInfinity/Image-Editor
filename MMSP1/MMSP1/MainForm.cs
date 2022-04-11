using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MMSP1
{
    public partial class MainForm : Form
    {
        private bool IsMultiplePictureViewActive = true;
        private Bitmap BitmapImg = null;


        public MainForm()
        {
            InitializeComponent();

            ToggleMultipleView();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void prikazKanalskihSlikaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleMultipleView();
        }


        private void ToggleMultipleView()
        {
            if (IsMultiplePictureViewActive)
            {
                var size = new System.Drawing.Size(506, 387); //prikaz jedne slike
                MinimumSize = size;
                MaximumSize = size;
                Size = size;

                pictureBoxRed.Hide();
                pictureBoxGreen.Hide();
                pictureBoxBlue.Hide();
            }
            else
            {
                var size = new System.Drawing.Size(994, 713); //prikaz vise slika
                MinimumSize = size;
                MaximumSize = size;
                Size = size;

                pictureBoxRed.Show();
                pictureBoxGreen.Show();
                pictureBoxBlue.Show();
            }

            IsMultiplePictureViewActive = !IsMultiplePictureViewActive;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFD.ShowDialog();
            if (result == DialogResult.OK)
            {
                string imgName = openFD.FileName;

                mainPictureBox.Image = Image.FromFile(imgName);

                BitmapImg = (Bitmap)Bitmap.FromFile(imgName);

                RefreshMultipleView();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mainPictureBox.Image == null) return;

            DialogResult result = saveFD.ShowDialog();
            if (result == DialogResult.OK)
            {
                string[] fileNameSplit = saveFD.FileName.Split(".");
                string formatStr = fileNameSplit[fileNameSplit.Length - 1].ToLower();

                ImageFormat chosenFormat;
                if (formatStr == "bmp")
                    chosenFormat = ImageFormat.Bmp;
                else if (formatStr == "jpg")
                    chosenFormat = ImageFormat.Jpeg;
                else if (formatStr == "png")
                    chosenFormat = ImageFormat.Png;
                else
                {
                    ShowError($"Izabrani format \'{formatStr}\' nije validan!");
                    return;
                }

                mainPictureBox.Image.Save(saveFD.FileName, chosenFormat);
            }
        }

        private void ShowError(string text, string title = "GRESKA")
        {
            MessageBox.Show(this, text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void RefreshMultipleView()
        {
            if (!IsMultiplePictureViewActive) return;

            CalculateChannelPictureByMask(pictureBoxRed, new byte[] { 255, 0, 0 });
            CalculateChannelPictureByMask(pictureBoxGreen, new byte[] { 0, 255, 0 });
            CalculateChannelPictureByMask(pictureBoxBlue, new byte[] { 0, 0, 255 });
        }

        private void CalculateChannelPictureByMask(PictureBox pictureBox, byte[] mask)
        {
            if (BitmapImg == null || !IsMultiplePictureViewActive) return;

            Bitmap b = (Bitmap)BitmapImg.Clone();

            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    System.Drawing.Color c = b.GetPixel(i, j);
                    b.SetPixel(i, j, Color.FromArgb(c.R & mask[0], c.G & mask[1], c.B & mask[2]));
                }
            }

            pictureBox.Image = b;
        }

    }
}
