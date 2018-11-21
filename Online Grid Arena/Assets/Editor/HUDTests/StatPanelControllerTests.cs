/*
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class StatPanelControllerTests
{
    StatPanelController sut;

    ICharacterStat stat1;
    ICharacterStat stat2;
    ICharacterStat stat3;

    IStatDisplay display1;
    IStatDisplay display2;
    IStatDisplay display3;
    
    ICharacterController characterController;

    List<ICharacterStat> statList;
    List<IStatDisplay> statDisplaysList;
    List<string> statNameList;
    
    const string STAT_NAME_1 = "Health";
    const string STAT_NAME_2 = "Damage";
    const string STAT_NAME_3 = "Speed";

    const float STAT_MAX_VALUE_1 = 100.0f;
    const float STAT_MAX_VALUE_2 = 20.0f;
    const float STAT_MAX_VALUE_3 = 5.0f;
    const float STAT_CURRENT_VALUE_1 = 100.0f;
    const float STAT_CURRENT_VALUE_2 = 20.0f;
    const float STAT_CURRENT_VALUE_3 = 5.0f;

    [SetUp]
    public void Init()
    {
        sut = new StatPanelController();

        stat1 = Substitute.For<ICharacterStat>();
        stat2 = Substitute.For<ICharacterStat>();
        stat3 = Substitute.For<ICharacterStat>();

        stat1.Value.Returns(STAT_MAX_VALUE_1);
        stat2.Value.Returns(STAT_MAX_VALUE_2);
        stat3.Value.Returns(STAT_MAX_VALUE_3);
        stat1.CurrentValue.Returns(STAT_CURRENT_VALUE_1);
        stat2.CurrentValue.Returns(STAT_CURRENT_VALUE_2);
        stat3.CurrentValue.Returns(STAT_CURRENT_VALUE_3);

        display1 = Substitute.For<IStatDisplay>();
        display2 = Substitute.For<IStatDisplay>();
        display3 = Substitute.For<IStatDisplay>();
        
        statList = new List<ICharacterStat>() { stat1, stat2, stat3 };
        statDisplaysList = new List<IStatDisplay>() { display1, display2, display3 };
        statNameList = new List<string>() { STAT_NAME_1, STAT_NAME_2, STAT_NAME_3 };
        
        sut.StatDisplays = statDisplaysList;
        sut.CharacterStats = statList;
        sut.StatNames = statNameList;

    }

    [Test]
    public void Updates_stat_values_when_stats_and_names_are_consistent()
    {
        sut.UpdateStatValues();

        display1.Received(1).SetMaxValueText(STAT_MAX_VALUE_1.ToString());
        display2.Received(1).SetMaxValueText(STAT_MAX_VALUE_2.ToString());
        display3.Received(1).SetMaxValueText(STAT_MAX_VALUE_3.ToString());
        display1.Received(1).SetCurrentValueText(STAT_CURRENT_VALUE_1.ToString());
        display2.Received(1).SetCurrentValueText(STAT_CURRENT_VALUE_2.ToString());
        display3.Received(1).SetCurrentValueText(STAT_CURRENT_VALUE_3.ToString());
    }

    [Test]
    public void Does_not_update_stat_values_when_stats_and_names_are_inconsistent()
    {
        sut.CharacterStats = new List<ICharacterStat>() { stat1, stat2 };

        sut.UpdateStatValues();

        display1.DidNotReceive();
        display2.DidNotReceive();
        display3.DidNotReceive();
    }

    [Test]
    public void Updates_stat_names_when_stats_and_names_are_consistent()
    {
        sut.UpdateStatNames();

        display1.Received(1).SetNameText(STAT_NAME_1);
        display2.Received(1).SetNameText(STAT_NAME_2);
        display3.Received(1).SetNameText(STAT_NAME_3);
    }

    [Test]
    public void Does_not_update_stat_names_when_stats_and_names_are_inconsistent()
    {
        sut.CharacterStats = new List<ICharacterStat>() { stat1, stat2 };

        sut.UpdateStatNames();

        display1.DidNotReceive();
        display2.DidNotReceive();
        display3.DidNotReceive();
    }

    [Test]
    public void Disable_stat_displays_deactivates_stat_displays()
    {
        sut.DisableStatDisplays();

        display1.Received(1).Deactivate();
        display2.Received(1).Deactivate();
        display3.Received(1).Deactivate();
    }

    [Test]
    public void Enable_stat_displays_activates_stat_displays()
    {
        sut.EnableStatDisplays();

        display1.Received(1).Activate();
        display2.Received(1).Activate();
        display3.Received(1).Activate();
    }
}
*/