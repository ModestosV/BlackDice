﻿using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class AbilityPanelControllerTests
{
    const int ABILITY_INDEX = 0;

    AbilityPanelController sut;
    IAbilityPanel abilityPanel;

    List<IAbility> abilities;
    List<IEffect> effects;

    [SetUp]
    public void Init()
    {
        abilityPanel = Substitute.For<IAbilityPanel>();
        sut = new AbilityPanelController(abilityPanel);
        abilities = new List<IAbility>();
        effects = new List<IEffect>();
    }

    [Test]
    public void Disable_ability_panel_hides_ability_panel()
    {
        sut.Hide();
        abilityPanel.Received(1).Hide();
    }

    [Test]
    public void Update_ability_panel_cooldowns()
    {
        abilities.Add(Substitute.For<IAbility>());
        abilities.Add(Substitute.For<IAbility>());
        abilities.Add(Substitute.For<IAbility>());

        sut.UpdateAbilityPanel(abilities, effects);
        abilityPanel.Received(3).UpdateCooldownSquares(Arg.Any<int>(), Arg.Any<bool>(), Arg.Any<int>());
    }

    [Test]
    public void Update_ability_panel_icons_updates_ability_and_stack_icons()
    {
        abilities.Add(Substitute.For<IAbility>());
        abilities.Add(Substitute.For<IAbility>());

        sut.UpdateAbilityPanel(abilities, effects);
        abilityPanel.Received(1).UpdateAbilityIcons(abilities);
        abilityPanel.Received(1).UpdateStackIcons(effects);
    }

    [Test]
    public void Handle_AbilityUsedEvent()
    {
        sut.Handle(new AbilitySelectedEvent(ABILITY_INDEX));

        abilityPanel.Received(1).SetAbilityColorDefaultToAll();
        abilityPanel.Received(1).SetAbilityColorUsed(ABILITY_INDEX);
    }

    [Test]
    public void Handle_UpdateSelectionModeEvent()
    {
        sut.Handle(new AbilitySelectedEvent(ABILITY_INDEX));

        abilityPanel.Received(1).SetAbilityColorDefaultToAll();
    }
}