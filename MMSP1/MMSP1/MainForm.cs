using MMSP1.Extensions;
using MMSP1.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MMSP1
{
    public partial class MainForm : Form
    {
        private bool IsMultiplePictureViewActive = true;
        private Bitmap BitmapImg = null;
        private readonly LinkedList<Bitmap> UndoBuffer = new LinkedList<Bitmap>();
        private readonly LinkedList<Bitmap> RedoBuffer = new LinkedList<Bitmap>();
        private int BufferCapacity = 10;


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

            RefreshMultipleView();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFD.ShowDialog();
            if (result == DialogResult.OK)
            {
                string imgName = openFD.FileName;
                LoadImage((Bitmap)Bitmap.FromFile(imgName));
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

        private void LoadImage(Bitmap img)
        {
            mainPictureBox.Image = img;
            BitmapImg = img;
            RefreshMultipleView();
        }

        private void RefreshMultipleView()
        {
            if (BitmapImg == null || !IsMultiplePictureViewActive) return;

            pictureBoxRed.Image = BMapFilters.CalculateChannelByMask(BitmapImg, new byte[] { 255, 0, 0 });
            pictureBoxGreen.Image = BMapFilters.CalculateChannelByMask(BitmapImg, new byte[] { 0, 255, 0 });
            pictureBoxBlue.Image = BMapFilters.CalculateChannelByMask(BitmapImg, new byte[] { 0, 0, 255 });
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            if (UndoBuffer.Count < 1) return;

            Bitmap img = UndoBuffer.Last.Value;
            UndoBuffer.RemoveLast();
            //RedoBuffer.AddElementAtEndAndRemoveFirstIfBufferOverflow(img, BufferSize);
            RedoBuffer.AddLast(BitmapImg); // Po uslovu zadatka, redo buffer nije limitiran
            LoadImage(img);
        }

        private void Redo_Click(object sender, EventArgs e)
        {
            if (RedoBuffer.Count < 1) return;

            Bitmap img = RedoBuffer.Last.Value;
            RedoBuffer.RemoveLast();
            UndoBuffer.AddElementAtEndAndRemoveFirstIfBufferOverflow(BitmapImg, BufferCapacity);
            LoadImage(img);
        }

        private void RegisterNewUndoAction(Bitmap img)
        {
            if (RedoBuffer.Count > 0)
                RedoBuffer.Clear();

            UndoBuffer.AddElementAtEndAndRemoveFirstIfBufferOverflow(img, BufferCapacity);
        }

        public void SetBufferCapacity(int newCapacity)
        {
            if (BufferCapacity == newCapacity) return;

            if (newCapacity < BufferCapacity)
            {
                int actionsNumToRemoveFromBuffer = UndoBuffer.Count - newCapacity;

                while (actionsNumToRemoveFromBuffer > 0)
                {
                    UndoBuffer.RemoveFirst();
                    actionsNumToRemoveFromBuffer--;
                }
            }

            BufferCapacity = newCapacity;
        }

        private void podesavanjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm(BufferCapacity);
            form.ShowDialog(this);
        }

        private void gammaFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili filter, prvo morate učitati sliku!");
                return;
            }

            GammaFilterInputForm gammaInput = new GammaFilterInputForm();
            if (gammaInput.ShowDialog() == DialogResult.OK)
            {
                if (BMapFilters.Gamma(BitmapImg, gammaInput.R, gammaInput.G, gammaInput.B, out Bitmap generatedBitmap))
                {
                    RegisterNewUndoAction(BitmapImg);
                    LoadImage(generatedBitmap);
                }
            }
        }

        private void sharpenFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
