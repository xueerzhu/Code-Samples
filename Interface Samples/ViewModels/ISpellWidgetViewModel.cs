using System;
using R3;

public interface ISpellWidgetViewModel : IViewModel
{
    ReactiveProperty<bool> IsActivated { get; }
    void OnActivateButtonClicked();
}

public class SpellWidgetViewModel : ISpellWidgetViewModel
{
    [NonSerialized] private readonly IBuildingController _buildingController;

    public SpellWidgetViewModel(IBuildingController buildingController)
    {
        _buildingController = buildingController;
    }

    public void OnActivateButtonClicked()
    {
        _buildingController.ToggleMachineMode();
    }

    public ReactiveProperty<bool> IsActivated => _buildingController.IsMachineActivated;

    public void Init()
    {
    }

    public void Dispose()
    {
    }
}