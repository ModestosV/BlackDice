using UnityEngine;

public sealed class InputManager : MonoBehaviour, IEventSubscriber
{
    public ISelectionManager SelectionManager { private get; set; }

    private IInputParameters lastInputParameters;

    void Update()
    {
        var inputParameters = GetInputParameters();

        // Do nothing if input has not changed
        if (lastInputParameters != null && inputParameters.Equals(lastInputParameters))
        {
            return;
        }
        lastInputParameters = inputParameters;

        SelectionManager.Update(inputParameters);

        if (inputParameters.IsAbilityKeyPressed() && SelectionManager.SelectedCharacterCanUseAbility(inputParameters.GetAbilityNumber()))
        {
            EventBus.Publish(new AbilityUsedEvent(inputParameters.GetAbilityNumber()));
        }
    }

    void UpdateOnAbilityClickEvent(int abilityIndex)
    {
        var inputParameters = GetInputParameters();

        inputParameters.IsKeyQDown = (abilityIndex == 0) ? true: false;
        inputParameters.IsKeyWDown = (abilityIndex == 1) ? true: false;
        inputParameters.IsKeyEDown = (abilityIndex == 2) ? true: false;
        inputParameters.IsKeyRDown = (abilityIndex == 3) ? true: false;

        // Do nothing if input has not changed
        if (lastInputParameters != null && inputParameters.Equals(lastInputParameters))
        {
            return;
        }
        lastInputParameters = inputParameters;

        SelectionManager.Update(inputParameters);

        if (inputParameters.IsAbilityKeyPressed() && SelectionManager.SelectedCharacterCanUseAbility(inputParameters.GetAbilityNumber()))
        {
            EventBus.Publish(new AbilityUsedEvent(inputParameters.GetAbilityNumber()));
        }
    }

    private IInputParameters GetInputParameters()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isMouseOverGrid = Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Tile";
        IHexTileController targetTile = null;
        if (isMouseOverGrid)
        {
            targetTile = hit.collider.gameObject.GetComponent<HexTile>().Controller;
        }

        IInputParameters inputParameters = new InputParameters()
        {
            IsKeyQDown = Input.GetKeyDown(KeyCode.Q),
            IsKeyWDown = Input.GetKeyDown(KeyCode.W),
            IsKeyEDown = Input.GetKeyDown(KeyCode.E),
            IsKeyRDown = Input.GetKeyDown(KeyCode.R),
            IsKeyFDown = Input.GetKeyDown(KeyCode.F),
            IsKeyEscapeDown = Input.GetKeyDown(KeyCode.Escape),
            IsKeyTabDown = Input.GetKeyDown(KeyCode.Tab),

            IsLeftClickDown = Input.GetMouseButtonDown(0),
            IsRightClickDown = Input.GetMouseButtonDown(1),

            IsMouseOverGrid = isMouseOverGrid,
            TargetTile = targetTile
        };

        return inputParameters;
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();

        if (type == typeof(AbilityClickEvent))
        {
            var newAbilityClicked = (AbilityClickEvent)@event;
            UpdateOnAbilityClickEvent(newAbilityClicked.AbilityIndex);
        }
    }
}
