using System;

public interface ISupportPageViewModel : IViewModel
{
    public void QuitToDesktop();
    public void WishlistGame();
}

public class SupportPageViewModel : ISupportPageViewModel
{
    [NonSerialized] private readonly MainMenuController _controller;

    public SupportPageViewModel(MainMenuController controller)
    {
        _controller = controller;
    }

    public void Init()
    {
    }

    public void Dispose()
    {
    }

    public void QuitToDesktop()
    {
        _controller.QuitToDesktop();
    }

    public void WishlistGame()
    {
    }
}