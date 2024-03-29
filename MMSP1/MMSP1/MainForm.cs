﻿using MMSP1.Extensions;
using MMSP1.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
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
        private bool RunFiltersInWin32Core = false;
        private bool IsChartsViewActive = false;
        private ElfakBitmap[] Downsamples = null;
        private bool IsChoosingPixelActive = false;

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

        private void ResizePictureBoxByImageSize()
        {
            var size = BitmapImg != null ? new System.Drawing.Size(BitmapImg.Width, BitmapImg.Height) : new System.Drawing.Size(830, 557);
            mainPictureBox.MinimumSize = size;
            mainPictureBox.MaximumSize = size;
            mainPictureBox.Size = size;
        }

        private void ToggleChannelPictureBoxes(bool toggle)
        {
            if (toggle)
            {
                pictureBoxRed.Show();
                pictureBoxGreen.Show();
                pictureBoxBlue.Show();
            }
            else
            {
                pictureBoxRed.Hide();
                pictureBoxGreen.Hide();
                pictureBoxBlue.Hide();
            }
        }

        private void ActivateView2WithPictureBoxes()
        {
            if (IsChartsViewActive)
            {
                ToggleCharts(false);
                IsChartsViewActive = false;
                prikazHistogramaToolStripMenuItem.Checked = false;

                if (IsMultiplePictureViewActive)
                    ToggleChannelPictureBoxes(true);
            }

            if (!IsMultiplePictureViewActive)
                ToggleMultipleView();
        }

        private void ToggleMultipleView()
        {
            Downsamples = null;

            if (IsMultiplePictureViewActive)
            { //prikaz jedne slike
                ResizePictureBoxByImageSize();

                MinimumSize = new Size(0, 0);
                MaximumSize = new Size(0, 0);

                ToggleChannelPictureBoxes(false);
                ToggleCharts(false);
            }
            else
            {
                var size = new System.Drawing.Size(412, 275);
                mainPictureBox.MinimumSize = size;
                mainPictureBox.MaximumSize = size;
                mainPictureBox.Size = size;

                size = new Size(855, 623); //prikaz vise slika
                MinimumSize = size;
                MaximumSize = size;
                Size = size;
                if (WindowState == FormWindowState.Maximized)
                    WindowState = FormWindowState.Normal;


                if (IsChartsViewActive)
                    ToggleCharts(true);
                else
                    ToggleChannelPictureBoxes(true);
            }

            IsMultiplePictureViewActive = !IsMultiplePictureViewActive;
            prikazKanalskihSlikaToolStripMenuItem.Checked = IsMultiplePictureViewActive;
            RefreshMultipleView();
        }

        private void RefreshMultipleView()
        {
            if (BitmapImg == null || !IsMultiplePictureViewActive) return;

            //if (RunFiltersInWin32Core) // u svrhu testiranja, ova promenljiva se odnosi samo na osnovni filter
            //{
            pictureBoxRed.Image = BMapFilters.CalculateChannelByMaskUnsafe(BitmapImg, new byte[] { 255, 0, 0 });
            pictureBoxGreen.Image = BMapFilters.CalculateChannelByMaskUnsafe(BitmapImg, new byte[] { 0, 255, 0 });
            pictureBoxBlue.Image = BMapFilters.CalculateChannelByMaskUnsafe(BitmapImg, new byte[] { 0, 0, 255 });
            /*}
            else
            {
                pictureBoxRed.Image = BMapFilters.CalculateChannelByMask(BitmapImg, new byte[] { 255, 0, 0 });
                pictureBoxGreen.Image = BMapFilters.CalculateChannelByMask(BitmapImg, new byte[] { 0, 255, 0 });
                pictureBoxBlue.Image = BMapFilters.CalculateChannelByMask(BitmapImg, new byte[] { 0, 0, 255 });
            } */

            RefreshChannelHistograms();
        }

        private void RefreshChannelHistograms()
        {
            if (!IsMultiplePictureViewActive || !IsChartsViewActive || pictureBoxRed.Image == null || pictureBoxGreen.Image == null || pictureBoxBlue.Image == null)
                return;

            List<int> xValues = new List<int>(256);
            for (int i = 0; i < 256; i++)
                xValues.Add(i);


            List<int> redValues = Histogram.GetHistogramValues((Bitmap)pictureBoxRed.Image, ChannelColor.Red);
            chartRed.Series["Series1"].Points.DataBindXY(xValues, redValues);

            List<int> greenValues = Histogram.GetHistogramValues((Bitmap)pictureBoxGreen.Image, ChannelColor.Green);
            chartGreen.Series["Series1"].Points.DataBindXY(xValues, greenValues);

            List<int> blueValues = Histogram.GetHistogramValues((Bitmap)pictureBoxBlue.Image, ChannelColor.Blue);
            chartBlue.Series["Series1"].Points.DataBindXY(xValues, blueValues);

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFD.Filter = "Image Files(*.bmp;*.jpg;*.png;*.elfbmp)|*.bmp;*.jpg;*.png;*.elfbmp";
            DialogResult result = openFD.ShowDialog();
            if (result == DialogResult.OK)
            {
                string imgName = openFD.FileName;
                Bitmap bmp;
                if (imgName.EndsWith(".elfbmp"))
                    bmp = ElfakBitmap.Load(imgName).ToBitmap();
                else
                    bmp = (Bitmap)Bitmap.FromFile(imgName);

                LoadImage(bmp);
                if (!IsMultiplePictureViewActive)
                    ResizePictureBoxByImageSize();

                RedoBuffer.Clear();
                UndoBuffer.Clear();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mainPictureBox.Image == null) return;

            saveFD.Filter = "PNG format|*.png|JPG format|*.jpg|BMP format|*.bmp";
            DialogResult result = saveFD.ShowDialog();
            if (result == DialogResult.OK)
            {
                string[] fileNameSplit = saveFD.FileName.Split('.');
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
                ShowInfo("Uspesno ste sacuvali sliku!");
            }
        }

        private void ShowError(string text, string title = "GRESKA")
        {
            MessageBox.Show(this, text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowInfo(string text, string title = "INFO")
        {
            MessageBox.Show(this, text, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadImage(Bitmap img)
        {
            mainPictureBox.Image = img;
            BitmapImg = img;
            RefreshMultipleView();
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
                if (RunFiltersInWin32Core)
                {
                    if (BMapFilters.GammaUnsafe(BitmapImg, gammaInput.R, gammaInput.G, gammaInput.B, out Bitmap generatedBitmap))
                    {
                        RegisterNewUndoAction(BitmapImg);
                        LoadImage(generatedBitmap);
                    }
                }
                else
                {
                    if (BMapFilters.Gamma(BitmapImg, gammaInput.R, gammaInput.G, gammaInput.B, out Bitmap generatedBitmap))
                    {
                        RegisterNewUndoAction(BitmapImg);
                        LoadImage(generatedBitmap);
                    }
                }
            }
        }

        private void sharpenFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili filter, prvo morate učitati sliku!");
                return;
            }

            if (BMapFilters.Sharpen(BitmapImg, 11, out Bitmap generatedBitmap))
            {
                RegisterNewUndoAction(BitmapImg);
                LoadImage(generatedBitmap);
            }
        }

        private void izvrsenjeUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunFiltersInWin32Core = !RunFiltersInWin32Core;
            stripItemRunInWin32.Checked = RunFiltersInWin32Core;
        }

        private void pixelateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili filter, prvo morate učitati sliku!");
                return;
            }

            InputForm inputForm = new InputForm();
            inputForm.SetTitle("Pixelate");
            inputForm.SetText("Unesite velicinu piksela:");
            inputForm.SetInputValue("15");
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                string input = inputForm.GetInputValue();
                if (short.TryParse(input, out short pixelSize) && pixelSize > 0)
                {
                    if (BMapFilters.Pixelate(BitmapImg, out Bitmap generatedBitmap, pixelSize))
                    {
                        RegisterNewUndoAction(BitmapImg);
                        LoadImage(generatedBitmap);
                    }
                }
                else
                    ShowError("Niste uneli validnu velicinu piksela!");
            }
        }

        private void promenljiviKonvulcioniFiltriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili filter, prvo morate učitati sliku!");
                return;
            }

            SharpenInput inputForm = new SharpenInput();
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                string matrixSize = inputForm.GetMatrixSize();

                if (matrixSize != "3x3" && matrixSize != "5x5" & matrixSize != "7x7")
                {
                    ShowError("Niste uneli validnu velicinu matrice!");
                    return;
                }

                int nWeight = inputForm.GetnWeight();

                if (BMapFilters.Sharpen(BitmapImg, nWeight, out Bitmap generatedBitmap, matrixSize))
                {
                    RegisterNewUndoAction(BitmapImg);
                    LoadImage(generatedBitmap);
                }
            }
        }

        private void edgeEnhanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili filter, prvo morate učitati sliku!");
                return;
            }

            InputForm inputForm = new InputForm();
            inputForm.SetTitle("EdgeEnhance");
            inputForm.SetText("Unesite vrednost za \r\nThreshold:");
            inputForm.SetInputValue("0");
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                string input = inputForm.GetInputValue();
                if (byte.TryParse(input, out byte threshold) && threshold >= 0)
                {
                    if (BMapFilters.EdgeEnhance(BitmapImg, threshold, out Bitmap generatedBitmap))
                    {
                        RegisterNewUndoAction(BitmapImg);
                        LoadImage(generatedBitmap);
                    }
                }
                else
                    ShowError("Niste uneli validnu vrednost za threshold!");
            }
        }

        private void kreiranjeBMPSlikeSa256IndeksiranihBojaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili filter, prvo morate učitati sliku!");
                return;
            }


            if (BMapFilters.Create256ColorBMP(BitmapImg, out Bitmap generatedBitmap))
            {
                RegisterNewUndoAction(BitmapImg);
                LoadImage(generatedBitmap);
            }
        }

        private void ToggleCharts(bool show)
        {
            if (show)
            {
                chartRed.Show();
                chartGreen.Show();
                chartBlue.Show();
            }
            else
            {
                chartRed.Hide();
                chartGreen.Hide();
                chartBlue.Hide();
            }
        }


        private void prikazHistogramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsChartsViewActive = !IsChartsViewActive;
            RefreshMultipleView();

            if (IsChartsViewActive)
            {
                if (IsMultiplePictureViewActive)
                {
                    ToggleChannelPictureBoxes(false);
                    ToggleCharts(true);
                }
                else
                    MessageBox.Show(this, "Aktivirali ste prikaz histograma, ali još uvek nije aktivan prikaz slika po kanalima. Aktivirajte ga da biste videli histograme (Filters->Prikaz kanalskih slika)", "NAPOMENA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (IsMultiplePictureViewActive)
                {
                    ToggleChannelPictureBoxes(true);
                    ToggleCharts(false);
                }
            }

            prikazHistogramaToolStripMenuItem.Checked = IsChartsViewActive;
        }

        private void minMaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili filter, prvo morate učitati sliku!");
                return;
            }

            HistogramMinMaxInput inputForm = new HistogramMinMaxInput();
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                ChannelColor channel = inputForm.GetSelectedChannel();
                byte min = inputForm.GetMin();
                byte max = inputForm.GetMax();
                if (min == 0 && max == 255) return; // nema svrhe da obradjujemo sliku ako nije promenjen opseg
                if (min <= max)
                {
                    if (Histogram.MinMaxFilter(BitmapImg, channel, min, max, out Bitmap generatedBitmap))
                    {
                        RegisterNewUndoAction(BitmapImg);
                        LoadImage(generatedBitmap);
                    }
                }
                else
                    ShowError("Niste uneli validan opseg Min-Max vrednosti!");
            }
        }

        private void grayscaleView2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili filter, prvo morate učitati sliku!");
                return;
            }


            GrayscaleCoefInput inputForm = new GrayscaleCoefInput();
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                decimal Cr = inputForm.GetCr();
                decimal Cg = inputForm.GetCg();
                decimal Cb = inputForm.GetCb();

                if (Histogram.Grayscale_AritmetickaSredina(BitmapImg, out Bitmap aritmetickaSredina) &&
                Histogram.Grayscale_MaxRGB(BitmapImg, out Bitmap maxRGB) &&
                Histogram.Grayscale_ColorCoef(BitmapImg, Cr, Cg, Cb, out Bitmap colorCoef))
                {
                    ActivateView2WithPictureBoxes();

                    pictureBoxRed.Image = aritmetickaSredina;
                    pictureBoxGreen.Image = maxRGB;
                    pictureBoxBlue.Image = colorCoef;
                }
            }
        }

        private void grayscaleAritmetickaSredinaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili filter, prvo morate učitati sliku!");
                return;
            }


            if (Histogram.Grayscale_AritmetickaSredina(BitmapImg, out Bitmap generatedBitmap))
            {
                RegisterNewUndoAction(BitmapImg);
                LoadImage(generatedBitmap);
            }
        }

        private void grayscaleMaxRGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili filter, prvo morate učitati sliku!");
                return;
            }


            if (Histogram.Grayscale_MaxRGB(BitmapImg, out Bitmap generatedBitmap))
            {
                RegisterNewUndoAction(BitmapImg);
                LoadImage(generatedBitmap);
            }
        }

        private void grayscaleColorCoefToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili filter, prvo morate učitati sliku!");
                return;
            }

            GrayscaleCoefInput inputForm = new GrayscaleCoefInput();
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                decimal Cr = inputForm.GetCr();
                decimal Cg = inputForm.GetCg();
                decimal Cb = inputForm.GetCb();

                if (Histogram.Grayscale_ColorCoef(BitmapImg, Cr, Cg, Cb, out Bitmap generatedBitmap))
                {
                    RegisterNewUndoAction(BitmapImg);
                    LoadImage(generatedBitmap);
                }
            }
        }

        private void orderedDitheringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili filter, prvo morate učitati sliku!");
                return;
            }

            if (DitheringFilters.OrderedDithering(BitmapImg, out Bitmap generatedBitmap))
            {
                RegisterNewUndoAction(BitmapImg);
                LoadImage(generatedBitmap);
            }
        }

        private void jarvisJudiceNinkeDitheringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili filter, prvo morate učitati sliku!");
                return;
            }

            if (DitheringFilters.JarvisJudiceNinkeDitheringUnsafe(BitmapImg, out Bitmap generatedBitmap))
            {
                RegisterNewUndoAction(BitmapImg);
                LoadImage(generatedBitmap);
            }
        }

        private void crossdomainColorizeAlgorithmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili filter, prvo morate učitati sliku!");
                return;
            }

            CrossDomainColorizeInput inputForm = new CrossDomainColorizeInput();
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                short newHue = inputForm.GetHue();
                double newSaturation = inputForm.GetSaturation();

                if (ColorizeFilters.CrossDomainColorize(BitmapImg, newHue, newSaturation, out Bitmap generatedBitmap))
                {
                    RegisterNewUndoAction(BitmapImg);
                    LoadImage(generatedBitmap);
                }
            }
        }

        private void dodajSlikuUzorakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFD.Filter = "Image Files(*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png";
            DialogResult result = openFD.ShowDialog();
            if (result == DialogResult.OK)
            {
                string imgName = openFD.FileName;
                Bitmap bmp = (Bitmap)Bitmap.FromFile(imgName);

                string[] strgs = imgName.Split('\\');
                string fileName = strgs[strgs.Length - 1];

                if (ColorizeFilters.RegisterNewSimpleColorizeMapping(fileName, bmp))
                {
                    MessageBox.Show($"Uspesno ste dodali novo mapiranje sa imenom \'{fileName}\'", "Uspesno dodavanje mapiranja");
                }
            }
        }


        private void pokreniAlgoritamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili filter, prvo morate učitati sliku!");
                return;
            }

            SimpleColorizeInput inputForm = new SimpleColorizeInput();
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                string selectedMapping = inputForm.GetMappingName();

                if (!ColorizeFilters.GetAllSimpleColorizeMappings().Contains(selectedMapping))
                {
                    ShowError("Izabrali ste nepostojece mapiranje!");
                    return;
                }

                if (ColorizeFilters.SimpleColorize(BitmapImg, selectedMapping, out Bitmap generatedBitmap))
                {
                    RegisterNewUndoAction(BitmapImg);
                    LoadImage(generatedBitmap);
                }
            }
        }

        private void downsamplingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili downsampling, prvo morate učitati sliku!");
                return;
            }

            ElfakBitmap elfakBitmapR = ElfakBitmap.FromBitmap(BitmapImg, ReduceType.ReduceGreenBlue);
            ElfakBitmap elfakBitmapG = ElfakBitmap.FromBitmap(BitmapImg, ReduceType.ReduceRedBlue);
            ElfakBitmap elfakBitmapB = ElfakBitmap.FromBitmap(BitmapImg, ReduceType.ReduceRedGreen);

            ActivateView2WithPictureBoxes();

            pictureBoxRed.Image = elfakBitmapR.ToBitmap();
            pictureBoxGreen.Image = elfakBitmapG.ToBitmap();
            pictureBoxBlue.Image = elfakBitmapB.ToBitmap();

            Downsamples = new ElfakBitmap[3]
            {
                elfakBitmapR,
                elfakBitmapG,
                elfakBitmapB
            };
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Downsamples == null)
            {
                ShowError("Da biste sačuvali sliku nakon downsampling-a, potrebno je da prvo pokrenete sam downsampling!");
                return;
            }

            DownsamplingSave inputForm = new DownsamplingSave();
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                int selectedDownsampling = inputForm.GetSelectedDownsampling();
                if (Downsamples != null && selectedDownsampling < Downsamples.Length)
                {
                    saveFD.Filter = "Elfak BMP format|*.elfbmp";
                    DialogResult result = saveFD.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Downsamples[selectedDownsampling].Save(saveFD.FileName);
                        ShowInfo("Uspesno ste sacuvali sliku!");
                    }
                }
            }
        }

        private void kuwaharaFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili filter, prvo morate učitati sliku!");
                return;
            }

            InputForm inputForm = new InputForm();
            inputForm.SetTitle("Kuwahara filter");
            inputForm.SetText("Unesite vrednost za Size:\r\n(Minimalna vrednost je 2)");
            inputForm.SetInputValue("13");
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                string input = inputForm.GetInputValue();
                if (short.TryParse(input, out short size) && size >= 2)
                {
                    if (BMapFilters.KuwaharaFilterUnsafe(BitmapImg, size, out Bitmap generatedBitmap))
                    {
                        RegisterNewUndoAction(BitmapImg);
                        LoadImage(generatedBitmap);
                    }
                }
                else
                    ShowError("Niste uneli validnu vrednost za Size!");
            }

        }

        private void ujednačavanjeBojaUSličnoObojenimZonamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BitmapImg == null)
            {
                ShowError("Da biste primenili filter, prvo morate učitati sliku!");
                return;
            }

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                ShowInfo("Izabrali se boju! Sada kliknite na neku tačku na slici!");

                if (IsMultiplePictureViewActive)
                    ToggleMultipleView();

                IsChoosingPixelActive = true;
            }
        }

        private void mainPictureBox_MouseClick(object sender, MouseEventArgs me)
        {
            if (!IsChoosingPixelActive) return;

            if (IsMultiplePictureViewActive)
            {
                ShowError("Zbog prezicnosti, neophodno je deaktiviranje pregleda po kanalima!");
                ToggleMultipleView();
                return;
            }


            IsChoosingPixelActive = false;

            InputForm inputForm = new InputForm();
            inputForm.SetTitle("Ujednačavanje boja");
            inputForm.SetText("Unesite vrednost za\r\nprag sličnosti:");
            inputForm.SetInputValue("100");
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                string input = inputForm.GetInputValue();
                if (double.TryParse(input, out double similarityThreshold))
                {
                    if (BMapFilters.UnificationOfSimilarColoredZones(BitmapImg, colorDialog.Color, me.X, me.Y, similarityThreshold, out Bitmap generatedBitmap))
                    {
                        RegisterNewUndoAction(BitmapImg);
                        LoadImage(generatedBitmap);
                    }
                }
                else
                    ShowError("Niste uneli validnu vrednost za prag sličnosti!");
            }
        }
    }
}
