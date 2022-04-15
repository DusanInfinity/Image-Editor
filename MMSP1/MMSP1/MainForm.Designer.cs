
namespace MMSP1
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prikazKanalskihSlikaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gammaFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sharpenFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kreiranjeBMPSlikeSa256IndeksiranihBojaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stripItemRunInWin32 = new System.Windows.Forms.ToolStripMenuItem();
            this.naRacunanjeKonvVrednostiUticuPrethodnoIzracunateVrednostiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoRedoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.podesavanjeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dodatniFilteriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.promenljiviKonvulcioniFiltriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeEnhanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pixelateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFD = new System.Windows.Forms.OpenFileDialog();
            this.saveFD = new System.Windows.Forms.SaveFileDialog();
            this.pictureBoxBlue = new System.Windows.Forms.PictureBox();
            this.pictureBoxGreen = new System.Windows.Forms.PictureBox();
            this.pictureBoxRed = new System.Windows.Forms.PictureBox();
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            this.mainStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainStrip
            // 
            this.mainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.filtersToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.undoRedoToolStripMenuItem,
            this.dodatniFilteriToolStripMenuItem});
            this.mainStrip.Location = new System.Drawing.Point(0, 0);
            this.mainStrip.Name = "mainStrip";
            this.mainStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.mainStrip.Size = new System.Drawing.Size(839, 24);
            this.mainStrip.TabIndex = 0;
            this.mainStrip.Text = "Main Menu Strip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prikazKanalskihSlikaToolStripMenuItem,
            this.gammaFilterToolStripMenuItem,
            this.sharpenFilterToolStripMenuItem,
            this.kreiranjeBMPSlikeSa256IndeksiranihBojaToolStripMenuItem});
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.filtersToolStripMenuItem.Text = "Filters";
            // 
            // prikazKanalskihSlikaToolStripMenuItem
            // 
            this.prikazKanalskihSlikaToolStripMenuItem.Name = "prikazKanalskihSlikaToolStripMenuItem";
            this.prikazKanalskihSlikaToolStripMenuItem.Size = new System.Drawing.Size(302, 22);
            this.prikazKanalskihSlikaToolStripMenuItem.Text = "Prikaz kanalskih slika";
            this.prikazKanalskihSlikaToolStripMenuItem.Click += new System.EventHandler(this.prikazKanalskihSlikaToolStripMenuItem_Click);
            // 
            // gammaFilterToolStripMenuItem
            // 
            this.gammaFilterToolStripMenuItem.Name = "gammaFilterToolStripMenuItem";
            this.gammaFilterToolStripMenuItem.Size = new System.Drawing.Size(302, 22);
            this.gammaFilterToolStripMenuItem.Text = "Gamma filter";
            this.gammaFilterToolStripMenuItem.Click += new System.EventHandler(this.gammaFilterToolStripMenuItem_Click);
            // 
            // sharpenFilterToolStripMenuItem
            // 
            this.sharpenFilterToolStripMenuItem.Name = "sharpenFilterToolStripMenuItem";
            this.sharpenFilterToolStripMenuItem.Size = new System.Drawing.Size(302, 22);
            this.sharpenFilterToolStripMenuItem.Text = "Sharpen filter";
            this.sharpenFilterToolStripMenuItem.Click += new System.EventHandler(this.sharpenFilterToolStripMenuItem_Click);
            // 
            // kreiranjeBMPSlikeSa256IndeksiranihBojaToolStripMenuItem
            // 
            this.kreiranjeBMPSlikeSa256IndeksiranihBojaToolStripMenuItem.Name = "kreiranjeBMPSlikeSa256IndeksiranihBojaToolStripMenuItem";
            this.kreiranjeBMPSlikeSa256IndeksiranihBojaToolStripMenuItem.Size = new System.Drawing.Size(302, 22);
            this.kreiranjeBMPSlikeSa256IndeksiranihBojaToolStripMenuItem.Text = "Kreiranje BMP slike sa 256 indeksiranih boja";
            this.kreiranjeBMPSlikeSa256IndeksiranihBojaToolStripMenuItem.Click += new System.EventHandler(this.kreiranjeBMPSlikeSa256IndeksiranihBojaToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripItemRunInWin32,
            this.naRacunanjeKonvVrednostiUticuPrethodnoIzracunateVrednostiToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // stripItemRunInWin32
            // 
            this.stripItemRunInWin32.Name = "stripItemRunInWin32";
            this.stripItemRunInWin32.Size = new System.Drawing.Size(426, 22);
            this.stripItemRunInWin32.Text = "Izvrsenje u Win32 Core";
            this.stripItemRunInWin32.Click += new System.EventHandler(this.izvrsenjeUToolStripMenuItem_Click);
            // 
            // naRacunanjeKonvVrednostiUticuPrethodnoIzracunateVrednostiToolStripMenuItem
            // 
            this.naRacunanjeKonvVrednostiUticuPrethodnoIzracunateVrednostiToolStripMenuItem.Name = "naRacunanjeKonvVrednostiUticuPrethodnoIzracunateVrednostiToolStripMenuItem";
            this.naRacunanjeKonvVrednostiUticuPrethodnoIzracunateVrednostiToolStripMenuItem.Size = new System.Drawing.Size(426, 22);
            this.naRacunanjeKonvVrednostiUticuPrethodnoIzracunateVrednostiToolStripMenuItem.Text = "Na racunanje konv. vrednosti uticu prethodno izracunate vrednosti";
            // 
            // undoRedoToolStripMenuItem
            // 
            this.undoRedoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.podesavanjeToolStripMenuItem});
            this.undoRedoToolStripMenuItem.Name = "undoRedoToolStripMenuItem";
            this.undoRedoToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.undoRedoToolStripMenuItem.Text = "Undo-Redo";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.Undo_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.Redo_Click);
            // 
            // podesavanjeToolStripMenuItem
            // 
            this.podesavanjeToolStripMenuItem.Name = "podesavanjeToolStripMenuItem";
            this.podesavanjeToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.podesavanjeToolStripMenuItem.Text = "Podesavanje";
            this.podesavanjeToolStripMenuItem.Click += new System.EventHandler(this.podesavanjeToolStripMenuItem_Click);
            // 
            // dodatniFilteriToolStripMenuItem
            // 
            this.dodatniFilteriToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.promenljiviKonvulcioniFiltriToolStripMenuItem,
            this.edgeEnhanceToolStripMenuItem,
            this.pixelateToolStripMenuItem});
            this.dodatniFilteriToolStripMenuItem.Name = "dodatniFilteriToolStripMenuItem";
            this.dodatniFilteriToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.dodatniFilteriToolStripMenuItem.Text = "Dodatni filteri";
            // 
            // promenljiviKonvulcioniFiltriToolStripMenuItem
            // 
            this.promenljiviKonvulcioniFiltriToolStripMenuItem.Name = "promenljiviKonvulcioniFiltriToolStripMenuItem";
            this.promenljiviKonvulcioniFiltriToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.promenljiviKonvulcioniFiltriToolStripMenuItem.Text = "Promenljivi konvolucioni filtri";
            this.promenljiviKonvulcioniFiltriToolStripMenuItem.Click += new System.EventHandler(this.promenljiviKonvulcioniFiltriToolStripMenuItem_Click);
            // 
            // edgeEnhanceToolStripMenuItem
            // 
            this.edgeEnhanceToolStripMenuItem.Name = "edgeEnhanceToolStripMenuItem";
            this.edgeEnhanceToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.edgeEnhanceToolStripMenuItem.Text = "EdgeEnhance";
            this.edgeEnhanceToolStripMenuItem.Click += new System.EventHandler(this.edgeEnhanceToolStripMenuItem_Click);
            // 
            // pixelateToolStripMenuItem
            // 
            this.pixelateToolStripMenuItem.Name = "pixelateToolStripMenuItem";
            this.pixelateToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.pixelateToolStripMenuItem.Text = "Pixelate";
            this.pixelateToolStripMenuItem.Click += new System.EventHandler(this.pixelateToolStripMenuItem_Click);
            // 
            // openFD
            // 
            this.openFD.FileName = "default";
            this.openFD.Filter = "Image Files(*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png";
            // 
            // saveFD
            // 
            this.saveFD.FileName = "default";
            this.saveFD.Filter = "PNG format|*.png|JPG format|*.jpg|BMP format|*.bmp";
            // 
            // pictureBoxBlue
            // 
            this.pictureBoxBlue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxBlue.Location = new System.Drawing.Point(423, 305);
            this.pictureBoxBlue.Name = "pictureBoxBlue";
            this.pictureBoxBlue.Size = new System.Drawing.Size(412, 275);
            this.pictureBoxBlue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBlue.TabIndex = 4;
            this.pictureBoxBlue.TabStop = false;
            // 
            // pictureBoxGreen
            // 
            this.pictureBoxGreen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxGreen.Location = new System.Drawing.Point(423, 23);
            this.pictureBoxGreen.Name = "pictureBoxGreen";
            this.pictureBoxGreen.Size = new System.Drawing.Size(412, 275);
            this.pictureBoxGreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxGreen.TabIndex = 5;
            this.pictureBoxGreen.TabStop = false;
            // 
            // pictureBoxRed
            // 
            this.pictureBoxRed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxRed.Location = new System.Drawing.Point(5, 305);
            this.pictureBoxRed.Name = "pictureBoxRed";
            this.pictureBoxRed.Size = new System.Drawing.Size(412, 275);
            this.pictureBoxRed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxRed.TabIndex = 6;
            this.pictureBoxRed.TabStop = false;
            // 
            // mainPictureBox
            // 
            this.mainPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPictureBox.Location = new System.Drawing.Point(5, 23);
            this.mainPictureBox.Name = "mainPictureBox";
            this.mainPictureBox.Size = new System.Drawing.Size(412, 275);
            this.mainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainPictureBox.TabIndex = 7;
            this.mainPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 584);
            this.Controls.Add(this.mainPictureBox);
            this.Controls.Add(this.pictureBoxRed);
            this.Controls.Add(this.pictureBoxGreen);
            this.Controls.Add(this.pictureBoxBlue);
            this.Controls.Add(this.mainStrip);
            this.MainMenuStrip = this.mainStrip;
            this.MaximumSize = new System.Drawing.Size(855, 623);
            this.MinimumSize = new System.Drawing.Size(855, 623);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MMS-P1";
            this.mainStrip.ResumeLayout(false);
            this.mainStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoRedoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem podesavanjeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prikazKanalskihSlikaToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFD;
        private System.Windows.Forms.SaveFileDialog saveFD;
        private System.Windows.Forms.ToolStripMenuItem gammaFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sharpenFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stripItemRunInWin32;
        private System.Windows.Forms.ToolStripMenuItem naRacunanjeKonvVrednostiUticuPrethodnoIzracunateVrednostiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kreiranjeBMPSlikeSa256IndeksiranihBojaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dodatniFilteriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem promenljiviKonvulcioniFiltriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edgeEnhanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pixelateToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBoxBlue;
        private System.Windows.Forms.PictureBox pictureBoxGreen;
        private System.Windows.Forms.PictureBox pictureBoxRed;
        private System.Windows.Forms.PictureBox mainPictureBox;
    }
}

