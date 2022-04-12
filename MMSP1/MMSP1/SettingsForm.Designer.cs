
namespace MMSP1
{
    partial class SettingsForm
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
            this.lblBufferCapacity = new System.Windows.Forms.Label();
            this.numBufferCapacity = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numBufferCapacity)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBufferCapacity
            // 
            this.lblBufferCapacity.AutoSize = true;
            this.lblBufferCapacity.Location = new System.Drawing.Point(44, 47);
            this.lblBufferCapacity.Name = "lblBufferCapacity";
            this.lblBufferCapacity.Size = new System.Drawing.Size(105, 15);
            this.lblBufferCapacity.TabIndex = 0;
            this.lblBufferCapacity.Text = "Kapacitet buffer-a:";
            // 
            // numBufferCapacity
            // 
            this.numBufferCapacity.Location = new System.Drawing.Point(155, 45);
            this.numBufferCapacity.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numBufferCapacity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBufferCapacity.Name = "numBufferCapacity";
            this.numBufferCapacity.Size = new System.Drawing.Size(57, 23);
            this.numBufferCapacity.TabIndex = 1;
            this.numBufferCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numBufferCapacity.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numBufferCapacity.ValueChanged += new System.EventHandler(this.numBufferCapacity_ValueChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 96);
            this.Controls.Add(this.numBufferCapacity);
            this.Controls.Add(this.lblBufferCapacity);
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Podesavanja";
            ((System.ComponentModel.ISupportInitialize)(this.numBufferCapacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBufferCapacity;
        private System.Windows.Forms.NumericUpDown numBufferCapacity;
    }
}