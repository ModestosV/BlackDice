using System.Collections.Generic;

public interface ITurnPanel : IMonoBehaviour
{
    ITurnPanelController Controller { get; }
}
