using System;
using System.Collections.Generic;
using R3;

public interface IGameViewModel : IViewModel
{
    public List<IBuildingItemViewModel> UnlockedBuildingViewModels { get; }
    public IDeckViewModel DeckViewModel { get; }
    public ITopBarViewModel TopBarViewModel { get; }
    public IHandWidgetViewModel HandWidgetViewModel { get; }
    public ISpellWidgetViewModel SpellWidgetViewModel { get; }
    public IGameLoseViewModel GameLoseViewModel { get; }
    public IGameWinViewModel GameWinViewModel { get; }
    public ReactiveProperty<bool> IsPaused { get; }

    public IPauseScreenViewModel PauseScreenViewModel { get; }
}

public class GameViewModel : IGameViewModel
{
    [NonSerialized] private readonly IGameController _gameController;

    public GameViewModel(IGameController gameController)
    {
        _gameController = gameController;
        Init();
    }

    public IDeckViewModel DeckViewModel { get; private set; }
    public ITopBarViewModel TopBarViewModel { get; private set; }

    public IHandWidgetViewModel HandWidgetViewModel { get; private set; }
    public ISpellWidgetViewModel SpellWidgetViewModel { get; private set; }
    public IGameLoseViewModel GameLoseViewModel { get; private set; }
    public IGameWinViewModel GameWinViewModel { get; private set; }

    public ReactiveProperty<bool> IsPaused => GameController.IsPaused;
    public IPauseScreenViewModel PauseScreenViewModel { get; private set; }

    public List<IBuildingItemViewModel> UnlockedBuildingViewModels
    {
        get
        {
            var
                unlockedBuildingViewModels = new List<IBuildingItemViewModel>();

            for (var i = 0; i < _gameController.BuildingController.KeyBindToUnlockedBuildingDataDict.Count; i++)
            {
                var keyBind = i + 1;
                unlockedBuildingViewModels.Add(
                    new BuildingItemViewModel(_gameController.BuildingController,
                        _gameController.BuildingController.KeyBindToUnlockedBuildingDataDict[keyBind], null, null));
            }

            return unlockedBuildingViewModels;
        }
    }

    public void Init()
    {
        PauseScreenViewModel = new PauseScreenViewModel(_gameController);
        DeckViewModel = new DeckViewModel(_gameController.DeckController);
        HandWidgetViewModel = new HandWidgetViewModel(_gameController.HandController);
        SpellWidgetViewModel = new SpellWidgetViewModel(_gameController.BuildingController);
        TopBarViewModel = new TopBarViewModel(_gameController);
        GameLoseViewModel = new GameLoseViewModel(_gameController);
        GameWinViewModel = new GameWinViewModel(_gameController);
    }

    public void Dispose()
    {
    }
}