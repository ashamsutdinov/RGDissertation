using System;
using System.Windows.Forms;
using RGUI.Resources;

namespace RGUI
{
  public partial class EnterParameters : Form
  {
    public EnterParameters()
    {
      InitializeComponent();
      
      lblN.Text = str.N;
      lblAlpha.Text = str.Alpha;
      btnOk.Text = str.Ok;
      btnCancel.Text = str.Cancel;
      cbSplit.Text = str.SplitToHalhPlains;
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

    public double N
    {
      get { return double.Parse(tbN.Text); }
    }

    public double Alpha
    {
      get { return double.Parse(tbAlpha.Text); }
    }

    public bool Split
    {
      get { return cbSplit.Checked; }
    }

    #endregion
  }
}
