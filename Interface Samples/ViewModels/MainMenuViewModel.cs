using System;

public interface IMainMenuViewModel : IViewModel
{
    ISettingsScreenViewModel SettingsScreenViewModel { get; }
    ISupportPageViewModel SupportPageViewModel { get; }
    void StartGame();
    void WishlistGame();
    void QuitGame();
}


public class MainMenuViewModel : IMainMenuViewModel
{
    [NonSerialized] private readonly MainMenuController _controller;

    public MainMenuViewModel(MainMenuController controller)
    {
        _controller = controller;
        Init();
    }

    public void StartGame()
    {
        _controller.GoToMachineSelection();
    }

    public void WishlistGame()
    {
        _controller.WishlistGame();
    }

    public ISettingsScreenViewModel SettingsScreenViewModel { get; private set; }
    public ISupportPageViewModel SupportPageViewModel { get; private set; }

    public void Init()
    {
        SettingsScreenViewModel = new SettingsScreenViewModel(_controller.LocalizationController);
        SupportPageViewModel = new SupportPageViewModel(_controller);
    }

    public void Dispose()
    {
    }

    public void QuitGame()
    {
        _controller.QuitToDesktop();
    }
}