using System;
using System.Windows.Forms;

namespace RGDynApp.UI
{
    public partial class Mode : Form
    {
        public Mode(AnalisysMode mode)
        {
            InitializeComponent();
            rbBoundaryAnalisysMode.Checked = mode ==  AnalisysMode.TrackBoundary;
            rbZoomMode.Checked = mode == AnalisysMode.Zoom;
            rbTrackPointMode.Checked = mode == AnalisysMode.TrackPoint;
        }

        private void OnChangeMode(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public AnalisysMode AnalisysMode
        {
            get {
                return rbZoomMode.Checked
                    ? AnalisysMode.Zoom
                    : (rbBoundaryAnalisysMode.Checked ? AnalisysMode.TrackBoundary : AnalisysMode.TrackPoint);
            }
        }
    }
}
