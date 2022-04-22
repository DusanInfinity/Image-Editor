using System.Windows.Forms;

namespace MMSP1
{
    public partial class DownsamplingSave : Form
    {
        public DownsamplingSave()
        {
            InitializeComponent();

            cbIzborKanala.SelectedIndex = 0;
        }

        public int GetSelectedDownsampling()
        {
            return cbIzborKanala.SelectedIndex;
        }
    }
}
