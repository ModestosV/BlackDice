
public interface IStatDisplay : IMonoBehaviour
{
    void SetNameText(string nameText);
    void SetCurrentValueText(string currentValueText);
    void SetMaxValueText(string maxValueText);
    void Activate();
    void Deactivate();
}
