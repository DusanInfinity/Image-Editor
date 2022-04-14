using System;
using System.Windows.Forms;

namespace MMSP1
{
    public partial class SharpenInput : Form
    {
        public SharpenInput()
        {
            InitializeComponent();

            cbVelicinaMatrice.SelectedIndex = 0;
        }

        public string GetMatrixSize()
        {
            return cbVelicinaMatrice.SelectedItem.ToString();
        }

        public int GetnWeight()
        {
            return Convert.ToInt32(numNWeight.Value);
        }
    }
}
