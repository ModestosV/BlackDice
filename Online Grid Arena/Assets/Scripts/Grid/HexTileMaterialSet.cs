using UnityEngine;

[CreateAssetMenu(menuName = "Hex Tile Material Set")]
public class HexTileMaterialSet : ScriptableObject
{
    public Material DefaultMaterial;
    public Material ClickedMaterial;
    public Material HoveredMaterial;
    public Material PathMaterial;
    public Material HoveredErrorMaterial;
    public Material Obstruction;
}