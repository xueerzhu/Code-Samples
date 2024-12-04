using System;
using System.Collections.Generic;
using R3;
using TowerLab.Map;

public interface IMapScreenViewModel : IViewModel
{
    ISettingsScreenViewModel SettingsScreenViewModel { get; }
    ReactiveProperty<int> LastFinishedLevelId { get; }
    List<int> FinishedLevels { get; }
    MapData MapData { get; }
    bool CanContinue { get; }
    ReactiveProperty<State> PlayerState { get; }
    void OpenSettingsMenu();

    bool TrySelectLevel(int roomId, int floorId);
    void SelectRoom(int roomId, int floorId);
}

public class MapScreenViewModel : IMapScreenViewModel
{
    [NonSerialized] private readonly MapScreenController _controller;

    public MapScreenViewModel(MapScreenController controller)
    {
        _controller = controller;
        Init();
    }

    public ReactiveProperty<int> LastFinishedLevelId => _controller.LastFinishedFloorId;
    public ReactiveProperty<State> PlayerState => _controller.PlayerState;
    public List<int> FinishedLevels => _controller.FinishedLevels;
    public MapData MapData => _controller.MapData;

    public void OpenSettingsMenu()
    {
        _controller.OpenSettingsMenu();
    }

    public ISettingsScreenViewModel SettingsScreenViewModel { get; private set; }

    public void Init()
    {
        SettingsScreenViewModel = new SettingsScreenViewModel(_controller.LocalizationController);
    }

    public bool TrySelectLevel(int roomId, int floorId)
    {
        return _controller.TrySelectLevel(roomId, floorId);
    }

    public void SelectRoom(int roomId, int floorId)
    {
        _controller.EnterPlay();
    }

    public bool CanContinue => _controller.CanContinue;

    public void Dispose()
    {
    }
}