using NUnit.Framework;

public class TestCharacterStat {

    private IStatModifier flatMod1;
    private IStatModifier flatMod2;
    private IStatModifier percentAddMod1;
    private IStatModifier percentAddMod2;
    private IStatModifier percentMultMod1;
    private IStatModifier percentMultMod2;

    private const float FINAL_VALUE_PRECISION = 0.0001f;

    [Test]
    public void Character_stat_contains_no_stats_on_init()
    {
        ICharacterStat characterStat = new CharacterStat();

        Assert.IsEmpty(characterStat.StatModifiers);
    }

    [Test]
    public void Character_stat_initialized_by_constructor()
    {
        ICharacterStat characterStat = new CharacterStat(20.0f);

        Assert.AreEqual(20.0f, characterStat.BaseValue);
    }

    [Test]
    public void Character_stat_modified_by_add_modifier()
    {
        ICharacterStat characterStat = new CharacterStat();
        IStatModifier mod = new StatModifier(2.0f, StatModType.Flat);
        characterStat.AddModifier(mod);

        Assert.AreEqual(characterStat.StatModifiers.Count, 1);
        Assert.Contains(mod, characterStat.StatModifiers);
    }

    [Test]
    public void Remove_modifier_removes_stat_from_character_stat()
    {
        ICharacterStat characterStat = new CharacterStat();
        IStatModifier mod = new StatModifier(2.0f, StatModType.Flat);
        characterStat.AddModifier(mod);
        characterStat.RemoveModifier(mod);

        Assert.IsFalse(characterStat.StatModifiers.Contains(mod));
        Assert.IsEmpty(characterStat.StatModifiers);
    }

    [Test]
    public void Remove_all_modifiers_removes_all_stats_from_character_stat()
    {
        ICharacterStat characterStat = new CharacterStat();
        object sourceObject1 = new object();
        object sourceObject2 = new object();


        IStatModifier mod1 = new StatModifier(2.0f, StatModType.Flat, sourceObject1);
        IStatModifier mod2 = new StatModifier(1.2f, StatModType.PercentAdd, sourceObject1);
        IStatModifier mod3 = new StatModifier(3.2f, StatModType.PercentMult, sourceObject2);
        characterStat.AddModifier(mod1);
        characterStat.AddModifier(mod2);
        characterStat.AddModifier(mod3);

        characterStat.RemoveAllModifiersFromSource(sourceObject1);

        Assert.IsFalse(characterStat.StatModifiers.Contains(mod1));
        Assert.IsFalse(characterStat.StatModifiers.Contains(mod2));
        Assert.IsTrue(characterStat.StatModifiers.Contains(mod3));
    }

    private void _instantiateStatModifiers()
    {
        flatMod1 = new StatModifier(2.0f, StatModType.Flat);
        flatMod2 = new StatModifier(5.0f, StatModType.Flat);
        percentAddMod1 = new StatModifier(0.05f, StatModType.PercentAdd);
        percentAddMod2 = new StatModifier(0.10f, StatModType.PercentAdd);
        percentMultMod1 = new StatModifier(0.3f, StatModType.PercentMult);
        percentMultMod2 = new StatModifier(0.2f, StatModType.PercentMult);
    }

    [Test]
    public void Add_modifier_sums_flat_stats_in_character_stat()
    {
        _instantiateStatModifiers();
        ICharacterStat characterStat = new CharacterStat(10.0f);
        characterStat.AddModifier(flatMod1);
        characterStat.AddModifier(flatMod2);

        // 10 + 2 + 5 = 17
        Assert.AreEqual(17.0f, characterStat.Value, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Add_modifier_sums_percent_stats_in_character_stat()
    {
        _instantiateStatModifiers();
        ICharacterStat characterStat = new CharacterStat(10.0f);
        characterStat.AddModifier(percentAddMod1);
        characterStat.AddModifier(percentAddMod2);

        // 10 * (1.0 + 0.05 + 0.10) = 10 * 1.15 = 11.5
        Assert.AreEqual(characterStat.Value, 11.5f, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Add_modifier_sums_percent_mult_stats_in_character_stat()
    {
        _instantiateStatModifiers();
        ICharacterStat characterStat = new CharacterStat(10.0f);
        characterStat.AddModifier(percentMultMod1);
        characterStat.AddModifier(percentMultMod2);

        // 10 * (1 + 0.3) * (1 + 0.2) = 10 * 1.3 * 1.2 = 15.6
        Assert.AreEqual(characterStat.Value, 15.6f, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Add_modifier_sums_flat_and_percent_stats_in_character_stat()
    {
        _instantiateStatModifiers();
        ICharacterStat characterStat = new CharacterStat(10.0f);
        characterStat.AddModifier(flatMod1);
        characterStat.AddModifier(percentAddMod1);
        characterStat.AddModifier(flatMod2);
        characterStat.AddModifier(percentAddMod2);

        // (10 + 2 + 5) * (1 + 0.05 + 0.10) = 17 * 1.15 = 19.55
        Assert.AreEqual(characterStat.Value, 19.55f, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Add_modifier_sums_flat_and_percent_mult_stats_in_character_stat()
    {
        _instantiateStatModifiers();
        ICharacterStat characterStat = new CharacterStat(10.0f);
        characterStat.AddModifier(percentMultMod1);
        characterStat.AddModifier(flatMod1);
        characterStat.AddModifier(flatMod2);
        characterStat.AddModifier(percentMultMod2);

        // (10 + 2 + 5) * (1 + 0.3) * (1 + 0.2) = 17 * 1.3 * 1.2 = 26.52
        Assert.AreEqual(characterStat.Value, 26.52f, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Add_modifier_sums_percent_and_percent_mult_stats_in_character_stat()
    {
        _instantiateStatModifiers();
        ICharacterStat characterStat = new CharacterStat(10.0f);
        characterStat.AddModifier(percentMultMod1);
        characterStat.AddModifier(percentAddMod1);
        characterStat.AddModifier(percentMultMod2);
        characterStat.AddModifier(percentAddMod2);

        // 10 * (1 + 0.05 + 0.10) * (1 + 0.3) * (1 + 0.2) = 10 * 1.15 * 1.3 * 1.2 = 17.94
        Assert.AreEqual(characterStat.Value, 17.94, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Add_modifier_sums_all_stats_in_character_stat()
    {
        _instantiateStatModifiers();
        ICharacterStat characterStat = new CharacterStat(10.0f);
        characterStat.AddModifier(percentMultMod1);
        characterStat.AddModifier(flatMod1);
        characterStat.AddModifier(percentAddMod1);
        characterStat.AddModifier(percentMultMod2);
        characterStat.AddModifier(flatMod2);
        characterStat.AddModifier(percentAddMod2);

        // (10 + 2 + 5) * (1 + 0.05 + 0.10) * (1 + 0.3) * (1 + 0.2) = 17 * 1.15 * 1.3 * 1.2 = 30.498
        Assert.AreEqual(characterStat.Value, 30.498f, FINAL_VALUE_PRECISION);
    }

}
