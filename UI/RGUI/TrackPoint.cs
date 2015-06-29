using System;
using System.Windows.Forms;
using RGUI.Resources;

namespace RGUI
{
  public partial class TrackPoint : Form
  {
    public TrackPoint()
    {
      InitializeComponent();

      lblIterations.Text = str.Iterations;
      lblN.Text = str.N;
      lblAlpha.Text = str.Alpha;
      btnOk.Text = str.Ok;
      btnCancel.Text = str.Cancel;
    }

    private void BtnOkClick(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
      Close();
    }

    private void BtnCancelClick(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }

    #region Properties

    public int Iterations
    {
      get { return int.Parse(tbIterations.Text); }
    }

    public double N
    {
      get { return double.Parse(tbN.Text); }
    }

    public double Alpha
    {
      get { return double.Parse(tbAlpha.Text); }
    }

    #endregion
  }
}
