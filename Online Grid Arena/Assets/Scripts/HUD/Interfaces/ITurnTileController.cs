using System.Collections.Generic;

public interface ITurnTileController
{
    ICharacterController Character { set; }
    void UpdateTile(ICharacterController character);
    void Hide();
    void Show();
}
