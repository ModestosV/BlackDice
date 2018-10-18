using UnityEngine;

[CreateAssetMenu(menuName = "Hex Grid/Tile Definition")]
public class HexTileDefinition : ScriptableObject
{
    public Material DefaultMaterial;
    public Material ClickedMaterial;
    public Material HoveredMaterial;
}