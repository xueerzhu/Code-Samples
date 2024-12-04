using System;
using R3;

public interface IGameController : IDisposable
{
    ReactiveProperty<int> HealthAmount { get; }
    ReactiveProperty<int> HealthTotalAmount { get; }
    ReactiveProperty<int> MatterAmount { get; }
    ILocalizationController LocalizationController { get; }
    IDeckController DeckController { get; }
    IHandController HandController { get; }
    IBuildingController BuildingController { get; }
    public void TogglePause();
    public void BackToTitle();
}