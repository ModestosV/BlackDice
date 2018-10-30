public abstract class InputController : IInputController
{
    public InputParameters InputParameters { get; set; }
    protected InputParameters lastInputParameters;

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
