using System.Windows.Forms;

namespace MMSP1
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();
        }

        public void SetText(string text)
        {
            lblText.Text = text;
        }

        public void SetInputValue(string value)
        {
            tbInput.Text = value;
        }

        public string GetInputValue()
        {
            return tbInput.Text;
        }

        public void SetTitle(string title)
        {
            Text = title;
        }
    }
}
