using System;
using System.Windows.Forms;

namespace RGDynApp.UI
{
    public partial class Initialize : Form
    {
        public Initialize()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public double Alpha
        {
            get { return double.Parse(_tbAlpha.Text); }
        }

        public double N
        {
            get { return double.Parse(_tbN.Text); }
        }

        public double B
        {
            get { return double.Parse(_tbB.Text); }
        }
    }
}
