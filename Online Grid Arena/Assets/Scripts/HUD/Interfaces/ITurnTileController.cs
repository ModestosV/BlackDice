using System.Collections.Generic;

public interface ITurnTileController
{
    ICharacterController Character { set; }
    void UpdateTile(ICharacterController character, int player);
    void Hide();
    void Show();
}
