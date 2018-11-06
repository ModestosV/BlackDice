using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class CharacterStatTests
{
    CharacterStat sut;

    List<IStatModifier> statModifiers;

    object sourceObject1;
    object sourceObject2;

    IStatModifier flatMod1;
    IStatModifier flatMod2;
    IStatModifier percentAddMod1;
    IStatModifier percentAddMod2;
    IStatModifier percentMultMod1;
    IStatModifier percentMultMod2;

    List<IStatModifier> statModifiersList;

    const float FINAL_VALUE_PRECISION = 0.0001f;

    [SetUp]
    public void Init()
    {
        statModifiersList = new List<IStatModifier>();

        sourceObject1 = new object();
        sourceObject2 = new object();


        flatMod1 = Substitute.For<IStatModifier>();
        flatMod1.Value.Returns(2.0f);
        flatMod1.Type.Returns(StatModType.Flat);
        flatMod1.Order.Returns(100);
        flatMod1.Source.Returns(sourceObject1);


        flatMod2 = Substitute.For<IStatModifier>();
        flatMod2.Value.Returns(5.0f);
        flatMod2.Type.Returns(StatModType.Flat);
        flatMod2.Order.Returns(100);
        flatMod2.Source.Returns(sourceObject2);


        percentAddMod1 = Substitute.For<IStatModifier>();
        percentAddMod1.Value.Returns(0.05f);
        percentAddMod1.Type.Returns(StatModType.PercentAdd);
        percentAddMod1.Order.Returns(200);
        percentAddMod1.Source.Returns(sourceObject1);

        percentAddMod2 = Substitute.For<IStatModifier>();
        percentAddMod2.Value.Returns(0.10f);
        percentAddMod2.Type.Returns(StatModType.PercentAdd);
        percentAddMod2.Order.Returns(200);
        percentAddMod2.Source.Returns(sourceObject2);


        percentMultMod1 = Substitute.For<IStatModifier>();
        percentMultMod1.Value.Returns(0.3f);
        percentMultMod1.Type.Returns(StatModType.PercentMult);
        percentMultMod1.Order.Returns(300);
        percentMultMod1.Source.Returns(sourceObject1);

        percentMultMod2 = Substitute.For<IStatModifier>();
        percentMultMod2.Value.Returns(0.2f);
        percentMultMod2.Type.Returns(StatModType.PercentMult);
        percentMultMod2.Order.Returns(300);
        percentMultMod2.Source.Returns(sourceObject2);
    }

    [Test]
    public void Parameterized_constructor_initializes_the_base_value_with_passed_value()
    {
        sut = new CharacterStat(10.0f, statModifiersList);

        Assert.AreEqual(10.0f, sut.Value);
    }

    [Test]
    public void Add_modifier_adds_the_passed_modifier_to_the_stat_modifiers_list()
    {
        sut = new CharacterStat(10.0f, statModifiersList);

        sut.AddModifier(flatMod1);

        Assert.AreEqual(statModifiersList.Count, 1);
        Assert.Contains(flatMod1, statModifiersList);
    }

    [Test]
    public void Remove_modifier_removes_the_passed_modifier_from_the_stat_modifier_list()
    {
        statModifiersList.Add(flatMod1);

        sut = new CharacterStat(10.0f, statModifiersList);

        sut.RemoveModifier(flatMod1);

        Assert.IsFalse(statModifiersList.Contains(flatMod1));
        Assert.IsEmpty(statModifiersList);
    }

    [Test]
    public void Remove_all_modifiers_from_source_removes_all_modifiers_with_passed_source_from_stat_modifier_list()
    {
        statModifiersList.Add(flatMod1);
        statModifiersList.Add(flatMod2);
        statModifiersList.Add(percentAddMod1);
        statModifiersList.Add(percentAddMod2);
        statModifiersList.Add(percentMultMod1);
        statModifiersList.Add(percentMultMod2);
        
        sut = new CharacterStat(10.0f, statModifiersList);

        sut.RemoveAllModifiersFromSource(sourceObject1);

        Assert.IsFalse(statModifiersList.Contains(flatMod1));
        Assert.IsTrue(statModifiersList.Contains(flatMod2));
        Assert.IsFalse(statModifiersList.Contains(percentAddMod1));
        Assert.IsTrue(statModifiersList.Contains(percentAddMod2));
        Assert.IsFalse(statModifiersList.Contains(percentMultMod1));
        Assert.IsTrue(statModifiersList.Contains(percentMultMod2));
    }

    [Test]
    public void Value_for_stat_with_flat_modifiers_calculates_correctly()
    {
        statModifiersList.Add(flatMod1);
        statModifiersList.Add(flatMod2);

        sut = new CharacterStat(10.0f, statModifiersList);

        // 10 + 2 + 5 = 17
        Assert.AreEqual(17.0f, sut.Value, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Value_for_stat_with_percent_add_modifiers_calculates_correctly()
    {
        statModifiersList.Add(percentAddMod1);
        statModifiersList.Add(percentAddMod2);

        sut = new CharacterStat(10.0f, statModifiersList);

        // 10 * (1.0 + 0.05 + 0.10) = 10 * 1.15 = 11.5
        Assert.AreEqual(sut.Value, 11.5f, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Value_for_stat_with_percent_mult_modifiers_calculates_correctly()
    {
        statModifiersList.Add(percentMultMod1);
        statModifiersList.Add(percentMultMod2);

        sut = new CharacterStat(10.0f, statModifiersList);;

        // 10 * (1 + 0.3) * (1 + 0.2) = 10 * 1.3 * 1.2 = 15.6
        Assert.AreEqual(sut.Value, 15.6f, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Value_for_stat_with_flat_and_percent_add_modifiers_calculates_correctly()
    {
        statModifiersList.Add(percentAddMod1);
        statModifiersList.Add(flatMod1);
        statModifiersList.Add(percentAddMod2);
        statModifiersList.Add(flatMod2);

        sut = new CharacterStat(10.0f, statModifiersList);

        // (10 + 2 + 5) * (1 + 0.05 + 0.10) = 17 * 1.15 = 19.55
        Assert.AreEqual(sut.Value, 19.55f, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Value_for_stat_with_flat_and_percent_mult_modifiers_calculates_correctly()
    {

        statModifiersList.Add(percentMultMod1);
        statModifiersList.Add(flatMod1);
        statModifiersList.Add(percentMultMod2);
        statModifiersList.Add(flatMod2);

        sut = new CharacterStat(10.0f, statModifiersList);

        // (10 + 2 + 5) * (1 + 0.3) * (1 + 0.2) = 17 * 1.3 * 1.2 = 26.52
        Assert.AreEqual(sut.Value, 26.52f, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Value_for_stat_with_percent_add_and_percent_mult_modifiers_calculates_correctly()
    {
        statModifiersList.Add(percentAddMod1);
        statModifiersList.Add(percentMultMod1);
        statModifiersList.Add(percentAddMod2);
        statModifiersList.Add(percentMultMod2);

        sut = new CharacterStat(10.0f, statModifiersList);

        // 10 * (1 + 0.05 + 0.10) * (1 + 0.3) * (1 + 0.2) = 10 * 1.15 * 1.3 * 1.2 = 17.94
        Assert.AreEqual(sut.Value, 17.94, FINAL_VALUE_PRECISION);
    }

    [Test]
    public void Value_for_stat_with_flat_and_percent_add_and_percent_mult_modifiers_calculates_correctly()
    {
        statModifiersList.Add(percentMultMod1);
        statModifiersList.Add(flatMod1);
        statModifiersList.Add(percentAddMod1);
        statModifiersList.Add(percentMultMod2);
        statModifiersList.Add(flatMod2);
        statModifiersList.Add(percentAddMod2);

        sut = new CharacterStat(10.0f, statModifiersList);

        // (10 + 2 + 5) * (1 + 0.05 + 0.10) * (1 + 0.3) * (1 + 0.2) = 17 * 1.15 * 1.3 * 1.2 = 30.498
        Assert.AreEqual(sut.Value, 30.498f, FINAL_VALUE_PRECISION);
    }

}

