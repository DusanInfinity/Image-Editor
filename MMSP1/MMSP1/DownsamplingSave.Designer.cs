
namespace MMSP1
{
    partial class DownsamplingSave
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblIzborKanala = new System.Windows.Forms.Label();
            this.cbIzborKanala = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(65, 154);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(165, 154);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblIzborKanala
            // 
            this.lblIzborKanala.AutoSize = true;
            this.lblIzborKanala.Location = new System.Drawing.Point(21, 84);
            this.lblIzborKanala.Name = "lblIzborKanala";
            this.lblIzborKanala.Size = new System.Drawing.Size(68, 13);
            this.lblIzborKanala.TabIndex = 2;
            this.lblIzborKanala.Text = "Izbor kanala:";
            // 
            // cbIzborKanala
            // 
            this.cbIzborKanala.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIzborKanala.FormattingEnabled = true;
            this.cbIzborKanala.Items.AddRange(new object[] {
            "Slika 1 - Redukovanje po GB",
            "Slika 2 - Redukovanje po RB",
            "Slika 3 - Redukovanje po RG"});
            this.cbIzborKanala.Location = new System.Drawing.Point(95, 81);
            this.cbIzborKanala.Name = "cbIzborKanala";
            this.cbIzborKanala.Size = new System.Drawing.Size(174, 21);
            this.cbIzborKanala.TabIndex = 3;
            // 
            // DownsamplingSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 189);
            this.Controls.Add(this.cbIzborKanala);
            this.Controls.Add(this.lblIzborKanala);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "DownsamplingSave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Downsampling - Save";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblIzborKanala;
        private System.Windows.Forms.ComboBox cbIzborKanala;
    }
}