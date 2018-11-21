public interface ISelectionManager
{
    SelectionMode SelectionMode { set; }

    void Update(IInputParameters inputParameters);
}
