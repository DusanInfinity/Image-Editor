using System;
using System.Windows.Forms;

namespace MMSP1
{
    public partial class CrossDomainColorizeInput : Form
    {
        public CrossDomainColorizeInput()
        {
            InitializeComponent();
        }

        public short GetHue()
        {
            return Convert.ToInt16(numNewHue.Value);
        }

        public double GetSaturation()
        {
            if (double.TryParse(tbSaturation.Text, out double saturation) &&
                saturation >= 0.0 && saturation <= 1.0)
                return saturation;

            return -1;
        }
    }
}
