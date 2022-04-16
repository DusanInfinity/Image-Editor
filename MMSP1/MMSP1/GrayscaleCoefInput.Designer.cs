
namespace MMSP1
{
    partial class GrayscaleCoefInput
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
            this.numCr = new System.Windows.Forms.NumericUpDown();
            this.lblCr = new System.Windows.Forms.Label();
            this.lblCg = new System.Windows.Forms.Label();
            this.numCg = new System.Windows.Forms.NumericUpDown();
            this.lblCb = new System.Windows.Forms.Label();
            this.numCb = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numCr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCb)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(33, 60);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(273, 26);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Unesite željene koeficijente za RGB kanale:\r\nNAPOMENA: zbir koeficijenata mora bi" +
    "ti 1 (Cr+Cb+Cg=1)";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // numCr
            // 
            this.numCr.DecimalPlaces = 2;
            this.numCr.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numCr.Location = new System.Drawing.Point(154, 135);
            this.numCr.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCr.Name = "numCr";
            this.numCr.Size = new System.Drawing.Size(61, 20);
            this.numCr.TabIndex = 1;
            this.numCr.Value = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            // 
            // lblCr
            // 
            this.lblCr.AutoSize = true;
            this.lblCr.Location = new System.Drawing.Point(119, 137);
            this.lblCr.Name = "lblCr";
            this.lblCr.Size = new System.Drawing.Size(29, 13);
            this.lblCr.TabIndex = 2;
            this.lblCr.Text = "Cr = ";
            // 
            // lblCg
            // 
            this.lblCg.AutoSize = true;
            this.lblCg.Location = new System.Drawing.Point(119, 179);
            this.lblCg.Name = "lblCg";
            this.lblCg.Size = new System.Drawing.Size(32, 13);
            this.lblCg.TabIndex = 4;
            this.lblCg.Text = "Cg = ";
            // 
            // numCg
            // 
            this.numCg.DecimalPlaces = 2;
            this.numCg.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numCg.Location = new System.Drawing.Point(154, 177);
            this.numCg.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCg.Name = "numCg";
            this.numCg.Size = new System.Drawing.Size(61, 20);
            this.numCg.TabIndex = 3;
            this.numCg.Value = new decimal(new int[] {
            59,
            0,
            0,
            131072});
            // 
            // lblCb
            // 
            this.lblCb.AutoSize = true;
            this.lblCb.Location = new System.Drawing.Point(119, 223);
            this.lblCb.Name = "lblCb";
            this.lblCb.Size = new System.Drawing.Size(32, 13);
            this.lblCb.TabIndex = 6;
            this.lblCb.Text = "Cb = ";
            // 
            // numCb
            // 
            this.numCb.DecimalPlaces = 2;
            this.numCb.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numCb.Location = new System.Drawing.Point(154, 221);
            this.numCb.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCb.Name = "numCb";
            this.numCb.Size = new System.Drawing.Size(61, 20);
            this.numCb.TabIndex = 5;
            this.numCb.Value = new decimal(new int[] {
            11,
            0,
            0,
            131072});
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(50, 289);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(220, 289);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // GrayscaleCoefInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 337);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblCb);
            this.Controls.Add(this.numCb);
            this.Controls.Add(this.lblCg);
            this.Controls.Add(this.numCg);
            this.Controls.Add(this.lblCr);
            this.Controls.Add(this.numCr);
            this.Controls.Add(this.lblInfo);
            this.Name = "GrayscaleCoefInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Grayscale: Color coef.";
            ((System.ComponentModel.ISupportInitialize)(this.numCr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.NumericUpDown numCr;
        private System.Windows.Forms.Label lblCr;
        private System.Windows.Forms.Label lblCg;
        private System.Windows.Forms.NumericUpDown numCg;
        private System.Windows.Forms.Label lblCb;
        private System.Windows.Forms.NumericUpDown numCb;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}