
namespace MMSP1
{
    partial class SharpenInput
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
            this.lblVelicinaMatrice = new System.Windows.Forms.Label();
            this.cbVelicinaMatrice = new System.Windows.Forms.ComboBox();
            this.lblNWeight = new System.Windows.Forms.Label();
            this.numNWeight = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numNWeight)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVelicinaMatrice
            // 
            this.lblVelicinaMatrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVelicinaMatrice.AutoSize = true;
            this.lblVelicinaMatrice.Location = new System.Drawing.Point(40, 66);
            this.lblVelicinaMatrice.Name = "lblVelicinaMatrice";
            this.lblVelicinaMatrice.Size = new System.Drawing.Size(93, 15);
            this.lblVelicinaMatrice.TabIndex = 0;
            this.lblVelicinaMatrice.Text = "Velicina matrice:";
            // 
            // cbVelicinaMatrice
            // 
            this.cbVelicinaMatrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbVelicinaMatrice.DisplayMember = "3x3";
            this.cbVelicinaMatrice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVelicinaMatrice.FormattingEnabled = true;
            this.cbVelicinaMatrice.Items.AddRange(new object[] {
            "3x3",
            "5x5",
            "7x7"});
            this.cbVelicinaMatrice.Location = new System.Drawing.Point(139, 63);
            this.cbVelicinaMatrice.Name = "cbVelicinaMatrice";
            this.cbVelicinaMatrice.Size = new System.Drawing.Size(77, 23);
            this.cbVelicinaMatrice.TabIndex = 1;
            // 
            // lblNWeight
            // 
            this.lblNWeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNWeight.AutoSize = true;
            this.lblNWeight.Location = new System.Drawing.Point(40, 103);
            this.lblNWeight.Name = "lblNWeight";
            this.lblNWeight.Size = new System.Drawing.Size(66, 15);
            this.lblNWeight.TabIndex = 2;
            this.lblNWeight.Text = "Weight (n):";
            // 
            // numNWeight
            // 
            this.numNWeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numNWeight.Location = new System.Drawing.Point(112, 101);
            this.numNWeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numNWeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numNWeight.Name = "numNWeight";
            this.numNWeight.Size = new System.Drawing.Size(104, 23);
            this.numNWeight.TabIndex = 3;
            this.numNWeight.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(40, 164);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(141, 164);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // SharpenInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 199);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.numNWeight);
            this.Controls.Add(this.lblNWeight);
            this.Controls.Add(this.cbVelicinaMatrice);
            this.Controls.Add(this.lblVelicinaMatrice);
            this.Name = "SharpenInput";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sharpen - prom. konv. filtri";
            ((System.ComponentModel.ISupportInitialize)(this.numNWeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVelicinaMatrice;
        private System.Windows.Forms.ComboBox cbVelicinaMatrice;
        private System.Windows.Forms.Label lblNWeight;
        private System.Windows.Forms.NumericUpDown numNWeight;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}