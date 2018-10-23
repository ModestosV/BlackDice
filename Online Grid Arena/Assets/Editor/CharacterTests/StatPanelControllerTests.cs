using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatPanelControllerTests
{
    StatPanelController sut;

    ICharacter selectedCharacter;

    ICharacterStat stat1;
    ICharacterStat stat2;
    ICharacterStat stat3;

    IStatDisplay display1;
    IStatDisplay display2;
    IStatDisplay display3;

    IStatDisplayController displayController1;
    IStatDisplayController displayController2;
    IStatDisplayController displayController3;

    ICharacterController characterController;

    List<ICharacterStat> statList;
    List<IStatDisplay> statDisplaysList;
    List<string> statNameList;

    CharacterStatNameSet characterStatNameSet;

    const string STAT_NAME_1 = "Health";
    const string STAT_NAME_2 = "Damage";
    const string STAT_NAME_3 = "Speed";

    const float STAT_VALUE_1 = 100.0f;
    const float STAT_VALUE_2 = 20.0f;
    const float STAT_VALUE_3 = 5.0f;

    [SetUp]
    public void Init()
    {
        sut = new StatPanelController();

        selectedCharacter = Substitute.For<ICharacter>();

        stat1 = Substitute.For<ICharacterStat>();
        stat2 = Substitute.For<ICharacterStat>();
        stat3 = Substitute.For<ICharacterStat>();

        stat1.Value.Returns(STAT_VALUE_1);
        stat2.Value.Returns(STAT_VALUE_2);
        stat3.Value.Returns(STAT_VALUE_3);

        display1 = Substitute.For<IStatDisplay>();
        display2 = Substitute.For<IStatDisplay>();
        display3 = Substitute.For<IStatDisplay>();

        displayController1 = Substitute.For<IStatDisplayController>();
        displayController2 = Substitute.For<IStatDisplayController>();
        displayController3 = Substitute.For<IStatDisplayController>();

        display1.Controller.Returns(displayController1);
        display2.Controller.Returns(displayController2);
        display3.Controller.Returns(displayController3);

        characterController = Substitute.For<ICharacterController>();

        selectedCharacter.Controller.Returns(characterController);

        statList = new List<ICharacterStat>() { stat1, stat2, stat3 };
        statDisplaysList = new List<IStatDisplay>() { display1, display2, display3 };
        statNameList = new List<string>() { STAT_NAME_1, STAT_NAME_2, STAT_NAME_3 };

        characterStatNameSet = (CharacterStatNameSet)ScriptableObject.CreateInstance("CharacterStatNameSet");
        characterStatNameSet.statNames = statNameList;

        characterController.CharacterStatNameSet.Returns(characterStatNameSet);
        characterController.CharacterStats.Returns(statList);

        sut.SelectedCharacter = selectedCharacter;
        sut.StatDisplays = statDisplaysList;

    }

    [Test]
    public void Set_character_with_consistent_stats_sets_character()
    {
        sut.SetCharacter(selectedCharacter);

        Assert.AreEqual(sut.SelectedCharacter, selectedCharacter);

        displayController1.Received(1).CharacterStat = stat1;
        displayController2.Received(1).CharacterStat = stat2;
        displayController3.Received(1).CharacterStat = stat3;
    }

    [Test]
    public void Set_character_with_inconsistent_stats_sets_character()
    {
        statNameList = new List<string>() { STAT_NAME_1, STAT_NAME_2 };
        characterStatNameSet.statNames = statNameList;

        sut.SetCharacter(selectedCharacter);

        Assert.AreEqual(sut.SelectedCharacter, selectedCharacter);
    }

    [Test]
    public void Set_character_with_consistent_stats_sets_character_stats_on_stat_displays_to_character_stats()
    {
        sut.SetCharacter(selectedCharacter);

        displayController1.Received(1).CharacterStat = stat1;
        displayController2.Received(1).CharacterStat = stat2;
        displayController3.Received(1).CharacterStat = stat3;
    }

    [Test]
    public void Set_character_with_inconsistent_stats_does_not_character_stats_on_stat_displays_to_character_stats()
    {
        statNameList = new List<string>() { STAT_NAME_1, STAT_NAME_2 };
        characterStatNameSet.statNames = statNameList;

        displayController1.DidNotReceive().CharacterStat = Arg.Any<ICharacterStat>();
        displayController2.DidNotReceive().CharacterStat = Arg.Any<ICharacterStat>();
        displayController3.DidNotReceive().CharacterStat = Arg.Any<ICharacterStat>();
    }

    [Test]
    public void Update_stat_values_updates_stat_display_value_text()
    {
        displayController1.CharacterStat = stat1;
        displayController2.CharacterStat = stat2;
        displayController3.CharacterStat = stat3;

        sut.SelectedCharacter = selectedCharacter;

        sut.UpdateStatValues();

        display1.Received(1).SetValueText(STAT_VALUE_1.ToString());
        display2.Received(1).SetValueText(STAT_VALUE_2.ToString());
        display3.Received(1).SetValueText(STAT_VALUE_3.ToString());
    }

    [Test]
    public void Update_stat_names_updates_stat_display_name_text()
    {
        displayController1.CharacterStat = stat1;
        displayController2.CharacterStat = stat2;
        displayController3.CharacterStat = stat3;

        sut.SelectedCharacter = selectedCharacter;

        sut.UpdateStatNames();

        display1.Received(1).SetNameText(STAT_NAME_1);
        display2.Received(1).SetNameText(STAT_NAME_2);
        display3.Received(1).SetNameText(STAT_NAME_3);
    }
}
