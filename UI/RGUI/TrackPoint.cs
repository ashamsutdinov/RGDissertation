using System;
using System.Windows.Forms;
using RGUI.Resources;
using RenormGroups;

namespace RGUI
{
  public partial class TrackPoint : Form
  {
    public TrackPoint()
    {
      InitializeComponent();

      cbSelectPoint.Text = str.SelectManually;
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

    private void CbSelectPointCheckedChanged(object sender, EventArgs e)
    {
      if (cbSelectPoint.Checked)
      {
        tbC0.Enabled = false;
        tbC1.Enabled = false;
        tbC2.Enabled = false;
      }
      else
      {
        tbC0.Enabled = true;
        tbC1.Enabled = true;
        tbC2.Enabled = true;
      }
    }

    #region Properties

    public CPoint CPoint
    {
      get 
      { 
        var c0 = double.Parse(tbC0.Text);
        var c1 = double.Parse(tbC1.Text);
        var c2 = double.Parse(tbC2.Text);
        return new CPoint {C0 = c0, C1 = c1, C2 = c2};
      }
    }

    public bool SelectManually
    {
      get { return cbSelectPoint.Checked; }
    }

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
