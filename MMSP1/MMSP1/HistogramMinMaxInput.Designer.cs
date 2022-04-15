
namespace MMSP1
{
    partial class HistogramMinMaxInput
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblInfo = new System.Windows.Forms.Label();
            this.numRedMin = new System.Windows.Forms.NumericUpDown();
            this.numRedMax = new System.Windows.Forms.NumericUpDown();
            this.lblMin = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.lblIzborKanala = new System.Windows.Forms.Label();
            this.cbIzborKanala = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numRedMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRedMax)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(25, 40);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(290, 13);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Unesite zeljena Min-Max ogranicenja za zeljeni RGB kanale.";
            // 
            // numRedMin
            // 
            this.numRedMin.Location = new System.Drawing.Point(89, 150);
            this.numRedMin.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numRedMin.Name = "numRedMin";
            this.numRedMin.Size = new System.Drawing.Size(56, 20);
            this.numRedMin.TabIndex = 2;
            // 
            // numRedMax
            // 
            this.numRedMax.Location = new System.Drawing.Point(232, 152);
            this.numRedMax.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numRedMax.Name = "numRedMax";
            this.numRedMax.Size = new System.Drawing.Size(56, 20);
            this.numRedMax.TabIndex = 3;
            this.numRedMax.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Location = new System.Drawing.Point(35, 152);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(51, 13);
            this.lblMin.TabIndex = 4;
            this.lblMin.Text = "Minimum:";
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(169, 154);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(60, 13);
            this.lblMax.TabIndex = 5;
            this.lblMax.Text = "Maksimum:";
            // 
            // lblIzborKanala
            // 
            this.lblIzborKanala.AutoSize = true;
            this.lblIzborKanala.Location = new System.Drawing.Point(86, 95);
            this.lblIzborKanala.Name = "lblIzborKanala";
            this.lblIzborKanala.Size = new System.Drawing.Size(79, 13);
            this.lblIzborKanala.TabIndex = 6;
            this.lblIzborKanala.Text = "Izaberite kanal:";
            // 
            // cbIzborKanala
            // 
            this.cbIzborKanala.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIzborKanala.FormattingEnabled = true;
            this.cbIzborKanala.Items.AddRange(new object[] {
            "Red",
            "Green",
            "Blue"});
            this.cbIzborKanala.Location = new System.Drawing.Point(171, 92);
            this.cbIzborKanala.Name = "cbIzborKanala";
            this.cbIzborKanala.Size = new System.Drawing.Size(76, 21);
            this.cbIzborKanala.TabIndex = 7;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(55, 226);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(198, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // HistogramMinMaxInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 279);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbIzborKanala);
            this.Controls.Add(this.lblIzborKanala);
            this.Controls.Add(this.lblMax);
            this.Controls.Add(this.lblMin);
            this.Controls.Add(this.numRedMax);
            this.Controls.Add(this.numRedMin);
            this.Controls.Add(this.lblInfo);
            this.MaximumSize = new System.Drawing.Size(357, 318);
            this.MinimumSize = new System.Drawing.Size(357, 318);
            this.Name = "HistogramMinMaxInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Histogram - MinMax filter";
            ((System.ComponentModel.ISupportInitialize)(this.numRedMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRedMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.NumericUpDown numRedMin;
        private System.Windows.Forms.NumericUpDown numRedMax;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label lblIzborKanala;
        private System.Windows.Forms.ComboBox cbIzborKanala;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}