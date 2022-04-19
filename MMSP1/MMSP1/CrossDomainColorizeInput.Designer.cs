
namespace MMSP1
{
    partial class CrossDomainColorizeInput
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
            this.lblHue = new System.Windows.Forms.Label();
            this.numNewHue = new System.Windows.Forms.NumericUpDown();
            this.lblSaturation = new System.Windows.Forms.Label();
            this.tbSaturation = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numNewHue)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHue
            // 
            this.lblHue.AutoSize = true;
            this.lblHue.Location = new System.Drawing.Point(90, 82);
            this.lblHue.Name = "lblHue";
            this.lblHue.Size = new System.Drawing.Size(50, 13);
            this.lblHue.TabIndex = 0;
            this.lblHue.Text = "newHue:";
            // 
            // numNewHue
            // 
            this.numNewHue.Location = new System.Drawing.Point(146, 80);
            this.numNewHue.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numNewHue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numNewHue.Name = "numNewHue";
            this.numNewHue.Size = new System.Drawing.Size(45, 20);
            this.numNewHue.TabIndex = 1;
            this.numNewHue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numNewHue.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // lblSaturation
            // 
            this.lblSaturation.AutoSize = true;
            this.lblSaturation.Location = new System.Drawing.Point(15, 117);
            this.lblSaturation.Name = "lblSaturation";
            this.lblSaturation.Size = new System.Drawing.Size(125, 13);
            this.lblSaturation.TabIndex = 2;
            this.lblSaturation.Text = "newSaturation (opciono):";
            // 
            // tbSaturation
            // 
            this.tbSaturation.Location = new System.Drawing.Point(147, 114);
            this.tbSaturation.Name = "tbSaturation";
            this.tbSaturation.Size = new System.Drawing.Size(44, 20);
            this.tbSaturation.TabIndex = 3;
            this.tbSaturation.Text = "0.5";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(37, 176);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(147, 176);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(59, 33);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(141, 13);
            this.lblInfo.TabIndex = 6;
            this.lblInfo.Text = "Unesite potrebne parametre.";
            // 
            // CrossDomainColorizeInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 211);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbSaturation);
            this.Controls.Add(this.lblSaturation);
            this.Controls.Add(this.numNewHue);
            this.Controls.Add(this.lblHue);
            this.Name = "CrossDomainColorizeInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cross-Domain Colorize";
            ((System.ComponentModel.ISupportInitialize)(this.numNewHue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHue;
        private System.Windows.Forms.NumericUpDown numNewHue;
        private System.Windows.Forms.Label lblSaturation;
        private System.Windows.Forms.TextBox tbSaturation;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblInfo;
    }
}