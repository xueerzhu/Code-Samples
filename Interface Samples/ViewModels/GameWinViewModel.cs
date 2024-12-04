using System;

public interface IGameWinViewModel : IViewModel
{
    void BackToTitle();
}

public class GameWinViewModel : IGameWinViewModel
{
    [NonSerialized] private readonly IGameController _controller;

    public GameWinViewModel(IGameController controller)
    {
        _controller = controller;
    }

    public void BackToTitle()
    {
        _controller.BackToTitle();
    }

    public void Init()
    {
    }

    public void Dispose()
    {
    }
}