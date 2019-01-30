using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class AbilityPanelControllerTests
{
    AbilityPanelController sut;

    List<IAbility> abilities;
    List<IEffect> effects;

    [SetUp]
    public void Init()
    {
        sut = new AbilityPanelController(Substitute.For<IAbilityPanel>());
        abilities = new List<IAbility>();
        effects = new List<IEffect>();
    }

    [Test]
    public void Disable_ability_panel_hides_ability_panel()
    {
        sut.Hide();
        sut.AbilityPanel.Received(1);
    }

    [Test]
    public void Update_ability_panel()
    {
        abilities.Add(Substitute.For<IAbility>());
        abilities.Add(Substitute.For<IAbility>());

        effects.Add(Substitute.For<IEffect>());

        sut.UpdateAbilityPanel(abilities, effects);
        sut.AbilityPanel.Received(1);
    }

}