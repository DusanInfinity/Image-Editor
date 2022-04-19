using MMSP1.Models;
using System.Windows.Forms;

namespace MMSP1
{
    public partial class SimpleColorizeInput : Form
    {
        public SimpleColorizeInput()
        {
            InitializeComponent();

            cbMapiranje.Items.Clear();
            cbMapiranje.Items.AddRange(ColorizeFilters.GetAllSimpleColorizeMappings());

            cbMapiranje.SelectedIndex = 0;
        }

        public string GetMappingName()
        {
            return cbMapiranje.SelectedItem.ToString();
        }
    }
}
