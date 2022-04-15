using System;
using System.Windows.Forms;

namespace MMSP1
{
    public partial class SettingsForm : Form
    {
        public SettingsForm(int capacity)
        {
            InitializeComponent();

            numBufferCapacity.Value = capacity;
        }

        private void numBufferCapacity_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;

            if (num.Value < 1 || num.Value == 1000 || !(Owner is MainForm mainForm)) return;

            mainForm.SetBufferCapacity(Convert.ToInt32(num.Value));
        }
    }
}
