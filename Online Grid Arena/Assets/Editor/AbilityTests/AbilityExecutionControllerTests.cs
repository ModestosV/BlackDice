using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class AbilityExecutionControllerTests
{
    AbilityExecutionController sut;

    IStatPanel statPanel;
    IStatPanelController statPanelController;
    IAbility ability;
    ICharacter character;
    ICharacterController characterController;
    List<float> abilityValues;

    float ABILITY_DAMAGE = 1.0f;

    [SetUp]
    public void Init()
    {
        statPanel = Substitute.For<IStatPanel>();
        statPanelController = Substitute.For<IStatPanelController>();
        statPanel.Controller.Returns(statPanelController);

        ability = Substitute.For<IAbility>();
        character = Substitute.For<ICharacter>();
        characterController = Substitute.For<ICharacterController>();
        character.Controller.Returns(characterController);

        abilityValues = new List<float>();
        abilityValues.Add(ABILITY_DAMAGE);

        ability.Values = abilityValues;

        sut = new AbilityExecutionController();

        sut.StatPanel = statPanel;

    }

    [Test]
    public void Execute_attack_ability_damages_character_and_updates_stat_panel()
    {
        sut.ExecuteAbility(ability, character);

        characterController.Received(1).Damage(ABILITY_DAMAGE);
        statPanel.Controller.Received(1).UpdateStatValues();
    }
}
