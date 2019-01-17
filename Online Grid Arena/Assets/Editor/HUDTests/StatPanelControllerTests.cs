using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class StatPanelControllerTests
{
    StatPanelController sut;

    ICharacterStat stat1;
    ICharacterStat stat2;

    IStatDisplay display1;
    IStatDisplay display2;

    Dictionary<string, ICharacterStat> stats;
    List<IStatDisplay> statDisplaysList;

    const float STAT_MAX_VALUE_1 = 100.0f;
    const float STAT_MAX_VALUE_2 = 20.0f;
    const float STAT_CURRENT_VALUE_1 = 100.0f;
    const float STAT_CURRENT_VALUE_2 = 20.0f;

    [SetUp]
    public void Init()
    {
        sut = new StatPanelController();

        stat1 = Substitute.For<ICharacterStat>();
        stat2 = Substitute.For<ICharacterStat>();

        stat1.Value.Returns(STAT_MAX_VALUE_1);
        stat2.Value.Returns(STAT_MAX_VALUE_2);
        stat1.CurrentValue.Returns(STAT_CURRENT_VALUE_1);
        stat2.CurrentValue.Returns(STAT_CURRENT_VALUE_2);

        display1 = Substitute.For<IStatDisplay>();
        display2 = Substitute.For<IStatDisplay>();

        stats = new Dictionary<string, ICharacterStat>() {
            { "health", stat1 },
            { "moves", stat2 }
        };
        statDisplaysList = new List<IStatDisplay>() { display1, display2 };
        
        sut.StatDisplays = statDisplaysList;
        sut.CharacterStats = stats;
    }

    [Test]
    public void Disable_stat_displays_deactivates_stat_displays()
    {
        sut.DisableStatDisplays();

        display1.Received(1).Deactivate();
        display2.Received(1).Deactivate();
    }

    [Test]
    public void Enable_stat_displays_activates_stat_displays()
    {
        sut.EnableStatDisplays();

        display1.Received(1).Activate();
        display2.Received(1).Activate();
    }
}