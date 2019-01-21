using System;

public class InputParameters : IInputParameters, IEquatable<InputParameters>
{
    public bool IsKeyQDown { get; set; }
    public bool IsKeyWDown { get; set; }
    public bool IsKeyEDown { get; set; }
    public bool IsKeyRDown { get; set; }
    public bool IsKeyFDown { get; set; }
    public bool IsKeyEscapeDown { get; set; }
    public bool IsKeyTabDown { get; set; }

    public bool IsLeftClickDown { get; set; }
    public bool IsRightClickDown { get; set; }

    public bool IsMouseOverGrid { get; set; }
    public IHexTileController TargetTile { get; set; }

    public bool IsAbilityKeyPressed()
    {
        if (IsKeyQDown || IsKeyWDown || IsKeyEDown || IsKeyRDown) return true;
        return false;
    }

    public int GetAbilityNumber()
    {
        if (IsKeyQDown) return 0;
        if (IsKeyWDown) return 1;
        if (IsKeyEDown) return 2;
        if (IsKeyRDown) return 3;
        return -1;
    }

    public bool Equals(InputParameters that)
    {
        if (this.IsKeyQDown != that.IsKeyQDown)
            return false;
        if (this.IsKeyWDown != that.IsKeyWDown)
            return false;
        if (this.IsKeyEDown != that.IsKeyEDown)
            return false;
        if (this.IsKeyRDown != that.IsKeyRDown)
            return false;
        if (this.IsKeyFDown != that.IsKeyFDown)
            return false;
        if (this.IsKeyEscapeDown != that.IsKeyEscapeDown)
            return false;
        if (this.IsKeyTabDown != that.IsKeyTabDown)
            return false;
        if (this.IsLeftClickDown != that.IsLeftClickDown)
            return false;
        if (this.IsRightClickDown != that.IsRightClickDown)
            return false;
        if (this.IsMouseOverGrid != that.IsMouseOverGrid)
            return false;
        if (this.TargetTile != that.TargetTile)
            return false;

        return true;
    }
}
