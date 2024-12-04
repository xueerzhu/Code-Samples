using System;


public interface IShopViewModel : IViewModel
{
    ISettingsScreenViewModel SettingsScreenViewModel { get; }
    void Continue();
    void Back();
}

public class ShopViewModel : IShopViewModel
{
    [NonSerialized] private readonly ShopController _controller;

    public ShopViewModel(ShopController shopController)
    {
        _controller = shopController;
        Init();
    }

    public void Continue()
    {
        _controller.GoToGameLevel();
    }        
    
    public void Back()
    {
        _controller.BackToMachineSelection();
    }

    public ISettingsScreenViewModel SettingsScreenViewModel { get; private set; }

    public void Init()
    {
        SettingsScreenViewModel = new SettingsScreenViewModel(_controller.LocalizationController);
    }

    public void Dispose()
    {
    }
}