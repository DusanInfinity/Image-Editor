using MMSP1.Models;
using System;
using System.Windows.Forms;

namespace MMSP1
{
    public partial class HistogramMinMaxInput : Form
    {
        public HistogramMinMaxInput()
        {
            InitializeComponent();

            cbIzborKanala.SelectedIndex = 0;
        }

        public ChannelColor GetSelectedChannel()
        {
            return (ChannelColor)cbIzborKanala.SelectedIndex;
        }

        public byte GetMin()
        {
            return Convert.ToByte(numRedMin.Value);
        }

        public byte GetMax()
        {
            return Convert.ToByte(numRedMax.Value);
        }
    }
}
