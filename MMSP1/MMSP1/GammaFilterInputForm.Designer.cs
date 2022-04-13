
namespace MMSP1
{
    partial class GammaFilterInputForm
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
            this.lblRed = new System.Windows.Forms.Label();
            this.numRed = new System.Windows.Forms.NumericUpDown();
            this.numGreen = new System.Windows.Forms.NumericUpDown();
            this.lblGreen = new System.Windows.Forms.Label();
            this.numBlue = new System.Windows.Forms.NumericUpDown();
            this.lblBlue = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBlue)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRed
            // 
            this.lblRed.AutoSize = true;
            this.lblRed.Location = new System.Drawing.Point(47, 89);
            this.lblRed.Name = "lblRed";
            this.lblRed.Size = new System.Drawing.Size(30, 15);
            this.lblRed.TabIndex = 0;
            this.lblRed.Text = "Red:";
            // 
            // numRed
            // 
            this.numRed.DecimalPlaces = 1;
            this.numRed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numRed.Location = new System.Drawing.Point(94, 87);
            this.numRed.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numRed.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.numRed.Name = "numRed";
            this.numRed.Size = new System.Drawing.Size(87, 23);
            this.numRed.TabIndex = 1;
            this.numRed.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // numGreen
            // 
            this.numGreen.DecimalPlaces = 1;
            this.numGreen.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numGreen.Location = new System.Drawing.Point(94, 124);
            this.numGreen.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numGreen.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.numGreen.Name = "numGreen";
            this.numGreen.Size = new System.Drawing.Size(87, 23);
            this.numGreen.TabIndex = 3;
            this.numGreen.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // lblGreen
            // 
            this.lblGreen.AutoSize = true;
            this.lblGreen.Location = new System.Drawing.Point(47, 126);
            this.lblGreen.Name = "lblGreen";
            this.lblGreen.Size = new System.Drawing.Size(41, 15);
            this.lblGreen.TabIndex = 2;
            this.lblGreen.Text = "Green:";
            // 
            // numBlue
            // 
            this.numBlue.DecimalPlaces = 1;
            this.numBlue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numBlue.Location = new System.Drawing.Point(94, 162);
            this.numBlue.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numBlue.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.numBlue.Name = "numBlue";
            this.numBlue.Size = new System.Drawing.Size(87, 23);
            this.numBlue.TabIndex = 5;
            this.numBlue.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // lblBlue
            // 
            this.lblBlue.AutoSize = true;
            this.lblBlue.Location = new System.Drawing.Point(47, 164);
            this.lblBlue.Name = "lblBlue";
            this.lblBlue.Size = new System.Drawing.Size(33, 15);
            this.lblBlue.TabIndex = 4;
            this.lblBlue.Text = "Blue:";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 24);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(229, 45);
            this.lblInfo.TabIndex = 6;
            this.lblInfo.Text = "Unesite zeljene vrednosti za Gamma filter. \r\nMinimalna vrednost je 0.2, maksimaln" +
    "a 5.\r\n(S=5)\r\n";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(33, 209);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(142, 209);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // GammaFilterInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 244);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.numBlue);
            this.Controls.Add(this.lblBlue);
            this.Controls.Add(this.numGreen);
            this.Controls.Add(this.lblGreen);
            this.Controls.Add(this.numRed);
            this.Controls.Add(this.lblRed);
            this.Name = "GammaFilterInputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gamma filter";
            ((System.ComponentModel.ISupportInitialize)(this.numRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBlue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRed;
        private System.Windows.Forms.NumericUpDown numRed;
        private System.Windows.Forms.NumericUpDown numGreen;
        private System.Windows.Forms.Label lblGreen;
        private System.Windows.Forms.NumericUpDown numBlue;
        private System.Windows.Forms.Label lblBlue;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}