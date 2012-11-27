namespace RenormGroups.Processing
{
  public abstract class SceneIterationAction : ProcessingAction
  {
    public double N { get; set; }

    public double Alpha { get; set; }

    protected CIterator Iterator = new CIterator();
  }
}