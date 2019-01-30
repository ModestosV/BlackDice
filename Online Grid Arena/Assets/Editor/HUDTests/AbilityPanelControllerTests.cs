using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class AbilityPanelControllerTests
{
    AbilityPanelController sut;

    readonly List<IAbility> abilities;
    readonly List<IEffect> effects;

    [SetUp]
    public void Init()
    {
        sut = new AbilityPanelController(Substitute.For<IAbilityPanel>());
    }

    [Test]
    public void Disable_ability_panel_hides_ability_panel()
    {
        sut.Hide();

        sut.Received(1).Hide();
    }

    [Test]
    public void Update_ability_panel()
    {
        List<IAbility> abilities = new List<IAbility>();
        List<IEffect> effects = new List<IEffect>();

        abilities.Add(Substitute.For<Scratch>());
        abilities.Add(Substitute.For<DefaultAttack>());

        effects.Add(Substitute.For<CatScratchFever>());

        sut.Received(1).UpdateAbilityPanel(abilities, effects);        
    }

}