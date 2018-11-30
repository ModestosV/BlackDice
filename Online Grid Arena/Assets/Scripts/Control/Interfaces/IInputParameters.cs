public interface IInputParameters
{
    bool IsKeyQDown { get; set; }
    bool IsKeyWDown { get; set; }
    bool IsKeyEDown { get; set; }
    bool IsKeyRDown { get; set; }
    bool IsKeyFDown { get; set; }
    bool IsKeyEscapeDown { get; set; }
    bool IsKeyTabDown { get; set; }

    bool IsLeftClickDown { get; set; }
    bool IsRightClickDown { get; set; }

    bool IsMouseOverGrid { get; set; }
    IHexTileController TargetTile { get; set; }

    int GetAbilityNumber();
    bool IsAbilityKeyPressed();
}
