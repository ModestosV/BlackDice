using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class HUDControllerTests
{
    const int ABILITY_INDEX = 0;

    HUDController sut;
    AbilityPanel abilityPanel;

    AbilityUsedEvent abilityUsedEvent;
    UpdateSelectionModeEvent updateSelectionModeEvent;

    [SetUp]
    public void Init()
    {
        sut = new HUDController();
        abilityPanel = Substitute.For<AbilityPanel>();
        abilityPanel.SetAbilityColorDefaultToAll().Returns(true);
        abilityPanel.SetAbilityColorUsed(ABILITY_INDEX).Returns(true);

        sut.AbilityPanel = abilityPanel;

        abilityUsedEvent = new AbilityUsedEvent(ABILITY_INDEX);
        updateSelectionModeEvent = new UpdateSelectionModeEvent(SelectionMode.FREE);
    }

    [Test]
    public void AbilityUsedEvent_handling()
    {
        sut.Handle(abilityUsedEvent);
        abilityPanel.Received(1).SetAbilityColorDefaultToAll();
        abilityPanel.Received(1).SetAbilityColorUsed(ABILITY_INDEX);
    }

    [Test]
    public void UpdateSelectionModeEvent_handling()
    {
        sut.Handle(updateSelectionModeEvent);
        abilityPanel.Received(1).SetAbilityColorDefaultToAll();
    }
}