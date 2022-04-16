using System;
using System.Windows.Forms;

namespace MMSP1
{
    public partial class GrayscaleCoefInput : Form
    {
        public GrayscaleCoefInput()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            decimal sum = numCr.Value + numCg.Value + numCb.Value;

            if (sum != 1)
            {
                MessageBox.Show(this, "Zbir svih koeficijenata mora biti 1!", "GRESKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        public decimal GetCr()
        {
            return numCr.Value;
        }

        public decimal GetCg()
        {
            return numCg.Value;
        }

        public decimal GetCb()
        {
            return numCb.Value;
        }
    }
}
