using System;
using System.Windows.Forms;

namespace RGDynApp.UI
{
    public partial class Mode : Form
    {
        public Mode(bool isBoundaryMode)
        {
            InitializeComponent();
            rbBoundaryAnalisysMode.Checked = isBoundaryMode;
            rbZoomMode.Checked = !isBoundaryMode;
        }

        private void OnChangeMode(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public bool IsBoundaryMode
        {
            get { return rbBoundaryAnalisysMode.Checked; }
        }
    }
}
