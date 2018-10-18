
[System.Serializable]
public class HexTileController2 {

    public int x;
    public int y;
    public int z;

    public bool isEnabled;
    public bool isSelected;

    public ISelectionController2 selectionController;

    public void Init(ISelectionController2 selectionController)
    {
        this.selectionController = selectionController;
    }

    public void Hover()
    {
        if (!isSelected)
        {
            selectionController.Hover();
        }
    }

    public void Blur()
    {
        if (!isSelected)
        {
            selectionController.Blur();
        }
    }

    public void Select()
    {
        if (!isSelected)
        {
            isSelected = true;
            selectionController.Select();
        }
    }

    public void Deselect()
    {
        if (isSelected)
        {
            isSelected = false;
            selectionController.Deselect();
        }
    }



}
