using System;

public interface IGameLoseViewModel : IViewModel
{
    void BackToTitle();
}

public class GameLoseViewModel : IGameLoseViewModel
{
    [NonSerialized] private readonly IGameController _controller;

    public GameLoseViewModel(IGameController controller)
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