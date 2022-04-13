using System;
using System.Windows.Forms;

namespace MMSP1
{
    public partial class GammaFilterInputForm : Form
    {
        public double R
        {
            get
            {
                return Convert.ToDouble(numRed.Value);
            }
        }
        public double G
        {
            get
            {
                return Convert.ToDouble(numGreen.Value);
            }
        }
        public double B
        {
            get
            {
                return Convert.ToDouble(numBlue.Value);
            }
        }

        public GammaFilterInputForm()
        {
            InitializeComponent();
        }
    }
}
