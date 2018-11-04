using System.Collections.Generic;

public interface ITurnPanel : IMonoBehaviour
{
    ITurnPanelController Controller { get; }

    void updateQueue(ICharacter ActiveCharacter, List<ICharacter> RefreshedCharacters, List<ICharacter> ExhaustedCharacters);
}
