using System;

public interface IPauseScreenViewModel : IViewModel
{
    void ContinueGame();
    void BackToTitle();
}

public class PauseScreenViewModel : IPauseScreenViewModel
{
    [NonSerialized] private readonly IGameController _gameController;

    public PauseScreenViewModel(IGameController gameController)
    {
        _gameController = gameController;
    }

    public void ContinueGame()
    {
        _gameController.TogglePause();
    }

    public void BackToTitle()
    {
        _gameController.BackToTitle();
    }

    public void Init()
    {
    }

    public void Dispose()
    {
    }
}