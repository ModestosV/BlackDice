using System.Collections.Generic;

public interface IPlayer
{
    List<ICharacterController> CharacterControllers { get; }
    void AddCharacterController(ICharacterController characterController);
    void RefreshCharacters();
    List<ICharacterController> GetUnusedCharacters();
}