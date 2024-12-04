using System;
using R3;

public interface ITopBarViewModel : IViewModel
{
    ISettingsScreenViewModel SettingsScreenViewModel { get; }
    ReactiveProperty<int> HealthAmount { get; }
    ReactiveProperty<int> HealthTotalAmount { get; }
    ReactiveProperty<int> MatterAmount { get; }
}

public class TopBarViewModel : ITopBarViewModel
{
    [NonSerialized] private readonly IGameController _gameController;


    public TopBarViewModel(IGameController gameController)
    {
        _gameController = gameController;

        Init();
    }

    public ISettingsScreenViewModel SettingsScreenViewModel { get; private set; }

    public ReactiveProperty<int> HealthAmount => _gameController.HealthAmount;
    public ReactiveProperty<int> HealthTotalAmount => _gameController.HealthTotalAmount;
    public ReactiveProperty<int> MatterAmount => _gameController.MatterAmount;


    public void Init()
    {
        SettingsScreenViewModel = new SettingsScreenViewModel(_gameController.LocalizationController);
    }

    public void Dispose()
    {
    }
}