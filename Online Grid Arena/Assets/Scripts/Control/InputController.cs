public abstract class InputController : IInputController
{
    public IInputParameters InputParameters { protected get; set; }
    protected IInputParameters lastInputParameters;

    protected bool DebounceUpdate()
    {
        if (lastInputParameters != null && InputParameters.Equals(lastInputParameters))
        {
            return true;
        }
        lastInputParameters = InputParameters;
        return false;
    }

    public abstract void Update();
}
