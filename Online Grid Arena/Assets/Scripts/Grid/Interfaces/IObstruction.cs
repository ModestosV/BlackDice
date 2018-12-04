public interface IObstruction : IMonoBehaviour
{
    void SetHoverMaterial();
    void SetErrorMaterial();
    void SetDefaultMaterial();
    void SetClickedMaterial();
    void SetHighlightMaterial();
    bool IsMouseOver();

    IObstructionController Controller { get; }
}
