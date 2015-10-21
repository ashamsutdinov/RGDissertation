using System;
using System.Windows.Forms;
using MainForm = RGDynApp.Main;

namespace RGDynApp.UI
{
    public partial class Dyn : 
        Form
    {
        private readonly MainForm _m;

        public Dyn(MainForm m)
        {
            InitializeComponent();
            _m = m;
        }

        private void Dyn_FormClosed(object sender, FormClosedEventArgs e)
        {
            _m.OnDynClose();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
           _m.OnMarkupDynamics(trackBar1.Value);
        }
    }
}
