using NUnit.Framework;
using System.Collections.Generic;

public class TestCharacterStat {

    private List<StatModifier> statModifiers;

    private object sourceObject1;
    private object sourceObject2;

    private StatModifier flatMod1;
    private StatModifier flatMod2;
    private StatModifier percentAddMod1;
    private StatModifier percentAddMod2;
    private StatModifier percentMultMod1;
    private StatModifier percentMultMod2;

    private const float FINAL_VALUE_PRECISION = 0.0001f;

    [SetUp]
    public void Init()
    {
        statModifiers = new List<StatModifier>();

        sourceObject1 = new object();
        sourceObject2 = new object();

        flatMod1 = new StatModifier(2.0f, StatModType.Flat, sourceObject1);
        flatMod2 = new StatModifier(5.0f, StatModType.Flat, sourceObject1);
        percentAddMod1 = new StatModifier(0.05f, StatModType.PercentAdd, sourceObject1);
        percentAddMod2 = new StatModifier(0.10f, StatModType.PercentAdd, sourceObject2);
        percentMultMod1 = new StatModifier(0.3f, StatModType.PercentMult, sourceObject2);
        percentMultMod2 = new StatModifier(0.2f, StatModType.PercentMult, sourceObject2);

    }

    [Test]
    public void Parameterized_constructor_initializes_the_base_value_with_passed_value()
    {
        var sut = new CharacterStat(10.0f, statModifiers);

        Assert.AreEqual(10.0f, sut.Value);
    }

    [Test]
    public void Add_modifier_adds_the_passed_modifier_to_the_stat_modifiers_list()
    {
        var sut = new CharacterStat(10.0f, statModifiers);

        sut.AddModifier(flatMod1);

        Assert.AreEqual(statModifiers.Count, 1);
        Assert.Contains(flatMod1, statModifiers);
    }

    [Test]
    public void Remove_modifier_removes_the_passed_modifier_from_the_stat_modifier_list()
    {
        statModifiers.Add(flatMod1);
        var sut = new CharacterStat(10.0f, statModifiers);

        sut.RemoveModifier(flatMod1);

        Assert.IsFalse(statModifiers.Contains(flatMod1));
        Assert.IsEmpty(statModifiers);
    }

    [Test]
    public void Remove_all_modifiers_from_source_removes_all_modifiers_with_passed_source_from_stat_modifier_list()
    {
        statModifiers.Add(flatMod1);
        statModifiers.Add(flatMod2);
        statModifiers.Add(percentAddMod1);
        statModifiers.Add(percentAddMod2);
        statModifiers.Add(percentMultMod1);
        statModifiers.Add(percentMultMod2);
        
        var sut = new CharacterStat(10.0f, statModifiers);

        sut.RemoveAllModifiersFromSource(sourceObject1);

        Assert.IsFalse(statModifiers.Contains(flatMod1));
        Assert.IsFalse(statModifiers.Contains(flatMod2));
        Assert.IsFalse(statModifiers.Contains(percentAddMod1));
        Assert.IsTrue(statModifiers.Contains(percentAddMod2));
        Assert.IsTrue(statModifiers.Contains(percentMultMod1));
        Assert.IsTrue(statModifiers.Contains(percentMultMod2));
    }

    [Test]
    public void Value_for_stat_with_flat_modifiers_calculates_correctly()
    {
        statModifiers.Add(flatMod1);
        statModifiers.Add(flatMod2);

        var sut = new CharacterStat(10.0f, statModifiers);

        // 10 + 2 + 5 = 17
        Assert.AreEqual(17.0f, sut.Value, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Value_for_stat_with_percent_add_modifiers_calculates_correctly()
    {
        statModifiers.Add(percentAddMod1);
        statModifiers.Add(percentAddMod2);

        var sut = new CharacterStat(10.0f, statModifiers);

        // 10 * (1.0 + 0.05 + 0.10) = 10 * 1.15 = 11.5
        Assert.AreEqual(sut.Value, 11.5f, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Value_for_stat_with_percent_mult_modifiers_calculates_correctly()
    {
        statModifiers.Add(percentMultMod1);
        statModifiers.Add(percentMultMod2);

        var sut = new CharacterStat(10.0f, statModifiers);;

        // 10 * (1 + 0.3) * (1 + 0.2) = 10 * 1.3 * 1.2 = 15.6
        Assert.AreEqual(sut.Value, 15.6f, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Value_for_stat_with_flat_and_percent_add_modifiers_calculates_correctly()
    {
        statModifiers.Add(percentAddMod1);
        statModifiers.Add(flatMod1);
        statModifiers.Add(percentAddMod2);
        statModifiers.Add(flatMod2);

        var sut = new CharacterStat(10.0f, statModifiers);

        // (10 + 2 + 5) * (1 + 0.05 + 0.10) = 17 * 1.15 = 19.55
        Assert.AreEqual(sut.Value, 19.55f, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Value_for_stat_with_flat_and_percent_mult_modifiers_calculates_correctly()
    {

        statModifiers.Add(percentMultMod1);
        statModifiers.Add(flatMod1);
        statModifiers.Add(percentMultMod2);
        statModifiers.Add(flatMod2);

        var sut = new CharacterStat(10.0f, statModifiers);

        // (10 + 2 + 5) * (1 + 0.3) * (1 + 0.2) = 17 * 1.3 * 1.2 = 26.52
        Assert.AreEqual(sut.Value, 26.52f, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Value_for_stat_with_percent_add_and_percent_mult_modifiers_calculates_correctly()
    {
        statModifiers.Add(percentAddMod1);
        statModifiers.Add(percentMultMod1);
        statModifiers.Add(percentAddMod2);
        statModifiers.Add(percentMultMod2);

        var sut = new CharacterStat(10.0f, statModifiers);

        // 10 * (1 + 0.05 + 0.10) * (1 + 0.3) * (1 + 0.2) = 10 * 1.15 * 1.3 * 1.2 = 17.94
        Assert.AreEqual(sut.Value, 17.94, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Value_for_stat_with_flat_and_percent_add_and_percent_mult_modifiers_calculates_correctly()
    {
        statModifiers.Add(percentMultMod1);
        statModifiers.Add(flatMod1);
        statModifiers.Add(percentAddMod1);
        statModifiers.Add(percentMultMod2);
        statModifiers.Add(flatMod2);
        statModifiers.Add(percentAddMod2);

        var sut = new CharacterStat(10.0f, statModifiers);

        // (10 + 2 + 5) * (1 + 0.05 + 0.10) * (1 + 0.3) * (1 + 0.2) = 17 * 1.15 * 1.3 * 1.2 = 30.498
        Assert.AreEqual(sut.Value, 30.498f, FINAL_VALUE_PRECISION);
    }

}

