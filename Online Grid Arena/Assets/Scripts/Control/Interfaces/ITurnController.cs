using System.Collections.Generic;

public interface ITurnController
{
    void StartNextTurn();
    void AddCharacters(List<ICharacter> characters);
    void AddCharacter(ICharacter character);
    void RemoveCharacter(ICharacter character);
}
