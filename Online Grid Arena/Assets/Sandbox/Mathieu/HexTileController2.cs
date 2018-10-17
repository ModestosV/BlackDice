using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HexTileController2 {

    public int x;
    public int y;
    public int z;

    public bool enabled;
    public bool isSelected;

    public ISelectionController selectionController;

    public void Init(ISelectionController selectionController)
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
        isSelected = false;
        selectionController.Deselect();
    }



}
