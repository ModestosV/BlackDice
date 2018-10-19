[System.Serializable]
public class CharacterController
{
    // Placeholder stats
    public CharacterStat health;
    public CharacterStat damage;

    public CharacterController() : this(new CharacterStat(0.0f), new CharacterStat(0.0f)) { }

    public CharacterController(CharacterStat health, CharacterStat damage)
    {
        this.health = health;
        this.damage = damage;
    }

    public override string ToString()
    {
        return string.Format("{0}, {1}", health.ToString(), damage.ToString());
    }
}
