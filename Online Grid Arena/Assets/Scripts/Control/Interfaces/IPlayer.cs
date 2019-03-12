using System.Collections.Generic;

public interface IPlayer
{
    string Name { get; }
    List<ICharacterController> CharacterControllers { get; }
    void AddCharacterController(ICharacterController characterController);
    void RefreshCharacters();
    List<ICharacterController> GetUnusedCharacters();
    bool AreAllCharactersDead();
}