namespace RenormGroups.Processing
{
  public abstract class ProcessingAction
  {
    public abstract void Apply<TStack>(TStack stack) where TStack : ProcessingStack;

    public bool IsSimple { get; protected set; }
  }
}
